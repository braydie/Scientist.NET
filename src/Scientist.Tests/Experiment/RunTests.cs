using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using Scientist.Exceptions;

namespace Scientist.Tests.Experiment
{
    [TestFixture]
    public class RunTests
    {
        private Experiment<string> _Sut;

        [SetUp]
        public void Setup()
        {
            _Sut = new Experiment<string> { PercentageEnabled = 100 };
        }

        [Test]
        public void Run_NoControlSpecified_ThrowsBehaviourMissingException()
        {
            Action Act = () => _Sut.Run();
            Act.ShouldThrow<BehaviourMissingException>().Where(e => e.Message.Equals("Experiment missing control behaviour", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void Run_HasControl_ReturnsControl()
        {
            _Sut.Use(() => "control");
            var Actual = _Sut.Run();
            Actual.Should().Be("control");
        }

        [Test]
        public void Run_SwallowsExceptionsThrownByCandidate()
        {
            _Sut.Use(() => "control");
            _Sut.Try(() => ExceptionalBehaviour("candidate"));

            var Actual = _Sut.Run();
            Actual.Should().Be("control");
        }

        [Test]
        // This test ensures that tests do not always run "control" then "candidate"
        // This is a hideous test...
        public void Run_ShufflesBehavioursBeforeRunning()
        {
            string LastCalled = null;
            var LastCalledMethodsInExperiments = new List<string>();

            _Sut.Use(() => LastCalled = "control");
            _Sut.Try(() => LastCalled = "candidate");

            // Loop a few times so that we can see some variations in execution order
            for (var i = 0; i < 2500; i++)
            {
                _Sut.Run();
                // Test is running to fast for the experiment!
                Thread.Sleep(1);
                LastCalledMethodsInExperiments.Add(LastCalled);
            }

            var Result = ContainsShuffledBehaviours(LastCalledMethodsInExperiments);

            Result.Should().BeTrue();
        }

        [Test]
        [TestCase(100)]
        [TestCase(70)]
        [TestCase(50)]
        [TestCase(30)]
        [TestCase(10)]
        public void Run_PercentageOfCandidatesRunIsCloseToPercentageEnabled(int PercentageEnabled)
        {
            const int EXPERIMENT_COUNT = 1000;
            var Sut = new Experiment<int>();
            int NumberOfTimesTryCalled = 0;
            Sut.PercentageEnabled = PercentageEnabled;
            Sut.Try(() => NumberOfTimesTryCalled++);
            Sut.Use(() => 1000);

            for (var i = 0; i < EXPERIMENT_COUNT; i++)
            {                
                Thread.Sleep(1);
                Sut.Run();
            }
            double ActualPercentCalled = (NumberOfTimesTryCalled / (double)EXPERIMENT_COUNT * 100);         
            ActualPercentCalled.ShouldBeNear(PercentageEnabled);            

        }

        private static string ExceptionalBehaviour(string Name)
        {
            throw new Exception(Name);
        }

        private static bool ContainsShuffledBehaviours(IEnumerable<string> Behaviours)
        {
            var AllLastCalledMethods = string.Join(string.Empty, Behaviours);
            var CombinationsOfMethodExecution = new[] { "controlcontrol", "candidatecandidate", "controlcandidate", "candidatecontrol" };
            return CombinationsOfMethodExecution.All(m => AllLastCalledMethods.Contains(m));
        }
    }
}

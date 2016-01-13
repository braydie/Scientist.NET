using System;
using System.Collections.Generic;
using System.Linq;
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
            Act.ShouldThrow<BehaviourMissingException>().Where(e=> e.Message.Equals("Experiment missing control behaviour", StringComparison.InvariantCultureIgnoreCase));
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
        // This test has a small chance of failing because of the random nature of Run()
        public void Run_ShufflesBehavioursBeforeRunning()
        {
            string Last = null;
            var LastCalledMethod = new List<string>();

            _Sut.Use(() => Last = "control");
            _Sut.Try(() => Last = "candidate");

            for (var i = 0; i < 5000; i++)
            {
                _Sut.Run();
                LastCalledMethod.Add(Last);                
            }
            LastCalledMethod.Distinct().Count().Should().BeGreaterThan(1);
        }
        
        private static string ExceptionalBehaviour(string Name)
        {
            throw new Exception(Name);
        }
    }
}

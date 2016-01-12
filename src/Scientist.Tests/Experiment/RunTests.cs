using System;
using FluentAssertions;
using NUnit.Framework;
using Scientist.Exceptions;

namespace Scientist.Tests.Experiment
{
    [TestFixture]
    public class RunTests
    {
        [Test]
        public void Run_NoControlSpecified_ThrowsBehaviourMissingException()
        {
            var Sut = new Experiment<string>();            
            Action Act = () => Sut.Run();
            Act.ShouldThrow<BehaviourMissingException>().Where(e=> e.Message.Equals("Experiment missing control behaviour", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void Run_HasControl_ReturnsControl()
        {
            var Sut = new Experiment<string>();
            Sut.Use(() => "control");
            var Actual = Sut.Run();
            Actual.Should().Be("control");
        }
    }
}

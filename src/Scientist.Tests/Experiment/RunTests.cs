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
            Sut.Use(() => null);
            Action Act = () => Sut.Run();
            Act.ShouldThrow<BehaviourMissingException>().Where(e=> e.Message.Equals("Experiment missing control behaviour", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

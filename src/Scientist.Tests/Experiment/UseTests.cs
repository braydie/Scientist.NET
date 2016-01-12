using System;
using FluentAssertions;
using NUnit.Framework;
using Scientist.Exceptions;

namespace Scientist.Tests.Experiment
{
    [TestFixture]
    public class UseTests
    {
        [Test]
        public void Use_MultipleAssignments_ThrowsBehvaiourNotUniqueException()
        {
            var Sut = new Experiment<string>();
            Sut.Use(() => "Do something");
            Action Act = () => Sut.Use(() => "Do something else");
            Act.ShouldThrow<BehaviourNotUniqueException>().Where(x=>x.Name == "Control" && x.Experiment == Sut);
        }
    }
}

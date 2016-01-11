using System;
using FluentAssertions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Scientist.Tests.Experiment
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        public void Ctor_ProvidesDefaultName()
        {
            var Sut = new Experiment<string>();
            Sut.Name.Should().Be("Experiment");
        }

        [Test]
        public void Ctor_NameOfExperimentCanBeSet()
        {
            var Expected = new Fixture().Create<string>();
            var Sut = new Experiment<string>(Expected);
            Sut.Name.Should().Be(Expected);
        }

        [Test]
        public void Ctor_PercentageEnabledDefaultsToZero()
        {
            var Sut = new Experiment<string>();
            Sut.PercentageEnabled.Should().Be(0);
        }

    }
}

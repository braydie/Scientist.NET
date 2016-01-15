using System;
using FluentAssertions;
using NUnit.Framework;

namespace Scientist.Tests.Observation
{
    [TestFixture]
    public class IsEquivalentToTests
    {        
        [Test]
        public void ComparesValues_ValuesAreTheSame_ReturnsTrue()
        {
            dynamic DummyFunction = new Func<string>(() => "dummy result");

            var Sut = new Scientist.Observation(null, "control", DummyFunction);
            var Other = new Scientist.Observation(null, "other", DummyFunction);

            Sut.IsEquivalentTo(Other).Should().BeTrue();
        }

        [Test]
        public void BothRaisedExceptions_ExceptionsAreSameTypeAndContainSameMessage()
        {
            dynamic ExceptionalDummyFunction = new Func<string>(() => ExceptionalBehaviour(new ApplicationException("this is a test")));

            var Sut = new Scientist.Observation(null, "control", ExceptionalDummyFunction);
            var Other = new Scientist.Observation(null, "other", ExceptionalDummyFunction);

            Sut.IsEquivalentTo(Other).Should().BeTrue();
        }

        [Test]
        public void BothRaisedExceptions_ExceptionsAreDifferent_ReturnFalse()
        {
            dynamic ExceptionalDummyFunction1 = new Func<string>(() => ExceptionalBehaviour(new ApplicationException("this is a test")));
            dynamic ExceptionalDummyFunction2 = new Func<string>(() => ExceptionalBehaviour(new InvalidOperationException("this is another test")));

            var Sut = new Scientist.Observation(null, "control", ExceptionalDummyFunction1);
            var Other = new Scientist.Observation(null, "other", ExceptionalDummyFunction2);

            Sut.IsEquivalentTo(Other).Should().BeFalse();
        }

        [Test]
        public void OnlyOneObservationThrowsException_ReturnsFalse()
        {
            dynamic ExceptionalDummyFunction = new Func<string>(() => ExceptionalBehaviour(new ApplicationException("this is a test")));
            dynamic DummyFunction = new Func<string>(() => "I work ok!");

            var Sut = new Scientist.Observation(null, "control", DummyFunction);
            var Other = new Scientist.Observation(null, "control", ExceptionalDummyFunction);

            Sut.IsEquivalentTo(Other).Should().BeFalse();
        }

        private string ExceptionalBehaviour(Exception ExceptionToThrow)
        {
            throw ExceptionToThrow;
        }
    }
}

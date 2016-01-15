using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}

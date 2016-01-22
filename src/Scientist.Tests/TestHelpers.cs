using FluentAssertions;

namespace Scientist.Tests
{
    public static class TestHelpers
    {
        public static void ShouldBeNear(this double Subject, double Expected, double Buffer = 1d)
        {
            Subject.Should().BeGreaterOrEqualTo(Expected - Buffer);
            Subject.Should().BeLessOrEqualTo(Expected + Buffer);
        }
    }
}

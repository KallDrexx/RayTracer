using RayTracer.Common.Core.Objects;
using RayTracer.Common.Core.Patterns;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core.Patterns
{
    public class RingPatternTests
    {
        [Fact]
        public void Ring_Extend_In_Both_X_And_Z()
        {
            var pattern = new RingPattern(Color.White, Color.Black);
            var unitSphere = new Sphere();
            
            pattern.ColorAt(new Point(0, 0, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(1, 0, 0), unitSphere).ShouldBe(Color.Black);
            pattern.ColorAt(new Point(0, 0, 1), unitSphere).ShouldBe(Color.Black);
            pattern.ColorAt(new Point(0, 1, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(0.708, 0, 0.708), unitSphere).ShouldBe(Color.Black);
        }
    }
}
using RayTracer.Common.Core.Objects;
using RayTracer.Common.Core.Patterns;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core.Patterns
{
    public class GradientPatternTests
    {
        [Fact]
        public void Linearly_Interpolates_Between_Colors()
        {
            var pattern = new GradientPattern(Color.White, Color.Black);
            var unitSphere = new Sphere();
            
            pattern.ColorAt(new Point(0, 0, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(0.25, 0, 0), unitSphere).ShouldBe(Color.White * .75);
            pattern.ColorAt(new Point(0.5, 0, 0), unitSphere).ShouldBe(Color.White * .5);
            pattern.ColorAt(new Point(0.75, 0, 0), unitSphere).ShouldBe(Color.White * .25);
        }
    }
}
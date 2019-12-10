using RayTracer.Common.Core.Objects;
using RayTracer.Common.Core.Patterns;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core.Patterns
{
    public class CheckersPatternTests
    {
        [Fact]
        public void Pattern_Repeats_In_X()
        {
            var pattern = new CheckersPattern(Color.White, Color.Black);
            var unitSphere = new Sphere();
            
            pattern.ColorAt(new Point(0, 0, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(0.99, 0, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(1.01, 0, 0), unitSphere).ShouldBe(Color.Black);
        }
        
        [Fact]
        public void Pattern_Repeats_In_Y()
        {
            var pattern = new CheckersPattern(Color.White, Color.Black);
            var unitSphere = new Sphere();
            
            pattern.ColorAt(new Point(0, 0, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(0, 0.99, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(0, 1.01, 0), unitSphere).ShouldBe(Color.Black);
        }
        
        [Fact]
        public void Pattern_Repeats_In_Z()
        {
            var pattern = new CheckersPattern(Color.White, Color.Black);
            var unitSphere = new Sphere();
            
            pattern.ColorAt(new Point(0, 0, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(0, 0, 0.99), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(0, 0, 1.01), unitSphere).ShouldBe(Color.Black);
        }
    }
}
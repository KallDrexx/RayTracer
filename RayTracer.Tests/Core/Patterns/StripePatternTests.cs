using RayTracer.Common.Core.Objects;
using RayTracer.Common.Core.Patterns;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core.Patterns
{
    public class StripePatternTests
    {
        [Fact]
        public void Pattern_Is_Constant_In_Y_Axis()
        {
            var pattern = new StripePattern(Color.White, Color.Black);
            var unitSphere = new Sphere();
            
            pattern.ColorAt(new Point(0, 0, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(0, 1, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(0, 2, 0), unitSphere).ShouldBe(Color.White);
        }

        [Fact]
        public void Pattern_Is_Constant_In_Z_Axis()
        {
            var pattern = new StripePattern(Color.White, Color.Black);
            var unitSphere = new Sphere();
            
            pattern.ColorAt(new Point(0, 0, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(0, 0, 1), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(0, 0, 2), unitSphere).ShouldBe(Color.White);
        }

        [Fact]
        public void Pattern_Alternates_In_X_Axis()
        {
            var pattern = new StripePattern(Color.White, Color.Black);
            var unitSphere = new Sphere();
            
            pattern.ColorAt(new Point(0, 0, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(0.9, 0, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(1, 0, 0), unitSphere).ShouldBe(Color.Black);
            pattern.ColorAt(new Point(2, 0, 0), unitSphere).ShouldBe(Color.White);
            pattern.ColorAt(new Point(-0.1, 0, 0), unitSphere).ShouldBe(Color.Black);
            pattern.ColorAt(new Point(-1.1, 0, 0), unitSphere).ShouldBe(Color.White);
        }

        [Fact]
        public void Stripes_With_An_Object_Transformation()
        {
            var pattern = new StripePattern(Color.White, Color.Black);
            var sphere = new Sphere(Matrix4X4.CreateScale(2, 2, 2));
            
            pattern.ColorAt(new Point(1.5, 0, 0), sphere).ShouldBe(Color.White);
        }

        [Fact]
        public void Stripe_With_Pattern_Transformation()
        {
            var pattern = new StripePattern(Color.White, Color.Black)
            {
                TransformMatrix = Matrix4X4.CreateScale(2, 2, 2),
            };
            
            pattern.ColorAt(new Point(1.5, 0, 0), new Sphere()).ShouldBe(Color.White);
        }

        [Fact]
        public void Stripe_With_Pattern_And_Object_Transform()
        {
            var pattern = new StripePattern(Color.White, Color.Black)
            {
                TransformMatrix = Matrix4X4.CreateTranslation(0.5, 0, 0),
            };
            
            var sphere = new Sphere(Matrix4X4.CreateScale(2, 2, 2));
            
            pattern.ColorAt(new Point(2.5, 0, 0), sphere).ShouldBe(Color.White);
        }
    }
}
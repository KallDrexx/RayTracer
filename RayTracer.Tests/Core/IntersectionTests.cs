using RayTracer.Common.Core;
using RayTracer.Common.Core.Objects;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core
{
    public class IntersectionTests
    {
        [Fact]
        public void Can_Create_Intersection()
        {
            var sphere = new Sphere();
            var intersection = new Intersection(1.5, sphere);
            
            intersection.Time.ShouldBe(1.5);
            intersection.Object.ShouldBe(sphere);
        }

        [Fact]
        public void Equality_Checks()
        {
            var sphere1 = new Sphere();
            var sphere2 = new Sphere();

            new Intersection(1, sphere1).ShouldBe(new Intersection(1, sphere1));
            new Intersection(1, sphere1).ShouldNotBe(new Intersection(2, sphere1));
            new Intersection(1, sphere1).ShouldNotBe(new Intersection(1, sphere2));
        }

        [Fact]
        public void Pre_Compute_State_Of_An_Intersection()
        {
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var sphere = new Sphere();
            var intersection = new Intersection(4, sphere);

            var preComputation = intersection.GetPreComputation(ray);
            
            preComputation.ShouldNotBeNull();
            preComputation.Time.ShouldBe(4);
            preComputation.Object.ShouldBe(sphere);
            preComputation.Point.ShouldBe(new Point(0, 0, -1));
            preComputation.EyeVector.ShouldBe(new Vector(0, 0, -1));
            preComputation.NormalVector.ShouldBe(new Vector(0, 0, -1));
            preComputation.IsInside.ShouldBeFalse();
        }

        [Fact]
        public void Pre_Computation_Originating_Inside_Sphere()
        {
            var ray = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            var sphere = new Sphere();
            var intersection = new Intersection(1, sphere);

            var preComputation = intersection.GetPreComputation(ray);
            
            preComputation.ShouldNotBeNull();
            preComputation.Time.ShouldBe(1);
            preComputation.Object.ShouldBe(sphere);
            preComputation.Point.ShouldBe(new Point(0, 0, 1));
            preComputation.EyeVector.ShouldBe(new Vector(0, 0, -1));
            preComputation.NormalVector.ShouldBe(new Vector(0, 0, -1));
            preComputation.IsInside.ShouldBeTrue();
        }

        [Fact]
        public void Pre_Computation_Should_Offset_Point()
        {
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var sphere = new Sphere(Matrix4X4.CreateTranslation(0, 0, 1));
            var intersection = new Intersection(5, sphere);

            var preComputation = intersection.GetPreComputation(ray);
            
            preComputation.OverPoint.Z.ShouldBeLessThan(-0.0001f / 2);
            preComputation.Point.Z.ShouldBeGreaterThan(preComputation.OverPoint.Z);
        }
    }
}
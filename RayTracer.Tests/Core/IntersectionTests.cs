using RayTracer.Common.Core;
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
    }
}
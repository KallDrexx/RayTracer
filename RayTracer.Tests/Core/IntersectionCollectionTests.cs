using RayTracer.Common.Core;
using RayTracer.Common.Core.Objects;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core
{
    public class IntersectionCollectionTests
    {
        [Fact]
        public void Hit_Is_Lowest_Non_Negative_Intersection()
        {
            var sphere = new Sphere();
            var first = new Intersection(5, sphere);
            var second = new Intersection(3, sphere);
            var third = new Intersection(10, sphere);
            var fourth = new Intersection(-5, sphere);
            
            var collection = new IntersectionCollection(first, second, third, fourth);
            var hit = collection.GetHit();
            
            hit.ShouldNotBeNull();
            hit.ShouldBe(second);
        }

        [Fact]
        public void All_Negative_Times_Give_Null_Hit()
        {
            var sphere = new Sphere();
            var first = new Intersection(-5, sphere);
            var second = new Intersection(-10, sphere);
            
            var collection = new IntersectionCollection(first, second);
            var hit = collection.GetHit();
            
            hit.ShouldBeNull();
        }
    }
}
using RayTracer.Common.Core;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core
{
    public class RayTests
    {
        [Fact]
        public void Ray_Has_Origin_And_Direction_Specified()
        {
            var origin = new Point(2, 3, 4);
            var direction = new Vector(1, 0, 0);
            var ray = new Ray(origin, direction);
            
            ray.Origin.ShouldBe(origin);
            ray.Direction.ShouldBe(direction);
        }

        [Fact]
        public void Can_Get_Position_At_Time_Intervals()
        {
            var origin = new Point(2, 3, 4);
            var direction = new Vector(1, 0, 0);
            var ray = new Ray(origin, direction);
            
            ray.PositionAt(0).ShouldBe(new Point(2, 3, 4));
            ray.PositionAt(1).ShouldBe(new Point(3, 3, 4));
            ray.PositionAt(-1).ShouldBe(new Point(1, 3, 4));
            ray.PositionAt(2.5).ShouldBe(new Point(4.5, 3, 4));
        }

        [Fact]
        public void Can_Calculate_Ray_Sphere_Intersection_Through_Sphere_Origin()
        {
            var sphere = new Sphere(new Point(0, 0, 0), 1);
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            var intersections = ray.Intersects(sphere);
            
            intersections.Count.ShouldBe(2);
            intersections.ShouldContain(new Intersection(4, sphere));
            intersections.ShouldContain(new Intersection(6, sphere));
        }

        [Fact]
        public void Can_Calculate_Ray_Sphere_Intersection_At_Tangent()
        {
            var sphere = new Sphere(new Point(0, 0, 0), 1);
            var ray = new Ray(new Point(0, 1, -5), new Vector(0, 0, 1));

            var intersections = ray.Intersects(sphere);
            
            intersections.Count.ShouldBe(2);
            intersections.ShouldContain(new Intersection(5, sphere));
            intersections.ShouldContain(new Intersection(5, sphere));
        }

        [Fact]
        public void No_Intersection_When_Ray_Doesnt_Hit_Sphere()
        {
            var sphere = new Sphere(new Point(0, 0, 0), 1);
            var ray = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));

            var intersections = ray.Intersects(sphere);
            
            intersections.Count.ShouldBe(0);
        }

        [Fact]
        public void Two_Intersections_When_Ray_Originates_Inside_Sphere()
        {
            var sphere = new Sphere(new Point(0, 0, 0), 1);
            var ray = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            
            var intersections = ray.Intersects(sphere);
            
            intersections.Count.ShouldBe(2);
            intersections.ShouldContain(new Intersection(-1, sphere));
            intersections.ShouldContain(new Intersection(1, sphere));
        }

        [Fact]
        public void Two_Intersection_When_Ray_Originates_In_Front_Of_Sphere()
        {
            var sphere = new Sphere(new Point(0, 0, 0), 1);
            var ray = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
            
            var intersections = ray.Intersects(sphere);
            
            intersections.Count.ShouldBe(2);
            intersections.ShouldContain(new Intersection(-6, sphere));
            intersections.ShouldContain(new Intersection(-4, sphere));
        }
    }
}
using System;
using RayTracer.Common.Core;
using RayTracer.Common.Core.Objects;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core.Objects
{
    public class PlaneTests
    {
        [Fact]
        public void Unit_Plane_Normal_Calculations()
        {
            var plane = new Plane();
            
            plane.NormalAt(new Point(1, 0, 0)).ShouldBe(new Vector(0, 1, 0));
            plane.NormalAt(new Point(0, 50,0)).ShouldBe(new Vector(0, 1, 0));
            plane.NormalAt(new Point(0, 0, 50)).ShouldBe(new Vector(0, 1, 0));
            plane.NormalAt(new Point(1, 1, 1)).ShouldBe(new Vector(0, 1, 0));
        }

        [Fact]
        public void Translated_Normal_Calculations()
        {
            var plane = new Plane {TransformMatrix = Matrix4X4.CreateTranslation(10, 10, 10)};
            
            plane.NormalAt(new Point(1, 0, 0)).ShouldBe(new Vector(0, 1, 0));
            plane.NormalAt(new Point(0, 50,0)).ShouldBe(new Vector(0, 1, 0));
            plane.NormalAt(new Point(0, 0, 50)).ShouldBe(new Vector(0, 1, 0));
            plane.NormalAt(new Point(1, 1, 1)).ShouldBe(new Vector(0, 1, 0));
        }        
        
        [Fact]
        public void Rotated_Normal_Calculations()
        {
            var transform = new Transform()
                .RotateZ(Math.PI / 2)
                .RotateY(Math.PI / 2)
                .GetTransformationMatrix();

            var plane = new Plane {TransformMatrix = transform};
            
            plane.NormalAt(new Point(1, 0, 0)).ShouldBe(new Vector(0, 0, 1));
            plane.NormalAt(new Point(0, 50,0)).ShouldBe(new Vector(0, 0, 1));
            plane.NormalAt(new Point(0, 0, 50)).ShouldBe(new Vector(0, 0, 1));
            plane.NormalAt(new Point(1, 1, 1)).ShouldBe(new Vector(0, 0, 1));
        }

        [Fact]
        public void No_Intersection_With_Parallel_Ray()
        {
            var plane = new Plane();
            var ray = new Ray(new Point(0, 10, 0), new Vector(0, 0, 1));

            var intersections = plane.GetIntersections(ray);
            intersections.Count.ShouldBe(0);
        }
        
        [Fact]
        public void No_Intersection_With_Coplanar_Ray()
        {
            var plane = new Plane();
            var ray = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));

            var intersections = plane.GetIntersections(ray);
            intersections.Count.ShouldBe(0);
        }

        [Fact]
        public void Intersection_From_Above()
        {
            var plane = new Plane();
            var ray = new Ray(new Point(0, 1, 0), new Vector(0, -1, 0));

            var intersections = plane.GetIntersections(ray);
            
            intersections.Count.ShouldBe(1);
            intersections[0].Time.ShouldBe(1);
            intersections[0].Object.ShouldBe(plane);
        }

        [Fact]
        public void Intersection_From_Below()
        {
            var plane = new Plane();
            var ray = new Ray(new Point(0, -5, 0), new Vector(0, 1, 0));

            var intersections = plane.GetIntersections(ray);
            
            intersections.Count.ShouldBe(1);
            intersections[0].Time.ShouldBe(5);
            intersections[0].Object.ShouldBe(plane);
        }
    }
}
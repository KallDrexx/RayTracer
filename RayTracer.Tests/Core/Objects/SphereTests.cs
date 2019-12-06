using System;
using RayTracer.Common.Core;
using RayTracer.Common.Core.Objects;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core.Objects
{
    public class SphereTests
    {
        [Fact]
        public void New_Sphere_Has_Identity_Matrix_Transformation()
        {
            var sphere = new Sphere();
            
            sphere.TransformMatrix.ShouldBe(Matrix4X4.IdentityMatrix);
        }

        [Fact]
        public void Can_Change_Sphere_Transform()
        {
            var sphere = new Sphere();
            var transform = Matrix4X4.CreateTranslation(1, 2, 3);

            sphere.TransformMatrix = transform;
            
            sphere.TransformMatrix.ShouldBe(transform);
        }

        [Fact]
        public void Unit_Sphere_Normal_Calculations()
        {
            var sphere = new Sphere();
            
            sphere.NormalAt(new Point(1, 0, 0)).ShouldBe(new Vector(1, 0, 0));
            sphere.NormalAt(new Point(0, 1, 0)).ShouldBe(new Vector(0, 1, 0));
            sphere.NormalAt(new Point(0, 0, 1)).ShouldBe(new Vector(0, 0, 1));
            sphere.NormalAt(new Point(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3))
                .ShouldBe(new Vector(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3));
        }

        [Fact]
        public void Transformed_Sphere_Normal_Calculations()
        {
            var transform = new Transform()
                .RotateZ(Math.PI / 5)
                .Scale(1, 0.5, 1);
            
            var translatedSphere = new Sphere(Matrix4X4.CreateTranslation(0, 1, 0));
            var transformedSphere = new Sphere(transform.GetTransformationMatrix());
            
            translatedSphere.NormalAt(new Point(0, 1.70711, -0.70711))
                .ShouldBe(new Vector(0, 0.70711, -0.70711));

            transformedSphere.NormalAt(new Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2))
                .ShouldBe(new Vector(0, 0.97014, -0.24254));
        }
        
        [Fact]
        public void Calculate_Intersection_Through_Sphere_Origin()
        {
            var sphere = new Sphere();
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            var intersections = sphere.GetIntersections(ray);
            
            intersections.Count.ShouldBe(2);
            intersections.ShouldContain(new Intersection(4, sphere));
            intersections.ShouldContain(new Intersection(6, sphere));
        }

        [Fact]
        public void Calculate_Intersection_At_Tangent()
        {
            var sphere = new Sphere();
            var ray = new Ray(new Point(0, 1, -5), new Vector(0, 0, 1));

            var intersections = sphere.GetIntersections(ray);
            
            intersections.Count.ShouldBe(2);
            intersections.ShouldContain(new Intersection(5, sphere));
            intersections.ShouldContain(new Intersection(5, sphere));
        }

        [Fact]
        public void No_Intersection_When_Ray_Doesnt_Hit_Sphere()
        {
            var sphere = new Sphere();
            var ray = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));

            var intersections = sphere.GetIntersections(ray);
            
            intersections.Count.ShouldBe(0);
        }

        [Fact]
        public void Two_Intersections_When_Ray_Originates_Inside_Sphere()
        {
            var sphere = new Sphere();
            var ray = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            
            var intersections = sphere.GetIntersections(ray);
            
            intersections.Count.ShouldBe(2);
            intersections.ShouldContain(new Intersection(-1, sphere));
            intersections.ShouldContain(new Intersection(1, sphere));
        }

        [Fact]
        public void Two_Intersection_When_Ray_Originates_In_Front_Of_Sphere()
        {
            var sphere = new Sphere();
            var ray = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
            
            var intersections = sphere.GetIntersections(ray);
            
            intersections.Count.ShouldBe(2);
            intersections.ShouldContain(new Intersection(-6, sphere));
            intersections.ShouldContain(new Intersection(-4, sphere));
        }

        [Fact]
        public void Scaled_Sphere_Intersection()
        {
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var sphere = new Sphere(Matrix4X4.CreateScale(2, 2, 2));

            var intersections = sphere.GetIntersections(ray);
            
            intersections.Count.ShouldBe(2);
            intersections.ShouldContain(new Intersection(3, sphere));
            intersections.ShouldContain(new Intersection(7, sphere));
        }

        [Fact]
        public void Translated_Sphere_Intersection()
        {
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var sphere = new Sphere(Matrix4X4.CreateTranslation(5, 0, 0));

            var intersections = sphere.GetIntersections(ray);
            
            intersections.Count.ShouldBe(0);
        }
    }
}
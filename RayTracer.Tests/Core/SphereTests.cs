using System;
using RayTracer.Common.Core;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core
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
    }
}
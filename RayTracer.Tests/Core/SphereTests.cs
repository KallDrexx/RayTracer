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
            
            sphere.Transform.ShouldBe(Matrix4X4.IdentityMatrix);
        }

        [Fact]
        public void Can_Change_Sphere_Transform()
        {
            var sphere = new Sphere();
            var transform = Matrix4X4.CreateTranslation(1, 2, 3);

            sphere.Transform = transform;
            
            sphere.Transform.ShouldBe(transform);
        }
    }
}
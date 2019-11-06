using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public class Sphere
    {
        public Matrix4X4 Transform { get; set; }

        public Sphere(Matrix4X4? transform = null)
        {
            Transform = transform ?? Matrix4X4.IdentityMatrix;
        }
    }
}
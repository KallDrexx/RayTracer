using System;
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

        public Vector NormalAt(Point point)
        {
            var (wasInvertible, inverse) = Transform.Invert();
            if (!wasInvertible)
            {
                throw new InvalidOperationException("Sphere had non-invertible transform");
            }

            var objectPoint = inverse * point;
            var objectNormal = objectPoint - new Point(0, 0, 0);
            var worldNormal = inverse.Transpose() * objectNormal;
            return worldNormal.Normalize();
        }
    }
}
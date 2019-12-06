using System;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public abstract class RayTraceableObject
    {
        private Matrix4X4 _transformMatrix;
        
        public Material Material { get; set; }

        public Matrix4X4 TransformMatrix
        {
            get => _transformMatrix;
            set
            {
                _transformMatrix = value;
                var (wasInvertible, inverse) = _transformMatrix.Invert();
                if (!wasInvertible)
                {
                    throw new InvalidOperationException("Object had a transform that can not be inverted");
                }

                InverseTransform = inverse;
            }
        }

        protected Matrix4X4 InverseTransform { get; private set; }
        

        public RayTraceableObject()
        {
            Material = new Material();
            TransformMatrix = Matrix4X4.IdentityMatrix;
        }

        public abstract Vector NormalAt(Point point);
        public abstract IntersectionCollection GetIntersections(Ray ray);

        protected Ray TransformToObjectSpace(Ray ray)
            => new Ray(InverseTransform * ray.Origin, InverseTransform * ray.Direction);

        protected Point TransformToObjectSpace(Point point)
            => InverseTransform * point;

        protected Vector NormalToWorldSpace(Vector normal)
            => InverseTransform.Transpose() * normal;
    }
}
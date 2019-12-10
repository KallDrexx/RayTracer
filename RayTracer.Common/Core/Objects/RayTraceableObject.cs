using System;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core.Objects
{
    public abstract class RayTraceableObject
    {
        private Matrix4X4 _transformMatrix;
        
        public Material Material { get; set; }

        public Matrix4X4 InverseTransform { get; private set; }
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

        protected RayTraceableObject()
        {
            Material = new Material();
            TransformMatrix = Matrix4X4.IdentityMatrix;
        }

        public IntersectionCollection GetIntersections(Ray ray)
        {
            var objectSpaceRay = new Ray(InverseTransform * ray.Origin, InverseTransform * ray.Direction);
            return GetLocalIntersections(objectSpaceRay);
        }
        
        public Vector NormalAt(Point point)
        {
            var objectPoint = InverseTransform * point;
            var objectNormal = LocalNormalAt(objectPoint);
            var worldNormal = InverseTransform.Transpose() * objectNormal;

            return worldNormal.Normalize();
        }

        protected abstract Vector LocalNormalAt(Point objectPoint);
        protected abstract IntersectionCollection GetLocalIntersections(Ray objectSpaceRay);
    }
}
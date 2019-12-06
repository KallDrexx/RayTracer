using System;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core.Objects
{
    public class Sphere : RayTraceableObject
    {
        public Sphere(Matrix4X4? transform = null)
        {
            TransformMatrix = transform ?? Matrix4X4.IdentityMatrix;
        }

        protected override Vector LocalNormalAt(Point objectPoint)
            => objectPoint - new Point(0, 0, 0);

        protected override IntersectionCollection GetLocalIntersections(Ray objectSpaceRay)
        {
            var distance = objectSpaceRay.Origin - new Point(0, 0, 0);
            var a = objectSpaceRay.Direction.Dot(objectSpaceRay.Direction);
            var b = 2 * objectSpaceRay.Direction.Dot(distance);
            var c = distance.Dot(distance) - 1;

            var discriminant = Math.Pow(b, 2) - 4 * a * c;
            if (discriminant < 0)
            {
                // Negative discriminants mean no intersection
                return new IntersectionCollection();
            }

            var firstIntersectionAt = (-b - Math.Sqrt(discriminant)) / (2 * a);
            var secondIntersectionAt = (-b + Math.Sqrt(discriminant)) / (2 * a); 

            return new IntersectionCollection
            {
                new Intersection(firstIntersectionAt, this),
                new Intersection(secondIntersectionAt, this), 
            };
        }
    }
}
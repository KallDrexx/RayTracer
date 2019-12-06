using System;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public class Sphere : RayTraceableObject
    {
        public Sphere(Matrix4X4? transform = null)
        {
            TransformMatrix = transform ?? Matrix4X4.IdentityMatrix;
        }

        public override Vector NormalAt(Point point)
        {
            var objectPoint = TransformToObjectSpace(point);
            var objectNormal = objectPoint - new Point(0, 0, 0);
            var worldNormal = NormalToWorldSpace(objectNormal);
            return worldNormal.Normalize();
        }

        public override IntersectionCollection GetIntersections(Ray ray)
        {
            var transformedRay = TransformToObjectSpace(ray);
            
            var distance = transformedRay.Origin - new Point(0, 0, 0);
            var a = transformedRay.Direction.Dot(transformedRay.Direction);
            var b = 2 * transformedRay.Direction.Dot(distance);
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
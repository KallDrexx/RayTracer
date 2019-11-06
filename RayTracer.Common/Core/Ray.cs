using System;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public struct Ray : IEquatable<Ray>
    {
        public Point Origin { get; }
        public Vector Direction { get; }
        
        public Ray(Point origin, Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Point PositionAt(double timeUnits)
        {
            return Origin + (Direction * timeUnits);
        }

        public Ray Transform(Matrix4X4 transformationMatrix)
        {
            var newOrigin = transformationMatrix * Origin;
            var newVector = transformationMatrix * Direction;
            
            return new Ray(newOrigin, newVector);
        }

        public IntersectionCollection Intersects(Sphere sphere)
        {
            var (wasInvertible, inverse) = sphere.Transform.Invert();
            if (!wasInvertible)
            {
                throw new InvalidOperationException("Sphere had a transform that can not be inverted");
            }
            
            // Transform the ray to account for the sphere's transformation matrix
            var transformedRay = Transform(inverse);
            
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
                new Intersection(firstIntersectionAt, sphere),
                new Intersection(secondIntersectionAt, sphere), 
            };
        }

        public bool Equals(Ray other)
        {
            return Origin.Equals(other.Origin) && 
                   Direction.Equals(other.Direction);
        }

        public override bool Equals(object obj)
        {
            return obj is Ray other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Origin.GetHashCode() * 397) ^ Direction.GetHashCode();
            }
        }
    }
}
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
            return Origin + Direction * timeUnits;
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
using System;
using RayTracer.Common.Core.Objects;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public struct Intersection : IEquatable<Intersection>
    {
        public double Time { get; }
        public RayTraceableObject Object { get; }

        public Intersection(double time, RayTraceableObject o)
        {
            Time = time;
            Object = o;
        }

        public IntersectionPreComputation GetPreComputation(Ray intersectingRay)
        {
            var position = intersectingRay.PositionAt(Time);
            var eyeVector = -intersectingRay.Direction;
            var normalVector = Object.NormalAt(position);
            var inside = false;

            if (normalVector.Dot(eyeVector) < 0)
            {
                normalVector = -normalVector;
                inside = true;
            }

            var overPoint = position + normalVector * 0.0001f;

            return new IntersectionPreComputation(Time, Object, position, eyeVector, normalVector, inside, overPoint);
        }

        public override string ToString()
        {
            return $"Intersection with {Object} at time {Time}";
        }

        public bool Equals(Intersection other)
        {
            return Time.Equals(other.Time) && 
                   Equals(Object, other.Object);
        }

        public override bool Equals(object obj)
        {
            return obj is Intersection other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Time.GetHashCode() * 397) ^ (Object != null ? Object.GetHashCode() : 0);
            }
        }
    }
}
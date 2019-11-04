using System;

namespace RayTracer.Common.Core
{
    public struct Intersection : IEquatable<Intersection>
    {
        public double Time { get; }
        public object Object { get; }

        public Intersection(double time, object o)
        {
            Time = time;
            Object = o;
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
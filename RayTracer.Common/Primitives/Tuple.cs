using System;

namespace RayTracer.Common.Primitives
{
    public struct Tuple : IEquatable<Tuple>
    {
        private const double Epsilon = 0.00001f;
        
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public double W { get; }
        
        public double Magnitude => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2) + Math.Pow(W, 2));

        public Tuple(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static bool operator ==(Tuple first, Tuple second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Tuple first, Tuple second)
        {
            return !first.Equals(second);
        }

        public static Tuple operator +(Tuple first, Tuple second)
        {
            return new Tuple(first.X + second.X,
                first.Y + second.Y,
                first.Z + second.Z,
                first.W + second.W);
        }
        
        public static Tuple operator -(Tuple first, Tuple second)
        {
            return new Tuple(first.X - second.X,
                first.Y - second.Y,
                first.Z - second.Z,
                first.W - second.W);
        }

        public static Tuple operator -(Tuple tuple)
        {
            return new Tuple(-tuple.X, -tuple.Y, -tuple.Z, -tuple.W);
        }

        public static Tuple operator *(Tuple tuple, double scalar)
        {
            return new Tuple(tuple.X * scalar, tuple.Y * scalar, tuple.Z * scalar, tuple.W * scalar);
        }
        
        public static Tuple operator /(Tuple tuple, double scalar)
        {
            return new Tuple(tuple.X / scalar, tuple.Y / scalar, tuple.Z / scalar, tuple.W / scalar);
        }

        public Tuple Normalize()
        {
            return new Tuple(X / Magnitude,
                Y / Magnitude,
                Z / Magnitude,
                W / Magnitude);
        }

        public double DotProduct(Tuple other)
        {
            return X * other.X +
                   Y * other.Y +
                   Z * other.Z +
                   W * other.W;
        }

        public bool Equals(Tuple other)
        {
            return Math.Abs(X - other.X) < Epsilon &&
                   Math.Abs(Y - other.Y) < Epsilon &&
                   Math.Abs(Z - other.Z) < Epsilon &&
                   Math.Abs(W - other.W) < Epsilon;
        }

        public override bool Equals(object obj)
        {
            return obj is Tuple other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                hashCode = (hashCode * 397) ^ W.GetHashCode();
                return hashCode;
            }
        }
        
        public override string ToString()
        {
            return $"Tuple ({X}, {Y}, {Z}, {W})";
        }
    }
}
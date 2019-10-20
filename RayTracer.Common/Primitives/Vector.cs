using System;

namespace RayTracer.Common.Primitives
{
    public struct Vector : IEquatable<Vector>
    {
        private const float Epsilon = 0.00001f;
        
        public static readonly Vector Zero  = new Vector(0f, 0f, 0f);
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public double Magnitude => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static bool operator ==(Vector first, Vector second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Vector first, Vector second)
        {
            return !first.Equals(second);
        }

        public static Vector operator +(Vector first, Vector second)
        {
            return new Vector(first.X + second.X,
                first.Y + second.Y,
                first.Z + second.Z);
        }

        public static Vector operator -(Vector first, Vector second)
        {
            return new Vector(first.X - second.X,
                first.Y - second.Y,
                first.Z - second.Z);
        }

        public static Vector operator -(Vector vector)
        {
            return Zero - vector;
        }

        public static Vector operator *(Vector vector, double scalar)
        {
            return new Vector(vector.X * scalar,
                vector.Y * scalar,
                vector.Z * scalar);
        }

        public static Vector operator /(Vector vector, double scalar)
        {
            return new Vector(vector.X / scalar,
                vector.Y / scalar,
                vector.Z / scalar);
        }

        public Vector Normalize()
        {
            var magnitude = Magnitude;
            return new Vector(X / magnitude, Y / magnitude, Z / magnitude);
        }

        public double Dot(Vector other)
        {
            return X * other.X +
                   Y * other.Y +
                   Z * other.Z;
        }

        public Vector Cross(Vector other)
        {
            return new Vector(Y * other.Z - Z * other.Y,
                Z * other.X - X * other.Z,
                X * other.Y - Y * other.X);
        }
        
        public bool Equals(Vector other)
        {
            return Math.Abs(X - other.X) < Epsilon &&
                   Math.Abs(Y - other.Y) < Epsilon &&
                   Math.Abs(Z - other.Z) < Epsilon;
        }
        
        public override string ToString()
        {
            return $"Vector ({X}, {Y}, {Z})";
        }

        public override bool Equals(object obj)
        {
            return obj is Vector other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }
    }
}
using System;

namespace RayTracer.Common.Primitives
{
    public struct Vector : IEquatable<Vector>
    {
        private const float Epsilon = 0.00001f;
        
        public static readonly Vector Zero  = new Vector(0f, 0f, 0f);
        
        internal Tuple Value { get; }
        public double X => Value.X;
        public double Y => Value.Y;
        public double Z => Value.Z;

        public double Magnitude => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));

        public Vector(double x, double y, double z)
        {
            Value = new Tuple(x, y, z, 0);
        }

        internal Vector(Tuple tuple)
        {
            if (Math.Abs(tuple.W) > Epsilon)
            {
                throw new InvalidOperationException("Tuple is not a valid vector tuple");
            }

            Value = tuple;
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
            var tuple = first.Value + second.Value;
            return new Vector(tuple);
        }

        public static Vector operator -(Vector first, Vector second)
        {
            var tuple = first.Value - second.Value;
            return new Vector(tuple);
        }

        public static Vector operator -(Vector vector)
        {
            return new Vector(-vector.Value);
        }

        public static Vector operator *(Vector vector, double scalar)
        {
            var tuple = vector.Value * scalar;
            return new Vector(tuple);
        }

        public static Vector operator /(Vector vector, double scalar)
        {
            var tuple = vector.Value / scalar;
            return new Vector(tuple);
        }

        public Vector Normalize()
        {
            return new Vector(Value.Normalize());
        }

        public double Dot(Vector other)
        {
            return Value.DotProduct(other.Value);
        }

        public Vector Cross(Vector other)
        {
            return new Vector(Y * other.Z - Z * other.Y,
                Z * other.X - X * other.Z,
                X * other.Y - Y * other.X);
        }

        public Vector Reflect(Vector normal)
        {
            return this - normal * 2 * Dot(normal);
        }
        
        public bool Equals(Vector other)
        {
            return Value.Equals(other.Value);
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
            return Value.GetHashCode();
        }
    }
}
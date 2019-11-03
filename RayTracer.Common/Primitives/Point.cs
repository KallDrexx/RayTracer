using System;

namespace RayTracer.Common.Primitives
{
    public struct Point : IEquatable<Point>
    {
        private const float Epsilon = 0.00001f;
        
        internal Tuple Value { get; }
        public double X => Value.X;
        public double Y => Value.Y;
        public double Z => Value.Z;

        public Point(double x, double y, double z)
        {
            Value = new Tuple(x, y, z, 1);
        }

        internal Point(Tuple tuple)
        {
            if (Math.Abs(tuple.W - 1) > Epsilon)
            {
                throw new InvalidOperationException("Tuple is not a valid tuple representing a point");
            }

            Value = tuple;
        }

        public static bool operator ==(Point first, Point second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Point first, Point second)
        {
            return !first.Equals(second);
        }

        public static Point operator +(Point point, Vector vector)
        {
            var tuple = point.Value + vector.Value;
            return new Point(tuple);
        }
        
        public static Point operator +(Vector vector, Point point)
        {
            var tuple = point.Value + vector.Value;
            return new Point(tuple);
        }

        public static Point operator -(Point point, Vector vector)
        {
            var tuple = point.Value - vector.Value;
            return new Point(tuple);
        }

        public static Vector operator -(Point first, Point second)
        {
            var tuple = first.Value - second.Value;
            return new Vector(tuple);
        }

        public bool Equals(Point other)
        {
            return Value.Equals(other.Value);
        }

        public override string ToString()
        {
            return $"Point ({X}, {Y}, {Z})";
        }

        public override bool Equals(object obj)
        {
            return obj is Point other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
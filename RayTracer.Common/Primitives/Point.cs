using System;

namespace RayTracer.Common.Primitives
{
    public struct Point : IEquatable<Point>
    {
        private const float Epsilon = 0.00001f;
        
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
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
            return new Point(point.X + vector.X,
                point.Y + vector.Y,
                point.Z + vector.Z);
        }
        
        public static Point operator +(Vector vector, Point point)
        {
            return new Point(point.X + vector.X,
                point.Y + vector.Y,
                point.Z + vector.Z);
        }

        public static Point operator -(Point point, Vector vector)
        {
            return new Point(point.X - vector.X,
                point.Y - vector.Y,
                point.Z - vector.Z);
        }

        public static Vector operator -(Point first, Point second)
        {
            return new Vector(first.X - second.X,
                first.Y - second.Y,
                first.Z - second.Z);
        }
        
        public static Point operator *(Matrix4X4 matrix, Point point)
        {
            // Dot product of the point to each row in the matrix
            var x = matrix.M11 * point.X +
                    matrix.M12 * point.Y +
                    matrix.M13 * point.Z +
                    matrix.M14;
            
            var y = matrix.M21 * point.X +
                    matrix.M22 * point.Y +
                    matrix.M23 * point.Z +
                    matrix.M24;
            
            var z = matrix.M31 * point.X +
                    matrix.M32 * point.Y +
                    matrix.M33 * point.Z +
                    matrix.M34;
            
            return new Point(x, y, z);
        }

        public bool Equals(Point other)
        {
            return Math.Abs(X - other.X) < Epsilon &&
                   Math.Abs(Y - other.Y) < Epsilon &&
                   Math.Abs(Z - other.Z) < Epsilon;
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
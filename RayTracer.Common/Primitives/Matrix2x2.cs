using System;

namespace RayTracer.Common.Primitives
{
    public struct Matrix2X2 : IEquatable<Matrix2X2>
    {
        private const float Epsilon = 0.00001f;

        public double Row0Col0, Row0Col1, 
            Row1Col0, Row1Col1;

        public static bool operator ==(Matrix2X2 first, Matrix2X2 second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Matrix2X2 first, Matrix2X2 second)
        {
            return !first.Equals(second);
        }

        public bool Equals(Matrix2X2 other)
        {
            return Math.Abs(Row0Col0 - other.Row0Col0) < Epsilon && 
                   Math.Abs(Row0Col1 - other.Row0Col1) < Epsilon && 
                   Math.Abs(Row1Col0 - other.Row1Col0) < Epsilon &&
                   Math.Abs(Row1Col1 - other.Row1Col1) < Epsilon;
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix2X2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Row0Col0.GetHashCode();
                hashCode = (hashCode * 397) ^ Row0Col1.GetHashCode();
                hashCode = (hashCode * 397) ^ Row1Col0.GetHashCode();
                hashCode = (hashCode * 397) ^ Row1Col1.GetHashCode();
                return hashCode;
            }
        }
    }
}
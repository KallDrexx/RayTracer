using System;

namespace RayTracer.Common.Primitives
{
    public struct Matrix3X3 : IEquatable<Matrix3X3>
    {
        private const float Epsilon = 0.00001f;

        public double Row0Col0, Row0Col1, Row0Col2, 
            Row1Col0, Row1Col1, Row1Col2,
            Row2Col0, Row2Col1, Row2Col2;

        public static bool operator ==(Matrix3X3 first, Matrix3X3 second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Matrix3X3 first, Matrix3X3 second)
        {
            return !first.Equals(second);
        }

        public bool Equals(Matrix3X3 other)
        {
            return Math.Abs(Row0Col0 - other.Row0Col0) < Epsilon && 
                   Math.Abs(Row0Col1 - other.Row0Col1) < Epsilon && 
                   Math.Abs(Row0Col2 - other.Row0Col2) < Epsilon &&
                   Math.Abs(Row1Col0 - other.Row1Col0) < Epsilon && 
                   Math.Abs(Row1Col1 - other.Row1Col1) < Epsilon && 
                   Math.Abs(Row1Col2 - other.Row1Col2) < Epsilon &&
                   Math.Abs(Row2Col0 - other.Row2Col0) < Epsilon && 
                   Math.Abs(Row2Col1 - other.Row2Col1) < Epsilon && 
                   Math.Abs(Row2Col2 - other.Row2Col2) < Epsilon;
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
                hashCode = (hashCode * 397) ^ Row0Col2.GetHashCode();
                hashCode = (hashCode * 397) ^ Row1Col0.GetHashCode();
                hashCode = (hashCode * 397) ^ Row1Col1.GetHashCode();
                hashCode = (hashCode * 397) ^ Row1Col2.GetHashCode();
                hashCode = (hashCode * 397) ^ Row2Col0.GetHashCode();
                hashCode = (hashCode * 397) ^ Row2Col1.GetHashCode();
                hashCode = (hashCode * 397) ^ Row2Col2.GetHashCode();
                return hashCode;
            }
        }
    }
}
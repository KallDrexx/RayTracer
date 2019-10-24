using System;

namespace RayTracer.Common.Primitives
{
    public struct Matrix4X4 : IEquatable<Matrix4X4>
    {
        private const float Epsilon = 0.00001f;

        public static readonly Matrix4X4 IdentityMatrix = new Matrix4X4
        {
            Row0Col0 = 1, Row1Col1 = 1, Row2Col2 = 1, Row3Col3 = 1
        };

        public double Row0Col0, Row0Col1, Row0Col2, Row0Col3,
            Row1Col0, Row1Col1, Row1Col2, Row1Col3,
            Row2Col0, Row2Col1, Row2Col2, Row2Col3,
            Row3Col0, Row3Col1, Row3Col2, Row3Col3;

        public static bool operator ==(Matrix4X4 first, Matrix4X4 second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Matrix4X4 first, Matrix4X4 second)
        {
            return !first.Equals(second);
        }

        public static Matrix4X4 operator *(Matrix4X4 first, Matrix4X4 second)
        {
            return new Matrix4X4
            {
                Row0Col0 = first.Row0Col0 * second.Row0Col0 +
                           first.Row0Col1 * second.Row1Col0 +
                           first.Row0Col2 * second.Row2Col0 +
                           first.Row0Col3 * second.Row3Col0,

                Row0Col1 = first.Row0Col0 * second.Row0Col1 +
                           first.Row0Col1 * second.Row1Col1 +
                           first.Row0Col2 * second.Row2Col1 +
                           first.Row0Col3 * second.Row3Col1,

                Row0Col2 = first.Row0Col0 * second.Row0Col2 +
                           first.Row0Col1 * second.Row1Col2 +
                           first.Row0Col2 * second.Row2Col2 +
                           first.Row0Col3 * second.Row3Col2,

                Row0Col3 = first.Row0Col0 * second.Row0Col3 +
                           first.Row0Col1 * second.Row1Col3 +
                           first.Row0Col2 * second.Row2Col3 +
                           first.Row0Col3 * second.Row3Col3,

                Row1Col0 = first.Row1Col0 * second.Row0Col0 +
                           first.Row1Col1 * second.Row1Col0 +
                           first.Row1Col2 * second.Row2Col0 +
                           first.Row1Col3 * second.Row3Col0,

                Row1Col1 = first.Row1Col0 * second.Row0Col1 +
                           first.Row1Col1 * second.Row1Col1 +
                           first.Row1Col2 * second.Row2Col1 +
                           first.Row1Col3 * second.Row3Col1,

                Row1Col2 = first.Row1Col0 * second.Row0Col2 +
                           first.Row1Col1 * second.Row1Col2 +
                           first.Row1Col2 * second.Row2Col2 +
                           first.Row1Col3 * second.Row3Col2,

                Row1Col3 = first.Row1Col0 * second.Row0Col3 +
                           first.Row1Col1 * second.Row1Col3 +
                           first.Row1Col2 * second.Row2Col3 +
                           first.Row1Col3 * second.Row3Col3,

                Row2Col0 = first.Row2Col0 * second.Row0Col0 +
                           first.Row2Col1 * second.Row1Col0 +
                           first.Row2Col2 * second.Row2Col0 +
                           first.Row2Col3 * second.Row3Col0,

                Row2Col1 = first.Row2Col0 * second.Row0Col1 +
                           first.Row2Col1 * second.Row1Col1 +
                           first.Row2Col2 * second.Row2Col1 +
                           first.Row2Col3 * second.Row3Col1,

                Row2Col2 = first.Row2Col0 * second.Row0Col2 +
                           first.Row2Col1 * second.Row1Col2 +
                           first.Row2Col2 * second.Row2Col2 +
                           first.Row2Col3 * second.Row3Col2,

                Row2Col3 = first.Row2Col0 * second.Row0Col3 +
                           first.Row2Col1 * second.Row1Col3 +
                           first.Row2Col2 * second.Row2Col3 +
                           first.Row2Col3 * second.Row3Col3,

                Row3Col0 = first.Row3Col0 * second.Row0Col0 +
                           first.Row3Col1 * second.Row1Col0 +
                           first.Row3Col2 * second.Row2Col0 +
                           first.Row3Col3 * second.Row3Col0,

                Row3Col1 = first.Row3Col0 * second.Row0Col1 +
                           first.Row3Col1 * second.Row1Col1 +
                           first.Row3Col2 * second.Row2Col1 +
                           first.Row3Col3 * second.Row3Col1,

                Row3Col2 = first.Row3Col0 * second.Row0Col2 +
                           first.Row3Col1 * second.Row1Col2 +
                           first.Row3Col2 * second.Row2Col2 +
                           first.Row3Col3 * second.Row3Col2,

                Row3Col3 = first.Row3Col0 * second.Row0Col3 +
                           first.Row3Col1 * second.Row1Col3 +
                           first.Row3Col2 * second.Row2Col3 +
                           first.Row3Col3 * second.Row3Col3,
            };
        }

        public Matrix4X4 Transpose()
        {
            return new Matrix4X4();
        }

        public override string ToString()
        {
            return $"{Row0Col0}, {Row0Col1}, {Row0Col2}, {Row0Col3}{Environment.NewLine}" +
                   $"{Row1Col0}, {Row1Col1}, {Row1Col2}, {Row1Col3}{Environment.NewLine}" +
                   $"{Row2Col0}, {Row2Col1}, {Row2Col2}, {Row2Col3}{Environment.NewLine}" +
                   $"{Row3Col0}, {Row3Col1}, {Row3Col2}, {Row3Col3}";
        }

        public bool Equals(Matrix4X4 other)
        {
            return Math.Abs(Row0Col0 - other.Row0Col0) < Epsilon && 
                   Math.Abs(Row0Col1 - other.Row0Col1) < Epsilon && 
                   Math.Abs(Row0Col2 - other.Row0Col2) < Epsilon &&
                   Math.Abs(Row0Col3 - other.Row0Col3) < Epsilon &&
                   Math.Abs(Row1Col0 - other.Row1Col0) < Epsilon && 
                   Math.Abs(Row1Col1 - other.Row1Col1) < Epsilon && 
                   Math.Abs(Row1Col2 - other.Row1Col2) < Epsilon &&
                   Math.Abs(Row1Col3 - other.Row1Col3) < Epsilon &&
                   Math.Abs(Row2Col0 - other.Row2Col0) < Epsilon && 
                   Math.Abs(Row2Col1 - other.Row2Col1) < Epsilon && 
                   Math.Abs(Row2Col2 - other.Row2Col2) < Epsilon &&
                   Math.Abs(Row2Col3 - other.Row2Col3) < Epsilon &&
                   Math.Abs(Row3Col0 - other.Row3Col0) < Epsilon && 
                   Math.Abs(Row3Col1 - other.Row3Col1) < Epsilon && 
                   Math.Abs(Row3Col2 - other.Row3Col2) < Epsilon &&
                   Math.Abs(Row3Col3 - other.Row3Col3) < Epsilon;
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
                hashCode = (hashCode * 397) ^ Row0Col3.GetHashCode();
                hashCode = (hashCode * 397) ^ Row1Col0.GetHashCode();
                hashCode = (hashCode * 397) ^ Row1Col1.GetHashCode();
                hashCode = (hashCode * 397) ^ Row1Col2.GetHashCode();
                hashCode = (hashCode * 397) ^ Row1Col3.GetHashCode();
                hashCode = (hashCode * 397) ^ Row2Col0.GetHashCode();
                hashCode = (hashCode * 397) ^ Row2Col1.GetHashCode();
                hashCode = (hashCode * 397) ^ Row2Col2.GetHashCode();
                hashCode = (hashCode * 397) ^ Row2Col3.GetHashCode();
                hashCode = (hashCode * 397) ^ Row3Col0.GetHashCode();
                hashCode = (hashCode * 397) ^ Row3Col1.GetHashCode();
                hashCode = (hashCode * 397) ^ Row3Col2.GetHashCode();
                hashCode = (hashCode * 397) ^ Row3Col3.GetHashCode();
                return hashCode;
            }
        }
    }
}
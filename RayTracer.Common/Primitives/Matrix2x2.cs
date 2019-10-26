using System;

namespace RayTracer.Common.Primitives
{
    public struct Matrix2X2 : IEquatable<Matrix2X2>
    {
        private const float Epsilon = 0.00001f;

        public double M11, M12, 
            M21, M22;

        public static bool operator ==(Matrix2X2 first, Matrix2X2 second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Matrix2X2 first, Matrix2X2 second)
        {
            return !first.Equals(second);
        }

        public double Determinant()
        {
            return M11 * M22 - M12 * M21;
        }

        public override string ToString()
        {
            return $"{M11}, {M12}{Environment.NewLine}" +
                   $"{M21}, {M22}{Environment.NewLine}";
        }

        public bool Equals(Matrix2X2 other)
        {
            return Math.Abs(M11 - other.M11) < Epsilon && 
                   Math.Abs(M12 - other.M12) < Epsilon && 
                   Math.Abs(M21 - other.M21) < Epsilon &&
                   Math.Abs(M22 - other.M22) < Epsilon;
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix2X2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = M11.GetHashCode();
                hashCode = (hashCode * 397) ^ M12.GetHashCode();
                hashCode = (hashCode * 397) ^ M21.GetHashCode();
                hashCode = (hashCode * 397) ^ M22.GetHashCode();
                return hashCode;
            }
        }
    }
}
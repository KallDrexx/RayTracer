using System;

namespace RayTracer.Common.Primitives
{
    public struct Matrix3X3 : IEquatable<Matrix3X3>
    {
        private const float Epsilon = 0.00001f;

        public double M11, M12, M13, 
            M21, M22, M23,
            M31, M32, M33;

        public static bool operator ==(Matrix3X3 first, Matrix3X3 second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Matrix3X3 first, Matrix3X3 second)
        {
            return !first.Equals(second);
        }

        public Matrix2X2 GetSubMatrix(int rowToRemove, int columnToRemove)
        {
            if (rowToRemove <= 0 || rowToRemove > 3 || columnToRemove <= 0 || columnToRemove > 3)
            {
                throw new InvalidOperationException($"({rowToRemove}, {columnToRemove}) is out of bounds for a 3x3 matrix");
            }
            
            return new Matrix2X2
            {
                M11 = rowToRemove == 1
                    ? columnToRemove == 1 ? M22 : M21
                    : columnToRemove == 1
                        ? M12
                        : M11,

                M12 = rowToRemove == 1
                    ? columnToRemove == 3 ? M22 : M23
                    : columnToRemove == 3
                        ? M12
                        : M13,

                M21 = rowToRemove == 3
                    ? columnToRemove == 1 ? M22 : M21
                    : columnToRemove == 1
                        ? M32
                        : M31,

                M22 = rowToRemove == 3
                    ? columnToRemove == 3 ? M22 : M23
                    : columnToRemove == 3
                        ? M32
                        : M33,
            };
        }

        public double GetMinor(int row, int column)
        {
            ValidateRowColumnBounds(row, column);

            var subMatrix = GetSubMatrix(row, column);
            return subMatrix.GetDeterminant();
        }

        public double GetCoFactor(int row, int column)
        {
            ValidateRowColumnBounds(row, column);

            var minor = GetMinor(row, column);
            return (row + column) % 2 == 0 ? minor : -minor;
        }

        public double GetDeterminant()
        {
            return M11 * GetCoFactor(1, 1) +
                   M12 * GetCoFactor(1, 2) +
                   M13 * GetCoFactor(1, 3);
        }

        public bool Equals(Matrix3X3 other)
        {
            return Math.Abs(M11 - other.M11) < Epsilon && 
                   Math.Abs(M12 - other.M12) < Epsilon && 
                   Math.Abs(M13 - other.M13) < Epsilon &&
                   Math.Abs(M21 - other.M21) < Epsilon && 
                   Math.Abs(M22 - other.M22) < Epsilon && 
                   Math.Abs(M23 - other.M23) < Epsilon &&
                   Math.Abs(M31 - other.M31) < Epsilon && 
                   Math.Abs(M32 - other.M32) < Epsilon && 
                   Math.Abs(M33 - other.M33) < Epsilon;
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
                hashCode = (hashCode * 397) ^ M13.GetHashCode();
                hashCode = (hashCode * 397) ^ M21.GetHashCode();
                hashCode = (hashCode * 397) ^ M22.GetHashCode();
                hashCode = (hashCode * 397) ^ M23.GetHashCode();
                hashCode = (hashCode * 397) ^ M31.GetHashCode();
                hashCode = (hashCode * 397) ^ M32.GetHashCode();
                hashCode = (hashCode * 397) ^ M33.GetHashCode();
                return hashCode;
            }
        }
        
        public override string ToString()
        {
            return $"{M11}, {M12}, {M13}{Environment.NewLine}" +
                   $"{M21}, {M22}, {M23}{Environment.NewLine}" +
                   $"{M31}, {M32}, {M33}{Environment.NewLine}";
        }

        private static void ValidateRowColumnBounds(int row, int column)
        {
            if (row <= 0 || row > 3 || column <= 0 || column > 3)
            {
                throw new InvalidOperationException($"({row}, {column}) is out of bounds for a 3x3 matrix");
            }
        }
    }
}
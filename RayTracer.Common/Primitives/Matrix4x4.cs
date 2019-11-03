using System;

namespace RayTracer.Common.Primitives
{
    public struct Matrix4X4 : IEquatable<Matrix4X4>
    {
        private const float Epsilon = 0.00001f;

        public static readonly Matrix4X4 IdentityMatrix = new Matrix4X4
        {
            M11 = 1, M22 = 1, M33 = 1, M44 = 1
        };

        public double M11, M12, M13, M14,
            M21, M22, M23, M24,
            M31, M32, M33, M34,
            M41, M42, M43, M44;

        public static Matrix4X4 CreateTranslation(double x, double y, double z)
        {
            var identity = IdentityMatrix;
            identity.M14 = x;
            identity.M24 = y;
            identity.M34 = z;

            return identity;
        }

        public static Matrix4X4 CreateScale(double x, double y, double z)
        {
            var identity = IdentityMatrix;
            identity.M11 = x;
            identity.M22 = y;
            identity.M33 = z;

            return identity;
        }

        public static Matrix4X4 CreateRotationX(double radians)
        {
            var matrix = IdentityMatrix;
            matrix.M22 = Math.Cos(radians);
            matrix.M23 = -Math.Sin(radians);
            matrix.M32 = Math.Sin(radians);
            matrix.M33 = Math.Cos(radians);

            return matrix;
        }

        public static Matrix4X4 CreateRotationY(double radians)
        {
            var matrix = IdentityMatrix;
            matrix.M11 = Math.Cos(radians);
            matrix.M13 = Math.Sin(radians);
            matrix.M31 = -Math.Sin(radians);
            matrix.M33 = Math.Cos(radians);

            return matrix;
        }

        public static Matrix4X4 CreateRotationZ(double radians)
        {
            var matrix = IdentityMatrix;
            matrix.M11 = Math.Cos(radians);
            matrix.M12 = -Math.Sin(radians);
            matrix.M21 = Math.Sin(radians);
            matrix.M22 = Math.Cos(radians);

            return matrix;
        }

        public static Matrix4X4 CreateSkew(double xToY, double xToZ, double yToX, double yToZ, double zToX, double zToY)
        {
            var matrix = IdentityMatrix;
            matrix.M12 = xToY;
            matrix.M13 = xToZ;
            matrix.M21 = yToX;
            matrix.M23 = yToZ;
            matrix.M31 = zToX;
            matrix.M32 = zToY;

            return matrix;
        }
        
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
                M11 = first.M11 * second.M11 +
                           first.M12 * second.M21 +
                           first.M13 * second.M31 +
                           first.M14 * second.M41,

                M12 = first.M11 * second.M12 +
                           first.M12 * second.M22 +
                           first.M13 * second.M32 +
                           first.M14 * second.M42,

                M13 = first.M11 * second.M13 +
                           first.M12 * second.M23 +
                           first.M13 * second.M33 +
                           first.M14 * second.M43,

                M14 = first.M11 * second.M14 +
                           first.M12 * second.M24 +
                           first.M13 * second.M34 +
                           first.M14 * second.M44,

                M21 = first.M21 * second.M11 +
                           first.M22 * second.M21 +
                           first.M23 * second.M31 +
                           first.M24 * second.M41,

                M22 = first.M21 * second.M12 +
                           first.M22 * second.M22 +
                           first.M23 * second.M32 +
                           first.M24 * second.M42,

                M23 = first.M21 * second.M13 +
                           first.M22 * second.M23 +
                           first.M23 * second.M33 +
                           first.M24 * second.M43,

                M24 = first.M21 * second.M14 +
                           first.M22 * second.M24 +
                           first.M23 * second.M34 +
                           first.M24 * second.M44,

                M31 = first.M31 * second.M11 +
                           first.M32 * second.M21 +
                           first.M33 * second.M31 +
                           first.M34 * second.M41,

                M32 = first.M31 * second.M12 +
                           first.M32 * second.M22 +
                           first.M33 * second.M32 +
                           first.M34 * second.M42,

                M33 = first.M31 * second.M13 +
                           first.M32 * second.M23 +
                           first.M33 * second.M33 +
                           first.M34 * second.M43,

                M34 = first.M31 * second.M14 +
                           first.M32 * second.M24 +
                           first.M33 * second.M34 +
                           first.M34 * second.M44,

                M41 = first.M41 * second.M11 +
                           first.M42 * second.M21 +
                           first.M43 * second.M31 +
                           first.M44 * second.M41,

                M42 = first.M41 * second.M12 +
                           first.M42 * second.M22 +
                           first.M43 * second.M32 +
                           first.M44 * second.M42,

                M43 = first.M41 * second.M13 +
                           first.M42 * second.M23 +
                           first.M43 * second.M33 +
                           first.M44 * second.M43,

                M44 = first.M41 * second.M14 +
                           first.M42 * second.M24 +
                           first.M43 * second.M34 +
                           first.M44 * second.M44,
            };
        }
        
        public static Point operator *(Matrix4X4 matrix, Point point)
        {
            var x = matrix.GetTupleForRow(1).DotProduct(point.Value);
            var y = matrix.GetTupleForRow(2).DotProduct(point.Value);
            var z = matrix.GetTupleForRow(3).DotProduct(point.Value);
            var w = matrix.GetTupleForRow(4).DotProduct(point.Value);
            
            return new Point(new Tuple(x, y, z, w));
        }
        
        public static Vector operator *(Matrix4X4 matrix, Vector vector)
        {
            var x = matrix.GetTupleForRow(1).DotProduct(vector.Value);
            var y = matrix.GetTupleForRow(2).DotProduct(vector.Value);
            var z = matrix.GetTupleForRow(3).DotProduct(vector.Value);
            var w = matrix.GetTupleForRow(4).DotProduct(vector.Value);
            
            return new Vector(new Tuple(x, y, z, w));
        }

        public Matrix4X4 Transpose()
        {
            return new Matrix4X4
            {
                M11 = M11,
                M12 = M21,
                M13 = M31,
                M14 = M41,
                M21 = M12,
                M22 = M22,
                M23 = M32,
                M24 = M42,
                M31 = M13,
                M32 = M23,
                M33 = M33,
                M34 = M43,
                M41 = M14,
                M42 = M24,
                M43 = M34,
                M44 = M44,
            };
        }

        public Matrix3X3 GetSubMatrix(int rowToRemove, int columnToRemove)
        {
            if (rowToRemove <= 0 || rowToRemove > 4 || columnToRemove <= 0 || columnToRemove > 4)
            {
                throw new InvalidOperationException($"({rowToRemove}, {columnToRemove}) is out of bounds for a 4x4 matrix");
            }
            
            return new Matrix3X3
            {
                M11 = rowToRemove == 1
                    ? columnToRemove == 1 ? M22 : M21
                    : columnToRemove == 1
                        ? M12
                        : M11,
                
                M12 = rowToRemove == 1
                    ? columnToRemove == 2 || columnToRemove == 1 ? M23 : M22
                    : columnToRemove == 2 || columnToRemove == 1 ? M13 : M12,
                
                M13 = rowToRemove == 1
                      ? columnToRemove == 4 ? M23 : M24
                      : columnToRemove == 4 ? M13 : M14,
                
                M21 = columnToRemove == 1
                      ? rowToRemove == 1 || rowToRemove == 2 ? M32 : M22
                      : rowToRemove == 1 || rowToRemove == 2 ? M31 : M21,
                
                M22 = columnToRemove == 1 || columnToRemove == 2
                      ? rowToRemove == 1 || rowToRemove == 2 ? M33 : M23
                      : rowToRemove == 1 || rowToRemove == 2 ? M32 : M22,
                
                M23 = columnToRemove == 4
                      ? rowToRemove == 1 || rowToRemove == 2 ? M33 : M23
                      : rowToRemove == 1 || rowToRemove == 2 ? M34 : M24,
                
                M31 = rowToRemove == 4
                      ? columnToRemove == 1 ? M32 : M31
                      : columnToRemove == 1 ? M42 : M41,
                
                M32 = rowToRemove == 4
                      ? columnToRemove == 1 || columnToRemove == 2 ? M33 : M32
                      : columnToRemove == 1 || columnToRemove == 2 ? M43 : M42,
                
                M33 = columnToRemove == 4
                      ? rowToRemove == 4 ? M33 : M43
                      : rowToRemove == 4 ? M34 : M44,
            };
        }

        public double Determinant()
        {
            return M11 * GetCoFactor(1, 1) +
                   M12 * GetCoFactor(1, 2) +
                   M13 * GetCoFactor(1, 3) +
                   M14 * GetCoFactor(1, 4);
        }

        public (bool wasInvertible, Matrix4X4 inverse) Invert()
        {
            var determinant = Determinant();
            if (Math.Abs(determinant) < Epsilon)
            {
                return (false, new Matrix4X4());
            }
            
            return (true, new Matrix4X4
            {
                M11 = GetCoFactor(1, 1) / determinant,
                M12 = GetCoFactor(2, 1) / determinant,
                M13 = GetCoFactor(3, 1) / determinant,
                M14 = GetCoFactor(4, 1) / determinant,
                M21 = GetCoFactor(1, 2) / determinant,
                M22 = GetCoFactor(2, 2) / determinant,
                M23 = GetCoFactor(3, 2) / determinant,
                M24 = GetCoFactor(4, 2) / determinant,
                M31 = GetCoFactor(1, 3) / determinant,
                M32 = GetCoFactor(2, 3) / determinant,
                M33 = GetCoFactor(3, 3) / determinant,
                M34 = GetCoFactor(4, 3) / determinant,
                M41 = GetCoFactor(1, 4) / determinant,
                M42 = GetCoFactor(2, 4) / determinant,
                M43 = GetCoFactor(3, 4) / determinant,
                M44 = GetCoFactor(4, 4) / determinant,
            });
        }

        public override string ToString()
        {
            return $"{M11}, {M12}, {M13}, {M14}{Environment.NewLine}" +
                   $"{M21}, {M22}, {M23}, {M24}{Environment.NewLine}" +
                   $"{M31}, {M32}, {M33}, {M34}{Environment.NewLine}" +
                   $"{M41}, {M42}, {M43}, {M44}{Environment.NewLine}";
        }

        public bool Equals(Matrix4X4 other)
        {
            return Math.Abs(M11 - other.M11) < Epsilon && 
                   Math.Abs(M12 - other.M12) < Epsilon && 
                   Math.Abs(M13 - other.M13) < Epsilon &&
                   Math.Abs(M14 - other.M14) < Epsilon &&
                   Math.Abs(M21 - other.M21) < Epsilon && 
                   Math.Abs(M22 - other.M22) < Epsilon && 
                   Math.Abs(M23 - other.M23) < Epsilon &&
                   Math.Abs(M24 - other.M24) < Epsilon &&
                   Math.Abs(M31 - other.M31) < Epsilon && 
                   Math.Abs(M32 - other.M32) < Epsilon && 
                   Math.Abs(M33 - other.M33) < Epsilon &&
                   Math.Abs(M34 - other.M34) < Epsilon &&
                   Math.Abs(M41 - other.M41) < Epsilon && 
                   Math.Abs(M42 - other.M42) < Epsilon && 
                   Math.Abs(M43 - other.M43) < Epsilon &&
                   Math.Abs(M44 - other.M44) < Epsilon;
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
                hashCode = (hashCode * 397) ^ M14.GetHashCode();
                hashCode = (hashCode * 397) ^ M21.GetHashCode();
                hashCode = (hashCode * 397) ^ M22.GetHashCode();
                hashCode = (hashCode * 397) ^ M23.GetHashCode();
                hashCode = (hashCode * 397) ^ M24.GetHashCode();
                hashCode = (hashCode * 397) ^ M31.GetHashCode();
                hashCode = (hashCode * 397) ^ M32.GetHashCode();
                hashCode = (hashCode * 397) ^ M33.GetHashCode();
                hashCode = (hashCode * 397) ^ M34.GetHashCode();
                hashCode = (hashCode * 397) ^ M41.GetHashCode();
                hashCode = (hashCode * 397) ^ M42.GetHashCode();
                hashCode = (hashCode * 397) ^ M43.GetHashCode();
                hashCode = (hashCode * 397) ^ M44.GetHashCode();
                return hashCode;
            }
        }

        private double GetCoFactor(int row, int column)
        {
            var minor = GetSubMatrix(row, column).Determinant();
            return (row + column) % 2 == 0 ? minor : -minor;
        }

        private Tuple GetTupleForRow(int row)
        {
            return row switch
            {
                1 => new Tuple(M11, M12, M13, M14),
                2 => new Tuple(M21, M22, M23, M24),
                3 => new Tuple(M31, M32, M33, M34),
                4 => new Tuple(M41, M42, M43, M44),
                _ => throw new IndexOutOfRangeException($"Row {row} is not valid for a 4x4 matrix")
            };
        }
    }
}
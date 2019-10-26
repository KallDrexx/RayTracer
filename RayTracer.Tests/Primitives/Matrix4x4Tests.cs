using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Primitives
{
    public class Matrix4X4Tests
    {
        [Fact]
        public void Equality_Check()
        {
            var matrix1 = new Matrix4X4
            {
                M11 = -3, M12 = 5, M13 = 5, M14 = 10,
                M21 = 1, M22 = -2, M23 = 12, M24 = 11,
                M31 = 8, M32 = -9, M33 = -10, M34 = 13,
                M41 = 8, M42 = -9, M43 = -10, M44 = 13,
            };
            
            var matrix2 = new Matrix4X4
            {
                M11 = -3, M12 = 5, M13 = 5, M14 = 10,
                M21 = 1, M22 = -2, M23 = 12, M24 = 11,
                M31 = 8, M32 = -9, M33 = -10, M34 = 13,
                M41 = 8, M42 = -9, M43 = -10, M44 = 13,
            };
            
            matrix1.ShouldBe(matrix2);
        }

        [Fact]
        public void Inequality_Check()
        {
            var matrix1 = new Matrix4X4
            {
                M11 = -3, M12 = 5, M13 = 5, M14 = 10,
                M21 = 1, M22 = -2, M23 = 12, M24 = 11,
                M31 = 8, M32 = -9, M33 = -10, M34 = 13,
                M41 = 8, M42 = -9, M43 = -10, M44 = 13,
            };
            
            var matrix2 = new Matrix4X4
            {
                M11 = -3, M12 = 5, M13 = 5, M14 = 10,
                M21 = 1, M22 = -2, M23 = 12, M24 = 11,
                M31 = 8, M32 = -10, M33 = -10, M34 = 13,
                M41 = 8, M42 = -9, M43 = -10, M44 = 13,
            };
            
            matrix1.ShouldNotBe(matrix2);
        }

        [Fact]
        public void Can_Multiply_Two_Matrices()
        {
            var matrix1 = new Matrix4X4
            {
                M11 = 1, M12 = 2, M13 = 3, M14 = 4,
                M21 = 5, M22 = 6, M23 = 7, M24 = 8,
                M31 = 9, M32 = 8, M33 = 7, M34 = 6,
                M41 = 5, M42 = 4, M43 = 3, M44 = 2,
            };
            
            var matrix2 = new Matrix4X4
            {
                M11 = -2, M12 = 1, M13 = 2, M14 = 3,
                M21 = 3, M22 = 2, M23 = 1, M24 = -1,
                M31 = 4, M32 = 3, M33 = 6, M34 = 5,
                M41 = 1, M42 = 2, M43 = 7, M44 = 8,
            };

            var result = matrix1 * matrix2;

            result.ShouldBe(new Matrix4X4
            {
                M11 = 20, M12 = 22, M13 = 50, M14 = 48,
                M21 = 44, M22 = 54, M23 = 114, M24 = 108,
                M31 = 40, M32 = 58, M33 = 110, M34 = 102,
                M41 = 16, M42 = 26, M43 = 46, M44 = 42,
            });
        }

        [Fact]
        public void Multiplication_Against_Identity_Matrix_Returns_Original()
        {
            var matrix1 = new Matrix4X4
            {
                M11 = 1, M12 = 2, M13 = 3, M14 = 4,
                M21 = 5, M22 = 6, M23 = 7, M24 = 8,
                M31 = 9, M32 = 8, M33 = 7, M34 = 6,
                M41 = 5, M42 = 4, M43 = 3, M44 = 2,
            };

            var result = matrix1 * Matrix4X4.IdentityMatrix;
            
            result.ShouldBe(matrix1);
        }

        [Fact]
        public void Can_Transpose_Matrix()
        {
            var matrix = new Matrix4X4
            {
                M11 = 0, M12 = 9, M13 = 3, M14 = 0,
                M21 = 9, M22 = 8, M23 = 0, M24 = 8,
                M31 = 1, M32 = 8, M33 = 5, M34 = 3,
                M41 = 0, M42 = 0, M43 = 5, M44 = 8,
            };

            var result = matrix.Transpose();
            
            result.ShouldBe(new Matrix4X4
            {
                M11 = 0, M12 = 9, M13 = 1, M14 = 0,
                M21 = 9, M22 = 8, M23 = 8, M24 = 0,
                M31 = 3, M32 = 0, M33 = 5, M34 = 5,
                M41 = 0, M42 = 8, M43 = 3, M44 = 8,
            });
        }

        [Fact]
        public void Identity_Matrix_Transposes_Into_Itself()
        {
            var matrix = Matrix4X4.IdentityMatrix;
            var result = matrix.Transpose();
            
            result.ShouldBe(Matrix4X4.IdentityMatrix);
        }

        [Fact]
        public void Can_Get_Sub_Matrix()
        {
            var matrix = new Matrix4X4
            {
                M11 = -6, M12 = 1, M13 = 1, M14 = 6,
                M21 = -8, M22 = 5, M23 = 8, M24 = 6,
                M31 = -1, M32 = 0, M33 = 8, M34 = 2,
                M41 = -7, M42 = 1, M43 = -1, M44 = 1,
            };

            matrix.GetSubMatrix(3, 2).ShouldBe(new Matrix3X3
            {
                M11 = -6, M12 = 1, M13 = 6,
                M21 = -8, M22 = 8, M23 = 6,
                M31 = -7, M32 = -1, M33 = 1,
            });
        }

        [Fact]
        public void Can_Get_Determinant()
        {
            var matrix1 = new Matrix4X4
            {
                M11 = -2, M12 = -8, M13 = 3, M14 = 5,
                M21 = -3, M22 = 1, M23 = 7, M24 = 3,
                M31 = 1, M32 = 2, M33 = -9, M34 = 6,
                M41 = -6, M42 = 7, M43 = 7, M44 = -9,
            };
            
            var matrix2 = new Matrix4X4
            {
                M11 = 6, M12 = 4, M13 = 4, M14 = 4,
                M21 = 5, M22 = 5, M23 = 7, M24 = 6,
                M31 = 4, M32 = -9, M33 = 3, M34 = -7,
                M41 = 9, M42 = 1, M43 = 7, M44 = -6,
            };

            matrix1.GetDeterminant().ShouldBe(-4071);
            matrix2.GetDeterminant().ShouldBe(-2120);
        }

        [Fact]
        public void Can_Test_If_Invertible()
        {
            var matrix1 = new Matrix4X4
            {
                M11 = 6, M12 = 4, M13 = 4, M14 = 4,
                M21 = 5, M22 = 5, M23 = 7, M24 = 6,
                M31 = 4, M32 = -9, M33 = 3, M34 = -7,
                M41 = 9, M42 = 1, M43 = 7, M44 = -5,
            };
            
            var matrix2 = new Matrix4X4
            {
                M11 = -4, M12 = 2, M13 = -2, M14 = -3,
                M21 = 9, M22 = 6, M23 = 2, M24 = 6,
                M31 = 0, M32 = -5, M33 = 1, M34 = -5,
                M41 = 0, M42 = 0, M43 = 0, M44 = 0,
            };
            
            matrix1.Invert().wasInvertible.ShouldBeTrue();
            matrix2.Invert().wasInvertible.ShouldBeFalse();
        }

        [Fact]
        public void Can_Invert_Matrix()
        {
            var matrix1 = new Matrix4X4
            {
                M11 = -5, M12 = 2, M13 = 6, M14 = -8,
                M21 = 1, M22 = -5, M23 = 1, M24 = 8,
                M31 = 7, M32 = 7, M33 = -6, M34 = -7,
                M41 = 1, M42 = -3, M43 = 7, M44 = 4,
            };
            
            var matrix2 = new Matrix4X4
            {
                M11 = 8, M12 = -5, M13 = 9, M14 = 2,
                M21 = 7, M22 = 5, M23 = 6, M24 = 1,
                M31 = -6, M32 = 0, M33 = 9, M34 = 6,
                M41 = -3, M42 = 0, M43 = -9, M44 = -4,
            };
            
            var matrix3 = new Matrix4X4
            {
                M11 = 9, M12 = 3, M13 = 0, M14 = 9,
                M21 = -5, M22 = -2, M23 = -6, M24 = -3,
                M31 = -4, M32 = 9, M33 = 6, M34 = 4,
                M41 = -7, M42 = 6, M43 = 6, M44 = 2,
            };
            
            matrix1.Invert().inverse.ShouldBe(new Matrix4X4
            {
                M11 = 0.21805, M12 = 0.45113, M13 = 0.24060, M14 = -0.04511,
                M21 = -0.80827, M22 = -1.45677, M23 = -0.44361, M24 = 0.52068,
                M31 = -0.07895, M32 = -0.22368, M33 = -0.05263, M34 = 0.19737,
                M41 = -0.52256, M42 = -0.81391, M43 = -0.30075, M44 = 0.30639,
            });
            
            matrix2.Invert().inverse.ShouldBe(new Matrix4X4
            {
                M11 = -0.15385, M12 = -0.15385, M13 = -0.28205, M14 = -0.53846,
                M21 = -0.07692, M22 = 0.12308, M23 = 0.02564, M24 = 0.03077,
                M31 = 0.35897, M32 = 0.35897, M33 = 0.43590, M34 = 0.92308,
                M41 = -0.69231, M42 = -0.69231, M43 = -0.76923, M44 = -1.92308,
            });
            
            matrix3.Invert().inverse.ShouldBe(new Matrix4X4
            {
                M11 = -0.04074, M12 = -0.07778, M13 = 0.14444, M14 = -0.22222,
                M21 = -0.07778, M22 = 0.03333, M23 = 0.36667, M24 = -0.33333,
                M31 = -0.02901, M32 = -0.14630, M33 = -0.10926, M34 = 0.12963,
                M41 = 0.17778, M42 = 0.06667, M43 = -0.26667, M44 = 0.33333,
            });
        }

        [Fact]
        public void Multiply_A_Product_By_Its_Inverse()
        {
            var matrix1 = new Matrix4X4
            {
                M11 = 3, M12 = -9, M13 = 7, M14 = 3,
                M21 = 3, M22 = -8, M23 = 2, M24 = -9,
                M31 = -4, M32 = 4, M33 = 4, M34 = 1,
                M41 = -6, M42 = 5, M43 = -1, M44 = 1,
            };
            
            var matrix2 = new Matrix4X4
            {
                M11 = 8, M12 = 2, M13 = 2, M14 = 2,
                M21 = 3, M22 = -1, M23 = 7, M24 = 0,
                M31 = 7, M32 = 0, M33 = 5, M34 = 4,
                M41 = 6, M42 = -2, M43 = 0, M44 = 5,
            };

            var product = matrix1 * matrix2;
            
            (product * matrix2.Invert().inverse).ShouldBe(matrix1);
        }
    }
}
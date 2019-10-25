using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Primitives
{
    public class Matrix3X3Tests
    {
        [Fact]
        public void Equality_Check()
        {
            var matrix1 = new Matrix3X3
            {
                M11 = -3, M12 = 5, M13 = 5,
                M21 = 1, M22 = -2, M23 = 12,
                M31 = 8, M32 = -9, M33 = -10,
            };
            
            var matrix2 = new Matrix3X3
            {
                M11 = -3, M12 = 5, M13 = 5,
                M21 = 1, M22 = -2, M23 = 12,
                M31 = 8, M32 = -9, M33 = -10,
            };
            
            matrix1.ShouldBe(matrix2);
        }

        [Fact]
        public void Inequality_Check()
        {
            var matrix1 = new Matrix3X3
            {
                M11 = -3, M12 = 5, M13 = 5,
                M21 = 1, M22 = -2, M23 = 12,
                M31 = 8, M32 = -9, M33 = -10,
            };
            
            var matrix2 = new Matrix3X3
            {
                M11 = -3, M12 = 5, M13 = 5,
                M21 = 1, M22 = -3, M23 = 12,
                M31 = 8, M32 = -9, M33 = -10,
            };
            
            matrix1.ShouldNotBe(matrix2);
        }

        [Fact]
        public void Can_Get_Bottom_Left_Sub_Matrix()
        {
            var matrix = new Matrix3X3
            {
                M11 = 1, M12 = 5, M13 = 0,
                M21 = -3, M22 = 2, M23 = 7,
                M31 = 0, M32 = 6, M33 = -3,
            };

            var result = matrix.GetSubMatrix(1, 3);
            
            result.ShouldBe(new Matrix2X2
            {
                M11 = -3, M12 = 2,
                M21 = 0, M22 = 6,
            });
        }
    }
}
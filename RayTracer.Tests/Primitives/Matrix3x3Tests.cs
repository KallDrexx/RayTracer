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

            matrix.GetSubMatrix(1, 3).ShouldBe(new Matrix2X2
            {
                M11 = -3, M12 = 2,
                M21 = 0, M22 = 6,
            });
        }

        [Fact]
        public void Can_Get_Sub_Matrix_With_Middle_Row_Removed()
        {
            var matrix = new Matrix3X3
            {
                M11 = 3, M12 = 5, M13 = 0,
                M21 = 2, M22 = -1, M23 = -7,
                M31 = 6, M32 = -1, M33 = 5,
            };

            matrix.GetSubMatrix(2, 1).ShouldBe(new Matrix2X2
            {
                M11 = 5, M12 = 0,
                M21 = -1, M22 = 5,
            });
        }

        [Fact]
        public void Can_Get_Minor_Value()
        {
            var matrix = new Matrix3X3
            {
                M11 = 3, M12 = 5, M13 = 0,
                M21 = 2, M22 = -1, M23 = -7,
                M31 = 6, M32 = -1, M33 = 5,
            };

            matrix.GetMinor(2, 1).ShouldBe(25);
        }

        [Fact]
        public void Can_Get_CoFactor()
        {
            var matrix = new Matrix3X3
            {
                M11 = 3, M12 = 5, M13 = 0,
                M21 = 2, M22 = -1, M23 = -7,
                M31 = 6, M32 = -1, M33 = 5,
            };
            
            matrix.GetCoFactor(1, 1).ShouldBe(-12);
            matrix.GetCoFactor(2,1).ShouldBe(-25);
        }

        [Fact]
        public void Can_Get_Determinant()
        {
            var matrix = new Matrix3X3
            {
                M11 = 1, M12 = 2, M13 = 6,
                M21 = -5, M22 = 8, M23 = -4,
                M31 = 2, M32 = 6, M33 = 4,
            };
            
            matrix.Determinant().ShouldBe(-196);
        }
    }
}
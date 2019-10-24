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
                Row0Col0 = -3, Row0Col1 = 5, Row0Col2 = 5, Row0Col3 = 10,
                Row1Col0 = 1, Row1Col1 = -2, Row1Col2 = 12, Row1Col3 = 11,
                Row2Col0 = 8, Row2Col1 = -9, Row2Col2 = -10, Row2Col3 = 13,
                Row3Col0 = 8, Row3Col1 = -9, Row3Col2 = -10, Row3Col3 = 13,
            };
            
            var matrix2 = new Matrix4X4
            {
                Row0Col0 = -3, Row0Col1 = 5, Row0Col2 = 5, Row0Col3 = 10,
                Row1Col0 = 1, Row1Col1 = -2, Row1Col2 = 12, Row1Col3 = 11,
                Row2Col0 = 8, Row2Col1 = -9, Row2Col2 = -10, Row2Col3 = 13,
                Row3Col0 = 8, Row3Col1 = -9, Row3Col2 = -10, Row3Col3 = 13,
            };
            
            matrix1.ShouldBe(matrix2);
        }

        [Fact]
        public void Inequality_Check()
        {
            var matrix1 = new Matrix4X4
            {
                Row0Col0 = -3, Row0Col1 = 5, Row0Col2 = 5, Row0Col3 = 10,
                Row1Col0 = 1, Row1Col1 = -2, Row1Col2 = 12, Row1Col3 = 11,
                Row2Col0 = 8, Row2Col1 = -9, Row2Col2 = -10, Row2Col3 = 13,
                Row3Col0 = 8, Row3Col1 = -9, Row3Col2 = -10, Row3Col3 = 13,
            };
            
            var matrix2 = new Matrix4X4
            {
                Row0Col0 = -3, Row0Col1 = 5, Row0Col2 = 5, Row0Col3 = 10,
                Row1Col0 = 1, Row1Col1 = -2, Row1Col2 = 12, Row1Col3 = 11,
                Row2Col0 = 8, Row2Col1 = -10, Row2Col2 = -10, Row2Col3 = 13,
                Row3Col0 = 8, Row3Col1 = -9, Row3Col2 = -10, Row3Col3 = 13,
            };
            
            matrix1.ShouldNotBe(matrix2);
        }

        [Fact]
        public void Can_Multiply_Two_Matrices()
        {
            var matrix1 = new Matrix4X4
            {
                Row0Col0 = 1, Row0Col1 = 2, Row0Col2 = 3, Row0Col3 = 4,
                Row1Col0 = 5, Row1Col1 = 6, Row1Col2 = 7, Row1Col3 = 8,
                Row2Col0 = 9, Row2Col1 = 8, Row2Col2 = 7, Row2Col3 = 6,
                Row3Col0 = 5, Row3Col1 = 4, Row3Col2 = 3, Row3Col3 = 2,
            };
            
            var matrix2 = new Matrix4X4
            {
                Row0Col0 = -2, Row0Col1 = 1, Row0Col2 = 2, Row0Col3 = 3,
                Row1Col0 = 3, Row1Col1 = 2, Row1Col2 = 1, Row1Col3 = -1,
                Row2Col0 = 4, Row2Col1 = 3, Row2Col2 = 6, Row2Col3 = 5,
                Row3Col0 = 1, Row3Col1 = 2, Row3Col2 = 7, Row3Col3 = 8,
            };

            var result = matrix1 * matrix2;

            result.ShouldBe(new Matrix4X4
            {
                Row0Col0 = 20, Row0Col1 = 22, Row0Col2 = 50, Row0Col3 = 48,
                Row1Col0 = 44, Row1Col1 = 54, Row1Col2 = 114, Row1Col3 = 108,
                Row2Col0 = 40, Row2Col1 = 58, Row2Col2 = 110, Row2Col3 = 102,
                Row3Col0 = 16, Row3Col1 = 26, Row3Col2 = 46, Row3Col3 = 42,
            });
        }

        [Fact]
        public void Multiplication_Against_Identity_Matrix_Returns_Original()
        {
            var matrix1 = new Matrix4X4
            {
                Row0Col0 = 1, Row0Col1 = 2, Row0Col2 = 3, Row0Col3 = 4,
                Row1Col0 = 5, Row1Col1 = 6, Row1Col2 = 7, Row1Col3 = 8,
                Row2Col0 = 9, Row2Col1 = 8, Row2Col2 = 7, Row2Col3 = 6,
                Row3Col0 = 5, Row3Col1 = 4, Row3Col2 = 3, Row3Col3 = 2,
            };

            var result = matrix1 * Matrix4X4.IdentityMatrix;
            
            result.ShouldBe(matrix1);
        }
    }
}
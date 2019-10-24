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
                Row0Col0 = -3, Row0Col1 = 5, Row0Col2 = 5,
                Row1Col0 = 1, Row1Col1 = -2, Row1Col2 = 12,
                Row2Col0 = 8, Row2Col1 = -9, Row2Col2 = -10,
            };
            
            var matrix2 = new Matrix3X3
            {
                Row0Col0 = -3, Row0Col1 = 5, Row0Col2 = 5,
                Row1Col0 = 1, Row1Col1 = -2, Row1Col2 = 12,
                Row2Col0 = 8, Row2Col1 = -9, Row2Col2 = -10,
            };
            
            matrix1.ShouldBe(matrix2);
        }

        [Fact]
        public void Inequality_Check()
        {
            var matrix1 = new Matrix3X3
            {
                Row0Col0 = -3, Row0Col1 = 5, Row0Col2 = 5,
                Row1Col0 = 1, Row1Col1 = -2, Row1Col2 = 12,
                Row2Col0 = 8, Row2Col1 = -9, Row2Col2 = -10,
            };
            
            var matrix2 = new Matrix3X3
            {
                Row0Col0 = -3, Row0Col1 = 5, Row0Col2 = 5,
                Row1Col0 = 1, Row1Col1 = -3, Row1Col2 = 12,
                Row2Col0 = 8, Row2Col1 = -9, Row2Col2 = -10,
            };
            
            matrix1.ShouldNotBe(matrix2);
        }
    }
}
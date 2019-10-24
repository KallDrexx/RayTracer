using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Primitives
{
    public class Matrix2X2Tests
    {
        [Fact]
        public void Equality_Check()
        {
            var matrix1 = new Matrix2X2
            {
                Row0Col0 = -3, Row0Col1 = 5,
                Row1Col0 = 1, Row1Col1 = -2,
            };
            
            var matrix2 = new Matrix2X2
            {
                Row0Col0 = -3, Row0Col1 = 5,
                Row1Col0 = 1, Row1Col1 = -2,
            };
            
            matrix1.ShouldBe(matrix2);
        }

        [Fact]
        public void Inequality_Check()
        {
            var matrix1 = new Matrix2X2
            {
                Row0Col0 = -3, Row0Col1 = 5,
                Row1Col0 = 1, Row1Col1 = -2,
            };
            
            var matrix2 = new Matrix2X2
            {
                Row0Col0 = -3, Row0Col1 = 5,
                Row1Col0 = 1, Row1Col1 = -5,
            };
            
            matrix1.ShouldNotBe(matrix2);
        }
    }
}
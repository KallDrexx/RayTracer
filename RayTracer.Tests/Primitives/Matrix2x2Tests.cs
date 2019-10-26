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
                M11 = -3, M12 = 5,
                M21 = 1, M22 = -2,
            };
            
            var matrix2 = new Matrix2X2
            {
                M11 = -3, M12 = 5,
                M21 = 1, M22 = -2,
            };
            
            matrix1.ShouldBe(matrix2);
        }

        [Fact]
        public void Inequality_Check()
        {
            var matrix1 = new Matrix2X2
            {
                M11 = -3, M12 = 5,
                M21 = 1, M22 = -2,
            };
            
            var matrix2 = new Matrix2X2
            {
                M11 = -3, M12 = 5,
                M21 = 1, M22 = -5,
            };
            
            matrix1.ShouldNotBe(matrix2);
        }

        [Fact]
        public void Can_Get_Determinate_Of_Matrix()
        {
            var matrix = new Matrix2X2
            {
                M11 = 1, M12 = 5,
                M21 = -3, M22 = 2,
            };

            var result = matrix.GetDeterminant();

            result.ShouldBe(17);
        }
    }
}
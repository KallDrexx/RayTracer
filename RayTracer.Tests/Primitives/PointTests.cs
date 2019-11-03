using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Primitives
{
    public class PointTests
    {
        [Fact]
        public void Can_Create_Point()
        {
            var point = new Point(1f, 2f, 3f);
            
            point.X.ShouldBe(1f);
            point.Y.ShouldBe(2f);
            point.Z.ShouldBe(3f);
        }

        [Fact]
        public void Point_Equality_Checks()
        {
            (new Point(1f, 2f, 3f) == new Point(1f, 2f, 3f))
                .ShouldBeTrue();
            
            (new Point(1f, 2f, 3f) != new Point(2f, 1f, 3f))
                .ShouldBeTrue();
            
            (new Point(1f, 1f, 1f) == new Point(1.0000001f, 1.0000001f, 1.0000001f))
                .ShouldBeTrue();
            
            new Point(1f, 1f, 1f).Equals(new Point(1.0000001f, 1.0000001f, 1.0000001f))
                .ShouldBeTrue();
            
            (new Point(1f, 2f, 3f) == new Point(2f, 3f, 4f))
                .ShouldBeFalse();
        }

        [Fact]
        public void Subtracting_Two_Points_Gives_Vector()
        {
            (new Point(1f, 2f, 3f) - new Point(4f, 5f, 6f))
                .ShouldBe(new Vector(-3f, -3f, -3f));
        }

        [Fact]
        public void Vector_Plus_Point_Equals_Point()
        {
            (new Vector(1f, 2f, 3f) + new Point(4f, 5f, 6f))
                .ShouldBe(new Point(5f, 7f, 9f));
            
            (new Point(4f, 5f, 6f) + new Vector(1f, 2f, 3f))
                .ShouldBe(new Point(5f, 7f, 9f));
        }

        [Fact]
        public void Can_Subtract_Vector_From_Point()
        {
            (new Point(1f, 2f, 3f) - new Vector(4f, 5f, 6f))
                .ShouldBe(new Point(-3f, -3f, -3f));
        }
    }
}
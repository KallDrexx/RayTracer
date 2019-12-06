using RayTracer.Common.Core;
using RayTracer.Common.Core.Objects;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core
{
    public class RayTests
    {
        [Fact]
        public void Ray_Has_Origin_And_Direction_Specified()
        {
            var origin = new Point(2, 3, 4);
            var direction = new Vector(1, 0, 0);
            var ray = new Ray(origin, direction);
            
            ray.Origin.ShouldBe(origin);
            ray.Direction.ShouldBe(direction);
        }

        [Fact]
        public void Can_Get_Position_At_Time_Intervals()
        {
            var origin = new Point(2, 3, 4);
            var direction = new Vector(1, 0, 0);
            var ray = new Ray(origin, direction);
            
            ray.PositionAt(0).ShouldBe(new Point(2, 3, 4));
            ray.PositionAt(1).ShouldBe(new Point(3, 3, 4));
            ray.PositionAt(-1).ShouldBe(new Point(1, 3, 4));
            ray.PositionAt(2.5).ShouldBe(new Point(4.5, 3, 4));
        }
    }
}
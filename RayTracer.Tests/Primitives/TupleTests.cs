using System;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;
using Tuple = RayTracer.Common.Primitives.Tuple;

namespace RayTracer.Tests.Primitives
{
    public class TupleTests
    {
        [Fact]
        public void Tuple_Equality_Checks()
        {
            (new Tuple(1, 2, 3, 4) == new Tuple(1, 2, 3, 4))
                .ShouldBeTrue();
            
            (new Tuple(1, 2, 3, 4) != new Tuple(2, 1, 3, 4))
                .ShouldBeTrue();
            
            (new Tuple(1, 1, 1, 2) == new Tuple(1.000001f, 1.0000001f, 1.0000001f, 2.0000001f))
                .ShouldBeTrue();
            
            new Tuple(1, 1, 1, 2).Equals(new Tuple(1.0000001f, 1.0000001f, 1.0000001f, 2.000000001f))
                .ShouldBeTrue();
            
            (new Tuple(1, 2, 3, 4) == new Tuple(2, 3, 4, 5))
                .ShouldBeFalse();
        }

        [Fact]
        public void Can_Add_Tuples()
        {
            (new Tuple(1.1f, -2.2f, 3.3f, -4.4f) + new Tuple(5.5f, 6.6f, 7.7f, 8.8f))
                .ShouldBe(new Tuple(6.6f, 4.4f, 11f, 4.4f));
        }
        
        [Fact]
        public void Can_Subtract_Tuples()
        {
            (new Tuple(1.1f, -2.2f, 3.3f, -4.4f) - new Tuple(5.5f, 6.6f, 7.7f, 8.8f))
                .ShouldBe(new Tuple(-4.4f, -8.8f, -4.4f, -13.2f));
        }

        [Fact]
        public void Can_Negate_Tuple()
        {
            (-new Tuple(1, -2, 3, -4))
                .ShouldBe(new Tuple(-1, 2, -3, 4));
        }

        [Fact]
        public void Can_Multiply_By_Scalar()
        {
            (new Tuple(2f, 3f, 4f, 5f) * 3.5f)
                .ShouldBe(new Tuple(7f, 10.5f, 14f, 17.5f));
            
            (new Tuple(2f, 3f, 4f, 5f) * 3)
                .ShouldBe(new Tuple(6f, 9f, 12f, 15f));
        }

        [Fact]
        public void Can_Divide_By_Scalar()
        {
            (new Tuple(6f, 8f, 10f, 12f) / 2f)
                .ShouldBe(new Tuple(3f, 4f, 5f, 6f));
        }
        
        [Fact]
        public void Can_Get_Magnitude()
        {
            new Tuple(1f, 0f, 0f, 0f).Magnitude.ShouldBe(1f);
            new Tuple(0f, 1f, 0f, 0f).Magnitude.ShouldBe(1f);
            new Tuple(0f, 0f, 1f, 0f).Magnitude.ShouldBe(1f);
            new Tuple(1f, 2f, 3f, 0f).Magnitude.ShouldBe(Math.Sqrt(14));
            new Tuple(-1f, -2f, -3f, 0f).Magnitude.ShouldBe(Math.Sqrt(14));
        }

        [Fact]
        public void Can_Normalize_Tuple()
        {
            new Tuple(4f, 0f, 0f, 0f).Normalize().ShouldBe(new Tuple(1f, 0f, 0f, 0f));
            new Tuple(1f, 2f, 3f, 4f).Normalize()
                .ShouldBe(new Tuple(1f / Math.Sqrt(30), 2f / Math.Sqrt(30), 3f / Math.Sqrt(30), 4f / Math.Sqrt(30)));
        }

        [Fact]
        public void Can_Get_Dot_Product_Of_Tuples()
        {
            new Tuple(1f, 2f, 3f, 4f).DotProduct(new Tuple(2f, 3f, 4f, 5f))
                .ShouldBe(40);
        }
    }
}
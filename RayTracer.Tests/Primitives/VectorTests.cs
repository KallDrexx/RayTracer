using System;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Primitives
{
    public class VectorTests
    {
        [Fact]
        public void Can_Create_Vector()
        {
            var vector = new Vector(1f, 2f, 3f);
            
            vector.X.ShouldBe(1f);
            vector.Y.ShouldBe(2f);
            vector.Z.ShouldBe(3f);
        }

        [Fact]
        public void Vector_Equality_Checks()
        {
            (new Vector(1f, 2f, 3f) == new Vector(1f, 2f, 3f))
                .ShouldBeTrue();
            
            (new Vector(1f, 2f, 3f) != new Vector(2f, 1f, 3f))
                .ShouldBeTrue();
            
            (new Vector(1f, 1f, 1f) == new Vector(1.0000001f, 1.0000001f, 1.0000001f))
                .ShouldBeTrue();
            
            new Vector(1f, 1f, 1f).Equals(new Vector(1.0000001f, 1.0000001f, 1.0000001f))
                .ShouldBeTrue();
            
            (new Vector(1f, 2f, 3f) == new Vector(2f, 3f, 4f))
                .ShouldBeFalse();
        }

        [Fact]
        public void Can_Add_Vectors()
        {
            (new Vector(1f, 2f, 3f) + new Vector(4f, 5f, 6f))
                .ShouldBe(new Vector(5f, 7f, 9f));
        }

        [Fact]
        public void Can_Subtract_Vectors()
        {
            (new Vector(1f, 2f, 3f) - new Vector(4f, 5f, 6f))
                .ShouldBe(new Vector(-3f, -3f, -3f));
        }

        [Fact]
        public void Can_Negate_Vector()
        {
            (- new Vector(1f, 2f, -3f))
                .ShouldBe(new Vector(-1f, -2f, 3f));
        }

        [Fact]
        public void Can_Multiply_By_Scalar()
        {
            (new Vector(2f, 3f, 4f) * 3.5f)
                .ShouldBe(new Vector(7f, 10.5f, 14f));
            
            (new Vector(2f, 3f, 4f) * 3.5d)
                .ShouldBe(new Vector(7f, 10.5f, 14f));
            
            (new Vector(2f, 3f, 4f) * 3)
                .ShouldBe(new Vector(6f, 9f, 12f));
        }

        [Fact]
        public void Can_Divide_By_Scalar()
        {
            (new Vector(6f, 8f, 10f) / 2d)
                .ShouldBe(new Vector(3f, 4f, 5f));
            
            (new Vector(6f, 8f, 10f) / 2f)
                .ShouldBe(new Vector(3f, 4f, 5f));
            
            (new Vector(6f, 8f, 10f) / 2)
                .ShouldBe(new Vector(3f, 4f, 5f));
        }

        [Fact]
        public void Can_Get_Magnitude()
        {
            new Vector(1f, 0f, 0f).Magnitude.ShouldBe(1f);
            new Vector(0f, 1f, 0f).Magnitude.ShouldBe(1f);
            new Vector(0f, 0f, 1f).Magnitude.ShouldBe(1f);
            new Vector(1f, 2f, 3f).Magnitude.ShouldBe(Math.Sqrt(14));
            new Vector(-1f, -2f, -3f).Magnitude.ShouldBe(Math.Sqrt(14));
        }

        [Fact]
        public void Can_Normalize_Vector()
        {
            new Vector(4f, 0f, 0f).Normalize().ShouldBe(new Vector(1f, 0f, 0f));
            new Vector(1f, 2f, 3f).Normalize()
                .ShouldBe(new Vector(1f / Math.Sqrt(14), 2f / Math.Sqrt(14), 3f / Math.Sqrt(14)));
        }

        [Fact]
        public void Can_Get_Dot_Product_Of_Vectors()
        {
            new Vector(1f, 2f, 3f).Dot(new Vector(2f, 3f, 4f))
                .ShouldBe(20);
        }

        [Fact]
        public void Can_Get_Cross_Product_Of_Vectors()
        {
            new Vector(1f, 2f, 3f).Cross(new Vector(2f, 3f, 4f))
                .ShouldBe(new Vector(-1f, 2f, -1f));
            
            new Vector(2f, 3f, 4f).Cross(new Vector(1f, 2f, 3f))
                .ShouldBe(new Vector(1f, -2f, 1f));
            
            new Vector(1f, 0f, 0f).Cross(new Vector(0f, 1f, 0f))
                .ShouldBe(new Vector(0f, 0f, 1f));
        }

        [Fact]
        public void Reflecting_Vector_Around_Normal()
        {
            new Vector(1, -1, 0).Reflect(new Vector(0, 1, 0))
                .ShouldBe(new Vector(1, 1, 0));

            new Vector(0, -1, 0).Reflect(new Vector(Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0))
                .ShouldBe(new Vector(1, 0, 0));
        }
    }
}
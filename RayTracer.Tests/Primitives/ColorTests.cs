using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Primitives
{
    public class ColorTests
    {
        [Fact]
        public void Can_Create_Color()
        {
            var color = new Color(0.25f, 0.5f, 0.75f);
            
            color.Red.ShouldBe(0.25);
            color.Green.ShouldBe(0.5f);
            color.Blue.ShouldBe(0.75f);
        }
        
        [Fact]
        public void Color_Equality_Checks()
        {
            (new Color(1f, 2f, 3f) == new Color(1f, 2f, 3f))
                .ShouldBeTrue();
            
            (new Color(1f, 2f, 3f) != new Color(2f, 1f, 3f))
                .ShouldBeTrue();
            
            (new Color(1f, 1f, 1f) == new Color(1.0000001f, 1.0000001f, 1.0000001f))
                .ShouldBeTrue();
            
            new Color(1f, 1f, 1f).Equals(new Color(1.0000001f, 1.0000001f, 1.0000001f))
                .ShouldBeTrue();
            
            (new Color(1f, 2f, 3f) == new Color(2f, 3f, 4f))
                .ShouldBeFalse();
        }

        [Fact]
        public void Can_Add_Colors()
        {
            (new Color(0.9f, 0.6f, 0.75f) + new Color(0.7f, 0.1f, 0.25f))
                .ShouldBe(new Color(1.6f, 0.7f, 1.0f));
            
            (new Color(0.25f, 0.3f, 0.4f) + new Color(0.1f, 0.2f, 0.3f))
                .ShouldBe(new Color(0.35f, 0.5f, 0.7f));
        }

        [Fact]
        public void Can_Subtract_Colors()
        {
            (new Color(0.9f, 0.6f, 0.75f) - new Color(0.7f, 0.1f, 0.25f))
                .ShouldBe(new Color(0.2f, 0.5f, 0.5f));
            
            (new Color(0.25f, 0.3f, 0.4f) - new Color(0.1f, 0.2f, 0.3f))
                .ShouldBe(new Color(0.15f, 0.1f, 0.1f));
        }

        [Fact]
        public void Can_Multiply_By_Scalar()
        {
            (new Color(0.2f, 0.3f, 0.4f) * 2)
                .ShouldBe(new Color(0.4f, 0.6f, 0.8f));
            
            (new Color(0.25f, 0.35f, 0.45f) * 3.2f)
                .ShouldBe(new Color(0.8f, 1.12f, 1.44f));
        }

        [Fact]
        public void Can_Multiply_Color_By_Color()
        {
            (new Color(1f, 0.2f, 0.4f) * new Color(0.9f, 1f, 0.1f))
                .ShouldBe(new Color(0.9f, 0.2f, 0.04f));
        }
    }
}
using RayTracer.Common;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;
using Xunit.Sdk;

namespace RayTracer.Tests
{
    public class CanvasTests
    {
        [Fact]
        public void Canvas_Created_With_Expected_Height_And_Width()
        {
            var canvas = new Canvas(10, 20);
            
            canvas.Width.ShouldBe(10);
            canvas.Height.ShouldBe(20);
        }

        [Fact]
        public void New_Canvas_Has_All_White_Pixels()
        {
            const int height = 10;
            const int width = 20;
            var canvas = new Canvas(width, height);

            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                var pixel = canvas[x, y];
                if (pixel != Color.White)
                {
                    var msg = $"Pixel at ({x} {y}) was not white but instead {pixel}";
                    throw new XunitException(msg);
                }
            }
        }

        [Fact]
        public void Can_Set_And_Retrieve_Pixel_From_Canvas()
        {
            var color = new Color(0.25, 0.5, 0.75);
            var canvas = new Canvas(800, 600) {[100, 125] = color};

            var result = canvas[100, 125];
            result.ShouldBe(color);
        }
    }
}
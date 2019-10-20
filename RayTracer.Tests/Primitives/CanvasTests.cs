using System;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;
using Xunit.Sdk;

namespace RayTracer.Tests.Primitives
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
        public void Exception_When_Accessing_Y_Greater_Than_Height()
        {
            var canvas = new Canvas(20, 10);

            Assert.ThrowsAny<Exception>(() => canvas[11, 11]);
        }

        [Fact]
        public void Exception_When_Accessing_X_Greater_Than_Width()
        {
            var canvas = new Canvas(10, 20);

            Assert.ThrowsAny<Exception>(() => canvas[11, 11]);
        }
    }
}
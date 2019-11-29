using System;
using RayTracer.Common.Core;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core
{
    public class CameraTests
    {
        [Fact]
        public void Can_Construct_A_Camera()
        {
            var camera = new Camera(160, 120, Math.PI / 2);
            
            camera.HorizontalPixelCount.ShouldBe(160);
            camera.VerticalPixelCount.ShouldBe(120);
            camera.FieldOfView.ShouldBe(Math.PI / 2);
            camera.ViewTransform.ShouldBe(Matrix4X4.IdentityMatrix);
        }

        [Fact]
        public void Pixel_Size_For_Horizontal_Canvas()
        {
            var camera = new Camera(200, 125, Math.PI / 2);
            
            camera.PixelSize.ShouldBe(0.01, 0.0001);
        }

        [Fact]
        public void Pixel_Size_For_Vertical_Canvas()
        {
            var camera = new Camera(125, 200, Math.PI / 2);
            
            camera.PixelSize.ShouldBe(0.01, 0.0001);
        }

        [Fact]
        public void Construct_Ray_Through_Center_Of_Canvas()
        {
            var camera = new Camera(201, 101, Math.PI / 2);
            var ray = camera.RayForPixel(100, 50);
            
            ray.Origin.ShouldBe(new Point(0, 0, 0));
            ray.Direction.ShouldBe(new Vector(0, 0, -1));
        }

        [Fact]
        public void Construct_Ray_Through_Corner_Of_Canvas()
        {
            var camera = new Camera(201, 101, Math.PI / 2);
            var ray = camera.RayForPixel(0, 0);
            
            ray.Origin.ShouldBe(new Point(0, 0, 0));
            ray.Direction.ShouldBe(new Vector(0.66519, 0.33259, -0.66851));
        }

        [Fact]
        public void Construct_Ray_When_Camera_Is_Transformed()
        {
            var camera = new Camera(201, 101, Math.PI / 2)
            {
                ViewTransform = Matrix4X4.CreateRotationY(Math.PI / 4) * Matrix4X4.CreateTranslation(0, -2, 5),
            };

            var ray = camera.RayForPixel(100, 50);
            
            ray.Origin.ShouldBe(new Point(0, 2, -5));
            ray.Direction.ShouldBe(new Vector(Math.Sqrt(2) / 2, 0, -Math.Sqrt(2) / 2));
        }

        [Fact]
        public void Render_World_Within_Camera()
        {
            var world = World.CreateDefaultWorld();
            var from = new Point(0, 0, -5);
            var to = new Point(0, 0, 0);
            var up = new Vector(0, 1, 0);

            var camera = new Camera(11, 11, Math.PI / 2)
            {
                ViewTransform = Matrix4X4.CreateViewTransform(@from, to, up)
            };

            var image = camera.Render(world);
            
            image[5, 5].ShouldBe(new Color(0.38066, 0.47583, 0.2855));
        }
    }
}
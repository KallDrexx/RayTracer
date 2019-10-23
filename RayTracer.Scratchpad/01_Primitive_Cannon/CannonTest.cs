using System;
using RayTracer.Common.Primitives;
using SkiaSharp;

namespace RayTracer.Scratchpad._01_Primitive_Cannon
{
    public static class CannonTest
    {
        public static void Run()
        {
            var environment = new Environment
            {
                Gravity = new Vector(0f, -0.1f, 0f),
                Wind = new Vector(-0.01f, 0, 0f),
            };

            var projectile = new Projectile
            {
                Position = new Point(0f, 1f, 0f),
                Velocity = new Vector(1, 1.8, 0).Normalize() * 11.25,
            };

            var canvas = new Canvas(900, 560);
            canvas.ClearToColor(Color.Black);

            var count = 0;
            Console.WriteLine($"Initial position: {projectile.Position}");
            do
            {
                // Map the position to a pixel on the canvas
                var x = (int) (projectile.Position.X) + 1;
                var y = (int) (canvas.Height - projectile.Position.Y) - 1;
                var color = new Color(255, 0, 0);
                canvas[x, y] = color;
                canvas[x + 1, y] = color;
                canvas[x - 1, y] = color;
                canvas[x, y + 1] = color;
                canvas[x, y - 1] = color;
                canvas[x + 1, y + 1] = color;
                canvas[x - 1, y + 1] = color;
                canvas[x + 1, y - 1] = color;
                canvas[x - 1, y - 1] = color;

                count++;
                Tick(environment, projectile);
                Console.WriteLine(
                    $"Position after {count:0000} ticks: {projectile.Position} (velocity: {projectile.Velocity})");
            } while (projectile.Position.Y >= 0);

            using var file = new SKFileWStream(@"c:\temp\test.png");
            using var bitmap = canvas.RenderToBitmap();
            SKPixmap.Encode(file, bitmap, SKEncodedImageFormat.Png, 100);
        }

        private static void Tick(Environment environment, Projectile projectile)
        {
            projectile.Position += projectile.Velocity;
            projectile.Velocity = projectile.Velocity + environment.Gravity + environment.Wind;
        }
    }
}
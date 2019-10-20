using System;
using System.IO;
using System.Threading.Tasks;
using RayTracer.Common.Primitives;
using RayTracer.Common.Primitives.CanvasExporters;

namespace RayTracer.Scratchpad._01_Primitive_Cannon
{
    public static class CannonTest
    {
        public static async Task Run()
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
            for (var y = 0; y < canvas.Height; y++)
            for (var x = 0; x < canvas.Width; x++)
            {
                canvas[x, y] = Color.Black;
            }

            var count = 0;
            Console.WriteLine($"Initial position: {projectile.Position}");
            do
            {
                // Map the position to a pixel on the canvas
                var x = (int)(projectile.Position.X) + 1;
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
                Console.WriteLine($"Position after {count:0000} ticks: {projectile.Position} (velocity: {projectile.Velocity})");
            } while (projectile.Position.Y >= 0);

            await using var file = File.OpenWrite(@"c:\temp\test.ppm");
            await PpmExporter.WriteToStreamAsync(canvas, file);
        }

        private static void Tick(Environment environment, Projectile projectile)
        {
            projectile.Position += projectile.Velocity;
            projectile.Velocity = projectile.Velocity + environment.Gravity + environment.Wind;
        }
    }
}
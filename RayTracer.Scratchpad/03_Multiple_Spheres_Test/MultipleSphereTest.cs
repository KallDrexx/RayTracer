using System.Linq;
using RayTracer.Common;
using RayTracer.Common.Core;
using RayTracer.Common.Primitives;

namespace RayTracer.Scratchpad._03_Multiple_Spheres_Test
{
    public class MultipleSphereTest : IExampleRunner
    {
        private const int Height = 500;
        private const int Width = 500;
        
        public Canvas Run()
        {
            var canvas = new Canvas(Width, Height);
            canvas.ClearToColor(Color.Black);

            var spheres = GetSpheres();

            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
            {
                var ray = GetRayForCanvasPosition(x, y);
                foreach (var sphere in spheres)
                {
                    var intersections = ray.Intersects(sphere);
                    if (intersections.Any())
                    {
                        canvas[x, y] = new Color(1, 0, 0);
                        break;
                    }
                }
            }

            return canvas;
        }

        private static Ray GetRayForCanvasPosition(int canvasX, int canvasY)
        {
            // Assumes the camera is looking at the Y/X intersection straight on,
            // with world (0,0) at the center of the canvas
            
            var rayX = (Width / 2) - Width + canvasX;
            var rayY = (-Height / 2) + Height - canvasY;
            
            return new Ray(new Point(rayX, rayY, -5), new Vector(0, 0, 1));
        }

        private static Sphere[] GetSpheres()
        {
            return new[]
            {
                new Sphere(new Transform().Scale(20, 20, 1).Translate(50, 50, 200).GetTransformationMatrix()),
                new Sphere(new Transform().Scale(50, 20, 1).Translate(250, 100, 0).GetTransformationMatrix()),
                new Sphere(new Transform().Scale(100, 5, 1).Translate(-100, -50, 0).GetTransformationMatrix()),
                new Sphere(new Transform().Scale(20, 90, 1).Translate(-50, 70, 0).GetTransformationMatrix()),
                new Sphere(new Transform().Scale(50, 50, 1).Translate(-260, -260, 0).GetTransformationMatrix()),
            };
        }
    }
}
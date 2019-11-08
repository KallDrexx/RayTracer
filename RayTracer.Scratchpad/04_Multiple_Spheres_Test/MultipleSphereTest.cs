using System.Linq;
using RayTracer.Common;
using RayTracer.Common.Core;
using RayTracer.Common.Primitives;

namespace RayTracer.Scratchpad._04_Multiple_Spheres_Test
{
    public class MultipleSphereTest : IExampleRunner
    {
        private const int CanvasSize = 500;

        public Canvas Run()
        {
            var canvas = new Canvas(CanvasSize, CanvasSize);
            canvas.ClearToColor(Color.Black);

            var rayOrigin = new Point(0, 0, -5);
            var spheres = GetSpheres();

            for (var x = 0; x < CanvasSize; x++)
            for (var y = 0; y < CanvasSize; y++)
            {
                var direction = GetRayDirection(rayOrigin, CanvasSize, x, y);
                var ray = new Ray(rayOrigin, direction);
                
                foreach (var sphere in spheres)
                {
                    var intersections = ray.Intersects(sphere);
                    if (intersections.GetHit().HasValue)
                    {
                        canvas[x, y] = new Color(1, 0, 0);
                        break;
                    }
                }
            }

            return canvas;
        }

        private static Vector GetRayDirection(Point rayOrigin, int pixelCount, int xIndex, int yIndex)
        {
            const float wallSize = 7;
            const int wallDepth = 10;
            var pixelSize = wallSize / pixelCount;

            var pointX = (-wallSize / 2) + pixelSize * xIndex;
            var pointY = (-wallSize / 2) + pixelSize * yIndex;
            var point = new Point(pointX, pointY, wallDepth);

            return (point - rayOrigin).Normalize();
        }

        private static Sphere[] GetSpheres()
        {
            return new[]
            {
                new Sphere(new Transform().Scale(1, 1, 1).Translate(1, 1, 200).GetTransformationMatrix()),
                new Sphere(new Transform().Scale(20, 2, 1).Translate(20, 20, 200).GetTransformationMatrix()),
            };
        }
    }
}
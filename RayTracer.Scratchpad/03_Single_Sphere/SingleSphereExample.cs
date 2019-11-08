using System;
using RayTracer.Common;
using RayTracer.Common.Core;
using RayTracer.Common.Primitives;

namespace RayTracer.Scratchpad._03_Single_Sphere
{
    public class SingleSphereExample : IExampleRunner
    {
        public Canvas Run()
        {
            const int canvasPixels = 500;
            var canvas = new Canvas(canvasPixels, canvasPixels);
            canvas.ClearToColor(Color.Black);
            
            var rayOrigin = new Point(0, 0, -5);
            var sphere = new Sphere();
            
            for (var x = 0; x < canvasPixels; x++)
            for (var y = 0; y < canvasPixels; y++)
            {
                var rayDirection = GetRayDirection(rayOrigin, canvasPixels, x, y);
                var ray = new Ray(rayOrigin, rayDirection);

                var intersections = ray.Intersects(sphere);
                var hit = intersections.GetHit();

                if (hit.HasValue)
                {
                    canvas[x, y] = new Color(1, 0, 0);
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
    }
}
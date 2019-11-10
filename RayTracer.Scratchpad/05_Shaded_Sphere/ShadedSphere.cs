using RayTracer.Common;
using RayTracer.Common.Core;
using RayTracer.Common.Primitives;

namespace RayTracer.Scratchpad._05_Shaded_Sphere
{
    public class ShadedSphere : IExampleRunner
    {
        public Canvas Run()
        {
            const int canvasPixels = 500;
            var canvas = new Canvas(canvasPixels, canvasPixels);
            canvas.ClearToColor(Color.Black);
            
            var rayOrigin = new Point(0, 0, -5);
            var transform = new Transform()
                //.Scale(1.1, 0.7, 1)
                //.Translate(0, 0.5, 0)
                .GetTransformationMatrix();
            
            var sphere = new Sphere(transform)
            {
                Material =
                {
                    Color = new Color(1, 0.2, 1),
                    Specular = 0.9,
                    Ambient = 0.1,
                    Diffuse = 0.5,
                    Shininess = 200,
                }
            };
            
            var light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));

            for (var x = 0; x < canvasPixels; x++)
            for (var y = 0; y < canvasPixels; y++)
            {
                var rayDirection = GetRayDirection(rayOrigin, canvasPixels, x, y);
                var ray = new Ray(rayOrigin, rayDirection);

                var intersections = ray.Intersects(sphere);
                var hit = intersections.GetHit();

                if (hit.HasValue)
                {
                    var hitPoint = ray.PositionAt(hit.Value.Time);
                    var normal = hit.Value.Object.NormalAt(hitPoint);
                    var eye = -rayDirection;
                    var color = hit.Value.Object.Material.CalculateLighting(light, hitPoint, eye, normal);

                    canvas[x, y] = color;
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
            var pointY = (wallSize / 2) - pixelSize * yIndex;
            var point = new Point(pointX, pointY, wallDepth);

            return (point - rayOrigin).Normalize();
        }
    }
}
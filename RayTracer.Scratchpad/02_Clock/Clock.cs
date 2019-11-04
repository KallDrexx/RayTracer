using System;
using RayTracer.Common;
using RayTracer.Common.Primitives;
using SkiaSharp;

namespace RayTracer.Scratchpad._02_Clock
{
    public static class Clock
    {
        public static void Run()
        {
            const int height = 1000;
            const int width = 1000;
            const int middleX = width / 2;
            const int middleY = height / 2;
            const int distanceFromCenter = (int)(middleY * 0.8);
            const int totalRotations = 12;
            const double rotationRadians = (2 * Math.PI) / totalRotations;

            var canvas = new Canvas(height, width);
            canvas.ClearToColor(Color.Black);
            
            var pixelColor = new Color(1, 0, 0);
            var origin = new Point(0, 0, 0);
            var transform = new Transform()
                .Translate(0, distanceFromCenter, 0); // Move up to 12 o'clock position
            
            for (var rotation = 0; rotation < totalRotations; rotation++)
            {
                var intensity = 1f; //(double)(rotation + 1) / rotation;
                var color = pixelColor * intensity;
                
                var rotationAmount = rotation * rotationRadians;
                var rotationTransform = transform.RotationZ(rotationAmount);

                var transformedLocation = rotationTransform.ApplyTo(origin);
                var (pixelX, pixelY) = (X: middleX - (int) transformedLocation.X, Y: middleY - (int) transformedLocation.Y);
                
                DrawPixelOnCanvas(canvas, color, pixelX, pixelY);
            }
            
            using var file = new SKFileWStream(@"c:\temp\test.png");
            using var bitmap = canvas.RenderToBitmap();
            SKPixmap.Encode(file, bitmap, SKEncodedImageFormat.Png, 100);
        }

        private static void DrawPixelOnCanvas(Canvas canvas, Color color, int canvasX, int canvasY)
        {
            const int sizeToExpandOnEachSide = 10 / 2;
            
            for (var x = -sizeToExpandOnEachSide; x <= sizeToExpandOnEachSide; x++)
            for (var y = -sizeToExpandOnEachSide; y <= sizeToExpandOnEachSide; y++)
            {
                canvas[canvasX + x, canvasY + y] = color;
            }
        }
    }
}
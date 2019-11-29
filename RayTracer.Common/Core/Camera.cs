using System;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public class Camera
    {
        private double _halfHeight, _halfWidth;
        
        public int HorizontalPixelCount { get; }
        public int VerticalPixelCount { get; }
        public double FieldOfView { get; }
        public Matrix4X4 ViewTransform { get; set;  }
        public double PixelSize { get; private set; }

        public Camera(int horizontalPixelCount, int verticalPixelCount, double fieldOfView)
        {
            HorizontalPixelCount = horizontalPixelCount;
            VerticalPixelCount = verticalPixelCount;
            FieldOfView = fieldOfView;
            ViewTransform = Matrix4X4.IdentityMatrix;

            CalculatePixelSize();
        }

        public Ray RayForPixel(int pixelX, int pixelY)
        {
            // Offset from the edge of the canvas
            var xOffset = (pixelX + 0.5) * PixelSize;
            var yOffset = (pixelY + 0.5) * PixelSize;
            
            // Untransformed coordinates of the pixel in world space
            var worldX = _halfWidth - xOffset;
            var worldY = _halfHeight - yOffset;
            
            var (wasInvertible, viewInverse) = ViewTransform.Invert();
            if (!wasInvertible)
            {
                throw new InvalidOperationException("View transform was not invertible");
            }

            // Transform the canvas point and origin, then compute the ray's direction vector
            var pixel = viewInverse * new Point(worldX, worldY, -1);
            var origin = viewInverse * new Point(0, 0, 0);
            var direction = (pixel - origin).Normalize();
            
            return new Ray(origin, direction);
        }

        public Canvas Render(World world)
        {
            var canvas = new Canvas(HorizontalPixelCount, VerticalPixelCount);
            
            for (var x = 0; x < HorizontalPixelCount; x++)
            for (var y = 0; y < VerticalPixelCount; y++)
            {
                var ray = RayForPixel(x, y);
                var color = world.ColorAtIntersection(ray);
                canvas[x, y] = color;
            }

            return canvas;
        }

        private void CalculatePixelSize()
        {
            var halfView = Math.Tan(FieldOfView / 2);
            var aspectRatio = (double)HorizontalPixelCount / VerticalPixelCount;

            if (HorizontalPixelCount >= VerticalPixelCount)
            {
                _halfWidth = halfView;
                _halfHeight = halfView / aspectRatio;
            }
            else
            {
                _halfHeight = halfView;
                _halfWidth = halfView * aspectRatio;
            }

            PixelSize = (_halfWidth * 2) / HorizontalPixelCount;
        }
    }
}
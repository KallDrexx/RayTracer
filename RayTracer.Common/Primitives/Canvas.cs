using System;
using SkiaSharp;

namespace RayTracer.Common.Primitives
{
    public class Canvas
    {
        private readonly Color[] _pixels;
        public int Height { get; }
        public int Width { get; }

        public Canvas(int width, int height)
        {
            Height = height;
            Width = width;
            
            _pixels = new Color[height * width];
            ClearToColor(Color.White);
        }

        public void ClearToColor(Color color)
        {
            for (var x = 0; x < _pixels.Length; x++)
            {
                _pixels[x] = color;
            }
        }

        public Color this[int x, int y]
        {
            get => _pixels[GetIndexForDimensions(x, y)];
            set => _pixels[GetIndexForDimensions(x, y)] = value;
        }

        public SKBitmap RenderToBitmap()
        {
            // By default make the bitmap MonoGame friendly
            var bitmap = new SKBitmap(Width, Height, SKColorType.Bgra8888, SKAlphaType.Premul);
            
            for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
            {
                var color = this[x, y];
                var red = color.Red < 0 ? 0
                    : color.Red > 1 ? 255
                    : color.Red * 255;
                
                var green = color.Green < 0 ? 0
                    : color.Green > 1 ? 255
                    : color.Green * 255;
                
                var blue = color.Blue < 0 ? 0
                    : color.Blue > 1 ? 255
                    : color.Blue * 255;

                var skiaColor = new SKColor((byte) red, (byte) green, (byte) blue);
                bitmap.SetPixel(x, y, skiaColor);
            }

            return bitmap;
        }

        private int GetIndexForDimensions(int x, int y)
        {
            if (x >= Width)
            {
                throw new IndexOutOfRangeException($"({x} {y}) is out of range of width {Width}");
            }

            if (y >= Height)
            {
                throw new IndexOutOfRangeException($"({x} {y}) is out of range of height {Height}");
            }

            return (y * Width) + x;
        }
    }
}
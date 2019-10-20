using System;

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
        }

        public Color this[int x, int y]
        {
            get => _pixels[GetIndexForDimensions(x, y)];
            set => _pixels[GetIndexForDimensions(x, y)] = value;
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
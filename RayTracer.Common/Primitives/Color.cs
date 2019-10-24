using System;

namespace RayTracer.Common.Primitives
{
    public struct Color : IEquatable<Color>
    {
        private const float Epsilon = 0.0001f;
        
        public static readonly Color White = new Color(1f, 1f, 1f);
        public static readonly Color Black = new Color(0f, 0f, 0f);
        
        public readonly double Red;
        public readonly double Green;
        public readonly double Blue;

        public Color(double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public static bool operator ==(Color first, Color second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Color first, Color second)
        {
            return !first.Equals(second);
        }

        public static Color operator +(Color first, Color second)
        {
            return new Color(first.Red + second.Red,
                first.Green + second.Green,
                first.Blue + second.Blue);
        }

        public static Color operator -(Color first, Color second)
        {
            return new Color(first.Red - second.Red,
                first.Green - second.Green,
                first.Blue - second.Blue);
        }

        public static Color operator *(Color first, double scalar)
        {
            return new Color(first.Red * scalar,
                first.Green * scalar,
                first.Blue * scalar);
        }

        public static Color operator *(Color first, Color second)
        {
            return new Color(first.Red * second.Red,
                first.Green * second.Green,
                first.Blue * second.Blue);
        }

        public override string ToString()
        {
            return $"Color ({Red}, {Green}, {Blue})";
        }

        public bool Equals(Color other)
        {
            return Math.Abs(Red - other.Red) < Epsilon &&
                   Math.Abs(Green - other.Green) < Epsilon &&
                   Math.Abs(Blue - other.Blue) < Epsilon;
        }

        public override bool Equals(object obj)
        {
            return obj is Color other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Red.GetHashCode();
                hashCode = (hashCode * 397) ^ Green.GetHashCode();
                hashCode = (hashCode * 397) ^ Blue.GetHashCode();
                return hashCode;
            }
        }
    }
}
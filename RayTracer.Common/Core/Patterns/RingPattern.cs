using System;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core.Patterns
{
    public class RingPattern : Pattern
    {
        private readonly Color _color1, _color2;

        public RingPattern(Color color1, Color color2)
        {
            _color1 = color1;
            _color2 = color2;
        }

        protected override Color GetColorAtAdjustedPoint(Point point)
        {
            var inner = Math.Pow(point.X, 2) + Math.Pow(point.Z, 2);
            return (int)Math.Floor(Math.Sqrt(inner)) % 2 == 0
                ? _color1
                : _color2;
        }
    }
}
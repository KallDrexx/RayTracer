using System;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core.Patterns
{
    public class CheckersPattern : Pattern
    {
        private readonly Color _color1, _color2;

        public CheckersPattern(Color color1, Color color2)
        {
            _color1 = color1;
            _color2 = color2;
        }

        protected override Color GetColorAtAdjustedPoint(Point point)
            => (int) (Math.Floor(point.X) + Math.Floor(point.Y) + Math.Floor(point.Z)) % 2 == 0
                ? _color1
                : _color2;
    }
}
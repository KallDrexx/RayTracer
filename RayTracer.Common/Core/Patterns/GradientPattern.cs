using System;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core.Patterns
{
    public class GradientPattern : Pattern
    {
        private readonly Color _color1, _color2;

        public GradientPattern(Color color1, Color color2)
        {
            _color1 = color1;
            _color2 = color2;
        }

        protected override Color GetColorAtAdjustedPoint(Point point)
        {
            var distance = _color2 - _color1;
            var fraction = point.X - Math.Floor(point.X);
            return _color1 + distance * fraction;
        }
    }
}
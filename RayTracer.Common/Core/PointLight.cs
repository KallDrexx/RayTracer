using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public class PointLight
    {
        public Point Position { get; }
        public Color Intensity { get; }

        public PointLight(Point position, Color intensity)
        {
            Position = position;
            Intensity = intensity;
        }
    }
}
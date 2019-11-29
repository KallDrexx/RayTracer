using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public class PointLight
    {
        public Point Position { get; set; }
        public Color Intensity { get; set; }

        public PointLight(Point position, Color intensity)
        {
            Position = position;
            Intensity = intensity;
        }
    }
}
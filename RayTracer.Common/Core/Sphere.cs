using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public class Sphere
    {
        public Point Origin { get; }
        public double Radius { get; }

        public Sphere()
        {
            Origin = new Point(0, 0, 0);
            Radius = 1;
        }
        
        public Sphere(Point origin, double radius)
        {
            Origin = origin;
            Radius = radius;
        }
    }
}
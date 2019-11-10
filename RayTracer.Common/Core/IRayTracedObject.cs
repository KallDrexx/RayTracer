using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public interface IRayTracedObject
    {
        Material Material { get; }
        Vector NormalAt(Point point);
    }
}
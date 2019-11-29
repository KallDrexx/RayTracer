using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public class IntersectionPreComputation
    {
        public double Time { get; }
        public IRayTracedObject Object { get; }
        public Point Point { get; }
        public Vector EyeVector { get; }
        public Vector NormalVector { get; }
        public bool IsInside { get; }

        public IntersectionPreComputation(double time, 
            IRayTracedObject obj, 
            Point point, 
            Vector eyeVector, 
            Vector normalVector, 
            bool isInside)
        {
            Time = time;
            Object = obj;
            Point = point;
            EyeVector = eyeVector;
            NormalVector = normalVector;
            IsInside = isInside;
        }
    }
}
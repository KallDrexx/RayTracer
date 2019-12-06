using RayTracer.Common.Core.Objects;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public class IntersectionPreComputation
    {
        public double Time { get; }
        public RayTraceableObject Object { get; }
        public Point Point { get; }
        public Vector EyeVector { get; }
        public Vector NormalVector { get; }
        public bool IsInside { get; }
        public Point OverPoint { get; }

        public IntersectionPreComputation(double time, 
            RayTraceableObject obj, 
            Point point, 
            Vector eyeVector, 
            Vector normalVector, 
            bool isInside, 
            Point overPoint)
        {
            Time = time;
            Object = obj;
            Point = point;
            EyeVector = eyeVector;
            NormalVector = normalVector;
            IsInside = isInside;
            OverPoint = overPoint;
        }
    }
}
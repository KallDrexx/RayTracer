using System;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core.Objects
{
    public class Plane : RayTraceableObject
    {
        // NOTE: Untransformed planes are assumed infinite across x/y axis
        
        protected override Vector LocalNormalAt(Point objectPoint)
            => new Vector(0, 1, 0);

        protected override IntersectionCollection GetLocalIntersections(Ray objectSpaceRay)
        {
            // if parallel, no intersections
            if (Math.Abs(objectSpaceRay.Direction.Y) < 0.0001f)
            {
                return new IntersectionCollection();
            }

            var time = -objectSpaceRay.Origin.Y / objectSpaceRay.Direction.Y;
            return new IntersectionCollection(new Intersection(time, this));
        }
    }
}
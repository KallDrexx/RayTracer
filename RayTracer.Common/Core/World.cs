using System.Collections.Generic;
using System.Linq;
using RayTracer.Common.Core.Objects;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public class World
    {
        public List<PointLight> PointLights { get; } = new List<PointLight>();
        public List<RayTraceableObject> Objects { get; } = new List<RayTraceableObject>();

        public IntersectionCollection Intersections(Ray ray)
        {
            var intersections = Objects.SelectMany(x => x.GetIntersections(ray))
                .OrderBy(x => x.Time)
                .ToArray();
            
            return new IntersectionCollection(intersections);
        }

        public Color ShadePreComputation(IntersectionPreComputation preComputation)
        {
            var color = Color.Black;

            foreach (var pointLight in PointLights)
            {
                var isInShadow = IsInShadow(preComputation.OverPoint, pointLight);
                
                color += preComputation.Object.Material.CalculateLighting(pointLight,
                    preComputation.Point,
                    preComputation.EyeVector,
                    preComputation.NormalVector,
                    isInShadow);
            }

            return color;
        }

        public Color ColorAtIntersection(Ray ray)
        {
            var intersections = Intersections(ray);
            var hit = intersections.GetHit();
            if (hit == null)
            {
                return Color.Black;
            }

            var preComputation = hit.Value.GetPreComputation(ray);
            return ShadePreComputation(preComputation);
        }

        public bool IsInShadow(Point point, PointLight light)
        {
            var vectorToLight = light.Position - point;
            var distance = vectorToLight.Magnitude;
            var ray = new Ray(point, vectorToLight.Normalize());
            foreach (var sphere in Objects)
            {
                var hit = sphere.GetIntersections(ray).GetHit();
                if (hit != null && hit.Value.Time < distance)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
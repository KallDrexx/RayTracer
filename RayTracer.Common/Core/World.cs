﻿using System.Collections.Generic;
using System.Linq;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public class World
    {
        public List<PointLight> PointLights { get; } = new List<PointLight>();
        public List<Sphere> Spheres { get; } = new List<Sphere>();

        public IntersectionCollection Intersections(Ray ray)
        {
            var intersections = Spheres.SelectMany(ray.Intersects)
                .OrderBy(x => x.Time)
                .ToArray();
            
            return new IntersectionCollection(intersections);
        }

        public Color ShadePreComputation(IntersectionPreComputation preComputation)
        {
            var color = Color.Black;

            foreach (var pointLight in PointLights)
            {
                var isInShadow = IsInShadow(preComputation.OverPoint);
                
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

        public bool IsInShadow(Point point)
        {
            foreach (var pointLight in PointLights)
            {
                var vectorToLight = pointLight.Position - point;
                var distance = vectorToLight.Magnitude;
                var ray = new Ray(point, vectorToLight.Normalize());
                foreach (var sphere in Spheres)
                {
                    var hit = ray.Intersects(sphere).GetHit();
                    if (hit != null && hit.Value.Time < distance)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
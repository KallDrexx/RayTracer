using System.Collections.Generic;
using System.Linq;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public class World
    {
        public List<PointLight> PointLights { get; } = new List<PointLight>();
        public List<Sphere> Spheres { get; } = new List<Sphere>();

        public static World CreateDefaultWorld()
        {
            return new World
            {
                PointLights = {new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1))},
                Spheres =
                {
                    new Sphere
                    {
                        Material = new Material
                        {
                            Color = new Color(0.8, 1.0, 0.6),
                            Diffuse = 0.7,
                            Specular = 0.2,
                        }
                    },

                    new Sphere(Matrix4X4.CreateScale(0.5, 0.5, 0.5)),
                }
            };
        }

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
                color += preComputation.Object.Material.CalculateLighting(pointLight,
                    preComputation.Point,
                    preComputation.EyeVector,
                    preComputation.NormalVector);
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
    }
}
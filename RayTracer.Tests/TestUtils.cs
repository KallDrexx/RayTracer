using RayTracer.Common.Core;
using RayTracer.Common.Primitives;

namespace RayTracer.Tests
{
    public static class TestUtils
    {
        public static World CreateTestWorld()
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
    }
}
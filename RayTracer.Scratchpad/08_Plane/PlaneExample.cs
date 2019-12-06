using System;
using RayTracer.Common;
using RayTracer.Common.Core;
using RayTracer.Common.Core.Objects;
using RayTracer.Common.Primitives;

namespace RayTracer.Scratchpad._08_Plane
{
    public class PlaneExample : IExampleRunner
    {
        public Canvas Run()
        {
            var world = CreateWorld();
            var camera = new Camera(500, 250, Math.PI / 3)
            {
                ViewTransform = Matrix4X4.CreateViewTransform(new Point(0, 1.5, -5), 
                    new Point(0, 1, 0), 
                    new Vector(0, 1, 0))
            };

            return camera.Render(world);
        }

        private static World CreateWorld()
        {
            var floor = new Plane
            {
                Material = new Material
                {
                    Color = new Color(1, 0.9, 0.9),
                    Specular = 0,
                }
            };

            var backWall = new Plane
            {
                TransformMatrix = new Transform().RotateX(Math.PI / 2)
                    .RotateY(-Math.PI / 2)
                    .Translate(1.5, 0, 0)
                    .GetTransformationMatrix(),
                
                Material = new Material
                {
                    Color = new Color(0, 0.5, 0.5),
                    Specular = 0,
                }
            };

            var middleSphere = new Sphere(Matrix4X4.CreateTranslation(-0.5, 1, 0.5))
            {
                Material = new Material
                {
                    Color = new Color(0.1, 1, 0.5),
                    Diffuse = 0.7,
                    Specular = 0.3,
                }
            };

            var rightSphere = new Sphere(Matrix4X4.CreateTranslation(1.5, 0.5, -0.5) *
                                         Matrix4X4.CreateScale(0.5, 0.5, 0.5))
            {
                Material = new Material
                {
                    Color = new Color(0.5, 1, 0.1),
                    Diffuse = 0.7,
                    Specular = 0.3,
                }
            };

            var leftSphere = new Sphere(Matrix4X4.CreateTranslation(-1.5, 0.33, -0.75) *
                                        Matrix4X4.CreateScale(0.33, 0.33, 0.33))
            {
                Material = new Material
                {
                    Color = new Color(1, 0.8, 0.1),
                    Diffuse = 0.7,
                    Specular = 0.3,
                }
            };
            
            var light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));

            return new World
            {
                Objects = {middleSphere, rightSphere, leftSphere, floor, backWall},
                PointLights = {light},
            };
        }
    }
}
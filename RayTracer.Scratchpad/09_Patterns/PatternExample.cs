﻿using System;
using RayTracer.Common;
using RayTracer.Common.Core;
using RayTracer.Common.Core.Objects;
using RayTracer.Common.Core.Patterns;
using RayTracer.Common.Primitives;

namespace RayTracer.Scratchpad._09_Patterns
{
    public class PatternExample : IExampleRunner
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
                    Pattern = new CheckersPattern(Color.White, Color.Black),
                    Color = Color.White,
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
                    Pattern = new StripePattern(new Color(0, 0.5, 0.5), new Color(1, 0, 0))
                    {
                        TransformMatrix = Matrix4X4.CreateRotationY(Math.PI / 4)
                    },
                    Specular = 0,
                }
            };

            var middleSphere = new Sphere(Matrix4X4.CreateTranslation(-0.5, 1, 0.5))
            {
                Material = new Material
                {
                    Pattern = new RingPattern(new Color(1, 0, 0), new Color(0, 1, 0))
                    {
                        TransformMatrix = Matrix4X4.CreateRotationX(Math.PI / -3) *
                                          Matrix4X4.CreateScale(0.25, 0.25, 0.25)
                    },
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
                    Pattern = new StripePattern(new Color(0, 0, 1), new Color(0, 1, 0))
                    {
                        TransformMatrix = Matrix4X4.CreateScale(0.25, 0.25, 0.25)
                    },
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
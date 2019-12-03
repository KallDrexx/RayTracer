
using System;
using System.Linq;
using RayTracer.Common.Core;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core
{
    public class WorldTests
    {
        [Fact]
        public void New_World_Is_Empty()
        {
            var world = new World();
            
            world.Spheres.ShouldBeEmpty();
            world.PointLights.ShouldBeEmpty();
        }

        [Fact]
        public void Default_World_Initial_Setup()
        {
            var world = TestUtils.CreateTestWorld();
            
            world.PointLights.Count.ShouldBe(1);
            world.PointLights[0].Intensity.ShouldBe(new Color(1, 1, 1));
            world.PointLights[0].Position.ShouldBe(new Point(-10, 10, -10));
            
            world.Spheres.Count.ShouldBe(2);
            world.Spheres.Any(x => x.Material.Color == new Color(0.8, 1, 0.6) &&
                                   Math.Abs(x.Material.Diffuse - 0.7) < 0.0001f &&
                                   Math.Abs(x.Material.Specular - 0.2) < 0.0001f &&
                                   x.Transform.Equals(Matrix4X4.IdentityMatrix))
                .ShouldBeTrue();
            
            var defaultMaterial = new Material();
            world.Spheres.Any(x => x.Material.Color.Equals(defaultMaterial.Color) &&
                                   Math.Abs(x.Material.Diffuse - defaultMaterial.Diffuse) < 0.0001f &&
                                   Math.Abs(x.Material.Specular - defaultMaterial.Specular) < 0.0001f &&
                                   x.Transform.Equals(Matrix4X4.CreateScale(0.5, 0.5, 0.5)))
                .ShouldBeTrue();
        }

        [Fact]
        public void Ray_World_Intersection()
        {
            var world = TestUtils.CreateTestWorld();
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            var intersections = world.Intersections(ray);
            
            intersections.ShouldNotBeNull();
            intersections.Count.ShouldBe(4);
            intersections.Any(x => Math.Abs(x.Time - 4) < 0.0001).ShouldBeTrue();
            intersections.Any(x => Math.Abs(x.Time - 4.5) < 0.0001).ShouldBeTrue();
            intersections.Any(x => Math.Abs(x.Time - 5.5) < 0.0001).ShouldBeTrue();
            intersections.Any(x => Math.Abs(x.Time - 6) < 0.0001).ShouldBeTrue();
        }

        [Fact]
        public void Shading_Intersection_From_Outside()
        {
            var world = TestUtils.CreateTestWorld();
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var sphere = world.Spheres[0];
            var intersection = new Intersection(4, sphere);
            var computation = intersection.GetPreComputation(ray);
            var color = world.ShadePreComputation(computation);
            
            color.ShouldBe(new Color(0.38066, 0.47583, 0.2855));
        }

        [Fact]
        public void Shading_Intersection_From_Inside()
        {
            var world = TestUtils.CreateTestWorld();
            world.PointLights[0].Position = new Point(0, 0.25, 0);
            world.PointLights[0].Intensity = new Color(1, 1, 1);
            
            var ray = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            var sphere = world.Spheres[1];
            var intersection = new Intersection(0.5, sphere);
            var computation = intersection.GetPreComputation(ray);
            var color = world.ShadePreComputation(computation);
            
            color.ShouldBe(new Color(0.90498, 0.90498, 0.90498));
        }

        [Fact]
        public void Color_When_Ray_Misses()
        {
            var world = TestUtils.CreateTestWorld();
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 1, 0));
            var color = world.ColorAtIntersection(ray);
            
            color.ShouldBe(Color.Black);
        }

        [Fact]
        public void Color_When_Ray_Hits()
        {
            var world = TestUtils.CreateTestWorld();
            var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var color = world.ColorAtIntersection(ray);
            
            color.ShouldBe(new Color(0.38066, 0.47583, 0.2855));
        }

        [Fact]
        public void Color_With_Intersection_In_Between_Two_Spheres()
        {
            var world = TestUtils.CreateTestWorld();
            var outerSphere = world.Spheres[0];
            var innerSphere = world.Spheres[1];
            var ray = new Ray(new Point(0, 0, 0.75), new Vector(0, 0, -1));

            outerSphere.Material.Ambient = 1;
            innerSphere.Material.Ambient = 1;

            var color = world.ColorAtIntersection(ray);
            
            color.ShouldBe(innerSphere.Material.Color);
        }

        [Fact]
        public void Not_In_Shadow_When_Nothing_Is_Collinear_With_Point_And_Light()
        {
            var world = TestUtils.CreateTestWorld();
            world.IsInShadow(new Point(0, 10, 0)).ShouldBeFalse();
        }

        [Fact]
        public void In_Shadow_When_Object_Between_Point_And_Light()
        {
            var world = TestUtils.CreateTestWorld();
            world.IsInShadow(new Point(10, -10, 10)).ShouldBeTrue();
        }

        [Fact]
        public void Not_In_Shadow_When_Object_Behind_Light()
        {
            var world = TestUtils.CreateTestWorld();
            world.IsInShadow(new Point(-20, 20, -20)).ShouldBeFalse();
        }

        [Fact]
        public void Not_In_Shadow_When_Object_Behind_Point()
        {
            var world = TestUtils.CreateTestWorld();
            world.IsInShadow(new Point(-2, 2, -2)).ShouldBeFalse();
        }

        [Fact]
        public void Shade_When_Given_Intersection_Is_In_Shadow()
        {
            var world = new World
            {
                PointLights = {new PointLight(new Point(0, 0, -10), new Color(1, 1, 1))},
                Spheres =
                {
                    new Sphere(),
                    new Sphere(Matrix4X4.CreateTranslation(0, 0, 10)),
                }
            };
            
            var ray = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
            var intersection = new Intersection(4, world.Spheres[1]);
            var computation = intersection.GetPreComputation(ray);
            var color = world.ShadePreComputation(computation);
            
            color.ShouldBe(new Color(0.1, 0.1, 0.1));
        }
    }
}
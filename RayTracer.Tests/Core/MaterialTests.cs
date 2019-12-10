using System;
using RayTracer.Common.Core;
using RayTracer.Common.Core.Objects;
using RayTracer.Common.Core.Patterns;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Core
{
    public class MaterialTests
    {
        [Fact]
        public void Default_Material_Values()
        {
            var material = new Material();
            
            material.Color.ShouldBe(new Color(1, 1, 1));
            material.Ambient.ShouldBe(0.1);
            material.Diffuse.ShouldBe(0.9);
            material.Specular.ShouldBe(0.9);
            material.Shininess.ShouldBe(200);
        }

        [Fact]
        public void Lighting_With_Eye_Between_Light_And_Surface()
        {
            var material = new Material();
            var point = new Point(0, 0, 0);
            var eyeVector = new Vector(0, 0, -1);
            var normalVector = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
            
            material.CalculateLighting(light, point, eyeVector, normalVector, false, new Sphere())
                .ShouldBe(new Color(1.9, 1.9, 1.9));
        }

        [Fact]
        public void Lighting_With_Eye_Between_Light_And_Surface_Eye_Offset_45_Degrees()
        {
            var material = new Material();
            var point = new Point(0, 0, 0);
            var eyeVector = new Vector(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var normalVector = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
            
            material.CalculateLighting(light, point, eyeVector, normalVector, false, new Sphere())
                .ShouldBe(new Color(1, 1, 1));
        }

        [Fact]
        public void Lighting_With_Eye_Opposite_Surface_Light_offset_45_Degrees()
        {
            var material = new Material();
            var point = new Point(0, 0, 0);
            var eyeVector = new Vector(0, 0, -1);
            var normalVector = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 10, -10), new Color(1, 1, 1));
            
            material.CalculateLighting(light, point, eyeVector, normalVector, false, new Sphere())
                .ShouldBe(new Color(0.7364, 0.7364, 0.7364));
        }

        [Fact]
        public void Lighting_With_Eye_In_Path_Of_Reflection_Vector()
        {
            var material = new Material();
            var point = new Point(0, 0, 0);
            var eyeVector = new Vector(0, -Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var normalVector = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 10, -10), new Color(1, 1, 1));
            
            material.CalculateLighting(light, point, eyeVector, normalVector, false, new Sphere())
                .ShouldBe(new Color(1.6364, 1.6364, 1.6364));
        }

        [Fact]
        public void Lighting_With_Light_Behind_Surface()
        {
            var material = new Material();
            var point = new Point(0, 0, 0);
            var eyeVector = new Vector(0, 0, -1);
            var normalVector = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, 10), new Color(1, 1, 1));
            
            material.CalculateLighting(light, point, eyeVector, normalVector, false, new Sphere())
                .ShouldBe(new Color(0.1, 0.1, 0.1));
        }

        [Fact]
        public void Lighting_With_Surface_In_Shadow()
        {
            var material = new Material();
            var point = new Point(0, 0, 0);
            var eyeVector = new Vector(0, 0, -1);
            var normalVector = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
            
            material.CalculateLighting(light, point, eyeVector, normalVector, true, new Sphere())
                .ShouldBe(new Color(material.Ambient, material.Ambient, material.Ambient));
        }

        [Fact]
        public void Lighting_With_Pattern_Applied()
        {
            var material = new Material
            {
                Pattern = new StripePattern(new Color(1, 1, 1), new Color(0, 0, 0)),
                Ambient = 1,
                Diffuse = 0,
                Specular = 0,
            };
            
            var eyeVector = new Vector(0, 0, -1);
            var normalVector = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
            
            material.CalculateLighting(light, new Point(0.9, 0, 0), eyeVector, normalVector, false, new Sphere())
                .ShouldBe(new Color(1, 1, 1));
            
            material.CalculateLighting(light, new Point(1.1, 0, 0), eyeVector, normalVector, false, new Sphere())
                .ShouldBe(new Color(0, 0, 0));
        }
    }
}
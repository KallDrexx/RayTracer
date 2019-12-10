using System;
using RayTracer.Common.Core.Objects;
using RayTracer.Common.Core.Patterns;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core
{
    public class Material
    {
        public Color Color { get; set; }
        public double Ambient { get; set; }
        public double Diffuse { get; set; }
        public double Specular { get; set; }
        public double Shininess { get; set; }
        public Pattern Pattern { get; set; }

        public Material()
        {
            Color = new Color(1, 1, 1);
            Ambient = 0.1;
            Diffuse = 0.9;
            Specular = 0.9;
            Shininess = 200;
        }

        public Color CalculateLighting(PointLight light, 
            Point pointBeingIlluminated, 
            Vector eyeVector,
            Vector normalVector,
            bool inShadow,
            RayTraceableObject objectBeingDrawn)
        {
            var materialColor = Pattern?.ColorAt(pointBeingIlluminated, objectBeingDrawn) ?? Color;
            var effectiveColor = materialColor * light.Intensity;
            var lightVector = (light.Position - pointBeingIlluminated).Normalize();
            var ambientContribution = effectiveColor * Ambient;
            
            // Represents the cosine of the angle between the light vector and normal vector
            var lightDotNormal = lightVector.Dot(normalVector);
            
            Color diffuseContribution;
            Color specularContribution;
            if (inShadow || lightDotNormal < 0)
            {
                // Light is on the other side of the surface
                diffuseContribution = Color.Black;
                specularContribution = Color.Black;
            }
            else
            {
                // Light is on the same side of the surface as the normal
                diffuseContribution = effectiveColor * Diffuse * lightDotNormal;
                var reflectionVector = (-lightVector).Reflect(normalVector);

                // Represents the cosine of the angle between the reflection vector and the eye vector.
                var reflectDotEye = reflectionVector.Dot(eyeVector);
                if (reflectDotEye < 0)
                {
                    // Light reflects away from the eye
                    specularContribution = Color.Black;
                }
                else
                {
                    var factor = Math.Pow(reflectDotEye, Shininess);
                    specularContribution = light.Intensity * Specular * factor;
                }
            }

            return ambientContribution + diffuseContribution + specularContribution;
        }
    }
}
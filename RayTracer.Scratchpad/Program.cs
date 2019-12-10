using System;
using System.Diagnostics;
using System.Linq;
using RayTracer.Scratchpad._01_Primitive_Cannon;
using RayTracer.Scratchpad._02_Clock;
using RayTracer.Scratchpad._03_Single_Sphere;
using RayTracer.Scratchpad._04_Multiple_Spheres_Test;
using RayTracer.Scratchpad._05_Shaded_Sphere;
using RayTracer.Scratchpad._06_Camera;
using RayTracer.Scratchpad._07_CameraTestDualLights;
using RayTracer.Scratchpad._08_Plane;
using RayTracer.Scratchpad._09_Patterns;
using SkiaSharp;

namespace RayTracer.Scratchpad
{
    class Program
    {
        static void Main(string[] args)
        {
            var examples = new IExampleRunner[]
            {
                new CannonTest(),
                new Clock(),
                new SingleSphereExample(), 
                new MultipleSphereTest(),
                new ShadedSphere(), 
                new CameraTest(), 
                new DualLightCameraTest(),
                new PlaneExample(),
                new PatternExample(), 
            };

            var exampleToChoose = examples.Last();

            var stopwatch = Stopwatch.StartNew();
            var canvas = exampleToChoose.Run();
            stopwatch.Stop();

            using var file = new SKFileWStream(@"c:\temp\test.png");
            using var bitmap = canvas.RenderToBitmap();
            SKPixmap.Encode(file, bitmap, SKEncodedImageFormat.Png, 100);
            
            Console.WriteLine($"Canvas rendered in {stopwatch.ElapsedMilliseconds:N}ms");
        }
    }
}
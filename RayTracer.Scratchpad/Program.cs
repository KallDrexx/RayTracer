using System;
using System.Diagnostics;
using System.IO;
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
using Environment = System.Environment;

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
            
            var outputDirectory = Path.Combine(new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.ToString(), "Examples");

            var examplesToRun = examples.Select((x, index) => new {Example = x, index})
                .Skip(examples.Length - 1);
            
            foreach (var item in examplesToRun)
            {
                var filename = Path.Combine(outputDirectory, $"{item.index:00}_{item.Example.GetType().Name}.png");
                var stopwatch = Stopwatch.StartNew();
                var canvas = item.Example.Run();
                stopwatch.Stop();
                
                using var file = new SKFileWStream(filename);
                using var bitmap = canvas.RenderToBitmap();
                SKPixmap.Encode(file, bitmap, SKEncodedImageFormat.Png, 100);
                
                Console.WriteLine($"Canvas rendered in {stopwatch.ElapsedMilliseconds:N}ms");
            }
        }
    }
}
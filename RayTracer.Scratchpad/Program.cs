using System;
using System.Diagnostics;
using RayTracer.Scratchpad._01_Primitive_Cannon;
using RayTracer.Scratchpad._02_Clock;
using RayTracer.Scratchpad._03_Single_Sphere;
using RayTracer.Scratchpad._04_Multiple_Spheres_Test;
using RayTracer.Scratchpad._05_Shaded_Sphere;
using RayTracer.Scratchpad._06_Camera;
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
            };

            var exampleToChoose = examples[5];

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
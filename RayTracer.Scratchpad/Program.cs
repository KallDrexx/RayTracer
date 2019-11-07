using RayTracer.Scratchpad._01_Primitive_Cannon;
using RayTracer.Scratchpad._02_Clock;
using RayTracer.Scratchpad._03_Multiple_Spheres_Test;
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
                new MultipleSphereTest(), 
            };

            var exampleToChoose = examples[1];
            var canvas = exampleToChoose.Run();
            
            using var file = new SKFileWStream(@"c:\temp\test.png");
            using var bitmap = canvas.RenderToBitmap();
            SKPixmap.Encode(file, bitmap, SKEncodedImageFormat.Png, 100);
        }
    }
}
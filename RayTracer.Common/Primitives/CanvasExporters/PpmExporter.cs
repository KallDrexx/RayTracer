using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Common.Primitives.CanvasExporters
{
    public static class PpmExporter
    {
        private const int MaxValue = 255;
        
        public static async Task WriteToStreamAsync(Canvas canvas, Stream stream)
        {
            await using var writer = new StreamWriter(stream, Encoding.ASCII, -1, true);
            
            // header
            await writer.WriteAsync("P3\n");
            await writer.WriteAsync($"{canvas.Width} {canvas.Height}\n");
            await writer.WriteAsync(MaxValue + "\n"); // 255 max value we will represent
            
            for (var y = 0; y < canvas.Height; y++)
            {
                var currentLine = new StringBuilder();
                for (var x = 0; x < canvas.Width; x++)
                {
                    if (x > 0)
                    {
                        currentLine.Append(" ");
                    }
                    
                    var pixel = canvas[x, y];
                    var (red, green, blue) = GetOutputValues(pixel);
                    currentLine.AppendFormat("{0} {1} {2}", red, green, blue);
                }
                
                // No line should be longer than 70 characters
                if (currentLine.Length > 70)
                {
                    var index = 69;
                    while (currentLine[index] != ' ')
                    {
                        index--;
                    }

                    currentLine[index] = '\n';
                }

                await writer.WriteAsync(currentLine);
                await writer.WriteAsync("\n");
                await writer.FlushAsync();
                currentLine.Clear();
            }
        }

        private static (int red, int green, int blue) GetOutputValues(Color color)
        {
            var red = (int) Math.Round(color.Red * MaxValue);
            var green = (int) Math.Round(color.Green * MaxValue);
            var blue = (int) Math.Round(color.Blue * MaxValue);

            if (red > MaxValue) red = MaxValue;
            if (red < 0) red = 0;
            if (green > MaxValue) green = MaxValue;
            if (green < 0) green = 0;
            if (blue > MaxValue) blue = MaxValue;
            if (blue < 0) blue = 0;

            return (red, green, blue);


        }
    }
}
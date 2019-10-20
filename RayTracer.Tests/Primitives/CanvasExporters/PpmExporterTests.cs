using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RayTracer.Common.Primitives;
using RayTracer.Common.Primitives.CanvasExporters;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Primitives.CanvasExporters
{
    public class PpmExporterTests
    {
        [Fact]
        public async Task Ppm_Header_Written_Correctly()
        {
            var stream = new MemoryStream();
            var canvas = new Canvas(5, 3);

            await PpmExporter.WriteToStreamAsync(canvas, stream);

            stream.Seek(0, SeekOrigin.Begin);
            var lines = (await new StreamReader(stream).ReadToEndAsync()).Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            lines.Length.ShouldBeGreaterThan(3);
            lines[0].ShouldBe("P3");
            lines[1].ShouldBe("5 3");
            lines[2].ShouldBe("255");
        }

        [Fact]
        public async Task Value_Over_One_Clamped_To_255()
        {
            var stream = new MemoryStream();
            var canvas = new Canvas(1, 1)
            {
                [0, 0] = new Color(1.5f, 5f, 3f)
            };

            await PpmExporter.WriteToStreamAsync(canvas, stream);

            stream.Seek(0, SeekOrigin.Begin);
            var lines = (await new StreamReader(stream).ReadToEndAsync()).Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            lines.Length.ShouldBeGreaterThan(3);
            lines[3].ShouldBe("255 255 255");
        }

        [Fact]
        public async Task Value_Under_Zero_Clamped_To_Zero()
        {
            var stream = new MemoryStream();
            var canvas = new Canvas(1, 1)
            {
                [0, 0] = new Color(-1.5f, -5f, -3f)
            };

            await PpmExporter.WriteToStreamAsync(canvas, stream);

            stream.Seek(0, SeekOrigin.Begin);
            var lines = (await new StreamReader(stream).ReadToEndAsync()).Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            lines.Length.ShouldBeGreaterThan(3);
            lines[3].ShouldBe("0 0 0");
        }

        [Fact]
        public async Task All_Pixels_Are_Written()
        {
            var stream = new MemoryStream();
            var canvas = new Canvas(5, 3)
            {
                [0, 0] = new Color(1.5, 0, 0),
                [2, 1] = new Color(0, 0.5, 0),
                [4, 2] = new Color(-0.5, 0, 1),
            };

            await PpmExporter.WriteToStreamAsync(canvas, stream);

            stream.Seek(0, SeekOrigin.Begin);
            var lines = (await new StreamReader(stream).ReadToEndAsync()).Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            lines.Length.ShouldBe(6);
            lines[3].ShouldBe("255 0 0 0 0 0 0 0 0 0 0 0 0 0 0");
            lines[4].ShouldBe("0 0 0 0 0 0 0 128 0 0 0 0 0 0 0");
            lines[5].ShouldBe("0 0 0 0 0 0 0 0 0 0 0 0 0 0 255");
        }

        [Fact]
        public async Task Split_Lines_After_70_Characters()
        {
            var stream = new MemoryStream();
            var canvas = new Canvas(10, 2);
            for (var y = 0; y < canvas.Height; y++)
            for (var x = 0; x < canvas.Width; x++)
            {
                canvas[x, y] = new Color(1, 0.8, 0.6);
            }

            await PpmExporter.WriteToStreamAsync(canvas, stream);

            stream.Seek(0, SeekOrigin.Begin);
            var lines = (await new StreamReader(stream).ReadToEndAsync()).Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            lines.Length.ShouldBe(7);
            lines[3].ShouldBe("255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204");
            lines[4].ShouldBe("153 255 204 153 255 204 153 255 204 153 255 204 153");
            lines[5].ShouldBe("255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204");
            lines[6].ShouldBe("153 255 204 153 255 204 153 255 204 153 255 204 153");
        }
        
        [Fact]
        public async Task Ppm_Data_Ends_On_A_Newline()
        {
            var stream = new MemoryStream();
            var canvas = new Canvas(5, 3);

            await PpmExporter.WriteToStreamAsync(canvas, stream);

            stream.Seek(0, SeekOrigin.Begin);
            var content = await new StreamReader(stream).ReadToEndAsync();
            
            content.Last().ShouldBe('\n');
        }
    }
}
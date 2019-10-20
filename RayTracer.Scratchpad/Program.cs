using System.Threading.Tasks;
using RayTracer.Scratchpad._01_Primitive_Cannon;

namespace RayTracer.Scratchpad
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await CannonTest.Run();
        }
    }
}
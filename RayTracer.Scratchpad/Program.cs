using System;
using RayTracer.Common.Primitives;

namespace RayTracer.Scratchpad
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix1 = new Matrix4X4
            {
                M11 = 3, M12 = -9, M13 = 7, M14 = 3,
                M21 = 3, M22 = -8, M23 = 2, M24 = -9,
                M31 = -4, M32 = 4, M33 = 4, M34 = 1,
                M41 = -6, M42 = 5, M43 = -1, M44 = 1,
            };

            var (success, inverse) = matrix1.Invert();
            Console.WriteLine(matrix1 * inverse);

            //CannonTest.Run();
        }
    }
}
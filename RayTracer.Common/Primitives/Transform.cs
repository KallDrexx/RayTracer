using System.Collections.Generic;
using System.Linq;

namespace RayTracer.Common.Primitives
{
    public class Transform
    {
        private readonly Matrix4X4[] _transformMatrices;

        public Transform()
        {
            _transformMatrices = new Matrix4X4[0];
        }

        private Transform(IEnumerable<Matrix4X4> transforms, Matrix4X4 newTransform)
        {
            _transformMatrices = transforms.Concat(new[] {newTransform}).ToArray();
        }

        public Transform RotateX(double radians)
        {
            return new Transform(_transformMatrices, Matrix4X4.CreateRotationX(radians));
        }

        public Transform RotationY(double radians)
        {
            return new Transform(_transformMatrices, Matrix4X4.CreateRotationY(radians));
        }

        public Transform RotationZ(double radians)
        {
            return new Transform(_transformMatrices, Matrix4X4.CreateRotationZ(radians));
        }

        public Transform Scale(double x, double y, double z)
        {
            return new Transform(_transformMatrices, Matrix4X4.CreateScale(x, y, z));
        }

        public Transform Skew(double x, double y, double z)
        {
            return new Transform(_transformMatrices, Matrix4X4.CreateScale(x, y, z));
        }

        public Transform Translate(double x, double y, double z)
        {
            return new Transform(_transformMatrices, Matrix4X4.CreateTranslation(x, y, z));
        }

        public Point ApplyTo(Point point)
        {
            return GetTransformationMatrix() * point;
        }

        public Vector ApplyTo(Vector vector)
        {
            return GetTransformationMatrix() * vector;
        }

        public Matrix4X4 GetTransformationMatrix()
        {
            var result = Matrix4X4.IdentityMatrix;
            for (var x = _transformMatrices.Length - 1; x >= 0; x--)
            {
                var nextToApply = _transformMatrices[x];
                result *= nextToApply;
            }

            return result;
        }
    }
}
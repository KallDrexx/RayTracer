using System;
using RayTracer.Common.Core.Objects;
using RayTracer.Common.Primitives;

namespace RayTracer.Common.Core.Patterns
{
    public abstract class Pattern
    {
        private Matrix4X4 _transformMatrix, _inverseTransform;
        
        public Matrix4X4 TransformMatrix
        {
            get => _transformMatrix;
            set
            {
                _transformMatrix = value;
                var (wasInvertible, inverse) = _transformMatrix.Invert();
                if (!wasInvertible)
                {
                    throw new InvalidOperationException("Pattern had a transform that can not be inverted");
                }

                _inverseTransform = inverse;
            }
        }

        protected Pattern()
        {
            TransformMatrix = Matrix4X4.IdentityMatrix;
        }
        
        public Color ColorAt(Point point, RayTraceableObject objectBeingDrawn)
        {
            var transformedPoint = objectBeingDrawn.InverseTransform * point;
            transformedPoint = _inverseTransform * transformedPoint;
            return GetColorAtAdjustedPoint(transformedPoint);
        }

        protected abstract Color GetColorAtAdjustedPoint(Point point);
    }
}
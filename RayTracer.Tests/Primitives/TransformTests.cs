using System;
using RayTracer.Common.Primitives;
using Shouldly;
using Xunit;

namespace RayTracer.Tests.Primitives
{
    public class TransformTests
    {
        [Fact]
        public void Transforms_Applied_In_Correct_Order()
        {
            var point = new Point(1, 0, 1);
            var transform = new Transform()
                .RotateX(Math.PI / 2)
                .Scale(5, 5, 5)
                .Translate(10, 5, 7);
            
            transform.ApplyTo(point).ShouldBe(new Point(15, 0, 7));
            (transform.GetTransformationMatrix() * point).ShouldBe(new Point(15, 0, 7));
        }

        [Fact]
        public void Transform_Fluent_Interface_Doesnt_Mutate_Previous_Versions()
        {
            var point = new Point(1, 0, 1);
            var baseTransform = new Transform();
            var positiveScale = baseTransform.Scale(2, 2, 2);
            var negativeScale = baseTransform.Scale(-1, -1, -1);
            
            positiveScale.ApplyTo(point).ShouldBe(new Point(2, 0, 2));
            negativeScale.ApplyTo(point).ShouldBe(new Point(-1, 0, -1));
        }
    }
}
using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class Waters<T>
    {
        public static float4x4 TransformInGlass1()
        {
            float4x4 result = Transforms.Scale(1.2f, 1.2f, 1.5f);
            result = mul(result, Transforms.Translate(-0.9f, -0.35f, 0.4f));
            return result;
        }

        public static float4x4 TransformInGlass2()
        {
            float4x4 result = Transforms.Scale(1.2f, 1.2f, 1.5f);
            result = mul(result, Transforms.Translate(1.8f, 1.2f, 0.5f));
            return result;
        }

        public static float4x4 TransformFlow()
        {
            float4x4 result = Transforms.Scale(1, 1, 1);
            result = mul(result, Transforms.RotateZGrad(6));
            result = mul(result, Transforms.Translate(-1f, -0.35f, 0.65f));
            return result;
        }

    }

}
using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class Waters<T>
    {
        public static float4x4 Transform()
        {
            float4x4 result = Transforms.Scale(1f, 1f, 1.3f);
            result = mul(result, Transforms.RotateYGrad(75));
            result = mul(result, Transforms.RotateZGrad(6));
            result = mul(result, Transforms.Translate(-2.5f, -0.35f, 2.2f));

            return result;
        }
        public static float4x4 Transform1()
        {
            float4x4 result = Transforms.Scale(0.68f, 0.68f, 1.5f);
            result = mul(result, Transforms.Translate(-0.7f, -0.35f, 0.7f));
            return result;
        }

        public static float4x4 Transform2()
        {
            float4x4 result = Transforms.Scale(0.68f, 0.68f, 1.5f);
            result = mul(result, Transforms.Translate(1.75f, 1f, 0.8f));
            return result;
        }

    }

}
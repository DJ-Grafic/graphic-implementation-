using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class JarOfWater<T>
    {
        public static float4x4 Transform()
        {
            float4x4 result = Transforms.Scale(1f, 1f, 1.3f);
            result = mul(result, Transforms.RotateYGrad(75));
            result = mul(result, Transforms.RotateZGrad(6));
            result = mul(result, Transforms.Translate(-2.5f, -0.35f, 2.3f));
            return result;
        }

    }

}
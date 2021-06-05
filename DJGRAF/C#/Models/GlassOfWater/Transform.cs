using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class GlassOfWater<T>
    {
        public static float4x4 Transform1()
        {
            float4x4 result = Transforms.Scale(0.7f, 0.7f, 1.5f);
            result = mul(result, Transforms.Translate(-0.8f, -0.35f, 0.7f));
            return result;
        }

        public static float4x4 Transform2()
        {
            float4x4 result = Transforms.Scale(0.7f, 0.7f, 1.5f);
            result = mul(result, Transforms.Translate(1.8f, 1.2f, 0.8f));
            return result;
        }
    }

}

using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class Clown<T>
    {
        public static float4x4 Transform()
        {
            float4x4 result = Transforms.Scale(8, 3f,0.2f);
            result = mul(result, Transforms.RotateZGrad(-20));
            result = mul(result, Transforms.Translate(0.15f, -0.2f, -1.4f));
            
            return result;
        }

  
    }

}

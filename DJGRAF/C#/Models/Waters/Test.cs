using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class Waters<T> where T : struct, ICoordinatesVertex<T>
    {
        public static Mesh<T> MeshTest ()
        {
            float3[] contourn =
            {   
                float3(0.7f,1,-0.7f),
                float3(0.7f,0.5f,1),
                float3(1.1f,0.5f,1.1f),
            };
            float3[] contourn1 =
            {   
                float3(0.7f,-1,-0.7f),
                float3(0.7f,-0.5f,1),
                float3(1.1f,-0.5f,1.1f),
            };
            
            Func<float, float, float3> func = (u, v) => {
                float3 x = float3(1,0,0) * (random() * 0.1f - 0.05f); 
                return (Tools.EvalBezier(contourn, u)) * (1-v) + (Tools.EvalBezier(contourn1, u)) * v + x;
            };
            var result = Manifold<T>.Surface(30,30, func );
            //var result = Manifold<T>.Surface(30,30, (p, v) => Tools.EvalBezier(contourn, v) );
            return result;
        }

        public static float4x4 TransforT()
        {
            float4x4 result = Transforms.Scale(1f, 1f, 1.3f);
            result = mul(result, Transforms.RotateYGrad(85));
            result = mul(result, Transforms.RotateZGrad(7));
            result = mul(result, Transforms.Translate(-2.5f, -0.35f, 2.45f));
            return result;
        }
    }

}
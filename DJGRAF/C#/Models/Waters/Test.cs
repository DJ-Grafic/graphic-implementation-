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
            float size = 0;
            float3[] contourn =
            {   
                float3(0f, 0f , -1.3f),
                float3(0.1f, 0.1f , -1.35f),
                float3(0.2f, 0.2f , -1.4f),
                float3(size, size , -1.4f),
                float3(size, size , -1.4f),
                float3(size, size , -1.4f),
                float3(size, size , -1.4f),
                float3(size, size , -1.4f),
                float3(size + 0.05f, size , -1.2f),
                float3(size + 0.1f, size , -1.1f),
                float3(size + 0.05f, size , -0.9f),
                float3(size, size , -0.6f ),
                float3(size + 0.3f, size , -0.4f),


                float3(0, 0 , 0),
                float3(0, 0, 1),
                float3(-0.5f, 0, 1.5f),
                float3(-1f, 0, 2f),

            };
            var result = Manifold<T>.Generative(30,30, (u) => float3(cos(u*two_pi), u,0), (p, v) => p + Tools.EvalBezier(contourn, v)  );
            return result;
        }
        public static float4x4 TransformT()
        {
            float4x4 result = Transforms.Scale(2f, 1f, 1f);
            return result;
        }
    }

}
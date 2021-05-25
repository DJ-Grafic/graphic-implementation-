using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class Waters<T> where T : struct, ICoordinatesVertex<T>
    {
        public static Mesh<T> MeshGlass(float size = 0.6f)
        {
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
                float3(size + 0.3f, size ,  -0.2f),
                float3(size + 0.3f, size ,  -0.25f),
                float3(size + 0.3f, size ,  -0.2f),
                float3(size , size ,  -0.3f),
                float3(size - 0.2f , size ,  -0.25f),
                float3(0f, 0 ,  -0.3f),
            };
            var result = Manifold<T>.Revolution(30, 30, t => Tools.EvalBezier(contourn, t), float3(0, 0, 1));
            return result;
        }
    }

}
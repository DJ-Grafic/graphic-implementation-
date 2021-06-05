using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class Waters<T> where T : struct, ICoordinatesVertex<T>
    {
        public static Mesh<T> MeshFlow ()
        {
            float size = 0.5f;
            float3[] border =
            {   
                float3(0,0,0),
                float3(0,-size,0),
                float3(size,0,0),
                float3(0,0,0)
            };

            float3[] contourn =
            {   
                float3(0,0,-0.2f),
                float3(0,0,0),
                float3(0f,0,0.5f),
                float3(-0.2f,0, 0.9f),
                float3(-0.2f,0, 0.8f),

            };

            Func<float, float, float3> func = (u, v) => Tools.EvalBezier(border, u);
            Func<float, float3> funcc = (u) => func(u, 0);
            Func<float3, float, float3> gen = (p, u) => { 
                return p + Tools.EvalBezier(contourn, u) + float3(0, random() * 0.2f - 0.1f ,0);
            };


            var result = Manifold<T>.Generative(30,30, funcc, gen );
            //var result = Manifold<T>.Surface(30,30, func  );
            return result;
        }
    }

}
using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class JarOfWater<T> where T : struct, ICoordinatesVertex<T>
    {
        public static Mesh<T> Mesh(float size = 0.5f)
        {
            float3[] contourn =
            {
                float3(0, 0 , -0.8f),
                float3(1.8f, size ,-0.6f),
                float3(1.3f, size , -0.5f ),
                float3(1.3f, size , -0.2f ),
                float3(0.5f, size , 0.6f),
                float3(  1f, size , 0.7f),
                float3(1.2f, size , 1f),


            };
            var result = Manifold<T>.Revolution(30, 30, t => Tools.EvalBezier(contourn, t), float3(0, 0, 1));
            return result;
        }
    }

}
using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class Waters<T> where T : struct, ICoordinatesVertex<T>
    {
        public static Mesh<T> MeshInGlass(bool mov = true)
        {
            float size = 0.5f;
            
            Func<float, float3> func;
            if(mov) func = (u) => float3(u*size, 0, random() * 0.04f - 0.02f);
            else func = (u) => float3(u*size, 0, 0);

            var result = Manifold<T>.Revolution(30, 30, func, float3(0, 0, 1));
            return result;
        }
    }

}
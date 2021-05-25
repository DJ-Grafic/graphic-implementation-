using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class Clown<T> where T : struct, ICoordinatesVertex<T>
    {
        public static Mesh<T> Mesh(float size = 0.6f)
        {
            var result =  Manifold<T>.Surface(30,30,
                (u, v) => float3(u,v,0)
            );
            return result;
        }
    }

}
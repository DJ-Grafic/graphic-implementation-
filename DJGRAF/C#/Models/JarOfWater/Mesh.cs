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
                float3(0, 0 , -0.8f), // base

                float3(2.3f, size ,-0.6f), // bottom size 
                
                float3(1.3f, size , -0.5f ), 
                float3(1.3f, size , -0.2f ), // bottom contourn

                float3(-0.1f, size , 0.7f), // inflexion contourn  
                
                float3( 1f, size , 0.8f), // top contourn
                float3( 1f, size , 0.85f),

                float3( 2.6f, size , 2f), //height 

                float3( 1f - 0.05f, size -0.05f , 0.85f),
                float3( 1f - 0.05f, size -0.05f , 0.8f), // top contourn

                float3(-0.15f, size - 0.05f , 0.7f),// inflexion contourn

                float3(1.25f, size - 0.05f , -0.2f ), 
                float3(1.25f, size - 0.05f , -0.5f ), // bottom inner contourn

                float3(2.25f, size - 0.05f ,-0.6f), // bottom size

                float3(0, 0 , -0.7f), // base

            };
            var result = Manifold<T>.Revolution(30, 30, t => Tools.EvalBezier(contourn, t), float3(0, 0, 1));
            return result;
        }
    }

}
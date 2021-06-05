using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class GlassOfWater<T> where T : struct, ICoordinatesVertex<T>
    {
        public static Mesh<T> Mesh(float size = 0.6f)
        {
            float3[] contourn =
            {   
                float3(0f, 0f , -1.35f),
                float3(size, size , -1.4f), //circe base
                float3(size, size , -1.4f), //circe base
                float3(size, size , -1.4f), //circe base
                float3(size, size , -1.4f), //circe base
                float3(size, size , -1.4f), //circe base

                float3(size + 0.2f, size + 0.02f , -0.1f ), // bottom contourn

                float3(size - 0.2f, size -0.2f, -0.6f ), // inflexion contourn
                
                float3(size + 0.7f, size + 0.7f, 0f ), // top contourn

                float3(size, size ,  0.5f), 
                float3(size, size ,  0.9f), // height
                float3(size - 0.08f, size - 0.08f ,  0.5f), 

                float3(size + 0.6f, size + 0.6f, 0f ), // top contourn

                float3(size - 0.1f, size -0.1f, -0.6f ), // inflexion contourn

                float3(size + 0.1f, size + 0.01f , -0.1f ), // bottom contourn

                float3(size, size , -1.25f), //inter circe base
                float3(size, size , -1.25f), //inter circe base
                float3(size, size , -1.25f), //inter circe base
                float3(size, size , -1.25f), //inter circe base
                float3(size, size , -1.25f), //inter circe base
                float3(0f, 0f , -1.2f), 
               
            };
            var result = Manifold<T>.Revolution(30, 30, t => Tools.EvalBezier(contourn, t), float3(0, 0, 1));
            return result;
        }
    }

}
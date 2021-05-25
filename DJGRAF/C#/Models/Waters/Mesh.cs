using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class Waters<T> where T : struct, ICoordinatesVertex<T>
    {
        public static Mesh<T> Mesh(float size = 0.1f)
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
                float3(1.2f, size , 1.1f),
                float3(1.21f, size , 1.1f),
                float3(2f, size , 1.05f),
                float3(2.4f, size , 1.02f),
                //float3(2.3f, size , 1.03f),
                //float3(1f, size , 1.5f),
                //float3(0.9f, size , 1f),
            };
            float3 axis = float3(0, 0, 1);
            Func<float3, float, float3> rov = (v, t) => mul(float4(v, 1), Transforms.Rotate(t * 2 * pi, axis)).xyz;
            Func<float, float, float3> gen = (u, v) => rov(Tools.EvalBezier(contourn, u), v);
            float xplane = 0.85f;
            Func<float, float, float3> cut = (u, v) => {
                float3 p = gen(u,v);
                float x = p.x, y = p.y, z = p.z;

                if (x < xplane) {
                    x = xplane;
                    
                    if (y > 0.7) y = 0.7f;
                    if (y < -0.7) y = -0.7f;
                }
                if (z > 0.8f && x < 1) {
                    x = 1;
                    if (z > 1.05 )z = 1.05f;
                    if (y > 0.5) y = 0.5f;
                    if (y < -0.5) y = -0.5f;
                }
                if (z > 1) { 
                    
                    if (y > 0.1) y = 0.1f;
                    if (y < -0.3) y = -0.3f;
                }
                //if (y < 0.5f) y = 0.5f;
                //if (y > -0.5f) y = -0.5f;

                return float3(x, y, z);
            };
            var result = Manifold<T>.Surface(30, 30, cut);
            return result;
        }
    }

}
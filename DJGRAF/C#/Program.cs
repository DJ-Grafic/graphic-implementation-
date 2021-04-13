using System;
using Rendering;
using GMath;
using System.Linq;
using static DJGraphic.Tools;
using static GMath.Gfx;

namespace DJGraphic
{
    class Program
    {
        static void Main(string[] args)
        {
            var render = Mesh();

            render.RenderTarget.Save("test.rbm");
            Console.WriteLine("Done.");
        }
        
        static Raster CloudPointer()
        {
            Raster render = new Raster(1024 + 512 , 1024);
            
            CloudPointerSet.Init(render);
            return render;
        }

        static Raster<MyVertex, MyProjectedVertex> Mesh() 
        {
            Raster<MyVertex, MyProjectedVertex> render = new Raster<MyVertex, MyProjectedVertex>(1024 + 512 , 1024);
            MeshSet.Init(render);
            return render;
        }
        static void A(Raster<MyVertex, MyProjectedVertex> render)
        {
             render.ClearRT(float4(0, 0, 0.2f, 1)); // clear with color dark blue.

            var primitive = CreateModel();

            /// Convert to a wireframe to render. Right now only lines can be rasterized.
            primitive = primitive.ConvertTo(Topology.Lines);
            primitive = primitive.Transform(Transforms.Scale(2f,1.5f,3.5f));


            #region viewing and projecting

            float4x4 viewMatrix = Transforms.LookAtLH(float3(0, -10f, 2.3f), float3(0, 0, 0), float3(0, 1, 0));
            float4x4 projectionMatrix = Transforms.PerspectiveFovLH(pi_over_4, render.RenderTarget.Height / (float)render.RenderTarget.Width, 0.01f, 20);

            // Define a vertex shader that projects a vertex into the NDC.
            render.VertexShader = v =>
            {
                float4 hPosition = float4(v.Position, 1);
                hPosition = mul(hPosition, viewMatrix);
                hPosition = mul(hPosition, projectionMatrix);
                return new MyProjectedVertex { Homogeneous = hPosition };
            };

            // Define a pixel shader that colors using a constant value
            render.PixelShader = p =>
            {
                return float4(p.Homogeneous.x / 1024.0f, p.Homogeneous.y / 512.0f, 1, 1);
            };

            #endregion

            // Draw the mesh.
            render.DrawMesh(primitive);
        }
        static Mesh<MyVertex> CreateModel()
        {
            float3[] contourn =
            {
                float3(0, 0 , -1f),
                float3(1.5f, 0.5f ,-0.7f),
                float3(1f, 0.5f , -0.65f ),
                float3(1f, 0.5f , -0.6f ),
                float3(0f, 0.5f , -0.2f),
                float3(1f, 0.5f , 0.2f),

            };
            // Parametric representation of a sphere.
            return Manifold<MyVertex>.Surface(30, 30, (u, v) =>
            {
                float alpha = u * 2 * pi;
                Func<float,float3> eval = t => EvalBezier(contourn, t);
                return float3(cos(alpha), sin(alpha), eval(v).z);
            });

            // Generative model
            //return Manifold<MyVertex>.Generative(30, 30,
            //    // g function
            //    u => float3(cos(2 * pi * u), 0, sin(2 * pi * u)),
            //    // f function
            //    (p, v) => p + float3(cos(v * pi), 2*v-1, 0)
            //);

            // Revolution Sample with Bezier
            return Manifold<MyVertex>.Revolution(30, 30, t => EvalBezier(contourn, t), float3(0, 1, 0));
        }
    }
}

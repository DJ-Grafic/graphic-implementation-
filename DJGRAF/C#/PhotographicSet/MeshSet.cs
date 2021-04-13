using Rendering;
using GMath;
using static DJGraphic.Tools;
using static GMath.Gfx;

namespace DJGraphic
{   
    static class MeshSet
    {
        public static void Init(Raster<MyVertex, MyProjectedVertex> render)
        {
            render.ClearRT(float4(0, 0, 0.2f, 1));
            var glass1 = new GlassOfWater().GetMesh();
            var glass2 = new GlassOfWater().GetMesh();
            var jar = new JarOfWater().GetMesh();
            var clown = new Clown().GetMesh();
            
            glass1 = glass1.Transform(Transforms.Translate(-2.5f,2.5f,1f));
            jar = jar.Transform(Transforms.Translate(-6.3f,0,4.3f));

            glass2 = glass2.Transform(Transforms.Translate(4.2f,6.4f,1.3f));
            clown = clown.Transform(Transforms.Translate(0.5f,3.8f,-5.5f));


            float4x4 viewMatrix = Transforms.LookAtLH(float3(0, -10f, 2.3f), float3(0, 0, 0), float3(0, 0, 1));
            float4x4 projectionMatrix = Transforms.PerspectiveFovLH(
						pi_over_4, render.RenderTarget.Height / (float)render.RenderTarget.Width, 0.01f, 40);

			  		 
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
                return float4(p.Homogeneous.x / (1024.0f + 512.0f), p.Homogeneous.y / 1024.0f, 1, 1);
            };
            // Draw the mesh.
            render.DrawMesh(glass1);
            render.DrawMesh(glass2);
            render.DrawMesh(jar);
            render.DrawMesh(clown);
        }

    }
    
}
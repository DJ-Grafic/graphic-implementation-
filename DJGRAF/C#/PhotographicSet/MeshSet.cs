using Rendering;
using GMath;
using static DJGraphic.Tools;
using static GMath.Gfx;

namespace DJGraphic
{   
    static class MeshSet<V, P> where V : struct , ICoordinatesVertex<V> where P : struct, IProjectedVertex<P>
    {
        public static void Init(Raster<V, P> render, float4x4 viewMatrix, float4x4 projectionMatrix)
        {
            render.ClearRT(float4(0, 0, 0.2f, 1));
            
            var glass1 = GlassOfWater<V>.Mesh(0.5f);
            glass1 = glass1.ConvertTo(Topology.Lines);
            glass1 = glass1.Transform(GlassOfWater<V>.Transform1());
            
            var glass2 = GlassOfWater<V>.Mesh(0.5f);
            glass2 = glass2.ConvertTo(Topology.Lines);
            glass2 = glass2.Transform(GlassOfWater<V>.Transform2());

            var jar = JarOfWater<V>.Mesh();
            jar = jar.ConvertTo(Topology.Lines);
            jar = jar.Transform(JarOfWater<V>.Transform());
            
            var jarb = JarOfWater<V>.MeshBack();
            jarb = jarb.ConvertTo(Topology.Lines);
            jarb = jarb.Transform(JarOfWater<V>.Transform());

            var flow = Waters<V>.MeshFlow();
            flow = flow.ConvertTo(Topology.Lines);
            flow = flow.Transform(Waters<V>.TransformFlow());
            
            var water = Waters<V>.MeshInGlass();
            water = water.ConvertTo(Topology.Lines);
			water = water.Transform(Waters<V>.TransformInGlass1());

            var water2 = Waters<V>.MeshInGlass(false);
            water2 = water2.ConvertTo(Topology.Lines);
			water2 = water2.Transform(Waters<V>.TransformInGlass2());

            var clown = Clown<V>.Mesh();
            clown = clown.ConvertTo(Topology.Lines);
			clown = clown.Transform(Clown<V>.Transform());

            var test = Waters<V>.MeshTest();
            test = test.ConvertTo(Topology.Lines);
            test = test.Transform(Waters<V>.TransforT());


			// Define a vertex shader that projects a vertex into the NDC.
            render.VertexShader = v =>
            {
                float4 hPosition = float4(v.Position, 1);
                hPosition = mul(hPosition, viewMatrix);
                hPosition = mul(hPosition, projectionMatrix);
                return new P { Homogeneous = hPosition };
            };

            // Define a pixel shader that colors using a constant value
            render.PixelShader = p =>
            {
                return float4(p.Homogeneous.x / (1024.0f + 512.0f), p.Homogeneous.y / 1024.0f, 1, 1);
            };
            // Draw the mesh.

            //render.DrawMesh(test);

            //render.DrawMesh(glass1);
            //render.DrawMesh(water);

            //render.DrawMesh(glass2);
            //render.DrawMesh(water2);
            //render.DrawMesh(clown);


            render.DrawMesh(jar);
            //render.DrawMesh(jarb);
            //render.DrawMesh(flow);


        }

    }
    
}
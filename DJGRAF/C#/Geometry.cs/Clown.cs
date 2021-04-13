using GMath;
using Rendering;
using static GMath.Gfx;

namespace DJGraphic
{
    class Clown : ObjGeometry
    {
        protected override float4x4[] transforms() => new float4x4[]{
            Transforms.Scale(27,12,0.2f),
            Transforms.RotateZGrad(-20)
        };
        protected override CloudPoints CloudPoints()
		{
			CloudPoints clown = new CloudPointsGenerator<Plane>(100000)
								.Between(Plane.ZLimit(0,0.2f))
								.ToCloudPoints();

			return clown;
		}

        protected override Mesh<MyVertex> Mesh()
        {
            float3[] contourn =
            {
                float3(0.6f, 0.6f , -1.4f),
                float3(0.65f, 0.6f , -1.2f),
                float3(0.7f, 0.6f , -1.1f),
                float3(0.65f, 0.6f , -0.9f),
                float3(0.6f, 0.6f , -0.6f ),
                float3(0.9f, 0.6f , -0.4f),
                float3(0.8f, 0.6f ,  0.2f),

            };
            var result = Manifold<MyVertex>.Generative(30, 30, 
                t => Tools.EvalBezier(contourn, t), 
                (v, t) => float3(v.x,v.y,0)
            );
            result =  Manifold<MyVertex>.Surface(30,30,
                (u, v) => float3(u,v,0)
            );
            result = result.ConvertTo(Topology.Lines);
            return result;
        }

    }
}
using GMath;
using Rendering;
using static GMath.Gfx;

namespace DJGraphic
{
    class GlassOfWater : ObjGeometry
    {
        protected override float4x4[] transforms() => new float4x4[]{
            Transforms.Scale(1.68f,1.68f,4.2f)
        };
        protected override CloudPoints CloudPoints()
		{
			CloudPoints ellipsoid = new CloudPointsGenerator<Ellipsoid>(100000)
									.WidthBoxing(x:0.96f, y:0.96f,z:2)
									.Between(Plane.ZLimit(-0.8f,0.2f))
									.ToCloudPoints(); 

			CloudPoints cylinder = new CloudPointsGenerator<Cylinder>(100000)
								.WidthBoxing(x:0.89f, y:0.89f, z:2)
								.Between(Plane.ZLimit(-1.4f,-0.8f))
								.ToCloudPoints();


			return ellipsoid + cylinder;
		}

        protected override Mesh<MyVertex> Mesh()
        {
            float3[] contourn =
            {   
                float3(0f, 0f , -1.3f),
                float3(0.1f, 0.1f , -1.35f),
                float3(0.2f, 0.2f , -1.4f),
                float3(0.6f, 0.6f , -1.4f),
                float3(0.6f, 0.6f , -1.4f),
                float3(0.6f, 0.6f , -1.4f),
                float3(0.6f, 0.6f , -1.4f),
                float3(0.6f, 0.6f , -1.4f),
                float3(0.65f, 0.6f , -1.2f),
                float3(0.7f, 0.6f , -1.1f),
                float3(0.65f, 0.6f , -0.9f),
                float3(0.6f, 0.6f , -0.6f ),
                float3(0.9f, 0.6f , -0.4f),
                float3(0.8f, 0.6f ,  0.2f),

            };
            var result = Manifold<MyVertex>.Revolution(30, 30, t => Tools.EvalBezier(contourn, t), float3(0, 0, 1));
            result = result.ConvertTo(Topology.Lines);
            return result;
        }

    }
}
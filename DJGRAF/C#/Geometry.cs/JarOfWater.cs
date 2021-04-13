using GMath;
using Rendering;
using static GMath.Gfx;

namespace DJGraphic
{
    class JarOfWater : ObjGeometry
    {
        protected override float4x4[] transforms() => new float4x4[]{
            Transforms.Scale(2f,1.5f,3.5f),
            Transforms.RotateYGrad(70),
            Transforms.RotateZGrad(-40),
        };
        protected override CloudPoints CloudPoints()
		{
			CloudPoints hyperb1 = new CloudPointsGenerator<Hyperboloid>(100000)
									  .Between( Plane.XLimit(0,2) )
									  .ToCloudPoints();
			
			return hyperb1;
		}
        public CloudPoints GetCloudPoints()
        {
            var hyperb1 = (this as ObjGeometry).GetCloudPoints();
            CloudPoints hyperb2 = new CloudPointsGenerator<Hyperboloid>(100000)
									  .Between( Plane.XLimit(0,2) )
									  .ToCloudPoints();


			hyperb2 = Tools.ApplyTransform( ~( hyperb2 ), Transforms.Scale(2f,1.5f,3.5f));
			hyperb2 = Tools.ApplyTransform( ~( hyperb2 ), Transforms.Translate(-0.2f,0f,-2.8f));
			hyperb2 = Tools.ApplyTransform( ~( hyperb2 ), Transforms.RotateYGrad(43));
			hyperb2 = Tools.ApplyTransform( ~( hyperb2 ), Transforms.RotateZGrad(-30));

            return hyperb1 + hyperb2;
        }

        protected override Mesh<MyVertex> Mesh()
        {
            float3[] contourn =
            {
                float3(0, 0 , -0.8f),
                float3(1.8f, 0.5f ,-0.6f),
                float3(1.3f, 0.5f , -0.5f ),
                float3(1.3f, 0.5f , -0.4f ),
                float3(0.5f, 0.5f , 0.5f),
                float3(1f, 0.5f , 0.7f),
                float3(1.5f, 0.5f , 1f),


            };
            var result = Manifold<MyVertex>.Revolution(30, 30, t => Tools.EvalBezier(contourn, t), float3(0, 0, 1));
            result = result.ConvertTo(Topology.Lines);
            return result;
        }

    }
}
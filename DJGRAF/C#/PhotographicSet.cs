using Rendering;
using GMath;
using static DJGraphic.Tools;
using static GMath.Gfx;

namespace DJGraphic
{
    static class PhotographicSet
    {
        public static void Init(Raster render)
        {
            render.ClearRT(float4(0, 0, 0.2f, 1));
			var glass1 = GlassOfWater();
			var glass2 = glass1.Clone();
			var jar = JarOfWater();
			var clown = Clown();

			glass1 = ApplyTransform(~glass1, Transforms.Translate(-2.5f,2.5f,1f));
			jar = ApplyTransform(~jar, Transforms.Translate(-6.3f,0,4.3f));

			glass2 = ApplyTransform(~glass2, Transforms.Translate(4.2f,6.4f,1.3f));
			clown = ApplyTransform(~clown, Transforms.Translate(0.5f,3.8f,-5.5f));

			
			float3[] points = ~( clown + jar + glass1 + glass2 );
			
			Draw(render, points);
        }
		private static CloudPoints Clown()
		{
			CloudPoints clown = new CloudPointsGenerator<Plane>(100000)
								.Between(Plane.ZLimit(0,0.2f))
								.ToCloudPoints();
			
			clown = ApplyTransform( ~( clown ), Transforms.Scale(27,12,0.2f));
			clown = ApplyTransform( ~( clown ), Transforms.RotateZGrad(-20));
			//clown = ApplyTransform( ~( clown ), Transforms.RotateXGrad(-5));

			return clown;
		}

		private static CloudPoints JarOfWater()
		{
			CloudPoints hyperb1 = new CloudPointsGenerator<Hyperboloid>(100000)
									  .Between( Plane.XLimit(0,2) )
									  .ToCloudPoints();
			CloudPoints hyperb2 = new CloudPointsGenerator<Hyperboloid>(100000)
									  .Between( Plane.XLimit(0,2) )
									  .ToCloudPoints();
									  
			hyperb1 = ApplyTransform( ~( hyperb1 ), Transforms.Scale(2f,1.5f,3.5f));
			hyperb1 = ApplyTransform( ~( hyperb1 ), Transforms.RotateYGrad(70));
			hyperb1 = ApplyTransform( ~( hyperb1 ), Transforms.RotateZGrad(-40));

			hyperb2 = ApplyTransform( ~( hyperb2 ), Transforms.Scale(2f,1.5f,3.5f));
			hyperb2 = ApplyTransform( ~( hyperb2 ), Transforms.Translate(-0.2f,0f,-2.8f));
			hyperb2 = ApplyTransform( ~( hyperb2 ), Transforms.RotateYGrad(43));
			hyperb2 = ApplyTransform( ~( hyperb2 ), Transforms.RotateZGrad(-30));

			return hyperb1 + hyperb2;
		}
		private static CloudPoints GlassOfWater()
		{
			CloudPoints ellipsoid = new CloudPointsGenerator<Ellipsoid>(100000)
									.WidthBoxing(x:0.96f, y:0.96f,z:2)
									.Between(Plane.ZLimit(-0.8f,0.2f))
									.ToCloudPoints(); 

			CloudPoints cylinder = new CloudPointsGenerator<Cylinder>(100000)
								.WidthBoxing(x:0.89f, y:0.89f, z:2)
								.Between(Plane.ZLimit(-1.4f,-0.8f))
								.ToCloudPoints();


			CloudPoints result = ApplyTransform( ~(ellipsoid + cylinder), 
										Transforms.Scale(1.68f,1.68f,4.2f));
			return result;
		}

		private static void Draw(Raster render,float3[] points)
		{
      		points = ApplyTransform(points, 
			  		 Transforms.LookAtLH(float3(0, -10f, 2.3f), float3(0, 0, 0), float3(0, 0, 1)));
      		points = ApplyTransform(points, 
			  		 Transforms.PerspectiveFovLH(
						pi_over_4, render.RenderTarget.Height / (float)render.RenderTarget.Width, 0.01f, 40));

      		render.DrawPoints(points);
		}
    }
}
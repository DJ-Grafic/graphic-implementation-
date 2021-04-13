using Rendering;
using GMath;
using static DJGraphic.Tools;
using static GMath.Gfx;

namespace DJGraphic
{
    static class CloudPointerSet
    {
        public static void Init(Raster render)
        {
            render.ClearRT(float4(0, 0, 0.2f, 1));
			var points = new Clown().GetCloudPoints();

			points = ApplyTransform(~(points), Transforms.Translate(0.5f,3.8f,-5.5f));
			points = ApplyTransform(~(points), 
			  		 Transforms.LookAtLH(float3(0, -10f, 2.3f), float3(0, 0, 0), float3(0, 0, 1)));
      		points = ApplyTransform(~(points), 
			  		 Transforms.PerspectiveFovLH(
						pi_over_4, render.RenderTarget.Height / (float)render.RenderTarget.Width, 0.01f, 40));

      		render.DrawPoints(~(points));
        }

	


    }
}
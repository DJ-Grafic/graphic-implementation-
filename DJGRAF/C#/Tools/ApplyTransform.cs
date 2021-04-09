using Rendering;
using GMath;
using static GMath.Gfx;

namespace DJGraphic
{
    static class Tools
    {
    	public static float3[] ApplyTransform(float3[] points, float4x4 matrix)
    	{
    		float3[] result = new float3[points.Length];

      		// Transform points with a matrix
      		// Linear transform in homogeneous coordinates
      		for (int i = 0; i < points.Length; i++)
      		{
        		float4 h = float4(points[i], 1);
        		h = mul(h, matrix);
        		result[i] = h.xyz / h.w;
      		}

      		return result;
    	}
        
    }
}
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
        public static float3 EvalBezier(float3[] control, float t)
        {
            // DeCasteljau
            if (control.Length == 1)
                return control[0]; // stop condition
            float3[] nestedPoints = new float3[control.Length - 1];
            for (int i = 0; i < nestedPoints.Length; i++)
                nestedPoints[i] = lerp(control[i], control[i + 1], t);
            return EvalBezier(nestedPoints, t);
        }
    }
}
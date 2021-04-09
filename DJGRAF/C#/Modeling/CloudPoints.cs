using System.Drawing;
using System.Linq;
using GMath;

namespace DJGraphic
{
    struct CloudPoints 
    {
        float3[] points;
        public CloudPoints(float3[] point){this.points = point;}
        public static implicit operator CloudPoints(float3[] points) => new CloudPoints(points); 
        public static float3[] operator ~(CloudPoints cp) => (float3[])cp.points.Clone();
        public static CloudPoints operator +(CloudPoints cp1, CloudPoints cp2) => new CloudPoints(cp1.points.Concat(cp2.points).ToArray());
        public CloudPoints Clone() => new CloudPoints((float3[])this.points.Clone());
    }
}
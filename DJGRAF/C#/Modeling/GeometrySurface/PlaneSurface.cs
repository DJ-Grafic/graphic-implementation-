using GMath;
using static GMath.Gfx;

namespace DJGraphic
{
    partial class Plane : ISurface
    {   
        public float3 GetRandomPoints() => float3( random(), random(), random() );
        public bool Contains(float3 point) => true;
    }
}
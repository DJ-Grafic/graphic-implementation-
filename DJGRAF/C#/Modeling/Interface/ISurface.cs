using GMath;

namespace DJGraphic
{
    interface ISurface
    {
        float3 GetRandomPoints();
        bool Contains(float3 point);
    }
}
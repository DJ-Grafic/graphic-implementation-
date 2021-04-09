using GMath;
using System;

namespace DJGraphic
{
    interface ICloudPointsGenerator
    {
        ICloudPointsGenerator Between (params Predicate<float3>[] pred);
        CloudPoints ToCloudPoints();
    }
}
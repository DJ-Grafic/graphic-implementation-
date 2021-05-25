using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static partial class WoodPlane<T> where T : struct, INormalVertex<T>, ICoordinatesVertex<T>
    {
        public static IRaycastGeometry<T> XY()
        {
            return Raycasting.PlaneXY.AttributesMap(
                a => new T { 
                    Position = a, 
                    Coordinates = float2(a.x*0.2f, a.y*0.2f), 
                    Normal = float3(0, 0, 1) }
            );
        }

        public static IRaycastGeometry<T> XZ()
        {
            return Raycasting.PlaneXZ.AttributesMap(
                a => new T { 
                    Position = a, 
                    Coordinates = float2(a.x*0.2f, a.z*0.2f), 
                    Normal = float3(0, -1, 0) }
            );
        }
    }

}
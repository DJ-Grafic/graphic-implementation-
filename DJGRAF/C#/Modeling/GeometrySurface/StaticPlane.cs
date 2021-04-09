using GMath;
using System;

namespace DJGraphic
{
    partial class Plane 
    {
        public static Predicate<float3> ZLimit(float _base, float top)
        {
            Predicate<float3> result = (p) => p.z < top && p.z > _base;
            return result;
        }
        public static Predicate<float3> Yimit(float _base, float top)
        {
            Predicate<float3> result = (p) => p.y < top && p.y > _base;
            return result;
        }
        public static Predicate<float3> XLimit(float _base, float top)
        {
            Predicate<float3> result = (p) => p.x < top && p.x > _base;
            return result;
        }
    }
}
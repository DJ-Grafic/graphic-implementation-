using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    struct MyProjectedVertex : IProjectedVertex<MyProjectedVertex>
    {
        public float4 Homogeneous { get; set; }

        public MyProjectedVertex Add(MyProjectedVertex other)
        {
            return new MyProjectedVertex
            {
                Homogeneous = this.Homogeneous + other.Homogeneous
            };
        }

        public MyProjectedVertex Mul(float s)
        {
            return new MyProjectedVertex
            {
                Homogeneous = this.Homogeneous * s
            };
        }
    }

}
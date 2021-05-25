using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    struct MyVertex : IVertex<MyVertex>
    {
        public float3 Position { get; set; }
		public MyVertex Add(MyVertex other)
        {
            return new MyVertex
            {
                    Position = this.Position + other.Position,
            };
        }

        public MyVertex Mul(float s)
        {
            return new MyVertex
            {
                    Position = this.Position * s,
            };
        }
    }

}
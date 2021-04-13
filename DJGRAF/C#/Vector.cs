using System;
using Rendering;
using GMath;
using System.Linq;

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
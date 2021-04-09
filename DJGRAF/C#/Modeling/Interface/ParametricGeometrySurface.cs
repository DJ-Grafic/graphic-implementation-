using GMath;
using System;
using GMath;
using static GMath.Gfx;


namespace DJGraphic
{
    abstract class ParametricGeometrySurface : ISurface
    {
        protected abstract float p1_domain_transform(float r1);
        protected abstract float p2_domain_transform(float r2);
        protected abstract float XParametricEquation(float p1, float p2);
        protected abstract float YParametricEquation(float p1, float p2);
        protected abstract float ZParametricEquation(float p1, float p2);
        public abstract bool Contains(float3 p);

        public float3 GetRandomPoints() 
        {
            float p1 = this.p1_domain_transform( random() );
            float p2 = this.p2_domain_transform( random() );
            return Gfx.float3(
                XParametricEquation( p1,p2 ),
                YParametricEquation( p1,p2 ),
                ZParametricEquation( p1,p2 )
            );
        }
    }
}
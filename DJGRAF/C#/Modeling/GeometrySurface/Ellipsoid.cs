using GMath;

namespace DJGraphic
{
    class Ellipsoid : ParametricGeometrySurface
    {   
        //public float3 GetRandomPoints()
        //{
        //    GRandom r = new GRandom();
        //    float u = Gfx.two_pi * r.random();
        //    float v = Gfx.pi *  r.random();
        //    return Gfx.float3(
        //        XParametricEquation(u,v),
        //        YParametricEquation(u,v),
        //        ZParametricEquation(u,v)
        //    );
        //}
        protected override float p1_domain_transform(float r1) => Gfx.two_pi * r1;
        protected override float p2_domain_transform(float r2) => Gfx.pi * r2;
        protected override float XParametricEquation(float p1, float p2) => Gfx.cos(p1) * Gfx.sin(p2);
        protected override float YParametricEquation(float p1, float p2) => Gfx.sin(p1) * Gfx.sin(p2);
        protected override float ZParametricEquation(float p1, float p2) => Gfx.cos(p2);
        public override bool Contains(float3 point) => true;
    }
}  
using GMath;

namespace DJGraphic
{
    class Cylinder : ParametricGeometrySurface
    {   
        //public float3 GetRandomPoints()
        //{
        //    GRandom r = new GRandom();
        //    float theta = Gfx.two_pi * r.random();
        //    float t = r.random(-1f,1f);
        //    return Gfx.float3(
        //        XParametricEquation(theta,t),
        //        YParametricEquation(theta,t),
        //        ZParametricEquation(theta,t)
        //    );
        //}
        protected override float p1_domain_transform(float r1) => Gfx.two_pi * r1;
        protected override float p2_domain_transform(float r2) => r2 * 2 - 1; // r2 * (1 - (-1)) + (-1)
        protected override float XParametricEquation(float p1, float p2) => Gfx.cos(p1);
        protected override float YParametricEquation(float p1, float p2) => Gfx.sin(p1);
        protected override float ZParametricEquation(float p1, float p2) => p2;
        public override bool Contains(float3 point) => true;
    }
}
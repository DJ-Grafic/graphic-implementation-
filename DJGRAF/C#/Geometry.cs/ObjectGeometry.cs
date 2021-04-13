using GMath;
using Rendering;

namespace DJGraphic
{
    abstract class ObjGeometry 
    {
        protected abstract float4x4[] transforms();
        protected abstract CloudPoints CloudPoints();
        
        public CloudPoints GetCloudPoints()
        {
            var result = this.CloudPoints();
            foreach (var item in this.transforms())
                result = Tools.ApplyTransform(~( result ), item);
            
            return result;
        }
        protected abstract Mesh<MyVertex> Mesh();
        public virtual Mesh<MyVertex> GetMesh()
        {
            var result = this.Mesh();
            foreach (var item in this.transforms())
                result = result.Transform(item);
            
            return result;
        }


    }
}
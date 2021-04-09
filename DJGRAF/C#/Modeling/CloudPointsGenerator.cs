using System;
using System.Collections.Generic;
using GMath;

namespace DJGraphic
{
    class CloudPointsGenerator<T> : ICloudPointsGenerator
    where T : ISurface, new()
    {
        ISurface surface;
        int accountPoints;
        float3 widthBoxing;
        List<Predicate<float3>> predList;        

        public CloudPointsGenerator(int N)
        {
            this.accountPoints = N;
            this.surface = new T();
            this.widthBoxing = Gfx.float3(1,1,1);
            this.predList = new List<Predicate<float3>>();
        }

        public ICloudPointsGenerator Between (params Predicate<float3>[] pred)
        {
            foreach (var item in pred)
                this.predList.Add(item);
            return this;
        }
        public ICloudPointsGenerator WidthBoxing(float x = 1, float y = 1, float z = 1)
        {
            this.widthBoxing = Gfx.float3(x,y,z);
            return this;
        }
        public CloudPoints ToCloudPoints()
        {
            float3[] result = new float3[this.accountPoints];
            for (int i = 0; i < this.accountPoints; i++)
            {
                var temp = this.widthBoxing * this.surface.GetRandomPoints();
                if( !this.PredEval(temp) ){ i--; continue; }
                result[i] = temp;
            }
            return new CloudPoints(result);
        }
        private bool PredEval(float3 point)
        {
            bool result = true;
            foreach (var pred in this.predList)
                result &= pred(point);

            return result;
        }
    }
}
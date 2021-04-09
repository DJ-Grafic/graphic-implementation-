using System;
using Rendering;
using GMath;
using System.Linq;

namespace DJGraphic
{
    class Program
    {
        static void Main(string[] args)
        {
            Raster render = new Raster(1024 + 512 , 1024);
            
            PhotographicSet.Init(render);

            render.RenderTarget.Save("test.rbm");
            Console.WriteLine("Done.");
        }
    }
}

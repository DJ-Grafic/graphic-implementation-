using System;
using Rendering;
using GMath;
using System.Linq;
using static DJGraphic.Tools;
using static GMath.Gfx;

namespace DJGraphic
{
    class Program
    {
        static Scene<PositionNormalCoordinate, Material> Set()
        {
            Scene<PositionNormalCoordinate, Material> scene = 
                new Scene<PositionNormalCoordinate, Material>();

            Texture2D planeTexture = Texture2D.LoadFromFile("../teachingImplentation/apkwood.jpg");
            scene.Add(WoodPlane<PositionNormalCoordinate>.XY(), ModelMaterial.Texture(planeTexture),
                Transforms.Translate(0,0,-1.4f));
            
            scene.Add(WoodPlane<PositionNormalCoordinate>.XZ(), ModelMaterial.Texture(planeTexture),
                Transforms.Translate(0,7,0));

            var glass1 = GlassOfWater<PositionNormalCoordinate>.Mesh(0.5f).Weld();
            glass1.ComputeNormals();
            scene.Add(
                glass1.AsRaycast(RaycastingMeshMode.Grid), 
                ModelMaterial.Glass, 
                GlassOfWater<PositionNormalCoordinate>.Transform1()
            );
            
            var glass2 = GlassOfWater<PositionNormalCoordinate>.Mesh(0.5f).Weld();
            glass2.ComputeNormals();
            scene.Add(
                glass2.AsRaycast(RaycastingMeshMode.Grid), 
                ModelMaterial.Glass, 
                GlassOfWater<PositionNormalCoordinate>.Transform2()
            );

            var jar = JarOfWater<PositionNormalCoordinate>.Mesh().Weld();
            jar.ComputeNormals();
            scene.Add(
                jar.AsRaycast(RaycastingMeshMode.Grid), 
                ModelMaterial.Glass, 
                JarOfWater<PositionNormalCoordinate>.Transform()
            );

            //var water = Waters<PositionNormalCoordinate>.Mesh();
			//water = water.Transform(Waters<PositionNormalCoordinate>.Transform());

            //var water1 = Waters<PositionNormalCoordinate>.MeshGlass(0.5f);
			//water1 = water1.Transform(Waters<PositionNormalCoordinate>.Transform1());

            //var water2 = Waters<PositionNormalCoordinate>.MeshGlass(0.5f);
			//water2 = water2.Transform(Waters<PositionNormalCoordinate>.Transform2());

            //var clown = Clown<PositionNormalCoordinate>.Mesh();
			//clown = clown.Transform(Clown<PositionNormalCoordinate>.Transform());

            return scene;
        }

        static float3 CameraPosition = float3(0, -4.5f, 1.1f);
        static float3 LightPosition = float3(3, -2, 5);
        static float3 LightIntensity = float3(1, 1, 1) * 200;

        static void Main(string[] args)
        {
            int Height = 1024;
            int Width  = 1024 + 512;

            float4x4 viewMatrix = Transforms.LookAtLH(CameraPosition , float3(0, 0, 0), float3(0, 0, 1));
            float4x4 projectionMatrix = Transforms.PerspectiveFovLH(
						pi_over_4, Height / (float)Width, 0.01f, 40);
    
            
            //Mesh(viewMatrix, projectionMatrix, Height, Width);
            //RaycastingMesh(viewMatrix, projectionMatrix, Height, Width);
            PathtracingMesh(viewMatrix, projectionMatrix, Height, Width);
        }
        

        static void Mesh(float4x4 viewMatrix, float4x4 projectionMatrix, int Height, int Width ) 
        {
            Raster<PositionNormalCoordinate, MyProjectedVertex> render = new Raster<PositionNormalCoordinate, MyProjectedVertex>(Width, Height);
            MeshSet<PositionNormalCoordinate, MyProjectedVertex>.Init(render, viewMatrix, projectionMatrix);
            
            render.RenderTarget.Save("test1.rbm");
            Console.WriteLine("Done.");

        }
        static void RaycastingMesh(float4x4 viewMatrix, float4x4 projectionMatrix, int Height, int Width )
        {
            Texture2D texture = new Texture2D(Width , Height);
            RaycastingSet.Init(Set(), texture, viewMatrix, projectionMatrix, LightPosition, LightIntensity );
            
            texture.Save("test1.rbm");
            Console.WriteLine("Done.");
        }

        static void PathtracingMesh(float4x4 viewMatrix, float4x4 projectionMatrix, int Height, int Width )
        {
            Texture2D texture = new Texture2D(Width , Height);
            var scene = Set();

            int pass = 0;
            while (true)
            {
                Console.Write("Pass: " + pass);
                PathtracingSet.Init(scene,texture,pass, viewMatrix, projectionMatrix, LightPosition, LightIntensity);
                texture.Save("test.rbm");
                Console.WriteLine();
                pass++;
            }
            
        }

    }
}

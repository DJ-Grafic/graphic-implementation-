﻿using System;
using Rendering;
using GMath;
using System.Linq;
using static DJGraphic.Tools;
using static GMath.Gfx;

namespace DJGraphic
{
    class Program
    {
        static float3 CameraPosition = float3(0, -4.5f, 1.1f);
        static float3[] LightPosition = new float3[] { float3(-1, -1, 4f), float3(-3, -2, 4f), float3(3, -2, 4f) };
        //static float3[] LightPosition = new float3[] {float3(0, -2, 4f) };

        static float3 LightIntensity = float3(1, 1, 1) * 500;

        static void Main(string[] args)
        {
            int Height = 1024;
            int Width  = 1024 + 512;

            float4x4 viewMatrix = Transforms.LookAtLH(CameraPosition , float3(0, 0, 0), float3(0, 0, 1));
            float4x4 projectionMatrix = Transforms.PerspectiveFovLH(
						pi_over_4, Height / (float)Width, 0.01f, 40);
    
            
            //Mesh(viewMatrix, projectionMatrix, Height, Width);
            RaycastingMesh(viewMatrix, projectionMatrix, Height, Width);
            //PathtracingMesh(viewMatrix, projectionMatrix, Height, Width);
        }
        

        static void Mesh(float4x4 viewMatrix, float4x4 projectionMatrix, int Height, int Width ) 
        {
            Raster<PositionNormalCoordinate, MyProjectedVertex> render = new Raster<PositionNormalCoordinate, MyProjectedVertex>(Width, Height);
            MeshSet<PositionNormalCoordinate, MyProjectedVertex>.Init(render, viewMatrix, projectionMatrix);
            
            render.RenderTarget.Save("test.rbm");
            Console.WriteLine("Done.");

        }
        static void RaycastingMesh(float4x4 viewMatrix, float4x4 projectionMatrix, int Height, int Width )
        {
            Texture2D texture = new Texture2D(Width , Height);
            Scene<PositionNormalCoordinate, Material> scene = 
                new Scene<PositionNormalCoordinate, Material>();

            RaycastingSet.Scene(scene);
            RaycastingConfig.Init(scene, texture, viewMatrix, projectionMatrix, LightPosition, LightIntensity );
            
            texture.Save("test.rbm");
            Console.WriteLine("Done.");
        }

        static void PathtracingMesh(float4x4 viewMatrix, float4x4 projectionMatrix, int Height, int Width )
        {
            Texture2D texture = new Texture2D(Width , Height);
           
            var patht = new Pathtracing() { 
                LightPosition = LightPosition[0],
                LightIntensity = LightIntensity,
                viewMatrix = viewMatrix, 
                projectionMatrix = projectionMatrix, 
                Bounces = 8,

            };

            patht.Scene();  

            int pass = 0;
            while (true)
            {
                Console.WriteLine("Pass: " + pass);
                patht.Trace(texture, pass );   
                texture.Save("test.rbm");
                pass++;
            }
            
        }

    }
}

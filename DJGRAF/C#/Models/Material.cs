using System;
using Rendering;
using GMath;
using System.Linq;
using static GMath.Gfx;

namespace DJGraphic
{
    static class ModelMaterial 
    {
        public static Material Glass = new Material
        {
            Specular = float3(0.95f, 0.97f, 1),
            SpecularPower = 260,

            WeightDiffuse = 0,
            WeightFresnel = 1.0f, // Glass sphere
            RefractionIndex = 1.6f
        };

        public static Material LightSource(float3 LightIntensity) => new Material
        {
            Emissive = LightIntensity / (4 * pi), // power per unit area
            WeightDiffuse = 0,
            WeightFresnel = 1.0f, // Glass sphere
            RefractionIndex = 1.0f
        };

        public static Material Texture(Texture2D planeTexture) => new Material 
        { 
            DiffuseMap = planeTexture, 
            Diffuse = float3(1, 1, 1), 
            TextureSampler = new Sampler { Wrap = WrapMode.Repeat, MinMagFilter = Filter.Linear }
        };

        public static Material Mirror = new Material
        {
                Specular = float3(1, 1, 1),
                SpecularPower = 260,

                //WeightDiffuse = 0.1f,
                WeightMirror = 1f, // Mirror sphere
                //WeightFresnel = 0.4f,
                //RefractionIndex = 1f,
        };

        public static Material Diffuse = new Material
        {
            Specular = float3(1, 1, 1)*0.1f,
            SpecularPower = 60,
            Diffuse = float3(1, 1, 1)
        };

        public static Material Water = new Material
        {
            //Specular = float3(1, 1, 1)*0.1f,
            //SpecularPower = 60,
            //Diffuse = float3(0.7f, 0.9f, 1f),
            Diffuse = float3(1, 1, 1),
            Specular = float3(1, 1, 1),
            SpecularPower = 260,

            WeightDiffuse = 0.1f,
            WeightFresnel = 1.0f, // Glass sphere
            RefractionIndex = 1.3f
        };

        public static Material Bubble = new Material
        {
            //Specular = float3(1, 1, 1)*0.1f,
            //SpecularPower = 60,
            //Diffuse = float3(0.7f, 0.9f, 1f),

            Emissive = 0.5f, // power per unit area
            Diffuse = float3(1, 1, 1),
            Specular = float3(1, 1, 1),
            SpecularPower = 260,

            //WeightDiffuse = 0.3f,
            WeightFresnel = 1f, // Glass sphere
            RefractionIndex = 1.3f
        };


    }

}
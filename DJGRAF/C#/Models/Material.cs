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
            Specular = float3(1, 1, 1),
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

                WeightDiffuse = 0,
                WeightMirror = 1.0f, // Mirror sphere
        };

        public static Material Diffuse = new Material
        {
            Specular = float3(1, 1, 1)*0.1f,
            SpecularPower = 60,
            Diffuse = float3(1, 1, 1)
        };


    }

}
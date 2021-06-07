using Rendering;
using GMath;
using System;
using static DJGraphic.Tools;
using static GMath.Gfx;

namespace DJGraphic
{   
    public partial class Pathtracing
    {
        private Scene<PositionNormalCoordinate, Material> scene;
        private Raytracer<PTRayPayload, PositionNormalCoordinate, Material> raycaster;

        public float3 LightPosition;
        public float3 LightIntensity;
        public float4x4 viewMatrix; 
        public float4x4 projectionMatrix; 
        public int Bounces;

        public void Trace(Texture2D texture, int pass)
        {
            raycaster = new Raytracer<PTRayPayload, PositionNormalCoordinate, Material>();
            
            RayTracerOnClosesHit();
            raycaster.OnMiss += delegate (IRaycastContext context, ref PTRayPayload payload)
            {
                payload.Color = float3(0, 0, 0); // Blue, as the sky.
            };

            
            /// Render all points of the screen
            for (int px = 0; px < texture.Width; px++)
                for (int py = 0; py < texture.Height; py++)
                {
                    int progress = (px * texture.Height + py);
                    if (progress % 10000 == 0)
                    {
                        Console.Write("\r" + progress * 100 / (float)(texture.Width * texture.Height) + "%            ");
                    }

                    RayDescription ray = RayDescription.FromScreen(px + 0.5f, py + 0.5f, texture.Width, texture.Height, inverse(viewMatrix), inverse(projectionMatrix), 0, 1000);

                    float4 accum = texture.Read(px, py) * pass;
                    PTRayPayload coloring = new PTRayPayload();
                    coloring.Importance = float3(1, 1, 1);
                    coloring.Bounces = Bounces;

                    raycaster.Trace(scene, ray, ref coloring);

                    texture.Write(px, py, float4((accum.xyz + coloring.Color) / (pass + 1), 1));
                }
        }

        public void RayTracerOnClosesHit()
        {
            raycaster.OnClosestHit += delegate (IRaycastContext context, PositionNormalCoordinate attribute, Material material, ref PTRayPayload payload)
            {
                // Move geometry attribute to world space
                attribute = attribute.Transform(context.FromGeometryToWorld);

                float3 V = -normalize(context.GlobalRay.Direction);

                attribute.Normal = normalize(attribute.Normal);

                if (material.BumpMap != null)
                {
                    float3 T, B;
                    createOrthoBasis(attribute.Normal, out T, out B);
                    float3 tangentBump = material.BumpMap.Sample(material.TextureSampler, attribute.Coordinates).xyz * 2 - 1;
                    float3 globalBump = tangentBump.x * T + tangentBump.y * B + tangentBump.z * attribute.Normal;
                    attribute.Normal = globalBump;// normalize(attribute.Normal + globalBump * 5f);
                }

                ScatteredRay outgoing = material.Scatter(attribute, V);

                float lambertFactor = abs(dot(attribute.Normal, outgoing.Direction));

                payload.Color += payload.Importance * material.Emissive;
                
                // Recursive calls for indirect light due to reflections and refractions
                if (payload.Bounces > 0)
                {
                    float3 D = outgoing.Direction; // recursive direction to check
                    float3 facedNormal = dot(D, attribute.Normal) > 0 ? attribute.Normal : -attribute.Normal; // normal respect to direction

                    RayDescription ray = new RayDescription { Direction = D, Origin = attribute.Position + facedNormal * 0.001f, MinT = 0.0001f, MaxT = 10000 };

                    payload.Importance *= outgoing.Ratio * lambertFactor / outgoing.PDF;
                    payload.Bounces--;

                    raycaster.Trace(scene, ray, ref payload);
                }
            };
        }
 

    }
    
}
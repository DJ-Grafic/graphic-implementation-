using Rendering;
using GMath;
using System;
using System.Collections.Generic;
using static DJGraphic.Tools;
using static GMath.Gfx;

namespace DJGraphic
{   
    static class RaycastingConfig
    {
        public static void Init(
            Scene<PositionNormalCoordinate, Material> scene,
            Texture2D texture, 
            float4x4 viewMatrix, float4x4 projectionMatrix, 
            float3[] LightPosition, float3 LightIntensity)
        {
            Raytracer<ShadowRayPayload, PositionNormalCoordinate, Material> shadower = 
                new Raytracer<ShadowRayPayload, PositionNormalCoordinate, Material>();
            ShadowerOnAnyHit(shadower);

            List<Raytracer<RTRayPayload, PositionNormalCoordinate, Material>> raycasters = 
                new List<Raytracer<RTRayPayload, PositionNormalCoordinate, Material>>();
            
            foreach (var light in LightPosition) 
            {
                var raycaster = new Raytracer<RTRayPayload, PositionNormalCoordinate, Material>();
                RayTracerOnClosesHit(raycaster, scene, shadower, light, LightIntensity);
                raycaster.OnMiss += delegate (IRaycastContext context, ref RTRayPayload payload)
                {
                    payload.Color = float3(0, 0, 0); // Blue, as the sky.
                };

                raycasters.Add(raycaster);
            }

            /// Render all points of the screen
            for (int px = 0; px < texture.Width; px++)
                for (int py = 0; py < texture.Height; py++)
                {
                    int progress = (px * texture.Height + py);
                    if (progress % 1000 == 0)
                    {
                        Console.Write("\r" + progress * 100 / (float)(texture.Width * texture.Height) + "%            ");
                    }

                    RayDescription ray = RayDescription.FromScreen(px + 0.5f, py + 0.5f, texture.Width, texture.Height, inverse(viewMatrix), inverse(projectionMatrix), 0, 1000);

                    RTRayPayload coloring = new RTRayPayload();
                    RTRayPayload aux = new RTRayPayload();
                    aux.Bounces = 8;    

                    coloring.Color = float3(0,0,0);
                    foreach (var raycaster in raycasters)
                    {
                        raycaster.Trace(scene, ray, ref aux);
                        coloring.Color += aux.Color;
                        coloring.Color.x = Math.Min(coloring.Color.x, 255);
                        coloring.Color.y = Math.Min(coloring.Color.y, 255);
                        coloring.Color.z = Math.Min(coloring.Color.z, 255);
                    }


                    texture.Write(px, py, float4(coloring.Color, 1));
                }

        }

        public static void ShadowerOnAnyHit(Raytracer<ShadowRayPayload, PositionNormalCoordinate, Material> shadower)
        {
            shadower.OnAnyHit += 
            delegate (IRaycastContext context, PositionNormalCoordinate attribute, Material material, ref ShadowRayPayload payload)
            {
                if (any(material.Emissive))
                    return HitResult.Discard; // Discard light sources during shadow test.

                // If any object is found in ray-path to the light, the ray is shadowed.
                payload.Shadowed = true;
                // No neccessary to continue checking other objects
                return HitResult.Stop;
            };
        }

        public static void RayTracerOnClosesHit(Raytracer<RTRayPayload, PositionNormalCoordinate, Material> raycaster, 
            Scene<PositionNormalCoordinate, Material> scene,
            Raytracer<ShadowRayPayload, PositionNormalCoordinate, Material> shadower,
            float3 LightPosition, float3 LightIntensity)
        {
            raycaster.OnClosestHit += delegate (IRaycastContext context, PositionNormalCoordinate attribute, Material material, ref RTRayPayload payload)
            {
                // Move geometry attribute to world space
                attribute = attribute.Transform(context.FromGeometryToWorld);

                float3 V = -normalize(context.GlobalRay.Direction);

                float3 L = (LightPosition - attribute.Position);
                float d = length(L);
                L /= d; // normalize direction to light reusing distance to light

                attribute.Normal = normalize(attribute.Normal);
                //if (dot(attribute.Normal, V) < 0) attribute.Normal *= -1;

                if (material.BumpMap != null)
                {
                    float3 T, B;
                    createOrthoBasis(attribute.Normal, out T, out B);
                    float3 tangentBump = material.BumpMap.Sample(material.TextureSampler, attribute.Coordinates).xyz * 2 - 1;
                    float3 globalBump = tangentBump.x * T + tangentBump.y * B + tangentBump.z * attribute.Normal;
                    attribute.Normal = globalBump;// normalize(attribute.Normal + globalBump * 5f);
                }

                float lambertFactor = abs(dot(attribute.Normal, L));

                // Check ray to light...
                ShadowRayPayload shadow = new ShadowRayPayload();
                shadower.Trace(scene,
                    RayDescription.FromDir(attribute.Position + attribute.Normal * 0.001f, // Move an epsilon away from the surface to avoid self-shadowing 
                    L), ref shadow);

                float3 Intensity = (shadow.Shadowed ? 0.2f : 1.0f) * LightIntensity / (d * d);

                payload.Color = material.Emissive + material.EvalBRDF(attribute, V, L) * Intensity * lambertFactor; // direct light computation

                // Recursive calls for indirect light due to reflections and refractions
                if (payload.Bounces > 0)
                    foreach (var impulse in material.GetBRDFImpulses(attribute, V))
                    {
                        float3 D = impulse.Direction; // recursive direction to check
                        float3 facedNormal = dot(D, attribute.Normal) > 0 ? attribute.Normal : -attribute.Normal; // normal respect to direction

                        RayDescription ray = new RayDescription { Direction = D, Origin = attribute.Position + facedNormal * 0.001f, MinT = 0.0001f, MaxT = 10000 };

                        RTRayPayload newPayload = new RTRayPayload
                        {
                            Bounces = payload.Bounces - 1
                        };

                        raycaster.Trace(scene, ray, ref newPayload);

                        payload.Color += newPayload.Color * impulse.Ratio;
                    }
            };
        }
 

    }
    
}
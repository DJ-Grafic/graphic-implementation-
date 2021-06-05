using Rendering;
using GMath;
using System;
using static DJGraphic.Tools;
using static GMath.Gfx;

namespace DJGraphic
{   
    public partial class Pathtracing
    {
        public void Scene()
        {
            scene = new Scene<PositionNormalCoordinate, Material>();
            Texture2D planeTexture = Texture2D.LoadFromFile("../teachingImplentation/apkwood.jpg");
            Texture2D towerTexture = Texture2D.LoadFromFile("../teachingImplentation/textil.jpg");

            light(scene, LightIntensity, LightPosition);

            planeXY(scene, planeTexture);
            planeXZ(scene, planeTexture);
            
            glass1(scene);
            //water1(scene);
            //for (int i = 0; i < 30; i++) bubble(scene, -0.4f, -1.2f, -0.2f);

            //glass2(scene);
            //water2(scene);
            //for (int i = 0; i < 30; i++) bubble(scene, 1.4f, 2.2f, 1.1f);
            //clown(scene, towerTexture);

            //jar(scene);
            //flow(scene);
       
        }


//////////////////////////////// Light //////////////////////////////////////////////////////////// 
        static Action<Scene<PositionNormalCoordinate, Material>, float3, float3> light = 
        (scene, LightIntensity, LightPosition) => {
            var sphereModel = Raycasting.UnitarySphere.AttributesMap(
                a => new PositionNormalCoordinate { 
                    Position = a, 
                    Coordinates = float2(atan2(a.z, a.x) * 0.5f / pi + 0.5f, a.y), 
                    Normal = normalize(a) 
                });
            scene.Add(sphereModel, new Material
                {
                    Emissive = LightIntensity / (4 * pi), // power per unit area
                    WeightDiffuse = 0,
                    WeightFresnel = 1.0f, // Glass sphere
                    RefractionIndex = 1.0f
                },
                mul(Transforms.Scale(2.4f, 0.4f, 2.4f), Transforms.Translate(LightPosition)));
        };
            

//////////////////////////////// Planes //////////////////////////////////////////////////////////// 
        static Action<Scene<PositionNormalCoordinate, Material>, Texture2D> planeXY = 
        (scene, planeTexture) => {
            scene.Add(
                WoodPlane<PositionNormalCoordinate>.XY(), 
                ModelMaterial.Texture(planeTexture),
                Transforms.Translate(0,0,-1.35f));
        };

        static Action<Scene<PositionNormalCoordinate, Material>, Texture2D> planeXZ = 
        (scene, planeTexture) => {
            scene.Add(
                WoodPlane<PositionNormalCoordinate>.XZ(), 
                ModelMaterial.Texture(planeTexture),
                Transforms.Translate(0,7,0));
        };

        static Action<Scene<PositionNormalCoordinate, Material>, Texture2D> clown = 
        (scene, planeTexture) => {
            var clown = Clown<PositionNormalCoordinate>.Mesh().Weld();
            clown.ComputeNormals();
            clown.InverterNormals();
            scene.Add(
                clown.AsRaycast(RaycastingMeshMode.Grid), 
                ModelMaterial.Texture(planeTexture),
                Clown<PositionNormalCoordinate>.Transform());
        };

//////////////////////////////// Glass //////////////////////////////////////////////////////////// 
        static Action<Scene<PositionNormalCoordinate, Material>> glass1 = 
        (scene) => {
            var glass1 = GlassOfWater<PositionNormalCoordinate>.Mesh(0.5f).Weld();
            glass1.ComputeNormals();
            scene.Add(
                glass1.AsRaycast(RaycastingMeshMode.Grid), 
                ModelMaterial.Glass, 
                GlassOfWater<PositionNormalCoordinate>.Transform1()
            );
        };

        static Action<Scene<PositionNormalCoordinate, Material>> glass2 = 
        (scene) => {
            var glass2 = GlassOfWater<PositionNormalCoordinate>.Mesh(0.5f).Weld();
            glass2.ComputeNormals();
            scene.Add(
                glass2.AsRaycast(RaycastingMeshMode.Grid), 
                ModelMaterial.Glass, 
                GlassOfWater<PositionNormalCoordinate>.Transform2()
            );
        };

        static Action<Scene<PositionNormalCoordinate, Material>> jar = 
        (scene) => {
            var jar = JarOfWater<PositionNormalCoordinate>.Mesh().Weld();
            jar.ComputeNormals();
            jar.InverterNormals();
            scene.Add(
                jar.AsRaycast(RaycastingMeshMode.Grid), 
                ModelMaterial.Glass, 
                JarOfWater<PositionNormalCoordinate>.Transform()
            );
        };
//////////////////////////////// Waters //////////////////////////////////////////////////////////// 
        static Action<Scene<PositionNormalCoordinate, Material>> water1 = 
        (scene) => {
            var water = Waters<PositionNormalCoordinate>.MeshInGlass().Weld();
            water.ComputeNormals();
            water.InverterNormals();
            scene.Add(
                water.AsRaycast(RaycastingMeshMode.Grid), 
                ModelMaterial.Water, 
                Waters<PositionNormalCoordinate>.TransformInGlass1()
            );
        };

        static Action<Scene<PositionNormalCoordinate, Material>> water2 = 
        (scene) => {
            var water = Waters<PositionNormalCoordinate>.MeshInGlass(false).Weld();
            water.ComputeNormals();
            water.InverterNormals();
            scene.Add(
                water.AsRaycast(RaycastingMeshMode.Grid), 
                ModelMaterial.Water, 
                Waters<PositionNormalCoordinate>.TransformInGlass2()
            );
        };

        static Action<Scene<PositionNormalCoordinate, Material>> flow = 
        (scene) => {
            var water = Waters<PositionNormalCoordinate>.MeshFlow().Weld();
            water.ComputeNormals();
            water.InverterNormals();
            scene.Add(
                water.AsRaycast(RaycastingMeshMode.Grid), 
                ModelMaterial.Water, 
                Waters<PositionNormalCoordinate>.TransformFlow()
            );
        };

        static Action<Scene<PositionNormalCoordinate, Material>, float, float, float> bubble = 
        (scene, a, b, y) => {
            var sphereModel = Raycasting.UnitarySphere.AttributesMap(
                a => new PositionNormalCoordinate { 
                    Position = a, 
                    Coordinates = float2(atan2(a.z, a.x) * 0.5f / pi + 0.5f, a.y), 
                    Normal = normalize(a) });


            float size = random() * 0.03f + 0.03f;
            float x = random() * (b-a)  + a;
            float z = random() * -1.65f + 0.25f;

            scene.Add(
                sphereModel, 
                ModelMaterial.Mirror, 
                mul(Transforms.Scale(size, size, size), Transforms.Translate(x, y, z))
            );
        };

            
    }
    
}
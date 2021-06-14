using Rendering;
using GMath;
using System;
using static DJGraphic.Tools;
using static GMath.Gfx;

namespace DJGraphic
{   
    static class RaycastingSet
    {
        public static void Scene(Scene<PositionNormalCoordinate, Material> scene)
        {
            Texture2D planeTexture = Texture2D.LoadFromFile("../teachingImplentation/apkwood.jpg");
            Texture2D towerTexture = Texture2D.LoadFromFile("../teachingImplentation/textil.jpg");
            
            
            planeXY(scene, planeTexture);
            planeXZ(scene, planeTexture);
            
            glass1(scene);
            water1(scene);
            for (int i = 0; i < 30; i++) bubble(scene, -0.5f, -1.3f, -0.25f, 0, 0);

            glass2(scene);
            water2(scene);
            for (int i = 0; i < 30; i++) bubble(scene, 1.5f, 2.1f, 1.15f, 0.2f, -0.1f);
            clown(scene, towerTexture);

            //jarfront(scene);
            jar(scene);
            flow(scene);
            water3(scene);       

        }

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
                ModelMaterial.Glass1, 
                GlassOfWater<PositionNormalCoordinate>.Transform2()
            );
        };

        static Action<Scene<PositionNormalCoordinate, Material>> jar = 
        (scene) => {
            var jar = JarOfWater<PositionNormalCoordinate>.Mesh().Weld();
            jar.ComputeNormals();
            //jar.InverterNormals();
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

        static Action<Scene<PositionNormalCoordinate, Material>> water3 = 
        (scene) => {
            var water = Waters<PositionNormalCoordinate>.MeshTest().Weld();
            water.ComputeNormals();
            water.InverterNormals();
            scene.Add(
                water.AsRaycast(RaycastingMeshMode.Grid), 
                ModelMaterial.Water, 
                Waters<PositionNormalCoordinate>.TransforT()
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

        static Action<Scene<PositionNormalCoordinate, Material>, float, float, float, float, float> bubble = 
        (scene, a, b, y, z1, s) => {
            var sphereModel = Raycasting.UnitarySphere.AttributesMap(
                a => new PositionNormalCoordinate { 
                    Position = a, 
                    Coordinates = float2(atan2(a.z, a.x) * 0.5f / pi + 0.5f, a.y), 
                    Normal = normalize(a) });


            float size = random() * (0.03f + s) + (0.03f + s);
            float x = random() * (b-a)  + a;
            float z = random() * -1.45f + 0.25f + 0.2f + z1;

            scene.Add(
                sphereModel, 
                ModelMaterial.Bubble, 
                mul(Transforms.Scale(size, size, size), Transforms.Translate(x, y, z))
            );
        };

            
    }
    
}
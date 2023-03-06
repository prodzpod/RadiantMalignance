using RoR2;
using System.Collections.Generic;
using UnityEngine;

namespace RiskyMonkeyBase.Tweaks
{
    public class ArtifactHints
    {
        public static float second = 0;
        public static Mesh spreadMesh;
        public static Mesh wrathMesh;
        public static void Patch()
        {
            Stage.onStageStartGlobal += (self) =>
            {
                if (self.sceneDef.cachedName == "snowyforest" && Reference.Mods("com.TPDespair.ZetArtifacts")) Revival();
                if (self.sceneDef.cachedName == "ancientloft" && Reference.Mods("com.TPDespair.ZetArtifacts")) Eclipse();
                if (self.sceneDef.cachedName == "sulfurpools" && Reference.Mods("com.Wolfo.ArtifactOfDissimilarity")) Wander();
                if (self.sceneDef.cachedName == "FBLScene" && Reference.Mods("com.Wolfo.ArtifactOfDissimilarity")) Brigade();
                if (self.sceneDef.cachedName == "rootjungle" && Reference.Mods("com.Wolfo.ArtifactOfDissimilarity")) Remodeling();
                if (self.sceneDef.cachedName == "slumberingsatellite" && Reference.Mods("PlasmaCore.ForgottenRelics", "com.TPDespair.ZetArtifacts")) Escalation();
                if (self.sceneDef.cachedName == "forgottenhaven" && Reference.Mods("PlasmaCore.ForgottenRelics", "zombieseatflesh7.ArtifactOfPotential")) Potential();
                if (self.sceneDef.cachedName == "moon2" && Reference.Mods("com.Wolfo.ArtifactOfDissimilarity")) Spiriting();
                if (self.sceneDef.cachedName == "itmoon" && Reference.Mods("HIFU.ArtifactOfBlindness")) Blindness();
                // void
                if (self.sceneDef.cachedName == "BulwarksHaunt_GhostWave" && Reference.Mods("com.TPDespair.ZetArtifacts")) Tossing();
            };
            On.RoR2.VoidStageMissionController.Start += (orig, self) =>
            {
                orig(self);
                if (Reference.Mods("com.Wolfo.ArtifactOfDissimilarity")) Transpose();
            };
            On.RoR2.VoidRaidGauntletExitController.OnBodyTeleport += (orig, self, body) =>
            {
                orig(self, body);
                if (Reference.Mods("com.Wolfo.ArtifactOfDissimilarity")) Kith();
            };
        }
        public static void Spiriting()
        {
            GameObject roof = GameObject.Find("mdlLunarBloodArenaRoof");
            if (roof == null) return;
            roof.GetComponent<MeshFilter>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/mdlLunarBloodArenaRoofSpecial.obj");
            roof.GetComponent<MeshCollider>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/mdlLunarBloodArenaRoofSpecial.obj");
        }
        public static void Kith()
        {
            foreach (var pname in new string[] { "BbRuinPillar1_LOD0", "BbRuinPillar1_LOD0 (1)", "BbRuinPillar1_LOD0 (2)", "BbRuinPillar1_LOD0 (3)", "BbRuinPillar1_LOD0 (4)", "BbRuinPillar1_LOD0 (5)", "BbRuinPillar1_LOD0 (6)", "BbRuinPillar1_LOD0 (7)", "BbRuinPillar1_LOD0 (8)", "BbRuinPillar1_LOD0 (9)", "BbRuinPillar1_LOD0 (10)" })
            {
                GameObject pillar = GameObject.Find(pname);
                if (pillar == null) continue;
                pillar.GetComponent<MeshFilter>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/BbRuinPillar1_LOD0Special.obj");
                pillar.GetComponent<MeshCollider>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/BbRuinPillar1_LOD0Special.obj");
            }
        }
        public static void Transpose()
        {
            GameObject root = GameObject.Find("meshVoidCenterStatue (2)");
            if (root == null) return;
            root.GetComponent<MeshFilter>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/meshVoidCenterStatueSpecial.obj");
            root.GetComponent<MeshCollider>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/meshVoidCenterStatueSpecial.obj");
        }
        public static void Blindness()
        {
            GameObject bowl = GameObject.Find("MoonArenaBaseLayerBowl (1)");
            if (bowl == null) return;
            bowl.GetComponent<MeshFilter>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/MoonArenaBaseLayerBowlSpecial.obj");
            bowl.GetComponent<MeshCollider>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/MoonArenaBaseLayerBowlSpecial.obj");
        }
        public static void Potential()
        {
            GameObject grate = GameObject.Find("GPRuinGrate (7)");
            if (grate == null) return;
            grate.GetComponent<MeshFilter>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/GPRuinGrateSpecial.obj");
            grate.GetComponent<MeshCollider>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/GPRuinGrateSpecial.obj");
        }

        public static void Escalation()
        {
            GameObject satellite = GameObject.Find("Satellite");
            if (satellite == null) return;
            satellite.GetComponent<MeshFilter>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/SatelliteSpecial.obj");
            satellite.GetComponent<MeshCollider>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/SatelliteSpecial.obj");
            satellite.transform.eulerAngles = new Vector3(72.1839f, 265.9064f, 330.7305f); // rotated lol
        }

        public static void Revival()
        {
            GameObject water = GameObject.Find("meshSnowyForestTerrainWater");
            if (water == null) return;
            water.GetComponent<MeshFilter>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/meshSnowyForestTerrainWaterSpecial.obj");
            water.GetComponent<MeshCollider>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/meshSnowyForestTerrainWaterSpecial.obj");
        }

        public static void Wander()
        {
            GameObject dome = GameObject.Find("meshSPDome");
            if (dome == null) return;
            dome.GetComponent<MeshFilter>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/meshSPDomeSpecial.obj");
            dome.GetComponent<MeshCollider>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/meshSPDomeSpecial.obj");
        }

        public static void Remodeling()
        {
            GameObject middle = GameObject.Find("Terrain Middle");
            if (middle == null) return;
            middle.GetComponent<MeshFilter>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/Terrain Middle Special.obj");
            middle.GetComponent<MeshCollider>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/Terrain Middle Special.obj");
        }

        public static void Tossing()
        {
            GameObject column = GameObject.Find("columnLarge");
            if (column == null) return;
            column.GetComponent<MeshFilter>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/columnLargeSpecial.obj");
            column.GetComponent<MeshCollider>().sharedMesh = RiskyMonkeyBase.AssetBundle.LoadAsset<Mesh>("Assets/columnLargeSpecial.obj");
        }

        public static void Eclipse()
        {
            List<GameObject> insts = new();
            insts.Add(RiskyMonkeyBase.AssetBundle.LoadAsset<GameObject>("Assets/ALSphere.prefab"));
            insts.Add(RiskyMonkeyBase.AssetBundle.LoadAsset<GameObject>("Assets/ALCube.prefab"));
            insts.Add(GameObject.Find("AncientLoft_CircleArchway"));
            List<Material> mats = new();
            mats.Add(GameObject.Find("TempleMain").GetComponent<MeshRenderer>().material);
            mats.Add(GameObject.Find("Terrain").GetComponent<MeshRenderer>().material);

            int[] _insts = { 0, 1, 0, 1, 1, 2, 0, 0 };
            int[] _mats = { 0, 1, 0, 1, 1, 2, 0, 1 };
            Vector3[] _pos = { new Vector3(272.8472f, 41.7174f, 63.4237f), new Vector3(271.5929f, 42.0435f, 64.9814f), new Vector3(15.5109f, 117.6409f, 139.1112f), new Vector3(-342.973f, 124.4446f, -1.4889f), new Vector3(-668.3638f, 187.09f, 335.1929f), new Vector3(167.2275f, 34.9909f, 49.6182f), new Vector3(273.3472f, 39.6211f, 64.711f), new Vector3(275.1739f, 39.7246f, 65.2419f) };
            Vector3[] _angle = { new Vector3(0f, 0f, 0f), new Vector3(355.978f, 359.6382f, 10.9019f), new Vector3(0f, 0f, 0f), new Vector3(5.7709f, 5.32f, 10.9018f), new Vector3(11.0383f, 10.4254f, 4.0928f), new Vector3(0f, 0f, 90f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f) };
            Vector3[] _size = { new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(16f, 16f, 16f), new Vector3(30f, 30f, 30f), new Vector3(45f, 45f, 45f), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.3f, 0.3f, 0.3f) };

            for (var i = 0; i < _insts.Length; i++)
            {
                var inst = Object.Instantiate(insts[_insts[i]]);
                if (_mats[i] < 2) inst.GetComponent<MeshRenderer>().material = mats[_mats[i]];
                inst.transform.position = _pos[i];
                inst.transform.eulerAngles = _angle[i];
                inst.transform.localScale = _size[i];
            }
        }

        public static void Brigade()
        {
            Vector3[] circlePos = new Vector3[] { new Vector3(-26.9246f, 212.9074f, 530.936f), new Vector3(-32.8682f, 200.5867f, 529.6067f), new Vector3(-19.4209f, 200.5867f, 532.3671f) };
            Vector3[] circleAngle = new Vector3[] { new Vector3(87.2268f, -0.0002f, 18.1163f), new Vector3(87.2268f, -0.0002f, 18.1163f), new Vector3(87.2268f, -0.0002f, 18.1163f) };
            GameObject circle = GameObject.Find("LightTotem");
            for (var i = 0; i < circlePos.Length; i++)
            {
                GameObject inst = Object.Instantiate(circle);
                inst.transform.position = circlePos[i];
                inst.transform.eulerAngles = circleAngle[i];
            }
            Vector3[] trianglePos = new Vector3[] { new Vector3(-32.361f, 214.2711f, 524.7567f), new Vector3(-30.561f, 204.6399f, 525.8929f), new Vector3(-25.1719f, 204.7033f, 526.9305f), new Vector3(-18.3083f, 209.2911f, 528.4629f), new Vector3(-16.7446f, 214.0656f, 528.923f), new Vector3(-25.9428f, 198.4229f, 527.3651f) };
            Vector3[] triangleAngle = new Vector3[] { new Vector3(331.737f, 77.8183f, 90f), new Vector3(62.2975f, 85.1628f, 93.4719f), new Vector3(90.4586f, 348.4852f, 0f), new Vector3(301.6861f, 82.4547f, 85.9255f), new Vector3(347.7221f, 75.981f, 90.0006f), new Vector3(86.5037f, 252.7273f, 270.0002f) };
            GameObject triangle = GameObject.Find("Triangle");
            for (var i = 0; i < trianglePos.Length; i++)
            {
                GameObject inst = Object.Instantiate(triangle);
                inst.transform.position = trianglePos[i];
                inst.transform.eulerAngles = triangleAngle[i];
                inst.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
    }
}

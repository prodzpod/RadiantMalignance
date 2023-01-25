using BulwarksHaunt.Achievements;
using MonoMod.RuntimeDetour.HookGen;
using RoR2;
using System;
using System.Reflection;
using UnityEngine;

namespace RiskyMonkeyBase.Achievements
{
    public class PaladinWinGhostWaveAchievement
    {
        public static UnlockableDef unlockableDef;
        public static void Patch()
        {
            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Skins.Paladin.BulwarksHaunt_Alt";
            unlockableDef.nameToken = "KRONONCONSPIRATOR_SKIN_PROVIPALADIN_NAME";
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
        }
        public static void PostPatch()
        {
            BodyCatalog.availability.CallWhenAvailable(() =>
            {
                MethodInfo method = typeof(SkinDef).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic);
                HookEndpointManager.Add(method, (Action<SkinDef> _, SkinDef _2) => { });
                try
                {
                    GameObject bodyPrefab = BodyCatalog.FindBodyPrefab("RobPaladinBody");
                    GameObject gameObject = bodyPrefab.GetComponent<ModelLocator>().modelTransform.gameObject;
                    ModelSkinController modelSkinController = (bool)gameObject ? gameObject.GetComponent<ModelSkinController>() : null;
                    if (modelSkinController == null) throw new Exception("ModelSkinController not found");
                    for (var i = 0; i < modelSkinController.skins.Length; i++)
                    {
                        SkinDef skin = modelSkinController.skins[i];
                        if (skin.name != "ProviPaladin") continue;
                        unlockableDef.achievementIcon = skin.icon;
                        skin.unlockableDef = unlockableDef;
                        modelSkinController.skins[i] = skin;
                    }
                    BodyCatalog.skins[(int)BodyCatalog.FindBodyIndex(bodyPrefab)] = modelSkinController.skins;
                }
                catch (Exception ex) { RiskyMonkeyBase.Log.LogError(ex); }
                HookEndpointManager.Remove(method, (Action<SkinDef> _, SkinDef _2) => { });
            });
        }
        
        [RegisterModdedAchievement("BulwarksHaunt_PaladinWinGhostWave", "Skins.Paladin.BulwarksHaunt_Alt", null, null, "com.rob.Paladin", "com.KrononConspirator.Thy_Providence", "com.themysticsword.bulwarkshaunt")]
        public class PaladinWinGhostWave : BaseWinGhostWavePerSurvivor
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("RobPaladinBody");
        }

        [RegisterModdedAchievement("BulwarksHaunt_TemplarWinGhostWave", "Skins.Templar.BulwarksHaunt_Alt", null, null, "com.themysticsword.bulwarkshaunt", "prodzpod.TemplarSkins")] public class TemplarWinGhostWave : BaseWinGhostWavePerSurvivor { public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor"); }
        [RegisterModdedAchievement("BulwarksHaunt_MinerWinGhostWave", "Skins.Miner.BulwarksHaunt_Alt", null, null, "com.themysticsword.bulwarkshaunt", "com.rob.DiggerUnearthed")] public class MinerWinGhostWave : BaseWinGhostWavePerSurvivor { public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MinerBody"); }
    }
}

using HarmonyLib;
using R2API;
using RoR2;
using RoR2.Achievements;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RiskyMonkeyBase.Achievements
{
    public class SimulacrumAchievement
    {
        public static Dictionary<string, UnlockableDef> unlockables;
        public static void Patch()
        {
            unlockables = new();
            if (Reference.Mods("HIFU.Inferno"))
            {
                if (Reference.Mods("com.dotflare.LTT1"))
                {
                    MakeUnlockable("Commando");
                    MakeUnlockable("Bandit");
                }
                if (Reference.Mods("com.FrostRay.FrostRaySkinPack")) MakeUnlockable("Huntress");
                if (Reference.Mods("com.ArtyBoi.CryingGolem")) MakeUnlockable("MulT");
                if (Reference.Mods("prodzpod.TemplarSkins"))
                {
                    MakeUnlockable("Engineer");
                    MakeUnlockable("Templar");
                }
                if (Reference.Mods("com.MAVRI.CaptainUnderglowDrip")) MakeUnlockable("Captain");
                if (Reference.Mods("com.ArtyBoi.YinYang")) MakeUnlockable("Mercenary");
                if (Reference.Mods("com.rob.Paladin")) MakeUnlockable("Paladin");
                if (Reference.Mods("com.LexLamb.MechArtificer")) MakeUnlockable("Artificer");
                if (Reference.Mods("com.ArtyBoi.KindredsLizard")) MakeUnlockable("Acrid");
                if (Reference.Mods("com.hilliurn.LunarVoidREX")) MakeUnlockable("REX");
                if (Reference.Mods("com.eyeknow.HighFashionLoader")) MakeUnlockable("Loader");
                if (Reference.Mods("com.rob.DiggerUnearthed")) MakeUnlockable("Miner");
                if (Reference.Mods("com.RetroInspired.RailGunnerSkins")) MakeUnlockable("Railgunner");
                if (Reference.Mods("com.ApexConspirator.MadVeteran")) MakeUnlockable("VoidFiend");
            }
            AchievementManager.onAchievementsRegistered += PostPatch;
            On.RoR2.Achievements.CompleteVoidEndingAchievement.CompleteWave50ServerAchievement.OnAllEnemiesDefeatedServer += (orig, self, waveController) =>
            {
                // method nuked :)
                return;
            };
        }

        public static void PostPatch()
        {
            if (Reference.Mods("HIFU.Inferno"))
            {
                if (Reference.Mods("com.dotflare.LTT1"))
                {
                    AddUnlockable("MobsterMando", "Commando");
                    AddUnlockable("HBandit", "Bandit");
                }
                if (Reference.Mods("com.FrostRay.FrostRaySkinPack")) AddUnlockable("HuntressRanger", "Huntress");
                if (Reference.Mods("com.ArtyBoi.CryingGolem")) AddUnlockable("CryingGolem", "MulT");
                if (Reference.Mods("prodzpod.TemplarSkins"))
                {
                    AddUnlockable("skinEngiVoidAlt", "Engineer");
                    AddUnlockable("skinTemplarVoidAlt", "Templar");
                }
                if (Reference.Mods("com.MAVRI.CaptainUnderglowDrip"))
                {
                    AddUnlockable("Underglow", "Captain");
                    AddUnlockable("Underglow_NoDecal", "Captain");
                }
                if (Reference.Mods("com.ArtyBoi.YinYang")) AddUnlockable("YingYang", "Mercenary");
                if (Reference.Mods("com.rob.Paladin")) AddUnlockable("PALADINBODY_SPECTER_SKIN_NAME", "Paladin");
                if (Reference.Mods("com.LexLamb.MechArtificer")) AddUnlockable("MechArtificer", "Artificer");
                if (Reference.Mods("com.ArtyBoi.KindredsLizard")) AddUnlockable("KindredsLizard", "Acrid");
                if (Reference.Mods("com.hilliurn.LunarVoidREX")) AddUnlockable("LunarVoidREXSkin", "REX");
                if (Reference.Mods("com.eyeknow.HighFashionLoader")) AddUnlockable("LoaderPlum", "Loader");
                if (Reference.Mods("com.rob.DiggerUnearthed")) AddUnlockable("MINERBODY_PUPLE_SKIN_NAME", "Miner");
                if (Reference.Mods("com.RetroInspired.RailGunnerSkins")) AddUnlockable("NightOp", "Railgunner");
                if (Reference.Mods("com.ApexConspirator.MadVeteran")) AddUnlockable("VoidSurvivorNeo", "VoidFiend");
            }
        }

        [RegisterModdedAchievement("CommandoClearGameSimulacrum", "Skins.Simulacrum_Commando", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.dotflare.LTT1")] public class CommandoClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CommandoBody"); }
        [RegisterModdedAchievement("HuntressClearGameSimulacrum", "Skins.Simulacrum_Huntress", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.FrostRay.FrostRaySkinPack")] public class HuntressClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("HuntressBody"); }
        [RegisterModdedAchievement("BanditClearGameSimulacrum", "Skins.Simulacrum_Bandit", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.dotflare.LTT1")] public class BanditClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Bandit2Body"); }
        [RegisterModdedAchievement("MulTClearGameSimulacrum", "Skins.Simulacrum_MulT", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.ArtyBoi.CryingGolem")] public class MulTClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("ToolbotBody"); }
        [RegisterModdedAchievement("EngineerClearGameSimulacrum", "Skins.Simulacrum_Engineer", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "prodzpod.TemplarSkins")] public class EngineerClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("EngiBody"); }
        [RegisterModdedAchievement("CaptainClearGameSimulacrum", "Skins.Simulacrum_Captain", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.MAVRI.CaptainUnderglowDrip")] public class CaptainClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CaptainBody"); }
        [RegisterModdedAchievement("MercenaryClearGameSimulacrum", "Skins.Simulacrum_Mercenary", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.ArtyBoi.YinYang")] public class MercenaryClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MercBody"); }
        [RegisterModdedAchievement("PaladinClearGameSimulacrum", "Skins.Simulacrum_Paladin", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.rob.Paladin")] public class PaladinClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("RobPaladinBody"); }
        [RegisterModdedAchievement("ArtificerClearGameSimulacrum", "Skins.Simulacrum_Artificer", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.LexLamb.MechArtificer")] public class ArtificerClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MageBody"); }
        [RegisterModdedAchievement("AcridClearGameSimulacrum", "Skins.Simulacrum_Acrid", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.ArtyBoi.KindredsLizard")] public class AcridClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CrocoBody"); }
        [RegisterModdedAchievement("REXClearGameSimulacrum", "Skins.Simulacrum_REX", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.hilliurn.LunarVoidREX")] public class REXClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("TreebotBody"); }
        [RegisterModdedAchievement("LoaderClearGameSimulacrum", "Skins.Simulacrum_Loader", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.eyeknow.HighFashionLoader")] public class LoaderClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("LoaderBody"); }
        [RegisterModdedAchievement("MinerClearGameSimulacrum", "Skins.Simulacrum_Miner", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.rob.DiggerUnearthed")] public class MinerClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MinerBody"); }
        [RegisterModdedAchievement("TemplarClearGameSimulacrum", "Skins.Simulacrum_Templar", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "prodzpod.TemplarSkins")] public class TemplarClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor"); }
        [RegisterModdedAchievement("RailgunnerClearGameSimulacrum", "Skins.Simulacrum_Railgunner", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.RetroInspired.RailGunnerSkins")] public class RailgunnerClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("RailgunnerBody"); }
        [RegisterModdedAchievement("VoidFiendClearGameSimulacrum", "Skins.Simulacrum_VoidFiend", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "HIFU.Inferno", "com.ApexConspirator.MadVeteran")] public class VoidFiendClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("VoidSurvivorBody"); }

        public static void MakeUnlockable(string name)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Skins.Simulacrum_" + name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            unlockables.Add(name, unlockableDef);
            RiskyMonkeyBase.Log.LogDebug("Registered Unlockable " + name);
        }
        public static void AddUnlockable(string skinName, string name)
        {
            SkinDef def = null;
            foreach (var skin in SkinCatalog.allSkinDefs) if (skin.name == skinName) def = skin;
            UnlockableDef unlockableDef = unlockables[name];
            RiskyMonkeyBase.Log.LogDebug("Fetched Unlockable " + name);
            unlockableDef.nameToken = def.nameToken;
            unlockableDef.achievementIcon = def.icon;
            def.unlockableDef = unlockableDef;
            AccessTools.FieldRefAccess<Sprite>(typeof(AchievementDef), "achievedIcon")(AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName)) = def.icon;
        }
        public class BasePerSurvivorClearGameSimulacrumAchievement : BaseEndingAchievement
        {
            public override void OnInstall() { base.OnInstall(); SetServerTracked(true); }

            public override void OnUninstall() { SetServerTracked(false); base.OnUninstall(); }

            public override bool ShouldGrant(RunReport runReport) => runReport.gameEnding == DLC1Content.GameEndings.VoidEnding;

            public class BasePerSurvivorClearGameSimulacrumServerAchievement : BaseServerAchievement
            {

                public override void OnInstall()
                {
                    base.OnInstall(); InfiniteTowerRun.onAllEnemiesDefeatedServer += new Action<InfiniteTowerWaveController>(OnAllEnemiesDefeatedServer);
                }

                public override void OnUninstall()
                {
                    InfiniteTowerRun.onAllEnemiesDefeatedServer -= new Action<InfiniteTowerWaveController>(OnAllEnemiesDefeatedServer); base.OnUninstall();
                }

                private void OnAllEnemiesDefeatedServer(InfiniteTowerWaveController waveController)
                {
                    InfiniteTowerRun instance = Run.instance as InfiniteTowerRun;
                    if (instance != null && instance.waveIndex >= 50) Grant();
                }
            }
        }
    }
}

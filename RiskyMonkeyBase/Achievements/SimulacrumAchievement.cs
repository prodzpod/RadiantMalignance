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
            if (Reference.Mods("prodzpod.Downpour"))
            {
                if (Reference.Mods("com.dotflare.LTT1"))
                {
                    MakeUnlockable("Commando");
                    MakeUnlockable("Bandit");
                }
                if (Reference.Mods("com.FrostRay.FrostRaySkinPack")) MakeUnlockable("Huntress");
                if (Reference.Mods("com.ArtyBoi.CryingGolem")) MakeUnlockable("MulT");
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
            if (Reference.Mods("prodzpod.Downpour"))
            {
                if (Reference.Mods("com.dotflare.LTT1"))
                {
                    AddUnlockable("MobsterMando", "Commando");
                    AddUnlockable("HBandit", "Bandit");
                }
                if (Reference.Mods("com.FrostRay.FrostRaySkinPack")) AddUnlockable("HuntressRanger", "Huntress");
                if (Reference.Mods("com.ArtyBoi.CryingGolem")) AddUnlockable("CryingGolem", "MulT");
                if (Reference.Mods("com.MAVRI.CaptainUnderglowDrip"))
                {
                    AddUnlockable("Underglow", "Captain");
                    AddUnlockable("Underglow_NoDecal", "Captain");
                }
                if (Reference.Mods("com.ArtyBoi.YinYang")) AddUnlockable("YingYang", "Mercenary");
                if (Reference.Mods("com.rob.Paladin")) AddUnlockable("PALADINBODY_SPECTER_SKIN_NAME", "Paladin");
                if (Reference.Mods("com.LexLamb.MechArtificer")) AddUnlockable("MechArtificer", "Artificer");
                if (Reference.Mods("com.ArtyBoi.KindredsLizard")) AddUnlockable("KindredsLizard", "Acrid");
                if (Reference.Mods("com.hilliurn.LunarVoidREX")) AddUnlockable("LunarVoid_REX_Skin", "REX");
                if (Reference.Mods("com.eyeknow.HighFashionLoader")) AddUnlockable("LoaderPlum", "Loader");
                if (Reference.Mods("com.rob.DiggerUnearthed")) AddUnlockable("MINERBODY_PUPLE_SKIN_NAME", "Miner");
                if (Reference.Mods("com.RetroInspired.RailGunnerSkins")) AddUnlockable("NightOp", "Railgunner");
                if (Reference.Mods("com.ApexConspirator.MadVeteran")) AddUnlockable("VoidSurvivorNeo", "VoidFiend");
            }
        }


        [RegisterModdedAchievement("CommandoClearGameSimulacrum", "Skins.Simulacrum_Commando", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.dotflare.LTT1")] public class CommandoClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CommandoBody"); }
        [RegisterModdedAchievement("HuntressClearGameSimulacrum", "Skins.Simulacrum_Huntress", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.FrostRay.FrostRaySkinPack")] public class HuntressClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("HuntressBody"); }
        [RegisterModdedAchievement("BanditClearGameSimulacrum", "Skins.Simulacrum_Bandit", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.dotflare.LTT1")] public class BanditClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Bandit2Body"); }
        [RegisterModdedAchievement("MulTClearGameSimulacrum", "Skins.Simulacrum_MulT", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.ArtyBoi.CryingGolem")] public class MulTClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("ToolbotBody"); }
        [RegisterModdedAchievement("CaptainClearGameSimulacrum", "Skins.Simulacrum_Captain", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.MAVRI.CaptainUnderglowDrip")] public class CaptainClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CaptainBody"); }
        [RegisterModdedAchievement("MercenaryClearGameSimulacrum", "Skins.Simulacrum_Mercenary", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.ArtyBoi.YinYang")] public class MercenaryClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MercBody"); }
        [RegisterModdedAchievement("PaladinClearGameSimulacrum", "Skins.Simulacrum_Paladin", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.rob.Paladin")] public class PaladinClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("RobPaladinBody"); }
        [RegisterModdedAchievement("ArtificerClearGameSimulacrum", "Skins.Simulacrum_Artificer", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.LexLamb.MechArtificer")] public class ArtificerClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MageBody"); }
        [RegisterModdedAchievement("AcridClearGameSimulacrum", "Skins.Simulacrum_Acrid", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.ArtyBoi.KindredsLizard")] public class AcridClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CrocoBody"); }
        [RegisterModdedAchievement("REXClearGameSimulacrum", "Skins.Simulacrum_REX", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.hilliurn.LunarVoidREX")] public class REXClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("TreebotBody"); }
        [RegisterModdedAchievement("LoaderClearGameSimulacrum", "Skins.Simulacrum_Loader", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.eyeknow.HighFashionLoader")] public class LoaderClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("LoaderBody"); }
        [RegisterModdedAchievement("MinerClearGameSimulacrum", "Skins.Simulacrum_Miner", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.rob.DiggerUnearthed")] public class MinerClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MinerBody"); }
        [RegisterModdedAchievement("RailgunnerClearGameSimulacrum", "Skins.Simulacrum_Railgunner", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.RetroInspired.RailGunnerSkins")] public class RailgunnerClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("RailgunnerBody"); }
        [RegisterModdedAchievement("VoidFiendClearGameSimulacrum", "Skins.Simulacrum_VoidFiend", "CompleteMainEnding", typeof(BasePerSurvivorClearGameSimulacrumServerAchievement), "prodzpod.Downpour", "com.ApexConspirator.MadVeteran")] public class VoidFiendClearGameSimulacrumAchievement : BasePerSurvivorClearGameSimulacrumAchievement {public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("VoidSurvivorBody"); }

        public static void MakeUnlockable(string name)
        {
            if (RiskyMonkeyAchievements.achievementBlacklist.Contains("Skins.Simulacrum_" + name)) return;
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Skins.Simulacrum_" + name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            unlockables.Add(name, unlockableDef);
            RiskyMonkeyAchievements.Log("Registered Unlockable " + name);
        }
        public static void AddUnlockable(string skinName, string name)
        {
            if (RiskyMonkeyAchievements.achievementBlacklist.Contains("Skins.Simulacrum_" + name)) return;
            SkinDef def = null;
            foreach (var skin in SkinCatalog.allSkinDefs) if (skin.name == skinName) def = skin;
            UnlockableDef unlockableDef = unlockables[name];
            RiskyMonkeyAchievements.Log("Fetched Unlockable " + name);
            unlockableDef.nameToken = def.nameToken;
            unlockableDef.achievementIcon = def.icon;
            def.unlockableDef = unlockableDef;
            AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName).achievedIcon = def.icon;
        }
        public class BasePerSurvivorClearGameSimulacrumAchievement : BaseEndingAchievement
        {
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); SetServerTracked(true); }

            public override void OnBodyRequirementBroken() { SetServerTracked(false); base.OnBodyRequirementBroken(); }

            public override bool ShouldGrant(RunReport runReport) => false;

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
                    if (instance != null && Downpour.DownpourPlugin.DownpourList.Contains(DifficultyCatalog.GetDifficultyDef(Run.instance.selectedDifficulty)) && instance.waveIndex >= 50) Grant();
                }
            }
        }
    }
}

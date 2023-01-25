using HarmonyLib;
using R2API;
using RoR2;
using RoR2.Achievements;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RiskyMonkeyBase.Achievements
{
    public class E8Achievement
    {
        public static Dictionary<string, UnlockableDef> unlockables;
        public static void Patch()
        {
            unlockables = new();
            if (Reference.Mods("com.ApexConspirator.MadVeteran"))
            {
                MakeUnlockable("Commando");
                MakeUnlockable("MulT");
                MakeUnlockable("Engineer");
                MakeUnlockable("Captain");
                MakeUnlockable("Artificer");
                MakeUnlockable("Acrid");
                MakeUnlockable("REX");
                MakeUnlockable("Loader");
                MakeUnlockable("Railgunner");
            }
            if (Reference.Mods("com.ArtyBoi.Kindred")) MakeUnlockable("Huntress");
            if (Reference.Mods("com.dotflare.LTT1")) MakeUnlockable("Bandit");
            if (Reference.Mods("com.dotflare.LTT1")) MakeUnlockable("Mercenary");
            if (Reference.Mods("com.rob.Paladin")) MakeUnlockable("Paladin");
            if (Reference.Mods("com.rob.DiggerUnearthed")) MakeUnlockable("Miner");
            if (Reference.Mods("prodzpod.TemplarSkins")) MakeUnlockable("Templar");
            if (Reference.Mods("com.dotflare.LTT1")) MakeUnlockable("VoidFiend");
            AchievementManager.onAchievementsRegistered += PostPatch;
        }

        public static void PostPatch()
        {
            if (Reference.Mods("com.ApexConspirator.MadVeteran"))
            {
                AddUnlockable("CommNeo", "Commando");
                AddUnlockable("MulTNeo", "MulT");
                AddUnlockable("EngiNeo", "Engineer");
                AddUnlockable("CapNeo", "Captain");
                AddUnlockable("ArtiNeo", "Artificer");
                AddUnlockable("AcridNeo", "Acrid");
                AddUnlockable("RexNeo", "REX");
                AddUnlockable("LoadNeo", "Loader");
                AddUnlockable("RgunnerNeo", "Railgunner");
            }
            if (Reference.Mods("com.ArtyBoi.Kindred")) AddUnlockable("ColdGiant", "Huntress");
            if (Reference.Mods("com.dotflare.LTT1")) AddUnlockable("PBandit", "Bandit");
            if (Reference.Mods("com.dotflare.LTT1")) AddUnlockable("ArMerc", "Mercenary");
            if (Reference.Mods("com.rob.Paladin")) AddUnlockable("PALADINBODY_CLAY_SKIN_NAME", "Paladin");
            if (Reference.Mods("com.rob.DiggerUnearthed")) AddUnlockable("MINERBODY_BLACKSMITH_SKIN_NAME", "Miner");
            if (Reference.Mods("prodzpod.TemplarSkins")) AddUnlockable("TemplarNeo", "Templar");
            if (Reference.Mods("com.dotflare.LTT1")) AddUnlockable("DViend", "VoidFiend");
        }

        [RegisterModdedAchievement("CommandoClearGameE8", "Skins.E8_Commando", "CompleteMainEnding", null, "com.ApexConspirator.MadVeteran")] public class CommandoClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CommandoBody"); }
        [RegisterModdedAchievement("HuntressClearGameE8", "Skins.E8_Huntress", "CompleteMainEnding", null, "com.ArtyBoi.Kindred")] public class HuntressClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("HuntressBody"); }
        [RegisterModdedAchievement("BanditClearGameE8", "Skins.E8_Bandit", "CompleteMainEnding", null, "com.dotflare.LTT1")] public class BanditClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Bandit2Body"); }
        [RegisterModdedAchievement("MulTClearGameE8", "Skins.E8_MulT", "CompleteMainEnding", null, "com.ApexConspirator.MadVeteran")] public class MulTClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("ToolbotBody"); }
        [RegisterModdedAchievement("EngineerClearGameE8", "Skins.E8_Engineer", "CompleteMainEnding", null, "com.ApexConspirator.MadVeteran")] public class EngineerClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("EngiBody"); }
        [RegisterModdedAchievement("CaptainClearGameE8", "Skins.E8_Captain", "CompleteMainEnding", null, "com.ApexConspirator.MadVeteran")] public class CaptainClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CaptainBody"); }
        [RegisterModdedAchievement("MercenaryClearGameE8", "Skins.E8_Mercenary", "CompleteMainEnding", null, "com.dotflare.LTT1")] public class MercenaryClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MercBody"); }
        [RegisterModdedAchievement("PaladinClearGameE8", "Skins.E8_Paladin", "CompleteMainEnding", null, "com.rob.Paladin")] public class PaladinClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("RobPaladinBody"); }
        [RegisterModdedAchievement("ArtificerClearGameE8", "Skins.E8_Artificer", "CompleteMainEnding", null, "com.ApexConspirator.MadVeteran")] public class ArtificerClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MageBody"); }
        [RegisterModdedAchievement("AcridClearGameE8", "Skins.E8_Acrid", "CompleteMainEnding", null, "com.ApexConspirator.MadVeteran")] public class AcridClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CrocoBody"); }
        [RegisterModdedAchievement("REXClearGameE8", "Skins.E8_REX", "CompleteMainEnding", null, "com.ApexConspirator.MadVeteran")] public class REXClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("TreebotBody"); }
        [RegisterModdedAchievement("LoaderClearGameE8", "Skins.E8_Loader", "CompleteMainEnding", null, "com.ApexConspirator.MadVeteran")] public class LoaderClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("LoaderBody"); }
        [RegisterModdedAchievement("MinerClearGameE8", "Skins.E8_Miner", "CompleteMainEnding", null, "com.rob.DiggerUnearthed")] public class MinerClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MinerBody"); }
        [RegisterModdedAchievement("TemplarClearGameE8", "Skins.E8_Templar", "CompleteMainEnding", null, "prodzpod.TemplarSkins")] public class TemplarClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor"); }
        [RegisterModdedAchievement("RailgunnerClearGameE8", "Skins.E8_Railgunner", "CompleteMainEnding", null, "com.ApexConspirator.MadVeteran")] public class RailgunnerClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("RailgunnerBody"); }
        [RegisterModdedAchievement("VoidFiendClearGameE8", "Skins.E8_VoidFiend", "CompleteMainEnding", null, "com.dotflare.LTT1")] public class VoidFiendClearGameE8Achievement : BasePerSurvivorClearGameE8Achievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("VoidSurvivorBody"); }

        public static void MakeUnlockable(string name)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Skins.E8_" + name;
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

        public class BasePerSurvivorClearGameE8Achievement : BaseAchievement
        {
            [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })]
            public override void OnBodyRequirementMet()
            {
                base.OnBodyRequirementMet();
                Run.onClientGameOverGlobal += OnClientGameOverGlobal;
            }

            [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })]
            public override void OnBodyRequirementBroken()
            {
                Run.onClientGameOverGlobal -= OnClientGameOverGlobal;
                base.OnBodyRequirementBroken();
            }

            [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })]
            private void OnClientGameOverGlobal(Run run, RunReport runReport)
            {
                if ((bool)runReport.gameEnding && runReport.gameEnding.isWin)
                {
                    if (runReport.ruleBook.FindDifficulty() == DifficultyIndex.Eclipse8 || (Reference.Mods("com.TPDespair.ZetArtifacts") && TPDespair.ZetArtifacts.ZetEclifact.Enabled))
                    {
                        runReport.gameEnding.lunarCoinReward = 15u;
                        runReport.gameEnding.showCredits = false;
                        Grant();
                    }
                }
            }
        }
    }
}

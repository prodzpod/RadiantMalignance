using HarmonyLib;
using Inferno.Unlocks;
using R2API;
using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RiskyMonkeyBase.Achievements
{
    public class InfernoAchievement
    {
        public static Dictionary<string, UnlockableDef> unlockables;
        public static void Patch()
        {
            unlockables = new();
            if (Reference.Mods("HIFU.Inferno", "com.dotflare.LTT1"))
            {
                MakeUnlockable("Huntress");
                MakeUnlockable("Toolbot");
                MakeUnlockable("Engi");
                MakeUnlockable("Croco");
                MakeUnlockable("Loader");
            }
            if (Reference.Mods("HIFU.Inferno", "prodzpod.TemplarSkins")) MakeUnlockable("Templar");
            if (Reference.Mods("HIFU.Inferno", "com.Wolfo.LittleGameplayTweaks")) MakeUnlockable("Treebot");
            if (Reference.Mods("HIFU.Inferno", "com.EmnoX.VoidDreamerVFSKIN")) MakeUnlockable("VoidSurvivor");
            if (Reference.Mods("HIFU.Inferno", "com.EmnoX.LightDreamer")) MakeUnlockable("Artificer");
            AchievementManager.onAchievementsRegistered += PostPatch;
        }
        public static void PostPatch()
        {
            if (!Reference.Mods("HIFU.Inferno")) return;
            if (Reference.Mods("com.dotflare.LTT1"))
            {
                AddUnlockable("VHuntress", "Huntress");
                AddUnlockable("GMulT", "Toolbot");
                AddUnlockable("WEngi", "Engi");
                AddUnlockable("DAcrid", "Croco");
                AddUnlockable("SPLodr", "Loader");
            }
            if (Reference.Mods("prodzpod.TemplarSkins")) AddUnlockable("skinTemplarInfernoAlt", "Templar");
            if (Reference.Mods("com.Wolfo.LittleGameplayTweaks")) AddUnlockable("skinTreebotWolfo", "Treebot");
            if (Reference.Mods("com.EmnoX.VoidDreamerVFSKIN")) AddUnlockable("DEFVoidSkin", "VoidSurvivor");
            if (Reference.Mods("com.EmnoX.LightDreamer")) AddUnlockable("DEFLightSKin", "Artificer");
        }

        [RegisterModdedAchievement("HuntressClearGameInferno", "Skins.Inferno_Huntress", "CompleteMainEnding", null, "HIFU.Inferno", "com.dotflare.LTT1")] public class HuntressClearGameInfernoAchievement : BasePerSurvivorClearGameInfernoAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("HuntressBody"); }
        [RegisterModdedAchievement("ToolbotClearGameInferno", "Skins.Inferno_Toolbot", "CompleteMainEnding", null, "HIFU.Inferno", "com.dotflare.LTT1")] public class ToolbotClearGameInfernoAchievement : BasePerSurvivorClearGameInfernoAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("ToolbotBody"); }
        [RegisterModdedAchievement("EngiClearGameInferno", "Skins.Inferno_Engi", "CompleteMainEnding", null, "HIFU.Inferno", "com.dotflare.LTT1")] public class EngiClearGameInfernoAchievement : BasePerSurvivorClearGameInfernoAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("EngiBody"); }
        [RegisterModdedAchievement("CrocoClearGameInferno", "Skins.Inferno_Croco", "CompleteMainEnding", null, "HIFU.Inferno", "com.dotflare.LTT1")] public class CrocoClearGameInfernoAchievement : BasePerSurvivorClearGameInfernoAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CrocoBody"); }
        [RegisterModdedAchievement("TreebotClearGameInferno", "Skins.Inferno_Treebot", "CompleteMainEnding", null, "HIFU.Inferno", "com.Wolfo.LittleGameplayTweaks")] public class TreebotClearGameInfernoAchievement : BasePerSurvivorClearGameInfernoAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("TreebotBody"); }
        [RegisterModdedAchievement("LoaderClearGameInferno", "Skins.Inferno_Loader", "CompleteMainEnding", null, "HIFU.Inferno", "com.dotflare.LTT1")] public class LoaderClearGameInfernoAchievement : BasePerSurvivorClearGameInfernoAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("LoaderBody"); }
        [RegisterModdedAchievement("TemplarClearGameInferno", "Skins.Inferno_Templar", "CompleteMainEnding", null, "HIFU.Inferno", "prodzpod.TemplarSkins")] public class TemplarClearGameInfernoAchievement : BasePerSurvivorClearGameInfernoAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor"); }
        [RegisterModdedAchievement("VoidSurvivorClearGameInferno", "Skins.Inferno_VoidSurvivor", "CompleteMainEnding", null, "HIFU.Inferno", "com.EmnoX.VoidDreamerVFSKIN")] public class VoidSurvivorClearGameInfernoAchievement : BasePerSurvivorClearGameInfernoAchievement { [SystemInitializer(new Type[] { typeof(HG.Reflection.SearchableAttribute.OptInAttribute) })] public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("VoidSurvivorBody"); }
        public static void MakeUnlockable(string name)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Skins.Inferno_" + name;
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
            AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName).achievedIcon = def.icon;
        }
    }
}

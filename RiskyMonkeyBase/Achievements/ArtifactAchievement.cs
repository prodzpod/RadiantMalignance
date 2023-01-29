using HarmonyLib;
using R2API;
using R2API.ContentManagement;
using R2API.ScriptableObjects;
using RoR2;
using RoR2.Achievements.Artifacts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RiskyMonkeyBase.Achievements
{
    public class ArtifactAchievement
    {
        public static Dictionary<string, UnlockableDef> unlockables;
        public static ArtifactDef blindness;
        public static void Patch()
        {
            unlockables = new();
            if (Reference.Mods("zombieseatflesh7.ArtifactOfPotential")) MakeUnlockable("Potential");
            if (Reference.Mods("CuteDoge.ArtifactOfChosen")) MakeUnlockable("Chosen");
            if (Reference.Mods("com.TPDespair.ZetArtifacts")) MakeZetArtifacts();
            if (Reference.Mods("com.Wolfo.ArtifactOfDissimilarity")) MakeArtifactOfDissimilarity();
            if (Reference.Mods("HIFU.ArtifactOfBlindness"))
            {
                ManagedSerializableContentPack cpack = AccessTools.StaticFieldRefAccess<Dictionary<string, ManagedSerializableContentPack>>(typeof(R2APIContentManager), "BepInModNameToSerializableContentPack")["HIFU.ArtifactOfBlindness"];
                foreach (var def in cpack.serializableContentPack.artifactDefs) if (def.cachedName == "ARTIFACT_HIFU_ArtifactOfBlindness") blindness = def;
                MakeUnlockable("Blindness");
            }
            AchievementManager.onAchievementsRegistered += PostPatch;
        }

        public static void PostPatch()
        {
            if (Reference.Mods("zombieseatflesh7.ArtifactOfPotential")) AddPotential();
            if (Reference.Mods("CuteDoge.ArtifactOfChosen")) AddChosen();
            if (Reference.Mods("com.TPDespair.ZetArtifacts")) AddZetArtifacts();
            if (Reference.Mods("com.Wolfo.ArtifactOfDissimilarity")) AddArtifactOfDissimilarity();
            if (Reference.Mods("HIFU.ArtifactOfBlindness")) AddUnlockable(blindness, "Blindness", 1, 3, 1, 3, 5, 3, 1, 3, 1);
        }

        public static void MakeZetArtifacts()
        {
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.DropifactEnable.Value == 1) MakeUnlockable("Tossing");
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.EarlifactEnable.Value == 1) MakeUnlockable("Sanction");
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.EclifactEnable.Value == 1) MakeUnlockable("Eclipse");
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.HoardifactEnable.Value == 1) MakeUnlockable("Accumulation");
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.LoopifactEnable.Value == 1) MakeUnlockable("Escalation");
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.MultifactEnable.Value == 1) MakeUnlockable("Multitudes");
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.RivivifactEnable.Value == 1) MakeUnlockable("Revival");
        }

        public static void MakeArtifactOfDissimilarity()
        {
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableWanderArtifact.Value) MakeUnlockable("Wander");
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableDissim.Value) MakeUnlockable("Dissimilarity");
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableTransposeArtifact.Value) MakeUnlockable("Transpose");
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableRemodelArtifact.Value) MakeUnlockable("Remodeling");
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableBrigadeArtifact.Value) MakeUnlockable("Brigade");
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableKith.Value) MakeUnlockable("Kith");
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableSpiritualArtifact.Value) MakeUnlockable("Spiriting");
        }

        public static void AddPotential()
        {
            AddUnlockable(ArtifactOfPotential.PotentialArtifact.Potential, "Potential", 7, 3, 3, 7, 5, 7, 3, 3, 7);
        }

        public static void AddChosen()
        {
            AddUnlockable(AOCMod.Artifact.MyArtifactDef, "Chosen", 1, 5, 1, 1, 7, 7, 7, 7, 7);
        }

        public static void AddZetArtifacts()
        {
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.DropifactEnable.Value == 1)  AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetDropifact, "Tossing", 1, 5, 1, 7, 5, 7, 1, 7, 1);
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.EarlifactEnable.Value == 1)  AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetEarlifact, "Sanction", 7, 5, 7, 1, 7, 1, 7, 5, 7);
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.EclifactEnable.Value == 1)   AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetEclifact, "Eclipse", 1, 7, 1, 7, 1, 7, 1, 1, 1);
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.HoardifactEnable.Value == 1) AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetHoardifact, "Accumulation", 7, 5, 7, 1, 7, 1, 7, 7, 7);
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.LoopifactEnable.Value == 1)  AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetLoopifact, "Escalation", 3, 5, 3, 3, 7, 3, 3, 3, 3);
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.MultifactEnable.Value == 1)  AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetMultifact, "Multitudes", 1, 1, 7, 1, 7, 7, 7, 7, 7);
            if (TPDespair.ZetArtifacts.ZetArtifactsPlugin.RivivifactEnable.Value == 1) AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetRevivifact, "Revival", 7, 7, 7, 7, 7, 7, 3, 7, 3);
        }

        public static void AddArtifactOfDissimilarity()
        {
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableWanderArtifact.Value)    AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Wander, "Wander", 3, 7, 3, 7, 7, 7, 3, 7, 3);
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableDissim.Value)            AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Dissimilarity, "Dissimilarity", 1, 1, 5, 1, 1, 1, 5, 1, 1);
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableTransposeArtifact.Value) AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Transpose, "Transpose", 3, 1, 3, 5, 7, 5, 3, 1, 3);
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableRemodelArtifact.Value)   AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Remodeling, "Remodeling", 1, 7, 1, 1, 5, 1, 1, 7, 1);
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableBrigadeArtifact.Value)   AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Brigade, "Brigade", 1, 3, 1, 3, 3, 3, 3, 1, 3);
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableKith.Value)              AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Kith, "Kith", 3, 5, 3, 3, 5, 3, 1, 1, 1);
            if (ArtifactDissimilarity.ArtifactDissimilarity.EnableSpiritualArtifact.Value) AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Spiriting, "Spiriting", 5, 3, 5, 1, 3, 1, 5, 3, 5);
        }

        public static void MakeUnlockable(string name)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Artifacts." + name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            unlockables.Add(name, unlockableDef);
            RiskyMonkeyBase.Log.LogDebug("Registered Unlockable " + name);
        }
        public static void AddUnlockable(ArtifactDef def, string name, int e1, int e2, int e3, int e4, int e5, int e6, int e7, int e8, int e9)
        {
            AddCode(def, e1, e2, e3, e4, e5, e6, e7, e8, e9);
            Sprite icon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/unlocks/texArtifact" + name + ".png");
            UnlockableDef unlockableDef = unlockables[name];
            RiskyMonkeyBase.Log.LogDebug("Fetched Unlockable " + name);
            unlockableDef.nameToken = def.nameToken;
            unlockableDef.achievementIcon = icon;
            def.unlockableDef = unlockableDef;
            PickupCatalog.GetPickupDef(PickupCatalog.FindPickupIndex(def.artifactIndex)).unlockableDef = unlockableDef;
            RuleCatalog.FindRuleDef("Artifacts." + def.cachedName).FindChoice("On").requiredUnlockable = unlockableDef;
            AccessTools.FieldRefAccess<Sprite>(typeof(AchievementDef), "achievedIcon")(AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName)) = icon;
        }

        public static void AddCode(ArtifactDef def, int e1, int e2, int e3, int e4, int e5, int e6, int e7, int e8, int e9)
        {
            ArtifactCode artifactCode = ScriptableObject.CreateInstance<ArtifactCode>();
            artifactCode.topRow = new Vector3Int(e1, e2, e3);
            artifactCode.middleRow = new Vector3Int(e4, e5, e6);
            artifactCode.bottomRow = new Vector3Int(e7, e8, e9);
            ArtifactCodeAPI.AddCode(def, artifactCode);
        }

        [RegisterModdedAchievement("ObtainArtifactPotential", "Artifacts.Potential", null, null, "zombieseatflesh7.ArtifactOfPotential")] public class ObtainPotentialAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactOfPotential.PotentialArtifact.Potential; }
        [RegisterModdedAchievement("ObtainArtifactChosen", "Artifacts.Chosen", null, null, "CuteDoge.ArtifactOfChosen")] public class ObtainChosenAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => AOCMod.Artifact.MyArtifactDef; }
        [RegisterModdedAchievement("ObtainArtifactTossing", "Artifacts.Tossing", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainTossingAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetDropifact; public static bool OnlyRegisterIf() { return TPDespair.ZetArtifacts.ZetArtifactsPlugin.DropifactEnable.Value == 1; } }
        [RegisterModdedAchievement("ObtainArtifactSanction", "Artifacts.Sanction", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainSanctionAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetEarlifact; public static bool OnlyRegisterIf() { return TPDespair.ZetArtifacts.ZetArtifactsPlugin.EarlifactEnable.Value == 1; } }
        [RegisterModdedAchievement("ObtainArtifactEclipse", "Artifacts.Eclipse", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainEclipseAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetEclifact; public static bool OnlyRegisterIf() { return TPDespair.ZetArtifacts.ZetArtifactsPlugin.EclifactEnable.Value == 1; } }
        [RegisterModdedAchievement("ObtainArtifactAccumulation", "Artifacts.Accumulation", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainAccumulationAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetHoardifact; public static bool OnlyRegisterIf() { return TPDespair.ZetArtifacts.ZetArtifactsPlugin.HoardifactEnable.Value == 1; } }
        [RegisterModdedAchievement("ObtainArtifactEscalation", "Artifacts.Escalation", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainEscalationAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetLoopifact; public static bool OnlyRegisterIf() { return TPDespair.ZetArtifacts.ZetArtifactsPlugin.LoopifactEnable.Value == 1; } }
        [RegisterModdedAchievement("ObtainArtifactMultitudes", "Artifacts.Multitudes", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainMultitudesAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetMultifact; public static bool OnlyRegisterIf() { return TPDespair.ZetArtifacts.ZetArtifactsPlugin.MultifactEnable.Value == 1; } }
        [RegisterModdedAchievement("ObtainArtifactRevival", "Artifacts.Revival", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainRevivalAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetRevivifact; public static bool OnlyRegisterIf() { return TPDespair.ZetArtifacts.ZetArtifactsPlugin.RivivifactEnable.Value == 1; } }
        [RegisterModdedAchievement("ObtainArtifactWander", "Artifacts.Wander", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainWanderAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Wander; public static bool OnlyRegisterIf() { return ArtifactDissimilarity.ArtifactDissimilarity.EnableWanderArtifact.Value; } }
        [RegisterModdedAchievement("ObtainArtifactDissimilarity", "Artifacts.Dissimilarity", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainDissimilarityAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Dissimilarity; public static bool OnlyRegisterIf() { return ArtifactDissimilarity.ArtifactDissimilarity.EnableDissim.Value; } }
        [RegisterModdedAchievement("ObtainArtifactTranspose", "Artifacts.Transpose", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainTransposeAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Transpose; public static bool OnlyRegisterIf() { return ArtifactDissimilarity.ArtifactDissimilarity.EnableTransposeArtifact.Value; } }
        [RegisterModdedAchievement("ObtainArtifactRemodeling", "Artifacts.Remodeling", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainRemodelingAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Remodeling; public static bool OnlyRegisterIf() { return ArtifactDissimilarity.ArtifactDissimilarity.EnableRemodelArtifact.Value; } }
        [RegisterModdedAchievement("ObtainArtifactBrigade", "Artifacts.Brigade", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainBrigadeAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Brigade; public static bool OnlyRegisterIf() { return ArtifactDissimilarity.ArtifactDissimilarity.EnableBrigadeArtifact.Value; } }
        [RegisterModdedAchievement("ObtainArtifactKith", "Artifacts.Kith", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainKithAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Kith; public static bool OnlyRegisterIf() { return ArtifactDissimilarity.ArtifactDissimilarity.EnableKith.Value; } }
        [RegisterModdedAchievement("ObtainArtifactSpiriting", "Artifacts.Spiriting", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainSpiritingAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Spiriting; public static bool OnlyRegisterIf() { return ArtifactDissimilarity.ArtifactDissimilarity.EnableSpiritualArtifact.Value; } }
        [RegisterModdedAchievement("ObtainArtifactBlindness", "Artifacts.Blindness", null, null, "HIFU.ArtifactOfBlindness")] public class ObtainBlindnessAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => blindness; }

    }
}

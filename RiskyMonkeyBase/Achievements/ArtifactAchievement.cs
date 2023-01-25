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
            if (Reference.Mods("com.TPDespair.ZetArtifacts"))
            {
                MakeUnlockable("Tossing");
                MakeUnlockable("Sanction");
                MakeUnlockable("Eclipse");
                MakeUnlockable("Accumulation");
                MakeUnlockable("Escalation");
                MakeUnlockable("Multitudes");
                MakeUnlockable("Revival");
            }
            if (Reference.Mods("com.Wolfo.ArtifactOfDissimilarity"))
            {
                MakeUnlockable("Wander");
                MakeUnlockable("Dissimilarity");
                MakeUnlockable("Transpose");
                MakeUnlockable("Remodeling");
                MakeUnlockable("Brigade");
                MakeUnlockable("Kith");
                MakeUnlockable("Spiriting");
            }
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
            if (Reference.Mods("zombieseatflesh7.ArtifactOfPotential"))
            {
                AddCode(ArtifactOfPotential.PotentialArtifact.Potential, 7, 3, 3, 7, 5, 7, 3, 3, 7);
                AddUnlockable(ArtifactOfPotential.PotentialArtifact.Potential, "Potential");
            }
            if (Reference.Mods("CuteDoge.ArtifactOfChosen"))
            {
                AddCode(AOCMod.Artifact.MyArtifactDef, 1, 5, 1, 1, 7, 7, 7, 7, 7);
                AddUnlockable(AOCMod.Artifact.MyArtifactDef, "Chosen");
            }
            if (Reference.Mods("com.TPDespair.ZetArtifacts"))
            {
                AddCode(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetDropifact, 1, 5, 1, 7, 5, 7, 1, 7, 1);
                AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetDropifact, "Tossing");
                AddCode(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetEarlifact, 7, 5, 7, 1, 7, 1, 7, 5, 7);
                AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetEarlifact, "Sanction");
                AddCode(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetEclifact, 1, 7, 1, 7, 7, 7, 1, 1, 1);
                AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetEclifact, "Eclipse");
                AddCode(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetHoardifact, 7, 5, 7, 1, 7, 1, 7, 7, 7);
                AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetHoardifact, "Accumulation");
                AddCode(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetLoopifact, 3, 5, 3, 3, 7, 3, 3, 3, 3);
                AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetLoopifact, "Escalation");
                AddCode(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetMultifact, 1, 1, 7, 1, 7, 7, 7, 7, 7);
                AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetMultifact, "Multitudes");
                AddCode(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetRevivifact, 7, 7, 7, 7, 7, 7, 3, 7, 3);
                AddUnlockable(TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetRevivifact, "Revival");
            }
            if (Reference.Mods("com.Wolfo.ArtifactOfDissimilarity"))
            {
                AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Wander, "Wander");
                AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Dissimilarity, "Dissimilarity");
                AddCode(ArtifactDissimilarity.ArtifactDissimilarity.Transpose, 3, 1, 3, 5, 7, 5, 3, 1, 3);
                AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Transpose, "Transpose");
                AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Remodeling, "Remodeling");
                AddCode(ArtifactDissimilarity.ArtifactDissimilarity.Brigade, 1, 3, 1, 3, 3, 3, 3, 1, 3);
                AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Brigade, "Brigade");
                AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Kith, "Kith");
                AddUnlockable(ArtifactDissimilarity.ArtifactDissimilarity.Spiriting, "Spiriting");
            }
            if (Reference.Mods("HIFU.ArtifactOfBlindness"))
            {
                AddCode(blindness, 1, 3, 1, 3, 5, 3, 1, 3, 1);
                AddUnlockable(blindness, "Blindness");
            }
        }

        public static void MakeUnlockable(string name)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Artifacts." + name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            unlockables.Add(name, unlockableDef);
            RiskyMonkeyBase.Log.LogDebug("Registered Unlockable " + name);
        }
        public static void AddUnlockable(ArtifactDef def, string name)
        {
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
        [RegisterModdedAchievement("ObtainArtifactTossing", "Artifacts.Tossing", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainTossingAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetDropifact; }
        [RegisterModdedAchievement("ObtainArtifactSanction", "Artifacts.Sanction", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainSanctionAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetEarlifact; }
        [RegisterModdedAchievement("ObtainArtifactEclipse", "Artifacts.Eclipse", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainEclipseAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetEclifact; }
        [RegisterModdedAchievement("ObtainArtifactAccumulation", "Artifacts.Accumulation", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainAccumulationAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetHoardifact; }
        [RegisterModdedAchievement("ObtainArtifactEscalation", "Artifacts.Escalation", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainEscalationAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetLoopifact; }
        [RegisterModdedAchievement("ObtainArtifactMultitudes", "Artifacts.Multitudes", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainMultitudesAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetMultifact; }
        [RegisterModdedAchievement("ObtainArtifactRevival", "Artifacts.Revival", null, null, "com.TPDespair.ZetArtifacts")] public class ObtainRevivalAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => TPDespair.ZetArtifacts.ZetArtifactsContent.Artifacts.ZetRevivifact; }
        [RegisterModdedAchievement("ObtainArtifactWander", "Artifacts.Wander", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainWanderAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Wander; }
        [RegisterModdedAchievement("ObtainArtifactDissimilarity", "Artifacts.Dissimilarity", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainDissimilarityAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Dissimilarity; }
        [RegisterModdedAchievement("ObtainArtifactTranspose", "Artifacts.Transpose", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainTransposeAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Transpose; }
        [RegisterModdedAchievement("ObtainArtifactRemodeling", "Artifacts.Remodeling", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainRemodelingAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Remodeling; }
        [RegisterModdedAchievement("ObtainArtifactBrigade", "Artifacts.Brigade", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainBrigadeAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Brigade; }
        [RegisterModdedAchievement("ObtainArtifactKith", "Artifacts.Kith", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainKithAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Kith; }
        [RegisterModdedAchievement("ObtainArtifactSpiriting", "Artifacts.Spiriting", null, null, "com.Wolfo.ArtifactOfDissimilarity")] public class ObtainSpiritingAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => ArtifactDissimilarity.ArtifactDissimilarity.Spiriting; }
        [RegisterModdedAchievement("ObtainArtifactBlindness", "Artifacts.Blindness", null, null, "HIFU.ArtifactOfBlindness")] public class ObtainBlindnessAchievement : BaseObtainArtifactAchievement { public override ArtifactDef artifactDef => blindness; }

    }
}

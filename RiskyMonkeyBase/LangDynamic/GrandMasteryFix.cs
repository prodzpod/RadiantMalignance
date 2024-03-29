﻿using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.Achievements;
using System;
using System.Reflection;

namespace RiskyMonkeyBase.LangDynamic
{
    public class GrandMasteryFix
    {   
        public static string[] Survivors = { "PALADIN_TYPHOONUNLOCKABLE", "MINER_TYPHOONUNLOCKABLE", "SNIPERCLASSIC_GRANDMASTERYUNLOCKABLE", "ENFORCER_GRANDMASTERYUNLOCKABLE", "NEMFORCER_TYPHOONUNLOCKABLE" }; // guh
        public static void Patch()
        {
            RiskyMonkeyBase.Log.LogInfo("[[Paladin grand mastery description fix]] module loaded");
            On.RoR2.Language.GetLocalizedStringByToken += (orig, self, token) =>
            {
                for (var i = 0; i < Survivors.Length; i++)
                {
                    if (token.StartsWith(Survivors[i]))
                    {
                        if ((token == Survivors[i] + "_ACHIEVEMENT_NAME" || token == Survivors[i] + "_UNLOCKABLE_NAME") && Reference.GrandDifficulty.Value == "INFERNO_NAME") return orig(self, Survivors[i] + "_ACHIEVEMENT_NAME_INFERNO");
                        if (token == Survivors[i] + "_ACHIEVEMENT_DESC" || token == Survivors[i] + "_UNLOCKABLE_DESC") return string.Format(orig(self, Survivors[i] + "_ACHIEVEMENT_DESC_HEADER"), orig(self, Reference.GrandDifficulty.Value));
                    }
                }
                return orig(self, token);
            };
            RiskyMonkeyBase.Log.LogDebug("Disabling Eclipse Check (this means harmony patching), Jank inbound...");
            if (Reference.Mods("com.rob.Paladin")) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchPaladin));
            if (Reference.Mods("com.rob.DiggerUnearthed")) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchMiner));
            if (Reference.Mods("com.Moffein.SniperClassic")) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchSniper));
            if (Reference.Mods("com.EnforcerGang.Enforcer")) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchEnforcer));
            if (Reference.Mods("com.TeamMoonstorm.Starstorm2-Nightly")) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchSS2));
        }

        [HarmonyPatch(typeof(PaladinMod.Achievements.BaseMasteryUnlockable), nameof(PaladinMod.Achievements.BaseMasteryUnlockable.OnClientGameOverGlobal))]
        public static class PatchPaladin
        {
            public static bool Prefix(PaladinMod.Achievements.BaseMasteryUnlockable __instance, Run run, RunReport runReport    )
            {
                if (__instance.RequiredDifficultyCoefficient >= 3.5f)
                {
                    if ((bool)runReport.gameEnding && runReport.gameEnding.isWin && DifficultyCatalog.GetDifficultyDef(runReport.ruleBook.FindDifficulty()).nameToken == Reference.GrandDifficulty.Value) __instance.Grant();
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(DiggerPlugin.Achievements.BaseMasteryUnlockable), nameof(DiggerPlugin.Achievements.BaseMasteryUnlockable.OnClientGameOverGlobal))]
        public static class PatchMiner
        {
            public static bool Prefix(PaladinMod.Achievements.BaseMasteryUnlockable __instance, Run run, RunReport runReport)
            {
                if (__instance.RequiredDifficultyCoefficient >= 3.5f)
                {
                    if ((bool)runReport.gameEnding && runReport.gameEnding.isWin && DifficultyCatalog.GetDifficultyDef(runReport.ruleBook.FindDifficulty()).nameToken == Reference.GrandDifficulty.Value) __instance.Grant();
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(SniperClassic.Modules.Achievements.BaseMasteryUnlockable), nameof(SniperClassic.Modules.Achievements.BaseMasteryUnlockable.OnClientGameOverGlobal))]
        public static class PatchSniper
        {
            public static bool Prefix(PaladinMod.Achievements.BaseMasteryUnlockable __instance, Run run, RunReport runReport)
            {
                if (__instance.RequiredDifficultyCoefficient >= 3.5f)
                {
                    if ((bool)runReport.gameEnding && runReport.gameEnding.isWin && DifficultyCatalog.GetDifficultyDef(runReport.ruleBook.FindDifficulty()).nameToken == Reference.GrandDifficulty.Value) __instance.Grant();
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(EnforcerPlugin.Achievements.BaseMasteryUnlockable), nameof(EnforcerPlugin.Achievements.BaseMasteryUnlockable.OnClientGameOverGlobal))]
        public static class PatchEnforcer
        {
            public static bool Prefix(PaladinMod.Achievements.BaseMasteryUnlockable __instance, Run run, RunReport runReport)
            {
                if (__instance.RequiredDifficultyCoefficient >= 3.5f)
                {
                    if ((bool)runReport.gameEnding && runReport.gameEnding.isWin && DifficultyCatalog.GetDifficultyDef(runReport.ruleBook.FindDifficulty()).nameToken == Reference.GrandDifficulty.Value) __instance.Grant();
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(Moonstorm.Starstorm2.Unlocks.GenericMasteryAchievement), nameof(Moonstorm.Starstorm2.Unlocks.GenericMasteryAchievement.OnClientGameOverGlobal))]
        public static class PatchSS2
        {
            public static bool Prefix(Moonstorm.Starstorm2.Unlocks.GenericMasteryAchievement __instance, Run run, RunReport runReport)
            {
                if (__instance.RequiredDifficultyCoefficient >= 3.5f)
                {
                    if ((bool)runReport.gameEnding && runReport.gameEnding.isWin && DifficultyCatalog.GetDifficultyDef(runReport.ruleBook.FindDifficulty()).nameToken == Reference.GrandDifficulty.Value) __instance.Grant();
                    return false;
                }
                return true;
            }
        }
    }
}

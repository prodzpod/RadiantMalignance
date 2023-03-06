using HarmonyLib;
using R2API;
using RiskyMonkeyBase.Tweaks;
using RoR2;
using RoR2.Achievements;
using System.Collections.Generic;
using UnityEngine;

namespace RiskyMonkeyBase.Achievements
{
    public class SurvivorAchievement
    {
        public static Dictionary<string, UnlockableDef> unlockables;
        public static void Patch()
        {
            unlockables = new();
            if (Reference.Mods("PlasmaCore.ForgottenRelics")) MakeForgottenRelics();
            AchievementManager.onAchievementsRegistered += PostPatch;
        }

        public static void MakeForgottenRelics()
        {
            if (!FRCSharp.VF2ConfigManager.disableForgottenHaven.Value) MakeUnlockable("Railgunner");
            RiskyMonkeyBase.Harmony.PatchAll(typeof(ForgottenRelicsTweaks.PatchBatteryContainer));
        }

        public static void PostPatch()
        {
            if (Reference.Mods("PlasmaCore.ForgottenRelics")) AddForgottenRelics();
            if (Reference.BetterEngiAchievement.Value && !RiskyMonkeyAchievements.achievementBlacklist.Contains("Items.Squid"))
            {
                UnlockableDef unlock = RoR2Content.Items.Squid.unlockableDef;
                Sprite icon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/unlocks/texSurvivorEngineer.png");
                unlock.achievementIcon = icon;
                RoR2Content.Survivors.Engi.unlockableDef = unlock;
                AchievementManager.GetAchievementDefFromUnlockable(unlock.cachedName).achievedIcon = icon;
                RoR2Content.Items.Squid.unlockableDef = null;
                AutomationActivation.requirement = 3;
                On.RoR2.Language.GetLocalizedStringByToken += (orig, self, token) =>
                {
                    if (token == "ACHIEVEMENT_AUTOMATIONACTIVATION_DESCRIPTION") return orig(self, token).Replace("6", "3");
                    return orig(self, token);
                };
            }
        }

        public static void AddForgottenRelics()
        {
            if (!FRCSharp.VF2ConfigManager.disableForgottenHaven.Value) AddUnlockable("RailgunnerBody", "Railgunner");
        }

        public static void MakeUnlockable(string name)
        {
            if (RiskyMonkeyAchievements.achievementBlacklist.Contains("Characters." + name)) return;
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Characters." + name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            unlockables.Add(name, unlockableDef);
            RiskyMonkeyAchievements.Log("Registered Unlockable " + name);
        }
        public static void AddUnlockable(string bodyName, string name)
        {
            if (RiskyMonkeyAchievements.achievementBlacklist.Contains("Characters." + name)) return;
            Sprite icon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/unlocks/texSurvivor" + name + ".png");
            UnlockableDef unlockableDef = unlockables[name];
            RiskyMonkeyAchievements.Log("Fetched Unlockable " + name);
            SurvivorDef def = SurvivorCatalog.GetSurvivorDef(SurvivorCatalog.GetSurvivorIndexFromBodyIndex(BodyCatalog.FindBodyIndex(bodyName)));
            unlockableDef.nameToken = def.displayNameToken;
            unlockableDef.achievementIcon = icon;
            def.unlockableDef = unlockableDef;
            AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName).achievedIcon = icon;
        }
    }
}

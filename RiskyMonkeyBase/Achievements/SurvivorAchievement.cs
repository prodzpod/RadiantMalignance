﻿using HarmonyLib;
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
            if (Reference.Mods("PlasmaCore.ForgottenRelics"))
            {
                MakeUnlockable("Railgunner");
                RiskyMonkeyBase.Harmony.PatchAll(typeof(ForgottenRelicsTweaks.PatchBatteryContainer));
            }
            if (Reference.Mods("com.Tymmey.Templar")) MakeUnlockable("Templar");
            AchievementManager.onAchievementsRegistered += PostPatch;
        }

        public static void PostPatch()
        {
            if (Reference.Mods("PlasmaCore.ForgottenRelics")) AddUnlockable("RailgunnerBody", "Railgunner");
            if (Reference.Mods("com.Tymmey.Templar")) AddUnlockable("Templar_Survivor", "Templar");
        }

        public static void MakeUnlockable(string name)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Characters." + name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            unlockables.Add(name, unlockableDef);
            RiskyMonkeyBase.Log.LogDebug("Registered Unlockable " + name);
        }
        public static void AddUnlockable(string bodyName, string name)
        {
            Sprite icon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/unlocks/texSurvivor" + name + ".png");
            UnlockableDef unlockableDef = unlockables[name];
            RiskyMonkeyBase.Log.LogDebug("Fetched Unlockable " + name);
            SurvivorDef def = SurvivorCatalog.GetSurvivorDef(SurvivorCatalog.GetSurvivorIndexFromBodyIndex(BodyCatalog.FindBodyIndex(bodyName)));
            unlockableDef.nameToken = def.displayNameToken;
            unlockableDef.achievementIcon = icon;
            def.unlockableDef = unlockableDef;
            AccessTools.FieldRefAccess<Sprite>(typeof(AchievementDef), "achievedIcon")(AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName)) = icon;
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Characters_Templar", "Characters.Templar", null, null, "com.Tymmey.Templar")]
    public class TemplarAchievement : BaseAchievement
    {
        public override void OnInstall() { base.OnInstall(); On.EntityStates.Missions.Goldshores.Exit.OnEnter += OnEnter; }
        public override void OnUninstall() { On.EntityStates.Missions.Goldshores.Exit.OnEnter -= OnEnter; base.OnUninstall(); }

        private void OnEnter(On.EntityStates.Missions.Goldshores.Exit.orig_OnEnter orig, EntityStates.Missions.Goldshores.Exit self)
        {
            Grant();
            orig(self);
        }
    }

}

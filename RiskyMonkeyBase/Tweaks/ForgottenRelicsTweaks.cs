using FRCSharp;
using HarmonyLib;
using R2API;
using RiskyMonkeyBase.Achievements;
using RoR2;
using RoR2.Achievements;
using System;
using UnityEngine;

namespace RiskyMonkeyBase.Tweaks
{
    public class ForgottenRelicsTweaks
    {
        public static void ForgottenRelics()
        {
            PatchBatteryContainer.onStaticPortalActivated += () => { };
        }

        [HarmonyPatch(typeof(BatteryContainerInteraction), nameof(BatteryContainerInteraction.Begin))]
        public class PatchBatteryContainer
        {
            public static event Action onStaticPortalActivated;
            public static void Postfix() { if (onStaticPortalActivated != null) onStaticPortalActivated(); }
        }


        [RegisterModdedAchievement("RiskyMonkey_Characters_Railgunner", "Characters.Railgunner", null, null, "PlasmaCore.ForgottenRelics")]
        public class RailgunnerAchievement : BaseAchievement
        {
            public static bool OnlyRegisterIf() { return !VF2ConfigManager.disableForgottenHaven.Value; }
            public override void OnInstall() { base.OnInstall(); PatchBatteryContainer.onStaticPortalActivated += OnStaticPortalActivated; }
            public override void OnUninstall() { PatchBatteryContainer.onStaticPortalActivated -= OnStaticPortalActivated; base.OnUninstall(); }
            public void OnStaticPortalActivated()
            {
                if (localUser != null && localUser.cachedBody != null) Grant();
            }
        }
    }
}

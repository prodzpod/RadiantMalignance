using FRCSharp;
using HarmonyLib;
using RiskyMonkeyBase.Achievements;
using RoR2.Achievements;
using RoR2.ContentManagement;
using System;
using UnityEngine;

namespace RiskyMonkeyBase.Tweaks
{
    public class ForgottenRelicsTweaks
    {
        public static void ForgottenRelics()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchVF2Start));
            if (!VF2ConfigManager.disableCoilGolem.Value) AccessTools.StaticFieldRefAccess<VF2ContentPackProvider, ContentPack>("contentPack").bodyPrefabs.Add(new GameObject[] { VF2ContentPackProvider.coilGolemBody }); // silly
        }

        [HarmonyPatch(typeof(VF2Plugin), "Start")]
        public class PatchVF2Start
        {
            public static bool trueDisableBellTower;
            public static void Prefix()
            {
                trueDisableBellTower = VF2ConfigManager.disableBellTower.Value;
            }
            public static void Postfix()
            {
                VF2ConfigManager.disableBellTower.Value = trueDisableBellTower;
            }
        }

        [HarmonyPatch(typeof(BatteryContainerInteraction), nameof(BatteryContainerInteraction.Begin))]
        public class PatchBatteryContainer
        {
            public static event Action onStaticPortalActivated;
            public static void Postfix()
            {
                onStaticPortalActivated();
            }
        }


        [RegisterModdedAchievement("RiskyMonkey_Characters_Railgunner", "Characters.Railgunner", null, null, "PlasmaCore.ForgottenRelics")]
        public class RailgunnerAchievement : BaseAchievement
        {
            public static bool OnlyRegisterIf() { return !VF2ConfigManager.disableForgottenHaven.Value; }
            public override void OnInstall() { base.OnInstall(); PatchBatteryContainer.onStaticPortalActivated += Grant; }
            public override void OnUninstall() { PatchBatteryContainer.onStaticPortalActivated -= Grant; base.OnUninstall(); }
        }
    }
}

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
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchVF2Start));
            if (!VF2ConfigManager.disableBellTower.Value) ReaddBT();
            if (!VF2ConfigManager.disableCoilGolem.Value) VF2ContentPackProvider.contentPack.bodyPrefabs.Add(new GameObject[] { VF2ContentPackProvider.coilGolemBody }); // silly
        }
        
        public static void ReaddBT()
        {
            DirectorCard card = new DirectorCard()
            {
                spawnCard = VF2ContentPackProvider.cscBellTower,
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            foreach (var stage in new DirectorAPI.Stage[] {
                DirectorAPI.Stage.AbyssalDepths,
                DirectorAPI.Stage.AbyssalDepthsSimulacrum,
                DirectorAPI.Stage.SirensCall,
                DirectorAPI.Stage.SkyMeadow,
                DirectorAPI.Stage.SkyMeadowSimulacrum,
                DirectorAPI.Stage.WetlandAspect
            })
            {
                DirectorAPI.Helpers.AddNewMonsterToStage(card, DirectorAPI.MonsterCategory.Champions, stage);
            }
        }

        [HarmonyPatch(typeof(VF2Plugin), "Start")]
        public class PatchVF2Start
        {
            public static bool trueDisableBellTower;
            public static void Prefix() { trueDisableBellTower = VF2ConfigManager.disableBellTower.Value; }
            public static void Postfix() { VF2ConfigManager.disableBellTower.Value = trueDisableBellTower; }
        }

        [HarmonyPatch(typeof(BatteryContainerInteraction), nameof(BatteryContainerInteraction.Begin))]
        public class PatchBatteryContainer
        {
            public static event Action onStaticPortalActivated;
            public static void Postfix()
            {
                if (onStaticPortalActivated != null) onStaticPortalActivated();
            }
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

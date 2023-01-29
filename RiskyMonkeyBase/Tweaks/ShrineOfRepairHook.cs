using HarmonyLib;
using RiskyMonkeyBase.Contents;
using RoR2;
using ShrineOfRepair.Modules.Interactables;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace RiskyMonkeyBase.Tweaks
{
    public class ShrineOfRepairHook
    {
        public static event Action<Interactor, PickupIndex> onRepair;
        public static void Patch()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchPurchase));
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchPicker));
        }

        [HarmonyPatch(typeof(ShrineOfRepairPurchase.RepairShrineManager), nameof(ShrineOfRepairPurchase.RepairShrineManager.RepairPurchaseAttempt))]
        public class PatchPurchase
        {
            public static void Prefix(Interactor interactor)
            {
                if (!NetworkServer.active) return;
                CharacterBody body = interactor.GetComponent<CharacterBody>();
                if (body == null || body.master == null) return;
                ShrineOfRepairPurchase.FillRepairItemsDictionary();
                foreach (KeyValuePair<ItemIndex, ItemIndex> pairedItems in ShrineOfRepairPurchase.RepairItemsDictionary)
                    if (body.inventory.GetItemCount(pairedItems.Key) > 0) onRepair(interactor, PickupCatalog.FindPickupIndex(pairedItems.Key));
                if (ShrineOfRepairPurchase.RepairEquipmentsDictionary.ContainsKey(body.equipmentSlot.equipmentIndex)) onRepair(interactor, PickupCatalog.FindPickupIndex(body.equipmentSlot.equipmentIndex));
                Reprogrammer.detected = false;
            }
        }

        [HarmonyPatch(typeof(ShrineOfRepairPicker.ShrineRepairManager), nameof(ShrineOfRepairPicker.ShrineRepairManager.HandleSelection))]
        public class PatchPicker
        {
            public static void Prefix(ShrineOfRepairPicker.ShrineRepairManager __instance, int selection)
            {
                if (!NetworkServer.active) return;
                Interactor interactor = AccessTools.FieldRefAccess<ShrineOfRepairPicker.ShrineRepairManager, Interactor>("interactor")(__instance);
                if (interactor != null && onRepair != null) onRepair(interactor, new PickupIndex(selection));
            }
        }
    }
}

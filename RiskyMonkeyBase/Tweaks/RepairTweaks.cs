using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace RiskyMonkeyBase.Tweaks
{
    public class RepairTweaks
    {
        public static Dictionary<GameObject, int> uses;
        public static List<string> repairList;

        // "stolen" from shrineofdisorder @ https://thunderstore.io/package/cbrl/ShrineOfDisorder/
        public static void PatchFrequency()
        {
            SceneDirector.onGenerateInteractableCardSelection += (director, selection) =>
            {
                if (!NetworkServer.active) return;
                int index = GetCategoryIndex(selection, "iscScrapper");
                if (index >= 0 && GetCard(selection, index, "iscScrapper") != null)
                {
                    int num = GetCard(selection, index, "iscScrapper").selectionWeight;
                    DirectorCard card = new()
                    {
                        spawnCard = LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/InteractableSpawnCard/iscScrapper"),
                        selectionWeight = (int)(num * Reference.ScrapperFrequency.Value)
                    };
                    selection.AddCard(index, card);
                }
            };
        }

        public static int GetCategoryIndex(DirectorCardCategorySelection dccs, string name)
        {
            for (int i = 0; i < dccs.categories.Length; ++i) foreach (DirectorCard card in dccs.categories[i].cards) if (card.spawnCard.name.StartsWith(name)) return i;
            return -1;
        }
        public static DirectorCard GetCard(DirectorCardCategorySelection dccs, int category, string name)
        {
            foreach (DirectorCard card in dccs.categories[category].cards)
                if (card.spawnCard.name.StartsWith(name)) return card;
            return null;
        }

        public static void PatchUses()
        {
            uses = new();
            On.EntityStates.Scrapper.ScrapperBaseState.OnEnter += (orig, self) =>
            {
                GameObject scrapper = self.outer.gameObject;
                if (!uses.ContainsKey(scrapper)) uses.Add(scrapper, Reference.ScrapperMaxUses.Value);
                orig(self);
                if (uses[scrapper] <= 0) self.outer.GetComponent<PickupPickerController>().SetAvailable(false);
            };
            On.EntityStates.Scrapper.Scrapping.OnEnter += (orig, self) =>
            {
                GameObject scrapper = self.outer.gameObject;
                if (scrapper != null && uses.ContainsKey(scrapper)) uses[scrapper]--;
                orig(self);
            };
        }

        public static void PatchScrapper()
        {
            On.RoR2.ScrapperController.Start += (orig, self) =>
            {
                self.maxItemsToScrapAtATime = Reference.ScrapperStackAtOnce.Value;
                orig(self);
            };
        }

        public static void PatchRepair()
        {
            repairList = new();
            foreach (var entry in Reference.RepairRepairList.Value.Split(',')) repairList.Add(entry.Trim());
            RiskyMonkeyBase.Log.LogDebug("Repair Repair List: " + repairList.Join());
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchStart));
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchSelection));
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchInteraction));
        }

        [HarmonyPatch(typeof(ShrineOfRepair.Modules.Interactables.ShrineOfRepairPicker.ShrineRepairManager), "<Start>b__8_0")]
        public class PatchStart
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchStloc(3));
                c.Index++;
                c.Emit(OpCodes.Ldloc_0); // pickupdef
                c.Emit(OpCodes.Ldloc_3); // itemcount
                c.EmitDelegate<Func<PickupDef, int, int>>((pickupDef, count) =>
                {
                    if (repairList.Contains(ItemCatalog.GetItemDef(pickupDef.itemIndex).name)) return Mathf.Min(count, Reference.RepairStackAtOnce.Value);
                    return count;
                });
                c.Emit(OpCodes.Stloc_3);
            }
        }

        [HarmonyPatch(typeof(ShrineOfRepair.Modules.Interactables.ShrineOfRepairPicker.ShrineRepairManager), "HandleSelection", typeof(int))]
        public class PatchSelection
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchStloc(2));
                c.Index++;
                c.Emit(OpCodes.Ldloc_0); // pickupdef
                c.Emit(OpCodes.Ldloc_2); // itemcount
                c.EmitDelegate<Func<PickupDef, int, int>>((pickupDef, count) =>
                {
                    if (repairList.Contains(ItemCatalog.GetItemDef(pickupDef.itemIndex).name)) return Mathf.Min(count, Reference.RepairStackAtOnce.Value);
                    return count;
                });
                c.Emit(OpCodes.Stloc_2);
            }
        }

        [HarmonyPatch(typeof(ShrineOfRepair.Modules.Interactables.ShrineOfRepairPicker.ShrineRepairManager), "HandleInteraction", typeof(Interactor))]
        public class PatchInteraction
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchStloc(3));
                c.Index++;
                c.Emit(OpCodes.Ldloc, 4); // repairItems
                c.Emit(OpCodes.Ldloc, 5); // itemcount
                c.EmitDelegate<Func<KeyValuePair<ItemIndex, ItemIndex>, int, int>>((repairItems, count) =>
                {
                    if (repairList.Contains(ItemCatalog.GetItemDef(repairItems.Key).name)) return Mathf.Min(count, Reference.RepairStackAtOnce.Value);
                    return count;
                });
                c.Emit(OpCodes.Stloc, 5);
            }
        }
    }
}

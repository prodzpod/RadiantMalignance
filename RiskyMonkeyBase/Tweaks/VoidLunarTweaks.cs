using BepInEx.Configuration;
using BubbetsItems;
using BubbetsItems.Equipments;
using BubbetsItems.Items;
using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RiskyMonkeyBase.LangDynamic;
using RoR2;
using RoR2.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RiskyMonkeyBase.Tweaks
{
    public class VoidLunarTweaks
    {
        public static WeightedSelection<PickupIndex> LunarShopList = new();
        public static void Bazaar()
        {
            Stage.onStageStartGlobal += (self) =>
            {
                GameObject lvs = GameObject.Find("LunarVoidShop(Clone)");
                if (lvs != null) lvs.SetActive(false);
            };
            RoR2Application.onLoad += () =>
            {
                foreach (var lunar in ItemCatalog.lunarItemList) LunarShopList.AddChoice(new() { value = PickupCatalog.FindPickupIndex(lunar), weight = 1 });
                foreach (var equip in EquipmentCatalog.equipmentList) if (EquipmentCatalog.GetEquipmentDef(equip).isLunar) 
                        LunarShopList.AddChoice(new() { value = PickupCatalog.FindPickupIndex(equip), weight = 1 });
                foreach (var item in ItemCatalog.allItemDefs) if (item.tier == BubbetsItemsPlugin.VoidLunarTier.tier)
                        LunarShopList.AddChoice(new() { value = PickupCatalog.FindPickupIndex(item.itemIndex), weight = Reference.VoidLunarOnBazaar.Value });
            };
            IL.RoR2.ShopTerminalBehavior.GenerateNewPickupServer_bool += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchCallOrCallvirt<PickupDropTable>(nameof(PickupDropTable.GenerateDrop)));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<PickupIndex, ShopTerminalBehavior, PickupIndex>>((orig, self) =>
                {
                    if (self.gameObject.name.Contains("LunarShopTerminal")) return PickupDropTable.GenerateDropFromWeightedSelection(self.rng, LunarShopList);
                    return orig;
                });
            };
        }
        public static void Donkey()
        {
            Run.onRunSetRuleBookGlobal += (run, ruleBook) =>
            {
                EquipmentIndex idx = EquipmentCatalog.FindEquipmentIndex("EquipmentDefHolographicDonkey");
                if (idx != EquipmentIndex.None && run.availableEquipment.Contains(idx))
                {
                    run.availableEquipment.Remove(idx);
                    PickupDropTable.RegenerateAll(run);
                }
            };
        }

        public static bool isVoidLunar(ItemTier tier)
        {
            return tier == BubbetsItemsPlugin.VoidLunarTier.tier;
        }
    }
}

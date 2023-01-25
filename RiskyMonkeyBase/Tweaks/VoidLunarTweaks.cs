using BepInEx.Configuration;
using BubbetsItems;
using BubbetsItems.Equipments;
using BubbetsItems.Items;
using HarmonyLib;
using RiskyMonkeyBase.LangDynamic;
using RoR2;
using RoR2.Items;
using System.Collections.Generic;
using System.Linq;

namespace RiskyMonkeyBase.Tweaks
{
    public class VoidLunarTweaks
    {
        public static Dictionary<ItemIndex, ItemIndex> VoidLunarConversions = new();
        public static void Bazaar()
        {
            On.RoR2.Items.ContagiousItemManager.Init += (orig) =>
            {
                orig();
                ItemIndex itemIndex = ~ItemIndex.None;
                for (ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount; itemIndex < itemCount; ++itemIndex)
                {
                    ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
                    if (itemDef.tier == BubbetsItemsPlugin.VoidLunarTier.tier)
                    {
                        ItemIndex originalItemIndex = ContagiousItemManager.GetOriginalItemIndex(itemDef.itemIndex);
                        if (!VoidLunarConversions.ContainsKey(originalItemIndex)) VoidLunarConversions.Add(originalItemIndex, itemDef.itemIndex);
                    }
                }
            };
            On.RoR2.ShopTerminalBehavior.SetPickupIndex += (orig, self, newPickupIndex, hidden) =>
            {
                if (self.GetComponentInParent<PurchaseInteraction>().name.StartsWith("LunarShop"))
                {
                    PickupDef pickup = PickupCatalog.GetPickupDef(self.NetworkpickupIndex);
                    if (pickup != null && VoidLunarConversions.ContainsKey(pickup.itemIndex) && Run.instance.stageRng.RangeFloat(0, 1) < Reference.VoidLunarOnBazaar.Value)
                    {
                        RiskyMonkeyBase.Log.LogDebug("Voiding the Bazaar...");
                        PickupIndex ret = PickupCatalog.FindPickupIndex(VoidLunarConversions[pickup.itemIndex]);
                        orig(self, ret, hidden); // since hook is guarded
                        return;
                    }
                }
                orig(self, newPickupIndex, hidden);
            };
        }
        public static void CleansingPool()
        {
            On.RoR2.ShrineCleanseBehavior.Init += (orig) =>
            {
                orig();
                List<ItemIndex> itemIndexList = new List<ItemIndex>(AccessTools.StaticFieldRefAccess<ShrineCleanseBehavior, ItemIndex[]>("cleansableItems"));
                ItemIndex itemIndex = ~ItemIndex.None;
                for (ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount; itemIndex < itemCount; ++itemIndex)
                {
                    ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
                    if (isVoidLunar(itemDef.tier)) itemIndexList.Add(itemIndex);
                }
                AccessTools.StaticFieldRefAccess<ShrineCleanseBehavior, ItemIndex[]>("cleansableItems") = itemIndexList.ToArray();
            };
            On.RoR2.ShrineCleanseBehavior.InventoryIsCleansable += (orig, self) =>
            {
                RiskyMonkeyBase.Log.LogDebug(AccessTools.StaticFieldRefAccess<ShrineCleanseBehavior, ItemIndex[]>("cleansableItems").Length);
                return orig(self);
            };
            On.RoR2.CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.Init += (orig) =>
            {
                orig();
                List<ItemIndex> indicies = new();
                indicies.AddRange(ItemCatalog.lunarItemList);
                ItemIndex itemIndex = ~ItemIndex.None;
                for (ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount; itemIndex < itemCount; ++itemIndex)
                {
                    ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
                    if (isVoidLunar(itemDef.tier)) indicies.Add(itemDef.itemIndex);
                }
                AccessTools.StaticFieldRefAccess<ItemIndex[]>(typeof(CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper), "lunarItemIndices") = indicies.ToArray();
            };
        }
        public static void Donkey()
        {
            EquipmentBase.Equipments.FirstOrDefault(x => x is HolographicDonkey).Enabled.Value = !Reference.SeriousMode.Value; // donkey
        }

        public static bool isVoidLunar(ItemTier tier)
        {
            return tier == BubbetsItemsPlugin.VoidLunarTier.tier;
        }
        public static void handleRepulsionMK2()
        {
            var isReduction = AccessTools.StaticFieldRefAccess<ConfigEntry<bool>>(typeof(RepulsionPlateMk2), "_reductionOnTrue").Value; // private config value lol
            RiskyMonkeyBase.Log.LogInfo("Repulsion Armor MK2 desc setting, Reduction: " + isReduction);
            BetterDesc.keyRepl.Add("REPULSION_ARMOR_MK2_PICKUP", isReduction ? "REPULSION_ARMOR_MK2_DESC_REDUCTION_SIMPLE" : "REPULSION_ARMOR_MK2_DESC_ARMOR_SIMPLE");
        }
    }
}

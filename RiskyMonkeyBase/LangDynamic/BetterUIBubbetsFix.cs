using BetterUI;
using BubbetsItems;
using RoR2;
using System.Collections.Generic;

namespace RiskyMonkeyBase.LangDynamic
{
    public class BetterUIBubbetsFix
    {
        public static void Patch()
        {
            /*
            foreach (var item in ItemBase.Items) items.Add(item.ItemDef.name, item);
            if (ConfigManager.MiscAdvancedPickupNotificationsItems.Value) On.RoR2.UI.GenericNotification.SetItem += (orig, self, def) =>
            {
                orig(self, def);
                if (items.ContainsKey(def.name)) self.descriptionText.token = items[def.name].GetFormattedDescription(LocalUserManager.GetFirstLocalUser().cachedBody.inventory);
            };
            if (ConfigManager.MiscShowPickupDescription.Value && ConfigManager.MiscPickupDescriptionAdvanced.Value) On.RoR2.GenericPickupController.GetContextString += (orig, self, activator) =>
            {
                PickupDef pickupDef = PickupCatalog.GetPickupDef(self.pickupIndex); 
                if (pickupDef.itemIndex != ItemIndex.None)
                {
                    ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);
                    if (items.ContainsKey(itemDef.name)) return orig(self, activator).Replace(Language.GetString(itemDef.descriptionToken), items[itemDef.name].GetFormattedDescription(LocalUserManager.GetFirstLocalUser().cachedBody.inventory));
                }
                return orig(self, activator);
            };
            */
        }
    }
}

using BepInEx.Bootstrap;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskyMonkeyBase.Contents
{
    public class WhiteGuillotine2
    {
        public static void PatchExeblade()
        {
            // oh my God which item mod template did this why is Nothing static i hate all of you
            vanillaVoid.Items.ItemBase exeblade = (Chainloader.PluginInfos[vanillaVoid.vanillaVoidPlugin.ModGuid].Instance as vanillaVoid.vanillaVoidPlugin).Items.Find(x => x.ItemLangTokenName == "EXEBLADE_ITEM");
            if (exeblade != null) exeblade.ItemDef._itemTierDef = ItemTierCatalog.GetItemTierDef(ItemTier.VoidTier1);
        }
    }
}

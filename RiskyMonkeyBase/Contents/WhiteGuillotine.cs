using BepInEx.Bootstrap;
using RoR2;
using UnityEngine;

namespace RiskyMonkeyBase.Contents
{
    public class WhiteGuillotine
    {
        public static void Patch()
        {
            On.RoR2.ItemCatalog.SetItemDefs += (orig, defs) =>
            {
                RoR2Content.Items.ExecuteLowHealthElite._itemTierDef = ItemTierCatalog.GetItemTierDef(ItemTier.Tier1);
                RoR2Content.Items.ExecuteLowHealthElite.pickupIconSprite = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/texExecuteLowHealthElite.png");
                RoR2Content.Items.ExecuteLowHealthElite.unlockableDef.achievementIcon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/unlocks/texItemExecuteLowHealthElite.png");
                orig(defs);
            };
            On.RoR2.ItemCatalog.SetItemDefs += (orig, defs) => { if (Reference.Mods("com.Zenithrium.vanillaVoid")) WhiteGuillotine2.PatchExeblade(); orig(defs); };
        }
    }
}

using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RiskyMonkeyBase.Tweaks
{
    public class WolfoTweaksTweaks
    {
        public static void Patch()
        {
            if (Reference.HEADSTChanges.Value) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchHeadstompers));
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchFeathers));
        }

        [HarmonyPatch(typeof(WolfoQualityOfLife.WolfoQualityOfLife), nameof(WolfoQualityOfLife.WolfoQualityOfLife.StupidHeadStomper))]
        public class PatchHeadstompers
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdcR4(10f));
                c.Remove();
                c.Emit(OpCodes.Ldc_R4, 0f);
            }
        }

        [HarmonyPatch]
        public class PatchFeathers
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchStloc(3));
                c.Index++;
                c.Emit(OpCodes.Ldloc_1);
                c.EmitDelegate<Func<CharacterBody, int>>(body =>
                {
                    int ret = body.inventory.GetItemCount(RoR2Content.Items.Feather);
                    foreach (var str in new string[] { "ZetAspectRed", "VV_ITEM_DASHQUILL_ITEM" }) {
                        if (ItemCatalog.FindItemIndex(str) != ItemIndex.None) ret += body.inventory.GetItemCount(ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(str)));
                    }
                    if (ItemCatalog.FindItemIndex("MintCondition") != ItemIndex.None)
                    {
                        int count = body.inventory.GetItemCount(ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("MintCondition")));
                        if (count > 0) ret += (count * 2) - 1;
                    }
                    return ret;
                });
                c.Emit(OpCodes.Stloc_3);
            }

            // bless you aaron
            public static MethodBase TargetMethod()
            {
                return AccessTools.DeclaredMethod(typeof(WolfoQualityOfLife.WolfoQualityOfLife).GetNestedType("<>c", AccessTools.all), "<BuffColorChanger>b__361_11");
            }
        }
    }
}

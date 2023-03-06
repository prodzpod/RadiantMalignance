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
        public static Dictionary<string, Func<int, int>> featherInteraction = new();
        public static void Patch()
        {
            if (Reference.HEADSTChanges.Value) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchHeadstompers));
            featherInteraction.Add("Feather", count => count);
            featherInteraction.Add("VV_ITEM_DASHQUILL_ITEM", count => count);
            featherInteraction.Add("ZetAspectRed", count => count > 0 ? 1 : 0);
            featherInteraction.Add("MysticsItems_Backpack", count => count > 0 ? 1 : 0);
            featherInteraction.Add("MintCondition", count => MintConditionJumps(count));
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
                    if (body?.inventory == null) return 0;
                    int ret = 0;
                    foreach (var key in featherInteraction.Keys) if (ItemCatalog.FindItemIndex(key) != ItemIndex.None) ret += featherInteraction[key](body.inventory.GetItemCount(ItemCatalog.FindItemIndex(key)));
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

        public static int MintConditionJumps(int count)
        {
            return Hex3Mod.Main.MintCondition_AddJumps.Value + ((count - 1) * Hex3Mod.Main.MintCondition_AddJumpsStack.Value);
        }
    }
}

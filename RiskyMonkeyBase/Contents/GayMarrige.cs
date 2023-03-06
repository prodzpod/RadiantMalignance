using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using System.Reflection;

namespace RiskyMonkeyBase.Contents
{
    public class GayMarrige
    {
        public static void Patch()
        {
            IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdcR4(2.5f), x => x.MatchLdloc(80));
                c.Next.Operand = Reference.IceRingDamage.Value;
                c.Index++;
                c.Emit(OpCodes.Ldloc, 5);
                c.EmitDelegate<Func<Inventory, float>>(inv =>
                {
                    if (inv.GetItemCount(RoR2Content.Items.FireRing) > 0) return 2;
                    return 1;
                });
                c.Emit(OpCodes.Mul);
                c.GotoNext(x => x.MatchLdcR4(3f), x => x.MatchLdloc(81));
                c.Next.Operand = Reference.FireRingDamage.Value;
                c.Index++;
                c.Emit(OpCodes.Ldloc, 5);
                c.EmitDelegate<Func<Inventory, float>>(inv =>
                {
                    if (inv.GetItemCount(RoR2Content.Items.IceRing) > 0) return 2;
                    return 1;
                });
                c.Emit(OpCodes.Mul);
            };
            NumberChanges.rules.Add("ITEM_ICERING_DESC", (orig, self, token) =>
            {
                string ret = orig(self, "RISKYMONKEY_ITEM_ICERING_DESC").Replace("{0}", (Reference.IceRingDamage.Value * 100).ToString());
                if (Reference.GayMarrige.Value) ret += " The damage <style=cIsUtility>doubles</style> with " + orig(self, "ITEM_FIRERING_NAME") + ".";
                return ret;
            });
            NumberChanges.rules.Add("ITEM_FIRERING_DESC", (orig, self, token) =>
            {
                string ret = orig(self, "RISKYMONKEY_ITEM_FIRERING_DESC").Replace("{0}", (Reference.FireRingDamage.Value * 100).ToString());
                if (Reference.GayMarrige.Value) ret += " The damage <style=cIsUtility>doubles</style> with " + orig(self, "ITEM_ICERING_NAME") + ".";
                return ret;
            });
        }

        public static void PatchRepair()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchRepairFRFR));
        }

        [HarmonyPatch(typeof(ShrineOfRepair.Modules.Interactables.ShrineOfRepairPicker.ShrineRepairManager), nameof(ShrineOfRepair.Modules.Interactables.ShrineOfRepairPicker.ShrineRepairManager.HandleSelection))]
        public class PatchRepairFRFR
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchCallOrCallvirt<Inventory>(nameof(Inventory.RemoveItem)));
                c.Index++;
                c.Remove();
                while (c.Next.OpCode != OpCodes.Ldloc_1) c.Remove();
                c.Emit(OpCodes.Ldloc_1);
                c.Emit(OpCodes.Ldloc_0);
                c.Emit(OpCodes.Ldloc, 4);
                c.Emit(OpCodes.Ldloc_2);
                c.EmitDelegate<Action<CharacterBody, PickupDef, ItemIndex, int>>((body, pickupDef, itemIndex, itemCount) =>
                {
                    if (pickupDef.itemIndex != DLC1Content.Items.ElementalRingVoid.itemIndex || (itemIndex != RoR2Content.Items.IceRing.itemIndex && itemIndex != RoR2Content.Items.FireRing.itemIndex))
                    {
                        body.inventory.GiveItem(itemIndex, itemCount);
                        return;
                    }
                    int iceCount = itemCount / 2;
                    int fireCount = itemCount / 2;
                    if (itemCount % 2 != 0)
                    {
                        if (Run.instance.treasureRng.nextBool) iceCount++;
                        else fireCount++;
                    }
                    if (iceCount > 0) body.inventory.GiveItem(RoR2Content.Items.IceRing.itemIndex, iceCount);
                    if (fireCount > 0) body.inventory.GiveItem(RoR2Content.Items.FireRing.itemIndex, fireCount);
                });
            }
        }
    }
}

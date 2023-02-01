using EntityStates.Headstompers;
using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
using RoR2;
using System;
using System.Reflection;
using UnityEngine;

namespace RiskyMonkeyBase.Tweaks
{
    public class HEADSTTweaks
    {
        public static void Patch()
        {
            HeadstompersCooldown.baseDuration = 0f;
            HeadstompersFall.maximumDamageCoefficient = 40f;
            On.EntityStates.Headstompers.HeadstompersCooldown.OnEnter += (orig, self) =>
            {
                orig(self);
                self.duration = 0;
            };
            RiskyMonkeyBase.Harmony.PatchAll(typeof(DoStompExplosionAuthorityTweaks));
            LanguageAPI.AddOverlay("ITEM_FALLBOOTS_DESC", "Increase <style=cIsUtility>jump height</style>. Creates a <style=cIsDamage>5m-100m</style> <style=cStack>(+20% per stack)</style> radius <style=cIsDamage>kinetic explosion</style> on hitting the ground, dealing <style=cIsDamage>1000%-4000%</style> base damage that scales up with <style=cIsDamage>fall distance</style>.");
            if (Reference.Mods("PlasmaCore.ForgottenRelics")) PatchVoid();
        }

        [HarmonyPatch(typeof(HeadstompersFall), nameof(HeadstompersFall.DoStompExplosionAuthority))]
        public class DoStompExplosionAuthorityTweaks
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                /*
                c.GotoNext(x => x.MatchStloc(3));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldloc_3);
                c.EmitDelegate<Func<HeadstompersFall, float, float>>((self, damage) =>
                {
                    return damage * (self.body.inventory ? self.body.inventory.GetItemCount(RoR2Content.Items.FallBoots) : 1);
                });
                c.Emit(OpCodes.Stloc_3);
                */
                c.GotoNext(x => x.MatchStloc(4));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldloc_2);
                c.EmitDelegate<Func<HeadstompersFall, float, float>>((self, t) =>
                {
                    float count = (self.body.inventory ? self.body.inventory.GetItemCount(RoR2Content.Items.FallBoots) : 1) - 1;
                    return Mathf.Lerp(HeadstompersFall.minimumRadius + count, HeadstompersFall.maximumRadius + (20 * count), t);
                });
                c.Emit(OpCodes.Stloc, 4);
            }
        }
        public static void PatchVoid()
        {
            ForgottenRelicsEntityStates.VoidStompers.VoidStompersCooldown.baseDuration = 5f;
            RiskyMonkeyBase.Harmony.PatchAll(typeof(OnEnterTweaks));
            RiskyMonkeyBase.Harmony.PatchAll(typeof(OnEnterTweaks2));
            LanguageAPI.AddOverlay("ITEM_FALLBOOTSVOID_DESC", "<style=cIsUtility>Float</style> while charging up a <style=cIsUtility>gravity shockwave</style>, forcing enemies into the air for <style=cIsDamage>200-2000%</style> <style=cStack>(+100% per stack)</style> base damage. Then create a <style=cIsUtility>30m</style> radius <style=cIsUtility>anti-gravity zone</style> for <style=cIsUtility>15</style> seconds. Recharges in <style=cIsUtility>5</style> seconds. <style=cIsVoid>Corrupts all H3AD-5T v2s</style>.");
        }

        [HarmonyPatch(typeof(ForgottenRelicsEntityStates.VoidStompers.VoidStompersExplode), nameof(ForgottenRelicsEntityStates.VoidStompers.VoidStompersExplode.OnEnter))]
        public class OnEnterTweaks
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchStloc(1));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldloc_1);
                c.EmitDelegate<Func<ForgottenRelicsEntityStates.VoidStompers.VoidStompersExplode, float, float>>((self, damage) =>
                {
                    return damage * (self.body.inventory ? self.body.inventory.GetItemCount(FRCSharp.VF2ContentPackProvider.voidStompersDef) : 1);
                });
                c.Emit(OpCodes.Stloc_1);
            }
        }

        [HarmonyPatch(typeof(ForgottenRelicsEntityStates.VoidStompers.VoidStompersCooldown), nameof(ForgottenRelicsEntityStates.VoidStompers.VoidStompersCooldown.OnEnter))]
        public class OnEnterTweaks2
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdarg(0), x => x.MatchLdarg(0));
                while (c.Next.Next.OpCode != OpCodes.Ret) c.Remove();
            }
        }
    }
}

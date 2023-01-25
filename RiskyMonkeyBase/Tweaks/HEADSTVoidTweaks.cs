using ForgottenRelicsEntityStates.VoidStompers;
using FRCSharp;
using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
using RoR2;
using System;
using System.Reflection;

namespace RiskyMonkeyBase.Tweaks
{
    public class HEADSTVoidTweaks
    {
        public static void Patch()
        {
            VoidStompersCooldown.baseDuration = 5f;
            RiskyMonkeyBase.Harmony.PatchAll(typeof(OnEnterTweaks));
            RiskyMonkeyBase.Harmony.PatchAll(typeof(OnEnterTweaks2));
            LanguageAPI.AddOverlay("ITEM_FALLBOOTSVOID_DESC", "<style=cIsUtility>Float</style> while charging up a <style=cIsUtility>gravity shockwave</style>, forcing enemies into the air for <style=cIsDamage>200-2000%</style> <style=cStack>(+100% per stack)</style> base damage. Then create a <style=cIsUtility>30m</style> radius <style=cIsUtility>anti-gravity zone</style> for <style=cIsUtility>15</style> seconds. Recharges in <style=cIsUtility>5</style> seconds. <style=cIsVoid>Corrupts all H3AD-5T v2s</style>.");
        }

        [HarmonyPatch(typeof(VoidStompersExplode), nameof(VoidStompersExplode.OnEnter))]
        public class OnEnterTweaks
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchStloc(1));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldloc_1);
                c.EmitDelegate<Func<VoidStompersExplode, float, float>>((self, damage) =>
                {
                    var body = AccessTools.FieldRefAccess<BaseVoidStompersState, CharacterBody>("body")(self);
                    return damage * (body.inventory ? body.inventory.GetItemCount(VF2ContentPackProvider.voidStompersDef) : 1);
                });
                c.Emit(OpCodes.Stloc_1);
            }
        }

        [HarmonyPatch(typeof(VoidStompersCooldown), nameof(VoidStompersCooldown.OnEnter))]
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

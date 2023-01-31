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
            On.EntityStates.Headstompers.HeadstompersCooldown.OnEnter += (orig, self) =>
            {
                orig(self);
                AccessTools.FieldRefAccess<HeadstompersCooldown, float>("duration")(self) = 0;
            };
            RiskyMonkeyBase.Harmony.PatchAll(typeof(DoStompExplosionAuthorityTweaks));
            LanguageAPI.AddOverlay("ITEM_FALLBOOTS_DESC", "Increase <style=cIsUtility>jump height</style>. Creates a <style=cIsDamage>5m-100m</style> <style=cStack>(+20% per stack)</style> radius <style=cIsDamage>kinetic explosion</style> on hitting the ground, dealing <style=cIsDamage>1000%-10000%</style> <style=cStack>(+100% per stack)</style> base damage that scales up with <style=cIsDamage>fall distance</style>.");
            if (Reference.Mods("PlasmaCore.ForgottenRelics")) HEADSTVoidTweaks.Patch();
        }

        [HarmonyPatch(typeof(HeadstompersFall), "DoStompExplosionAuthority")]
        public class DoStompExplosionAuthorityTweaks
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchStloc(3));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldloc_3);
                c.EmitDelegate<Func<HeadstompersFall, float, float>>((self, damage) =>
                {
                    var body = AccessTools.FieldRefAccess<HeadstompersFall, CharacterBody>("body")(self);
                    return damage * (body.inventory ? body.inventory.GetItemCount(RoR2Content.Items.FallBoots) : 1);
                });
                c.Emit(OpCodes.Stloc_3);
                c.GotoNext(x => x.MatchStloc(4));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldloc_2);
                c.EmitDelegate<Func<HeadstompersFall, float, float>>((self, t) =>
                {
                    var body = AccessTools.FieldRefAccess<HeadstompersFall, CharacterBody>("body")(self);
                    float count = (body.inventory ? body.inventory.GetItemCount(RoR2Content.Items.FallBoots) : 1) - 1;
                    return Mathf.Lerp(HeadstompersFall.minimumRadius + count, HeadstompersFall.maximumRadius + (20 * count), t);
                });
                c.Emit(OpCodes.Stloc, 4);
            }
        }
    }
}

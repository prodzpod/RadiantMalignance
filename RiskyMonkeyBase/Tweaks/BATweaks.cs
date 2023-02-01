using HarmonyLib;
using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RiskyMonkeyBase.Tweaks
{
    public class BATweaks
    {
        public static void Patch()
        {
            RiskyMonkeyBase.Log.LogDebug("Method Nuked!");
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchSendModHint));
        }

        [HarmonyPatch(typeof(BossAntiSoftlock.BossAntiSoftlock), nameof(BossAntiSoftlock.BossAntiSoftlock.SendModHint))]
        public class PatchSendModHint
        {
            public static bool Prefix()
            {
                // method nuked!
                return false;
            }
        }
    }
}

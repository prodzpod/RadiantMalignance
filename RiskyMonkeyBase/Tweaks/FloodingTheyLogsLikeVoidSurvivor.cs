using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;

namespace RiskyMonkeyBase.Tweaks
{
    public class FloodingTheyLogsLikeVoidSurvivor
    {
        public static void Patch()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchLog));
        }

        [HarmonyPatch(typeof(Lachee.IO.NamedPipeClientStream), MethodType.Constructor, new Type[] { typeof(string), typeof(string) })]
        public class PatchLog
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel) 
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdstr("Created new NamedPipeClientStream '{0}' => '{1}'")); // STOP
                while (c.Next.OpCode != OpCodes.Ret) c.Remove();
            }
        }
    }
}

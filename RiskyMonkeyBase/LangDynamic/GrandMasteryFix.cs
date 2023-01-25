using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.Achievements;
using System;
using System.Reflection;

namespace RiskyMonkeyBase.LangDynamic
{
    public class GrandMasteryFix
    {
        public static string[] Survivors = { "PALADIN_TYPHOONUNLOCKABLE", "MINER_TYPHOONUNLOCKABLE", "SNIPERCLASSIC_GRANDMASTERYUNLOCKABLE", "ENFORCER_GRANDMASTERYUNLOCKABLE", "NEMFORCER_TYPHOONUNLOCKABLE" }; // guh
        public static void Patch()
        {
            RiskyMonkeyBase.Log.LogInfo("[[Paladin grand mastery description fix]] module loaded");
            On.RoR2.Language.GetLocalizedStringByToken += (orig, self, token) =>
            {
                for (var i = 0; i < Survivors.Length; i++)
                {
                    if (token.StartsWith(Survivors[i]))
                    {
                        if ((token == Survivors[i] + "_ACHIEVEMENT_NAME" || token == Survivors[i] + "_UNLOCKABLE_NAME") && Reference.GrandDifficulty.Value == "INFERNO_NAME") return orig(self, Survivors[i] + "_ACHIEVEMENT_NAME_INFERNO");
                        if (token == Survivors[i] + "_ACHIEVEMENT_DESC" || token == Survivors[i] + "_UNLOCKABLE_DESC") return string.Format(orig(self, Survivors[i] + "_ACHIEVEMENT_DESC_HEADER"), orig(self, Reference.GrandDifficulty.Value));
                    }
                }
                return orig(self, token);
            };
            RiskyMonkeyBase.Log.LogDebug("Disabling Eclipse Check (this means harmony patching), Jank inbound...");
            if (Reference.Mods("com.rob.Paladin")) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchPaladin));
            if (Reference.Mods("com.rob.DiggerUnearthed")) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchMiner));
            if (Reference.Mods("com.Moffein.SniperClassic")) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchSniper));
            if (Reference.Mods("com.EnforcerGang.Enforcer")) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchEnforcer));
        }

        [HarmonyPatch]
        public static class PatchPaladin
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel _)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdloc(2));
                while (c.Next.Next.OpCode != OpCodes.Ret) c.Remove(); // keep label AND ret alive for previous rets
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldloc_2);
                c.EmitDelegate<Action<object, DifficultyDef>>((_self, def) =>
                {
                    if (def.nameToken == Reference.GrandDifficulty.Value) ((BaseAchievement)_self).Grant();
                });
                RiskyMonkeyBase.Log.LogDebug("Patched " + original.DeclaringType.FullName);
            }

            public static MethodBase TargetMethod()
            {
                return AccessTools.Method(AccessTools.TypeByName("PaladinMod.Achievements.BaseMasteryUnlockable"), "OnClientGameOverGlobal", new Type[] { typeof(Run), typeof(RunReport) });
            }
        }

        [HarmonyPatch]
        public static class PatchMiner
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel _)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdloc(2));
                while (c.Next.Next.OpCode != OpCodes.Ret) c.Remove();
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldloc_2);
                c.EmitDelegate<Action<object, DifficultyDef>>((_self, def) =>
                {
                    if (def.nameToken == Reference.GrandDifficulty.Value) ((BaseAchievement)_self).Grant();
                });
                RiskyMonkeyBase.Log.LogDebug("Patched " + original.DeclaringType.FullName);
            }

            public static MethodBase TargetMethod()
            {
                return AccessTools.Method(AccessTools.TypeByName("DiggerPlugin.Achievements.BaseMasteryUnlockable"), "OnClientGameOverGlobal", new Type[] { typeof(Run), typeof(RunReport) });
            }
        }

        [HarmonyPatch]
        public static class PatchSniper
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel _)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdsfld<bool>("SniperClassic.SniperClassic::infernoPluginLoaded"));
                while (c.Next.Next.OpCode != OpCodes.Ret) c.Remove();
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldloc_2);
                c.EmitDelegate<Action<object, DifficultyDef>>((_self, def) =>
                {
                    if (def.nameToken == Reference.GrandDifficulty.Value) ((BaseAchievement)_self).Grant();
                });
                RiskyMonkeyBase.Log.LogDebug("Patched " + original.DeclaringType.FullName);
            }

            public static MethodBase TargetMethod()
            {
                return AccessTools.Method(AccessTools.TypeByName("SniperClassic.Modules.Achievements.BaseMasteryUnlockable"), "OnClientGameOverGlobal", new Type[] { typeof(Run), typeof(RunReport) });
            }
        }

        [HarmonyPatch]
        public static class PatchEnforcer
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel _)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdloc(2));
                while (c.Next.Next.OpCode != OpCodes.Ret) c.Remove();
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldloc_2);
                c.EmitDelegate<Action<object, DifficultyDef>>((_self, def) =>
                {
                    if (def.nameToken == Reference.GrandDifficulty.Value) ((BaseAchievement)_self).Grant();
                });
                RiskyMonkeyBase.Log.LogDebug("Patched " + original.DeclaringType.FullName);
            }

            public static MethodBase TargetMethod()
            {
                return AccessTools.Method(AccessTools.TypeByName("EnforcerPlugin.Achievements.BaseMasteryUnlockable"), "OnClientGameOverGlobal", new Type[] { typeof(Run), typeof(RunReport) });
            }
        }
    }
}

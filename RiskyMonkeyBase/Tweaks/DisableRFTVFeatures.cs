using HarmonyLib;
using ReleasedFromTheVoid.Scripts;
using System.Reflection;

namespace RiskyMonkeyBase.Tweaks
{
    public class DisableRFTVFeatures
    {
        public static void VoidCoin()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchVoidCoin));
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchRulebookExtras));
        }

        [HarmonyPatch(typeof(VoidCoins), nameof(VoidCoins.Init))]
        public class PatchVoidCoin
        {
            static bool Prefix()
            {
                RiskyMonkeyBase.Log.LogDebug("Method Nuked!");
                return false; // method nuked
            }
        }

        [HarmonyPatch]
        public class PatchRulebookExtras
        {
            static bool Prefix()
            {
                RiskyMonkeyBase.Log.LogDebug("Method Nuked!");
                return false; // method nuked
            }
            public static MethodBase TargetMethod()
            {
                return AccessTools.Method(AccessTools.TypeByName("ReleasedFromTheVoid.Scripts.RulebookExtras"), "Init");
            }
        }
        public static void VoidSuppressor()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchVoidSuppressor));
        }

        [HarmonyPatch]
        public class PatchVoidSuppressor
        {
            static bool Prefix()
            {
                RiskyMonkeyBase.Log.LogDebug("Method Nuked!");
                return false; // method nuked
            }
            public static MethodBase TargetMethod()
            {
                return AccessTools.Method(AccessTools.TypeByName("ReleasedFromTheVoid.Scripts.VoidSupressorSpawner"), "Init"); // typo in class :/
            }
        }

        public static void ItemEnable()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchItemEnable));
        }

        [HarmonyPatch]
        public class PatchItemEnable
        {
            static bool Prefix()
            {
                RiskyMonkeyBase.Log.LogDebug("Method Nuked!");
                return false; // method nuked
            }
            public static MethodBase TargetMethod()
            {
                return AccessTools.Method(AccessTools.TypeByName("ReleasedFromTheVoid.Scripts.ItemEnabler"), "Init");
            }
        }

        public static void CommandoSkin()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchCommandoSkin));
        }

        [HarmonyPatch]
        public class PatchCommandoSkin
        {
            static bool Prefix()
            {
                RiskyMonkeyBase.Log.LogDebug("Method Nuked!");
                return false; // method nuked
            }
            public static MethodBase TargetMethod()
            {
                return AccessTools.Method(AccessTools.TypeByName("ReleasedFromTheVoid.Scripts.CommandoSkinPatcher"), "Init");
            }
        }

        public static void LocusTweaks()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchLocusTweaks));
        }

        [HarmonyPatch]
        public class PatchLocusTweaks
        {
            static bool Prefix()
            {
                RiskyMonkeyBase.Log.LogDebug("Method Nuked!");
                return false; // method nuked
            }
            public static MethodBase TargetMethod()
            {
                return AccessTools.Method(AccessTools.TypeByName("ReleasedFromTheVoid.Scripts.VoidLocusTweaker"), "Init");
            }
        }
    }
}

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

        [HarmonyPatch(typeof(RulebookExtras), nameof(RulebookExtras.Init))]
        public class PatchRulebookExtras
        {
            static bool Prefix()
            {
                RiskyMonkeyBase.Log.LogDebug("Method Nuked!");
                return false; // method nuked
            }
        }
        public static void VoidSuppressor()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchVoidSuppressor));
        }

        [HarmonyPatch(typeof(VoidSupressorSpawner), nameof(VoidSupressorSpawner.Init))]
        public class PatchVoidSuppressor
        {
            static bool Prefix()
            {
                RiskyMonkeyBase.Log.LogDebug("Method Nuked!");
                return false; // method nuked
            }
        }

        public static void ItemEnable()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchItemEnable));
        }

        [HarmonyPatch(typeof(ItemEnabler), nameof(ItemEnabler.Init))]
        public class PatchItemEnable
        {
            static bool Prefix()
            {
                RiskyMonkeyBase.Log.LogDebug("Method Nuked!");
                return false; // method nuked
            }
        }

        public static void CommandoSkin()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchCommandoSkin));
        }

        [HarmonyPatch(typeof(CommandoSkinPatcher), nameof(CommandoSkinPatcher.Init))]
        public class PatchCommandoSkin
        {
            static bool Prefix()
            {
                RiskyMonkeyBase.Log.LogDebug("Method Nuked!");
                return false; // method nuked
            }
        }

        public static void LocusTweaks()
        {
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchLocusTweaks));
        }

        [HarmonyPatch(typeof(VoidLocusTweaker), nameof(VoidLocusTweaker.Init))]
        public class PatchLocusTweaks
        {
            static bool Prefix()
            {
                RiskyMonkeyBase.Log.LogDebug("Method Nuked!");
                return false; // method nuked
            }
        }
    }
}

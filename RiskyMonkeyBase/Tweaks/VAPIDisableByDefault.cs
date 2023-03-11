using ArtifactOfPotential;
using HarmonyLib;
using RoR2;
using VAPI;
using VAPI.Components;

namespace RiskyMonkeyBase.Tweaks
{
    public class VAPIDisableByDefault
    {
        public static void Patch()
        {
            On.RoR2.RuleDef.FromExpansion += (orig, self) =>
            {
                RuleDef ret = orig(self);
                if (self.nameToken == "VAPI_EXPANSION_NAME") ret.defaultChoiceIndex = 1; // off
                return ret;
            };
            RiskyMonkeyBase.Harmony.PatchAll(typeof(VariantName));
        }

        [HarmonyPatch(typeof(BodyVariantManager), nameof(BodyVariantManager.ModifyName))]
        public class VariantName
        {
            public static void Prefix(BodyVariantManager __instance, VariantDef.VariantOverrideName[] overrideNames)
            {
                if (overrideNames.Length > 0) __instance.CharacterBody.baseNameToken = Language.GetString(__instance.CharacterBody.baseNameToken).Replace("Stone ", "").Replace("Lesser ", "").Replace("Greater ", "").Replace("Wandering ", "").Replace("Clay ", "").Replace("Ancient ", "").Replace("Archaic ", "").Replace("Imp ", "").Replace("Hermit ", "").Replace("Bighorn ", "").Replace("Brass ", "").Replace(" Queen", "").Replace("Mini ", "").Replace("Alloy ", "");
            }
            public static void Postfix(BodyVariantManager __instance, VariantDef.VariantOverrideName[] overrideNames)
            {
                string baseToken = overrideNames[0].token.Replace("_PREFIX", "").Replace("_SUFFIX", "").Replace("_OVERRIDE", "");
                string rmoverride = Language.GetString(baseToken + "_RMOVERRIDE");
                if (rmoverride != (baseToken + "_RMOVERRIDE")) __instance.CharacterBody.baseNameToken = rmoverride;
                __instance.CharacterBody.baseNameToken = "<style=cWorldEvent>" + __instance.CharacterBody.baseNameToken + "</style>";
            }
        }
    }
}

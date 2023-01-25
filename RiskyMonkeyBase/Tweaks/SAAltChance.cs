using RoR2;
using StageAesthetic;
using System.Collections.Generic;

namespace RiskyMonkeyBase.Tweaks
{
    public class SAAltChance
    {
        public static List<string> plainsList;
        public static List<string> roostList;
        public static List<string> forestList;

        public static List<string> wetlandList;
        public static List<string> aqueductList;
        public static List<string> aphelianList;

        public static List<string> deltaList;
        public static List<string> acresList;
        public static List<string> sulfurList;
        public static List<string> fogboundList;

        public static List<string> depthsList;
        public static List<string> groveList;
        public static List<string> sirenList;

        public static List<string> meadowList;

        // no commencement bc its Weird
        public static List<string> locusList;
        public static List<string> planetariumList;

        public static bool initialized = false;

        public static void Patch()
        {
            On.RoR2.SceneDirector.Start += (orig, self) =>
            {
                if (!initialized)
                {
                    plainsList = new(SwapVariants.plainsList);
                    roostList = new(SwapVariants.roostList);
                    forestList = new(SwapVariants.forestList);
                    wetlandList = new(SwapVariants.wetlandList);
                    aqueductList = new(SwapVariants.aqueductList);
                    aphelianList = new(SwapVariants.aphelianList);
                    deltaList = new(SwapVariants.deltaList);
                    acresList = new(SwapVariants.acresList);
                    sulfurList = new(SwapVariants.sulfurList);
                    fogboundList = new(SwapVariants.fogboundList);
                    depthsList = new(SwapVariants.depthsList);
                    groveList = new(SwapVariants.groveList);
                    sirenList = new(SwapVariants.sirenList);
                    meadowList = new(SwapVariants.meadowList);
                    locusList = new(SwapVariants.locusList);
                    planetariumList = new(SwapVariants.planetariumList);
                    initialized = true;
                } else
                {
                    ulong seed = (ulong)(Run.instance.GetStartTimeUtc().Ticks ^ (Run.instance.stageClearCount << 16));
                    Xoroshiro128Plus rng = new(seed);
                    bool forceVanilla = rng.RangeFloat(0, 1) > Reference.SAAltChance.Value;
                    RiskyMonkeyBase.Log.LogDebug("SA Forced Vanilla: " + forceVanilla + ", Test: " + rng.RangeFloat(0, 1) + " / " + Reference.SAAltChance.Value);
                    if (forceVanilla && SwapVariants.VanillaPlains.Value) SwapVariants.plainsList = new(new string[] { "vanilla" }); else SwapVariants.plainsList = new(plainsList);
                    if (forceVanilla && SwapVariants.VanillaRoost.Value) SwapVariants.roostList = new(new string[] { "vanilla" }); else SwapVariants.roostList = new(roostList);
                    if (forceVanilla && SwapVariants.VanillaForest.Value) SwapVariants.forestList = new(new string[] { "vanilla" }); else SwapVariants.forestList = new(forestList);
                    if (forceVanilla && SwapVariants.VanillaWetland.Value) SwapVariants.wetlandList = new(new string[] { "vanilla" }); else SwapVariants.wetlandList = new(wetlandList);
                    if (forceVanilla && SwapVariants.VanillaAqueduct.Value) SwapVariants.aqueductList = new(new string[] { "vanilla" }); else SwapVariants.aqueductList = new(aqueductList);
                    if (forceVanilla && SwapVariants.VanillaAphelian.Value) SwapVariants.aphelianList = new(new string[] { "vanilla" }); else SwapVariants.aphelianList = new(aphelianList);
                    if (forceVanilla && SwapVariants.VanillaDelta.Value) SwapVariants.deltaList = new(new string[] { "vanilla" }); else SwapVariants.deltaList = new(deltaList);
                    if (forceVanilla && SwapVariants.VanillaAcres.Value) SwapVariants.acresList = new(new string[] { "vanilla" }); else SwapVariants.acresList = new(acresList);
                    if (forceVanilla && SwapVariants.VanillaSulfur.Value) SwapVariants.sulfurList = new(new string[] { "vanilla" }); else SwapVariants.sulfurList = new(sulfurList);
                    if (forceVanilla && SwapVariants.VanillaLagoon.Value) SwapVariants.fogboundList = new(new string[] { "vanilla" }); else SwapVariants.fogboundList = new(fogboundList);
                    if (forceVanilla && SwapVariants.VanillaDepths.Value) SwapVariants.depthsList = new(new string[] { "vanilla" }); else SwapVariants.depthsList = new(depthsList);
                    if (forceVanilla && SwapVariants.VanillaGrove.Value) SwapVariants.groveList = new(new string[] { "vanilla" }); else SwapVariants.groveList = new(groveList);
                    if (forceVanilla && SwapVariants.VanillaSiren.Value) SwapVariants.sirenList = new(new string[] { "vanilla" }); else SwapVariants.sirenList = new(sirenList);
                    if (forceVanilla && SwapVariants.VanillaMeadow.Value) SwapVariants.meadowList = new(new string[] { "vanilla" }); else SwapVariants.meadowList = new(meadowList);
                    if (forceVanilla && SwapVariants.VanillaLocus.Value) SwapVariants.locusList = new(new string[] { "vanilla" }); else SwapVariants.locusList = new(locusList);
                    if (forceVanilla && SwapVariants.VanillaPlanetarium.Value) SwapVariants.planetariumList = new(new string[] { "vanilla" }); else SwapVariants.planetariumList = new(planetariumList);
                }
                orig(self);
            }; // thank you LIFO
        }
    }
}

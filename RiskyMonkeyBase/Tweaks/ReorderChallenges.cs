using HarmonyLib;
using RoR2;
using System.Collections.Generic;

namespace RiskyMonkeyBase.Tweaks
{
    public class ReorderChallenges
    {
        public static List<string> challengesOrder;
        public static void Patch()
        {
            challengesOrder = new();
            foreach (var entry in Reference.ChallengesToReorder.Value.Split(',')) challengesOrder.Add(entry.Trim());
            RiskyMonkeyBase.Log.LogDebug("Challenge Order: " + challengesOrder.Join());
            On.RoR2.UnlockableCatalog.SetUnlockableDefs += (orig, unlockableDefs) =>
            {
                orig(unlockableDefs);
                string txt = "";
                for (var i = 0; i < UnlockableCatalog.unlockableCount; i++)
                {
                    UnlockableDef def = UnlockableCatalog.GetUnlockableDef((UnlockableIndex)i);
                    int idx = challengesOrder.IndexOf(def.cachedName);
                    if (idx != -1) def.sortScore = idx; // vertical align
                    txt += "Unlockable " + def.cachedName + ": " + def.sortScore + "\n";
                }
                RiskyMonkeyBase.Log.LogDebug(txt);
            };
        }
    }
}

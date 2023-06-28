using HarmonyLib;
using System.Collections.Generic;
using System.Globalization;

namespace RiskyMonkeyBase.Tweaks
{
    public class ReorderSurvivors
    {
        public static Dictionary<string, float> survivorsOrder;
        public static void Patch()
        {
            survivorsOrder = new();
            foreach (var entry in Reference.SurvivorsOrder.Value.Split(','))
            {
                string[] entries = entry.Split('-');
                if (entries.Length != 2) continue;
                survivorsOrder.Add(entries[0].Trim(), float.Parse(entries[1].Trim(), CultureInfo.InvariantCulture));
            }
            RiskyMonkeyBase.Log.LogDebug("Survivor Order: " + survivorsOrder.Join());
            On.RoR2.SurvivorCatalog.SetSurvivorDefs += (orig, defs) =>
            {
                foreach (var def in defs)
                {
                    if (survivorsOrder.ContainsKey(def.cachedName)) def.desiredSortPosition = survivorsOrder[def.cachedName];
                    RiskyMonkeyBase.Log.LogDebug("Survivor " + def.cachedName + ", Order: " + def.desiredSortPosition);
                }
                orig(defs);
            };
        }
    }
}

using RoR2;
using System.Collections.Generic;

namespace RiskyMonkeyBase.Tweaks
{
    public class RemoveDifficultyTweaks
    {
        public static List<string> diffsToRemove;

        public static void Patch()
        {
            diffsToRemove = new();
            foreach (var entry in Reference.DifficultiesToRemove.Value.Split(',')) diffsToRemove.Add(entry.Trim());
            RiskyMonkeyBase.Log.LogInfo("[[Remove difficulties]] module loaded");

            On.RoR2.RuleDef.FromDifficulty += (orig) =>
            {
                RuleDef ruleDef = orig();
                RiskyMonkeyBase.Log.LogInfo("Removing: [" + string.Join("], [", diffsToRemove) + "]");
                for (var index = ruleDef.choices.Count - 1; index >= 0; index--)
                    if (diffsToRemove.Contains(DifficultyCatalog.GetDifficultyDef(ruleDef.choices[index].difficultyIndex).nameToken))
                        ruleDef.choices.RemoveAt(index);
                return ruleDef;
            };
        }
    }
}

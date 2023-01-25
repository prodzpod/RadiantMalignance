using HarmonyLib;
using RiskOfOptions;
using System;
using System.Collections.Generic;

namespace RiskyMonkeyBase.Tweaks
{
    public class RiskOfOptionsHideTweaks
    {
        public static List<string> categoriesToRemove;

        public static void Patch()
        {
            On.RoR2.UI.MainMenu.BaseMainMenuScreen.OnEnter += (orig, self, mainMenuController) =>
            {
                orig(self, mainMenuController);
                if (categoriesToRemove == null)
                {
                    object modIndexedOptionCollection = AccessTools.StaticFieldRefAccess<object>(typeof(ModSettingsManager), "OptionCollection");
                    object optionCollections = AccessTools.Field(modIndexedOptionCollection.GetType(), "_optionCollections").GetValue(modIndexedOptionCollection);
                    object _identifierModGuidMap = AccessTools.Field(modIndexedOptionCollection.GetType(), "_identifierModGuidMap").GetValue(modIndexedOptionCollection);
                    ICollection<string> keys = (ICollection<string>)AccessTools.Property(optionCollections.GetType(), "Keys").GetValue(optionCollections);
                    categoriesToRemove = new();
                    foreach (var entry in Reference.RiskOfOptionsHideList.Value.Split(',')) if (keys.Contains(entry.Trim())) categoriesToRemove.Add(entry.Trim());
                    if (Reference.SeriousMode.Value && !categoriesToRemove.Contains("com.weliveinasociety.CustomEmotesAPI") && keys.Contains("com.weliveinasociety.CustomEmotesAPI")) categoriesToRemove.Add("com.weliveinasociety.CustomEmotesAPI");
                    RiskyMonkeyBase.Log.LogDebug("ROO Category: Removing" + categoriesToRemove.Join());
                    foreach (var entry in categoriesToRemove) AccessTools.Method(optionCollections.GetType(), "Remove", new Type[] { typeof(string) }).Invoke(optionCollections, new string[] { entry });
                    Dictionary<string, string> identifierModGuidMap = (Dictionary<string, string>)_identifierModGuidMap;
                    List<string> identifiersToRemove = new();
                    foreach (var key in identifierModGuidMap.Keys) if (categoriesToRemove.Contains(identifierModGuidMap[key])) identifiersToRemove.Add(key);
                    foreach (var key in identifiersToRemove) identifierModGuidMap.Remove(key);
                    // debug
                    object new_modIndexedOptionCollection = AccessTools.StaticFieldRefAccess<object>(typeof(ModSettingsManager), "OptionCollection");
                    object new_optionCollections = AccessTools.Field(new_modIndexedOptionCollection.GetType(), "_optionCollections").GetValue(new_modIndexedOptionCollection);
                    ICollection<string> new_keys = (ICollection<string>)AccessTools.Property(new_optionCollections.GetType(), "Keys").GetValue(new_optionCollections);
                    RiskyMonkeyBase.Log.LogDebug("New List: " + new_keys.Join());
                }
            };
        }
    }
}
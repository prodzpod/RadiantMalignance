using HarmonyLib;
using RiskOfOptions;
using RiskOfOptions.Containers;
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
                    ModIndexedOptionCollection modIndexedOptionCollection = ModSettingsManager.OptionCollection;
                    Dictionary<string, OptionCollection> optionCollections = modIndexedOptionCollection._optionCollections;
                    Dictionary<string, string> identifierModGuidMap = modIndexedOptionCollection._identifierModGuidMap;
                    ICollection<string> keys = optionCollections.Keys;
                    categoriesToRemove = new();
                    foreach (var entry in Reference.RiskOfOptionsHideList.Value.Split(',')) if (keys.Contains(entry.Trim())) categoriesToRemove.Add(entry.Trim());
                    if (Reference.EnableEmotes.Value && !categoriesToRemove.Contains("com.weliveinasociety.CustomEmotesAPI") && keys.Contains("com.weliveinasociety.CustomEmotesAPI")) categoriesToRemove.Add("com.weliveinasociety.CustomEmotesAPI");
                    RiskyMonkeyBase.Log.LogDebug("ROO Category: Removing" + categoriesToRemove.Join());
                    foreach (var entry in categoriesToRemove) optionCollections.Remove(entry);
                    List<string> identifiersToRemove = new();
                    foreach (var key in identifierModGuidMap.Keys) if (categoriesToRemove.Contains(identifierModGuidMap[key])) identifiersToRemove.Add(key);
                    foreach (var key in identifiersToRemove) identifierModGuidMap.Remove(key);
                    RiskyMonkeyBase.Log.LogDebug("New List: " + ModSettingsManager.OptionCollection._optionCollections.Keys.Join()); // debug
                }
            };
        }
    }
}
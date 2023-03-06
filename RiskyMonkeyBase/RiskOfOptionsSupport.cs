using RiskOfOptions;
using RiskOfOptions.Options;
using UnityEngine;

namespace RiskyMonkeyBase
{
    public class RiskOfOptionsSupport
    {
        public static void Patch()
        {
            ModSettingsManager.SetModDescription("RM Modpack Settings", Reference.PluginGUID, Reference.PluginDisplayName);
            ModSettingsManager.SetModIcon(RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/iconModpack.png"), Reference.PluginGUID, Reference.PluginDisplayName);
            ModSettingsManager.AddOption(new CheckBoxOption(Reference.SeriousMode, true), Reference.PluginGUID, Reference.PluginDisplayName);
            ModSettingsManager.AddOption(new CheckBoxOption(Reference.EnableEmotes, true), Reference.PluginGUID, Reference.PluginDisplayName);
            ModSettingsManager.AddOption(new CheckBoxOption(Reference.ResetTutorial, true), Reference.PluginGUID, Reference.PluginDisplayName);
            ModSettingsManager.AddOption(new CheckBoxOption(Reference.GetChangelog, true), Reference.PluginGUID, Reference.PluginDisplayName);
        }
    }
}

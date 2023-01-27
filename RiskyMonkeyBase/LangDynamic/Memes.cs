using R2API;
using RoR2;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace RiskyMonkeyBase.LangDynamic
{
    public class Memes
    {
        public static void Patch()
        {
            RiskyMonkeyBase.Log.LogInfo("[[Return to monkey]] module loaded");
            RiskyMonkeyBase.Log.LogInfo("loading " + Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "memes.notlanguage"));
            if (File.Exists(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "memes.notlanguage"))) LanguageAPI.AddPath(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "memes.notlanguage"));
            else if (File.Exists(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "Lang\\memes.notlanguage"))) LanguageAPI.AddPath(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "Lang\\memes.notlanguage"));
            RiskyMonkeyBase.Log.LogInfo("loading " + Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "memes.notlanguage"));
            if (File.Exists(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "memes.moddedlanguage"))) LanguageAPI.AddOverlayPath(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "memes.moddedlanguage"));
            else if (File.Exists(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "Lang\\memes.moddedlanguage"))) LanguageAPI.AddOverlayPath(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "Lang\\memes.moddedlanguage"));
            On.RoR2.CharacterBody.GetUserName += (orig, self) => { return Language.GetString(orig(self)); };
            On.RoR2.Util.GetBestMasterName += (orig, self) => { return Language.GetString(orig(self)); };
            On.RoR2.CharacterBody.GetColoredUserName += (orig, self) =>
            {
                var raw = orig(self);
                return Util.GenerateColoredString(Language.GetString(raw.Substring(15, raw.Length - 23)), new Color32(127, 127, 127, byte.MaxValue));
            };
        }
    }
}

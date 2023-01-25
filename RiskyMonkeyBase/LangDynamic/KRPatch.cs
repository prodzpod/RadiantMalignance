using R2API;
using System.Reflection;

namespace RiskyMonkeyBase.LangDynamic
{
    public class KRPatch
    {
        public static void Patch()
        {
            RiskyMonkeyBase.Log.LogInfo("[[Korean Patch]] module loaded");
            RiskyMonkeyBase.Log.LogInfo("loading " + Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "Lang\\krpatch.overlaylanguage"));
            LanguageAPI.AddPath(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "Lang\\krpatch.overlaylanguage"));
        }
    }
}

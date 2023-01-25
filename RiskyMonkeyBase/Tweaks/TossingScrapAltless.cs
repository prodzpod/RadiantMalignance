using R2API;
using TPDespair.ZetArtifacts;

namespace RiskyMonkeyBase.Tweaks
{
    public class TossingScrapAltless
    {
        public static void Patch()
        {
            if (ZetArtifactsPlugin.DropifactEnable.Value < 1) return; // no dropifact lol
            string scrapKey = "LeftAlt + M2";
            if (Reference.TossingAlt.Value) scrapKey = "M2";
            LanguageAPI.AddOverlay("ARTIFACT_ZETDROPIFACT_DESC", "Allows players to drop and scrap items.\n<style=cStack>M1 to drop, " + scrapKey + " to scrap</style>");
        }

        public static bool isTossingEnabled()
        {
            return ZetArtifactsPlugin.DropifactEnable.Value == 2;
        }
    }
}

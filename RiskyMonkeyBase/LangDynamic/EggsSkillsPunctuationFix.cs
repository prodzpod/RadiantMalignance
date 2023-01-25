using System.Collections.Generic;

namespace RiskyMonkeyBase.LangDynamic
{
    public class EggsSkillsPunctuationFix
    {
        public static List<string> skillsMissingPeriod = new List<string>(new string[] { "ACHIEVEMENT_ES_CROCOMANYPOISONED_DESCRIPTION", "ACHIEVEMENT_ES_BANDIT2LONGINVIS_DESCRIPTION", "ACHIEVEMENT_ES_BANDIT2FASTKILLS_DESCRIPTION", "ES_CAPTAIN_SECONDARY_DEBUFFNADE_DESCRIPTION", "ACHIEVEMENT_ES_CAPTAINSTAGENOBEACON_DESCRIPTION", "ACHIEVEMENT_ES_COMMANDOM1KILLS_DESCRIPTION", "ES_COMMANDO_UTILITY_DASH_DESCRIPTION", "ACHIEVEMENT_ES_COMMANDOFINISHTELEPORTERLOWHEALTH_DESCRIPTION", "ACHIEVEMENT_ES_HUNTRESSPRIMARYUTILITYONLY_DESCRIPTION", "ACHIEVEMENT_ES_MERCEXPOSEENEMIES_DESCRIPTION", "ES_TOOLBOT_SECONDARY_NANOBOTS_DESCRIPTION", "ACHIEVEMENT_ES_TOOLBOTMANYDRONES_DESCRIPTION", "ACHIEVEMENT_ES_TREEBOTMANYCLOSEKILLS_DESCRIPTION" });

        public static void Patch()
        {
            RiskyMonkeyBase.Log.LogInfo("[[Eggs skills proper punctuation]] module loaded");
            On.RoR2.Language.GetLocalizedStringByToken += (orig, self, token) =>
            {
                if (skillsMissingPeriod.Contains(token)) return orig(self, token) + ".";
                return orig(self, token);
            };
        }
    }
}

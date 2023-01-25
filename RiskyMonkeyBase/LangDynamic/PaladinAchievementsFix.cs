using System.Collections.Generic;

namespace RiskyMonkeyBase.LangDynamic
{
    public class PaladinAchievementsFix
    {
        public static List<string> achievements = new List<string>(new string[] { "PALADIN_POISONUNLOCKABLE_ACHIEVEMENT_NAME", "PALADIN_LIGHTNINGSPEARUNLOCKABLE_ACHIEVEMENT_NAME", "PALADIN_LUNARSHARDUNLOCKABLE_ACHIEVEMENT_NAME", "PALADIN_HEALUNLOCKABLE_ACHIEVEMENT_NAME", "PALADIN_TORPORUNLOCKABLE_ACHIEVEMENT_NAME", "PALADIN_CRUELSUNUNLOCKABLE_ACHIEVEMENT_NAME", "PALADIN_CLAYUNLOCKABLE_ACHIEVEMENT_NAME", "PALADIN_POISONUNLOCKABLE_UNLOCKABLE_NAME", "PALADIN_LIGHTNINGSPEARUNLOCKABLE_UNLOCKABLE_NAME", "PALADIN_LUNARSHARDUNLOCKABLE_UNLOCKABLE_NAME", "PALADIN_HEALUNLOCKABLE_UNLOCKABLE_NAME", "PALADIN_TORPORUNLOCKABLE_UNLOCKABLE_NAME", "PALADIN_CRUELSUNUNLOCKABLE_UNLOCKABLE_NAME", "PALADIN_CLAYUNLOCKABLE_UNLOCKABLE_NAME" });

        public static void Patch()
        {
            RiskyMonkeyBase.Log.LogInfo("[[Paladin skill fix]] module loaded");
            On.RoR2.Language.GetLocalizedStringByToken += (orig, self, token) =>
            {
                if (achievements.Contains(token)) return orig(self, "PALADIN_PREFIX") + orig(self, token);
                return orig(self, token);
            };
        }
    }
}

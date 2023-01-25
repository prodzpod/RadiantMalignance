using R2API;
using RoR2;
using UnityEngine;

namespace RiskyMonkeyBase.LangDynamic
{
    public class TemplarSkillFix
    {
        public static void Patch()
        {
            RiskyMonkeyBase.Log.LogInfo("[[Templar skill fix]] module loaded");
            LanguageAPI.AddOverlay("TEMPLAR_PRIMARY_MINIGUN_DESCRIPTION", "<style=cIsDamage>Rapidfire</style>. Rev up and fire your <style=cIsUtility>minigun</style>, dealing <style=cIsDamage>" + (Templar.Templar.minigunDamageCoefficient.Value * 100f).ToString() + "% damage</style> per bullet. <style=cIsUtility>Slow</style> your movement while shooting, but gain <style=cIsHealing>bonus armor</style>.");
        }
        public static void LogbookPatch()
        {
            Templar.Templar.TemplarPrefab.GetComponent<DeathRewards>().logUnlockableDef = null;
            SkillLocator component = Templar.Templar.myCharacter.GetComponent<SkillLocator>();
            // goofy ahh character tbh
            ContentAddition.AddSkillFamily(component.primary.skillFamily);
            ContentAddition.AddSkillFamily(component.secondary.skillFamily);
            ContentAddition.AddSkillFamily(component.utility.skillFamily);
            ContentAddition.AddSkillFamily(component.special.skillFamily);
            CharacterBody body = Templar.Templar.myCharacter.GetComponent<CharacterBody>();
            body.portraitIcon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/unlocks/texSurvivorTemplar.png").texture;
            body.baseNameToken = "TEMPLAR_SURVIVOR_NAME";
        }
    }
}

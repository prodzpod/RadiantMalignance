using RiskyMonkeyBase.Tweaks;
using RoR2;
using UnityEngine;

namespace RiskyMonkeyBase.Tutorials
{
    public class TutorialOther
    {
        public static void Patch()
        {
            On.RoR2.UserProfile.AddAchievement += (orig, self, achievementName, isExternal) =>
            {
                orig(self, achievementName, isExternal);
                TutorialHelper.Tutorial(Reference.TutorialAchievements, "achievements");
            };
            On.RoR2.TeleporterInteraction.IdleState.OnInteractionBegin += (orig, self, activator) =>
            {
                orig(self, activator);
                TutorialHelper.Tutorial(Reference.TutorialTeleporterZone, "teleporterzone");
            };
            On.RoR2.Run.OnServerBossDefeated += (orig, self, bossGroup) =>
            {
                orig(self, bossGroup);
                if (Reference.Mods("com.Chris-Stetvenson-Git.FasterBossWait")) TutorialHelper.Tutorial(Reference.TutorialTeleporterPost, "teleporterpost");
            };
            On.RoR2.GlobalEventManager.OnCharacterHitGroundServer += (orig, self, characterBody, impactVelocity) =>
            {
                orig(self, characterBody, impactVelocity);
                if (((bool)characterBody.inventory ? characterBody.inventory.GetItemCount(RoR2Content.Items.FallBoots) : 0) <= 0 && (characterBody.bodyFlags & CharacterBody.BodyFlags.IgnoreFallDamage) == CharacterBody.BodyFlags.None && Mathf.Max(Mathf.Abs(impactVelocity.y) - (characterBody.jumpPower + 20f), 0.0f) > 0)
                    TutorialHelper.Tutorial(Reference.TutorialOSP, "osp");
            };
            GlobalEventManager.onCharacterDeathGlobal += (damageReport) =>
            {
                if (damageReport.victimIsElite && (bool)damageReport.attackerMaster && damageReport.attackerMaster.gameObject == LocalUserManager.GetFirstLocalUser().currentNetworkUser.masterObject)
                    TutorialHelper.Tutorial(Reference.TutorialElite, "elite");
            };
            On.RoR2.TeleporterInteraction.FinishedState.OnEnter += (orig, self) =>
            {
                orig(self);
                if (Reference.TutorialInfo.Value && Reference.Mods("0.fr4nsson.MultiplayerPause")) TutorialHelper.Tutorial(Reference.TutorialPause, "pause");
                else TutorialHelper.Tutorial(Reference.TutorialInfo, "info");
                if (Reference.Mods("com.TPDespair.ZetArtifacts") && TossingScrapAltless.isTossingEnabled()) TutorialHelper.Tutorial(Reference.TutorialDrop, "drop");
            };
        }
    }
}

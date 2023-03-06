using RiskyMonkeyBase.Tutorials;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskyMonkeyBase.Tweaks
{
    public class InvulnerableInUITweaks
    {
        public static bool invopen = false;
        public static bool enabled = false;
        public static void Patch()
        {
            On.RoR2.UI.ScoreboardController.OnEnable += (orig, self) => { orig(self); invopen = true; };
            On.RoR2.UI.ScoreboardController.OnDisable += (orig, self) => { orig(self); invopen = false; };
            RoR2Application.onFixedUpdate += () =>
            {
                if (TutorialHelper.input == null) return;
                LocalUser lu = LocalUserManager.GetFirstLocalUser();
                if (lu == null || lu.cachedMaster == null || lu.cachedBody == null) return;
                if (!invopen && (TutorialHelper.input.eventSystem.cursorOpenerCount > 0 || TutorialHelper.input.eventSystem.cursorOpenerForGamepadCount > 0))
                {
                    if (!enabled)
                    {   
                        enabled = true;
                        lu.cachedMaster.godMode = true;
                        lu.cachedMaster.UpdateBodyGodMode();
                        lu.cachedBody.AddBuff(RoR2Content.Buffs.Immune);
                    }
                }
                else
                {
                    if (enabled)
                    {
                        enabled = false;
                        lu.cachedMaster.godMode = false;
                        lu.cachedMaster.UpdateBodyGodMode();
                        lu.cachedBody.RemoveBuff(RoR2Content.Buffs.Immune);
                    }
                }
            };
        }
    }
}

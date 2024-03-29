﻿using BepInEx.Configuration;
using R2API.Utils;
using RoR2.UI;
using UnityEngine;

namespace RiskyMonkeyBase.Tutorials
{
    public class TutorialHelper
    {
        public static MPInput input;

        public struct ComplexDialogBox
        {
            public SimpleDialogBox box;
            public MPEventSystem events;
            public ComplexDialogBox(SimpleDialogBox box, MPEventSystem events) { this.box = box; this.events = events; }
        }

        public static ComplexDialogBox ShowPopup(string title, string desc, bool noCancel = false)
        {
            Time.timeScale = 0f;
            input.eventSystem.cursorOpenerCount++;
            input.eventSystem.cursorOpenerForGamepadCount++;
            SimpleDialogBox box = SimpleDialogBox.Create();
            box.headerToken = new SimpleDialogBox.TokenParamsPair(title);
            box.descriptionToken = new SimpleDialogBox.TokenParamsPair(desc);
            if (!noCancel) box.AddActionButton(() => DefaultCancel(input.eventSystem), "RISKYMONKEY_TUTORIAL_ACKNOWLEDGE");
            return new ComplexDialogBox(box, input.eventSystem);
        }
        
        public static void DefaultCancel(MPEventSystem events)
        {
            events.cursorOpenerCount--;
            events.cursorOpenerForGamepadCount--;
            if (SimpleDialogBox.instancesList.Count <= 1) Time.timeScale = 1f;
        }

        public static bool Tutorial(ConfigEntry<bool> tutorial, string token)
        {
            if (!tutorial.Value)
            {
                RiskyMonkeyBase.Log.LogDebug("Showing tutorial for " + token);
                ShowPopup("RISKYMONKEY_TUTORIAL_" + token.ToUpper() + "_TITLE", "RISKYMONKEY_TUTORIAL_" + token.ToUpper() + "_DESC");
                tutorial.Value = true; 
                Reference.Config.Save();
                return true;
            }
            return false;
        }
        
    }
}

using BepInEx.Configuration;
using UnityEngine;

namespace RiskyMonkeyBase.Tweaks
{
    public class EmoteKeybind
    {
        public static void Patch()
        {
            if (Reference.SeriousMode.Value) EmotesAPI.Settings.EmoteWheel.Value = KeyboardShortcut.Empty;
            else if (EmotesAPI.Settings.EmoteWheel.Value.Equals(KeyboardShortcut.Empty)) EmotesAPI.Settings.EmoteWheel.Value = new KeyboardShortcut(KeyCode.C);
        }
    }
}

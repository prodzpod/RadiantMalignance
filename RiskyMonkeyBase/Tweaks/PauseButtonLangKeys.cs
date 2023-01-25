using RoR2;
using RoR2.UI;
using RoR2.UI.SkinControllers;
using UnityEngine;

namespace RiskyMonkeyBase.Tweaks
{
    public class PauseButtonLangKeys
    {
        public static void PatchQuickRestart()
        {
            RiskyMonkeyBase.Log.LogInfo("[[quick restart langkey]] module loaded");
            On.RoR2.UI.PauseScreenController.Awake += (orig, self) =>
            {
                orig(self);
                if (Run.instance is null || PreGameController.instance) return;
                Transform panel = self.mainPanel.GetChild(0);
                for (var i = 0; i < panel.childCount; i++)
                {
                    var button = panel.GetChild(i);
                    if (button.name != "Button") continue;
                    for (var j = 0; j < button.childCount; j++)
                    {
                        var textBox = button.GetChild(j);
                        if (textBox.name != "Text") continue;
                        var text = textBox.GetComponent<HGTextMeshProUGUI>().text;
                        if (text == "Restart") self.mainPanel.GetChild(0).GetChild(i).GetChild(j).GetComponent<HGTextMeshProUGUI>().text = Language.GetString("BUTTON_RESTART");
                        else if (text == "Character Select") self.mainPanel.GetChild(0).GetChild(i).GetChild(j).GetComponent<HGTextMeshProUGUI>().text = Language.GetString("BUTTON_CHARSELECT");
                    }
                }
            };
        }

        public static void PatchPhotoMode()
        {
            RiskyMonkeyBase.Log.LogInfo("[[photo mode langkey]] module loaded");
            On.RoR2.UI.PauseScreenController.Awake += (orig, self) =>
            {
                orig(self);
                Transform controller = self.GetComponentInChildren<ButtonSkinController>().gameObject.transform.parent;
                for (var i = 0; i < controller.childCount; i++)
                {
                    Transform child = controller.GetChild(i);
                    if (child.name != "GenericMenuButton (Photo mode)") continue;
                    child.GetComponent<ButtonSkinController>().GetComponent<LanguageTextMeshController>().token = Language.GetString("BUTTON_PHOTOMODE");
                }
            };
        }

        public static void PatchOrder()
        {
            On.RoR2.UI.PauseScreenController.Awake += (orig, self) =>
            {
                orig(self);
                Transform controller = self.GetComponentInChildren<ButtonSkinController>().gameObject.transform.parent;
                Transform quit1 = null;
                Transform quit2 = null;
                for (var i = 0; i < controller.childCount; i++)
                {
                    Transform child = controller.GetChild(i);
                    if (child.name == "GenericMenuButton (Quit to Desktop)") quit1 = child;
                    else if (child.name == "GenericMenuButton (Quit to Main)") quit2 = child;
                }
                if (quit1 != null) quit1.SetParent(null);
                if (quit2 != null) quit2.SetAsLastSibling();
            };
        }
    }
}

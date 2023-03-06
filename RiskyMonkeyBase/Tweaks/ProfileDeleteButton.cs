using UnityEngine;
using UnityEngine.UI;
using RoR2;
using RoR2.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using Object = UnityEngine.Object;
using RoR2.UI.SkinControllers;

namespace RiskyMonkeyBase.Tweaks
{
    public class ProfileDeleteButton
    {
        public static Sprite Icon;
        public static GameObject Parent;
        public static GameObject Container;
        public static GameObject Button;
        public static List<GameObject> temps = new();
        public static void Patch()
        {
            Icon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/iconDelete.png");
            On.RoR2.UI.UserProfileListController.RebuildElements += (orig, self) =>
            {
                orig(self);
                RiskyMonkeyBase.Log.LogDebug("DeleteButtons Added");
                Parent = GameObject.Find("MENU: Profile").transform.Find("ProfileMenu").Find("PopupPanel").Find("SelectionPanel").Find("ContentArea").Find("Panel").Find("UserProfileList").Find("Scroll View").Find("Viewport").Find("Content").gameObject;
                List<GameObject> profiles = new();
                foreach (var element in self.elementsList) profiles.Add(element.gameObject);
                foreach (var temp in temps) if (temp != null) RemoveAll(temp);
                temps.Clear();
                foreach (var profile in profiles)
                {
                    GameObject container = Object.Instantiate(Container);
                    profile.GetComponent<LayoutElement>().preferredWidth = 784;
                    profile.transform.SetParent(container.transform);
                    GameObject button = Object.Instantiate(Button);
                    button.GetComponent<HGButton>().onClick = PromptRemove(profile.gameObject);
                    button.transform.SetParent(container.transform);
                    container.transform.SetParent(Parent.transform);
                    temps.Add(container);
                }
            };
            On.RoR2.UI.MainMenu.BaseMainMenuScreen.OnEnter += (orig, self, _) =>
            {
                orig(self, _);
                if (Container == null)
                {
                    RiskyMonkeyBase.Log.LogDebug("DeleteButtons Container Initialized");
                    Parent = GameObject.Find("MENU: Profile").transform.Find("ProfileMenu").Find("PopupPanel").Find("SelectionPanel").Find("ContentArea").Find("Panel").Find("UserProfileList").Find("Scroll View").Find("Viewport").Find("Content").gameObject;
                    Container = Object.Instantiate(Parent);
                    List<GameObject> toDelete = new();
                    for (var i = 0; i < Container.transform.childCount; i++) toDelete.Add(Container.transform.GetChild(i).gameObject);
                    foreach (var o in toDelete) RemoveAll(o);
                    Object.DestroyImmediate(Container.GetComponent<VerticalLayoutGroup>());
                    Container.AddComponent<HorizontalLayoutGroup>();
                    Container.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
                if (Button == null)
                {
                    RiskyMonkeyBase.Log.LogDebug("DeleteButtons Button Initialized");
                    Button = Object.Instantiate(GameObject.Find("MENU: More").transform.Find("MoreMenu").Find("Main Panel").Find("InfoPanel").Find("Contents, Contact").Find("Strip").Find("GenericIconButton").gameObject);
                    Button.transform.Find("ButtonImage").GetComponent<Image>().sprite = Icon;
                }
            };
        }

        public static void RemoveAll(GameObject o)
        {
            List<GameObject> children = new();
            for (var i = 0; i < o.transform.childCount; i++) children.Add(o.transform.GetChild(i).gameObject);
            foreach (var child in children) RemoveAll(child);
            foreach (var type in new Type[]
            {
                typeof(HGTextMeshProUGUI), typeof(Image), typeof(RefreshCanvasDrawOrder), typeof(ButtonSkinController), typeof(UserProfileListElementController), typeof(HGButton), typeof(MPEventSystemLocator), typeof(Canvas), typeof(CanvasRenderer)
            }) foreach (Component c in o.GetComponents(type)) if (c is not RectTransform) Object.DestroyImmediate(c);
            Object.DestroyImmediate(o);
        }

        public static Button.ButtonClickedEvent PromptRemove(GameObject caller)
        {
            UserProfileListElementController component = caller.GetComponent<UserProfileListElementController>();
            Button.ButtonClickedEvent ret = new();
            InvokableCall call = new(() => 
            {
                if (component.userProfile == null)
                {
                    Debug.LogError("!!!???");
                }
                else
                {
                    SimpleDialogBox simpleDialogBox1 = SimpleDialogBox.Create();
                    string consoleString = "user_profile_delete \"" + component.userProfile.fileName + "\"";
                    SimpleDialogBox simpleDialogBox2 = simpleDialogBox1;
                    SimpleDialogBox.TokenParamsPair tokenParamsPair1 = new SimpleDialogBox.TokenParamsPair();
                    tokenParamsPair1.token = "USER_PROFILE_DELETE_HEADER";
                    tokenParamsPair1.formatParams = null;
                    SimpleDialogBox.TokenParamsPair tokenParamsPair2 = tokenParamsPair1;
                    simpleDialogBox2.headerToken = tokenParamsPair2;
                    SimpleDialogBox simpleDialogBox3 = simpleDialogBox1;
                    tokenParamsPair1 = new SimpleDialogBox.TokenParamsPair();
                    tokenParamsPair1.token = "USER_PROFILE_DELETE_DESCRIPTION";
                    tokenParamsPair1.formatParams = new object[1] { component.userProfile.name };
                    SimpleDialogBox.TokenParamsPair tokenParamsPair3 = tokenParamsPair1;
                    simpleDialogBox3.descriptionToken = tokenParamsPair3;
                    simpleDialogBox1.AddCommandButton(consoleString, "USER_PROFILE_DELETE_YES");
                    simpleDialogBox1.AddCancelButton("USER_PROFILE_DELETE_NO");
                }
            });
            ret.AddCall(call);
            return ret;
        }
    }
}

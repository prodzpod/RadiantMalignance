using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using R2API;
using R2API.Utils;
using RiskyMonkeyBase.Achievements;
using RiskyMonkeyBase.LangDynamic;
using RiskyMonkeyBase.Tutorials;
using RiskyMonkeyBase.Tweaks;
using RiskyMonkeyBase.Contents;
using RoR2;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using RoR2.UI;
using TMPro;
using UnityEngine.Events;
using System.Reflection;
using System.IO;
namespace RiskyMonkeyBase
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin(Reference.PluginGUID, Reference.PluginName, Reference.PluginVersion)]
    [R2APISubmoduleDependency(nameof(LanguageAPI), nameof(LoadoutAPI), nameof(DifficultyAPI), nameof(ArtifactCodeAPI), nameof(ItemAPI), nameof(DirectorAPI))]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]

    [BepInDependency("bubbet.bubbetsitems", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.12GaugeAwayFromFace.TeamFortress2_Engineer_Engineer_Skin", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Anreol.ReleasedFromTheVoid", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.ApexConspirator.MadVeteran", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.ArtyBoi.CryingGolem", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.ArtyBoi.Kindred", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.ArtyBoi.KindredsLizard", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.ArtyBoi.SkullDuggery", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.ArtyBoi.YinYang", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.bobblet.UltrakillV1BanditSkin", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Borbo.LazyBastardEngineer", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Chris-Stetvenson-Git.FasterBossWait", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.cuno.discord", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.doctornoodlearms.huntressmomentum", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.dotflare.LTT1", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Dragonyck.PhotoMode", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Egg.EggsSkills", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.EmnoX.LightDreamer", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.EmnoX.VoidDreamerVFSKIN", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.EnforcerGang.Enforcer", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.eyeknow.HighFashionLoader", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.FrostRay.FrostRaySkinPack", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Heyimnoob.BioDroneAcrid", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.HIFU.StageAesthetic", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.hilliurn.LunarVoidREX", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.IkalaGaming.QuickRestart", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.justinderby.bossantisoftlock", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.KrononConspirator.ScavangerLoader", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.KrononConspirator.Solus_RailGunner", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.KrononConspirator.Thy_Providence", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.LexLamb.MechArtificer", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.lodington.AutoShot", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.macawesone.EngiShotgun", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.marklow.HellfireCaptain", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.MAVRI.CaptainUnderglowDrip", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Moffein.BanditDynamite", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Moffein.EngiAutoFire", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Moffein.SniperClassic", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Ner0ls.HolyCrapForkIsBack", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Rero.MasquePack", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.RetroInspired.Gupmando", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.RetroInspired.RailGunnerSkins", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.rob.DiggerUnearthed", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.rob.Direseeker", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.rob.Paladin", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.sixfears7.M1BitePlus", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Skell.GoldenCoastPlus", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.SussyBnuuy.PEPSIMANVoidFiend", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.TailLover.DinoMulT", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.TailLover.VoidJailerFiend", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Takrak.RailgunnerAltTextures", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.themysticsword.bulwarkshaunt", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.TPDespair.ZetArtifacts", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Tymmey.Templar", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Viliger.ShrineOfRepair", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.weliveinasociety.CustomEmotesAPI", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Wolfo.ArtifactOfDissimilarity", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Wolfo.LittleGameplayTweaks", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Wolfo.WolfoQualityOfLife", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.xoxfaby.BetterUI", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("CuteDoge.ArtifactOfChosen", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("de.userstorm.banditweaponmodes", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("de.userstorm.captainshotgunmodes", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("HIFU.ArtifactOfBlindness", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("HIFU.Inferno", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("PlasmaCore.ForgottenRelics", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("prodzpod.TemplarSkins", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("zombieseatflesh7.ArtifactOfPotential", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("xyz.yekoc.PassiveAgression", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Wolfo.WolfoQualityOfLife", BepInDependency.DependencyFlags.SoftDependency)]

    public class RiskyMonkeyBase : BaseUnityPlugin
    {
        // It's Risky Monkey for me, Radiant Malignance for them, but for you, it's Reflection Monstrocity :smile
        public static ManualLogSource Log;
        public static Harmony Harmony;
        internal static PluginInfo pluginInfo;
        private static AssetBundle _assetBundle;
        private static bool lobbyVisited = false;
        private static bool wonLastGame = false;
        public static AssetBundle AssetBundle
        {
            get
            {
                if (_assetBundle == null)
                    _assetBundle = AssetBundle.LoadFromFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(pluginInfo.Location), "riskymonkey"));
                return _assetBundle;
            }
        }
        public void Awake()
        {
            pluginInfo = Info;
            Log = Logger;
            Harmony = new Harmony(Reference.PluginGUID); // uh oh!
            Reference.InitConfig(Paths.ConfigPath);
            BetterDesc.Patch();

            GrandMasteryFix.Patch();
            if (Reference.Mods("com.rob.Paladin"))
            {
                if (Reference.FixPaladinAchievementNames.Value) PaladinAchievementsFix.Patch();
            }
            if (Reference.Mods("com.Egg.EggsSkills")) EggsSkillsPunctuationFix.Patch();
            RemoveDifficultyTweaks.Patch();
            if (Reference.Mods("com.IkalaGaming.QuickRestart")) PauseButtonLangKeys.PatchQuickRestart();
            if (Reference.Mods("com.Dragonyck.PhotoMode")) PauseButtonLangKeys.PatchPhotoMode();

            if (Reference.RadiantMalignance.Value)
            {
                if (File.Exists(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "fixes.modpacklanguage"))) LanguageAPI.AddPath(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "fixes.modpacklanguage"));
                else if (File.Exists(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "Lang\\fixes.modpacklanguage"))) LanguageAPI.AddPath(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "Lang\\fixes.modpacklanguage"));
            }
            if (File.Exists(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "krpatch.overlaylanguage"))) LanguageAPI.AddPath(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "krpatch.overlaylanguage"));
            else if (File.Exists(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "Lang\\krpatch.overlaylanguage"))) LanguageAPI.AddPath(Assembly.GetExecutingAssembly().Location.Replace(Reference.PluginName + ".dll", "Lang\\krpatch.overlaylanguage"));
            if (!Reference.SeriousMode.Value) Memes.Patch();
            if (Reference.Mods("com.doctornoodlearms.huntressmomentum")) LanguageAPI.AddOverlay("NOODLE_HUNTRESSPASSIVE_DESC", $"Sprinting gives stacks of momentum.\nAt 10 stacks, the next attack will be a <style=cIsDamage>Critical Strike</style>.");
            if (Reference.Mods("com.xoxfaby.BetterUI")) BetterUIStatsLangKey.Patch();
            if (Reference.Mods("com.Tymmey.Templar"))
            {
                TemplarSkillFix.Patch();
                TemplarSkillFix.LogbookPatch();
            }
            if (Reference.Mods("com.TPDespair.ZetArtifacts"))
            {
                TossingScrapAltless.Patch();
                if (!Reference.ReviveBetweenMithrixPhase.Value) ReviveBetweenMithrixPhase.Patch();
            }
            if (Reference.Mods("com.HIFU.StageAesthetic") && Reference.SAAltChance.Value < 1) SAAltChance.Patch();
            if (Reference.Mods("bubbet.bubbetsitems"))
            {
                if (Reference.VoidLunarOnBazaar.Value > 0) VoidLunarTweaks.Bazaar();
                if (Reference.CleansingPoolVoidLunar.Value) VoidLunarTweaks.CleansingPool();
                VoidLunarTweaks.Donkey();
            }
            PauseButtonLangKeys.PatchOrder();
            if (Reference.Mods("com.rune580.riskofoptions")) RiskOfOptionsSupport.Patch();
            if (Reference.Mods("com.themysticsword.bulwarkshaunt")) LanguageAPI.AddOverlay("ACHIEVEMENT_BULWARKSHAUNT_VOIDSURVIVORWINGHOSTWAVE_NAME", "¡¸V??oid Fiend¡»: Spiritual Success"); // bad >:(
            if (Reference.Mods("com.Anreol.ReleasedFromTheVoid"))
            {
                if (Reference.RFTVDisableVoidCoin.Value) DisableRFTVFeatures.VoidCoin();
                if (Reference.RFTVDisableVoidSuppressor.Value) DisableRFTVFeatures.VoidSuppressor();
                if (Reference.RFTVDisableLocusTweaks.Value) DisableRFTVFeatures.LocusTweaks();
                if (Reference.RFTVDisableItemEnable.Value) DisableRFTVFeatures.ItemEnable();
                if (Reference.RFTVDisableCommandoSkin.Value) DisableRFTVFeatures.CommandoSkin();
            }
            if (Reference.FocusedConvergenceRangeLimit.Value != 3 || Reference.FocusedConvergenceRateLimit.Value != 3) FocusedConvergenceFix.Patch();
            if (Reference.Mods("com.weliveinasociety.CustomEmotesAPI")) EmoteKeybind.Patch();
            ReorderSurvivors.Patch();
            if (Reference.Mods("PlasmaCore.ForgottenRelics")) ForgottenRelicsTweaks.ForgottenRelics();
            if (Reference.Mods("com.justinderby.bossantisoftlock")) BATweaks.Patch();
            if (Reference.ScrapperFrequency.Value != 1f) RepairTweaks.PatchFrequency();
            if (Reference.ScrapperMaxUses.Value != 0) RepairTweaks.PatchUses();
            if (Reference.ScrapperStackAtOnce.Value != 0) RepairTweaks.PatchScrapper();
            if (Reference.Mods("com.Viliger.ShrineOfRepair"))
            {
                ShrineOfRepairHook.Patch();
                if (Reference.ReprogrammerActivate.Value) Reprogrammer.Patch();
                RepairTweaks.repairList = new();
                if (Reference.RepairStackAtOnce.Value != 0) RepairTweaks.PatchRepair();
            }
            if (Reference.Mods("com.cuno.discord")) FloodingTheyLogsLikeVoidSurvivor.Patch();
            if (Reference.HEADSTChanges.Value) HEADSTTweaks.Patch();
            if (Reference.LunarBudOnStage2.Value) LunarBudTweaks.Patch();
            if (Reference.Mods("com.Wolfo.WolfoQualityOfLife")) WolfoTweaksTweaks.Patch();

            // achievements
            RiskyMonkeyAchievements.Patch();
            TutorialInit.Patch();
            TutorialSeen.Patch();
            TutorialOther.Patch();
            PaladinWinGhostWaveAchievement.Patch();
            ArtifactAchievement.Patch();
            InfernoAchievement.Patch();
            E8Achievement.Patch();
            SimulacrumAchievement.Patch();
            EnemyAchievement.Patch();
            CleanUpMiscAchievements.Patch();
            ExtraSkinAchievement.Patch();
            ItemAchievement.Patch();
            LoadoutAchievement.Patch();
            SurvivorAchievement.Patch();
            ReorderChallenges.Patch();
            ArtifactHints.Patch();

            // downpour
            if (Reference.DownpourScaling.Value > 0) DownpourDifficulty.AddDifficulty();
            if (Reference.RadiantMalignance.Value)
            {
                On.RoR2.UI.MainMenu.BaseMainMenuScreen.OnEnter += (orig, self, mainMenuController) =>
                {
                    orig(self, mainMenuController);
                    GameObject _1 = GameObject.Find("GenericMenuButton (Music&More)");
                    if (_1 != null)
                    {
                        HGTextMeshProUGUI _2 = _1.GetComponentInChildren<HGTextMeshProUGUI>();
                        if (_2 != null && _2.text == "Credits") return;
                    }
                    GameObject obj = GameObject.Find("LogoImage");
                    if (obj != null)
                    {
                        Log.LogDebug("Changing Logo Image");
                        obj.transform.localPosition = new Vector3(0, 200, 0);
                        obj.transform.localScale = new Vector3(2, 2, 2);
                        obj.GetComponent<Image>().sprite = AssetBundle.LoadAsset<Sprite>(Reference.SeriousMode.Value ? style("Assets/logo.png", "Assets/logoVoid.png", "Assets/logoLunar.png") : "Assets/logoMeme.png");
                    }
                    if (mainMenuController.titleMenuScreen != null)
                    {
                        Log.LogDebug("Changing Title Camera");
                        mainMenuController.titleMenuScreen.desiredCameraTransform.position = new Vector3(style(-20, -10, -10), style(5, 35, 30), style(-10, -20, 45));
                        mainMenuController.titleMenuScreen.desiredCameraTransform.rotation = new Quaternion(style(-0.104f, -0.130f, 0.366f), style(0.130f, 0.043f, 0.453f), style(-0.014f, -0.017f, 0.211f), style(0.986f, 0.983f, 0.785f));
                    }
                    GameObject volume = GameObject.Find("GlobalPostProcessVolume");
                    if (volume == null) return; // what is this place lmfao
                    ColorGrading cgrade = volume.GetComponent<PostProcessVolume>().profile.GetSetting<ColorGrading>();
                    if (cgrade != null)
                    {
                        Log.LogDebug("Changing PostProcessing");
                        cgrade.gain.value = new Vector4(style(1, 1, 1), style(1, 0, 1), style(1, 1, 5), style(0, -1, 32));
                        cgrade.gain.overrideState = true;
                        cgrade.hueShift.value = style(-200, 10, -20);
                        cgrade.hueShift.overrideState = true;
                        cgrade.saturation.value = style(100, -25, -50);
                        cgrade.saturation.overrideState = true;
                        cgrade.contrast.value = style(150, 150, 200);
                        cgrade.contrast.overrideState = true;
                        if (lobbyVisited && wonLastGame) GameObject.Find("Rain").SetActive(false);
                    }
                    Log.LogDebug("Changing Buttons");
                    if (Reference.Mods("com.xoxfaby.BetterUI")) // sorry :(
                    {
                        GameObject gameObject = GameObject.Find("BetterUIButton");
                        if (gameObject != null) gameObject.transform.SetParent(null);
                    }
                    GameObject gameObject2 = GameObject.Find("GenericMenuButton (Signup)");
                    if (gameObject2 != null) gameObject2.transform.SetParent(null);
                    GameObject gameObject3 = GameObject.Find("GenericMenuButton (Music&More)");
                    if (gameObject3 != null) gameObject3.GetComponentInChildren<HGTextMeshProUGUI>().text = "Credits";

                    Log.LogDebug("Changing Credits");
                    GameObject infoPanel = GameObject.Find("MENU: More").transform.Find("MoreMenu").Find("Main Panel").Find("InfoPanel").gameObject;
                    GameObject header = infoPanel.transform.Find("HeaderContainer").GetChild(0).gameObject;
                    if (header != null && header.transform.GetChild(1).gameObject.name != "GenericHeaderButton (Contact)")
                    {
                        header.transform.Find("GenericHeaderButton (Contact)").SetAsFirstSibling();
                        header.transform.GetChild(1).SetAsFirstSibling(); // gamepad glyph return
                        infoPanel.transform.Find("Contents, Music").SetAsLastSibling();
                        infoPanel.transform.Find("Contents, Licensing").SetAsLastSibling();

                        GameObject contactPanel = infoPanel.transform.Find("Contents, Contact").gameObject;
                        GameObject title = Instantiate(contactPanel.transform.Find("TMPText").gameObject, contactPanel.transform);
                        GameObject modpackCredits = Instantiate(contactPanel.transform.Find("Strip").gameObject, contactPanel.transform);
                        GameObject modsCredits = Instantiate(contactPanel.transform.Find("Strip").gameObject, contactPanel.transform);
                        GameObject prod = Instantiate(contactPanel.transform.Find("Strip").gameObject, contactPanel.transform);
                        prod.transform.SetAsFirstSibling();
                        modsCredits.transform.SetAsFirstSibling();
                        modpackCredits.transform.SetAsFirstSibling();
                        title.transform.SetAsFirstSibling();

                        title.GetComponent<LanguageTextMeshController>().token = "RISKYMONKEY_CREDITS_DESC";
                        modpackCredits.transform.GetChild(0).Find("ButtonImage").gameObject.GetComponent<Image>().sprite = AssetBundle.LoadAsset<Sprite>("Assets/iconOutline.png");
                        modpackCredits.transform.Find("SSText").gameObject.GetComponent<HGTextMeshProUGUI>().text = "Modpack Credits";
                        modpackCredits.transform.GetChild(0).gameObject.GetComponent<HGButton>().onClick = OpenURL(modpackCredits.transform.GetChild(0).gameObject, "https://prodzpod.github.io/RadiantMalignance/modpackCredits.html");
                        modsCredits.transform.GetChild(0).Find("ButtonImage").gameObject.GetComponent<Image>().sprite = AssetBundle.LoadAsset<Sprite>("Assets/iconMods.png");
                        modsCredits.transform.Find("SSText").gameObject.GetComponent<HGTextMeshProUGUI>().text = "Included Mod Credits";
                        modsCredits.transform.GetChild(0).gameObject.GetComponent<HGButton>().onClick = OpenURL(modsCredits.transform.GetChild(0).gameObject, "https://prodzpod.github.io/RadiantMalignance/modsCredits.html");
                        prod.transform.GetChild(0).Find("ButtonImage").gameObject.GetComponent<Image>().sprite = AssetBundle.LoadAsset<Sprite>("Assets/iconProd.png");
                        prod.transform.Find("SSText").gameObject.GetComponent<HGTextMeshProUGUI>().text = "Contact prod through discord: @prod#0339";
                        prod.transform.GetChild(0).gameObject.GetComponent<HGButton>().onClick = OpenURL(prod.transform.GetChild(0).gameObject, "https://github.com/prodzpod");

                        for (var i = 0; i < contactPanel.transform.childCount; i++)
                        {
                            GameObject child = contactPanel.transform.GetChild(i).gameObject;
                            if (!child.name.Contains("Strip")) continue;
                            GameObject button = child.transform.GetChild(0).gameObject;
                            button.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 10);
                            button.transform.localPosition = new Vector3(-250, 0, 0);
                        }
                    }
                    if (mainMenuController.moreMenuScreen == self) header.transform.Find("GenericHeaderButton (Contact)").gameObject.GetComponent<HGButton>().InvokeClick();
                };
                On.RoR2.UI.SteamBuildIdLabel.Start += (orig, self) =>
                {
                    orig(self);
                    self.GetComponent<TextMeshProUGUI>().text += " <style=cIsDamage>+ RM " + Reference.PluginVersion + "</style>";
                };
                On.RoR2.SceneCatalog.OnActiveSceneChanged += (orig, oldScene, newScene) =>
                {
                    if (newScene.name.Contains("lobby")) lobbyVisited = true;
                    orig(oldScene, newScene);
                };
                InfiniteTowerRun.onAllEnemiesDefeatedServer += (waveController) =>
                {
                    InfiniteTowerRun instance = Run.instance as InfiniteTowerRun;
                    if (instance != null) wonLastGame = instance.waveIndex >= 50;
                };
                Run.onClientGameOverGlobal += (run, runReport) =>
                {
                    if (run == null || runReport == null || run as InfiniteTowerRun != null) return;
                    if (runReport.gameEnding != null) wonLastGame = runReport.gameEnding.isWin;
                };
            }
        }

        public Button.ButtonClickedEvent OpenURL(GameObject caller, string url)
        {
            Button.ButtonClickedEvent ret = new();
            InvokableCall call = new(() => { caller.GetComponent<EventFunctions>().OpenURL(url); });
            ret.AddCall(call);
            return ret;
        }

        public T style<T>(T start, T lose, T win)
        {
            if (!lobbyVisited) return start;
            if (wonLastGame) return win;
            return lose;
        }

        public void Start()
        {
            if (Reference.Mods("com.rune580.riskofoptions")) RiskOfOptionsHideTweaks.Patch();
            ReorderSkins.Patch();
            if (Reference.EnableDamageNumbers.Value) SettingsConVars.enableDamageNumbers.value = true;
            if (Reference.Mods("com.rob.Paladin", "com.KrononConspirator.Thy_Providence", "com.themysticsword.bulwarkshaunt") && Reference.EnablePaladinBulwark.Value) PaladinWinGhostWaveAchievement.PostPatch();
        }
    }
}

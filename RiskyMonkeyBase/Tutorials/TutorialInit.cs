using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.ExpansionManagement;
using RoR2.UI;
using RoR2.UI.MainMenu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RiskyMonkeyBase.Tutorials
{
    public class TutorialInit
    {
        public static bool shownStuffAlready;
        public static bool hasWonGame = false;
        public static List<ArtifactDef> lockedArts;
        public static void Patch()
        {
            shownStuffAlready = false;
            lockedArts = new();
            RoR2.ContentManagement.ContentManager.onContentPacksAssigned += (obj) =>
            {
                if (Reference.ResetTutorial.Value) // watch them at least once lol
                {
                    SplashScreenController.cvSplashSkip.SetBool(false);
                    IntroCutsceneController.cvIntroSkip.SetBool(false);
                }
            };
            On.RoR2.UI.MainMenu.BaseMainMenuScreen.OnEnter += (orig, self, mainMenuController) =>
            {
                orig(self, mainMenuController);
                if (!shownStuffAlready)
                {
                    shownStuffAlready = true;
                    if (!Reference.RadiantMalignance.Value)
                    { // disable tutorials
                        Reference.SetAllTutorials(true);
                        Reference.ResetTutorial.Value = false;
                        Reference.GetChangelog.Value = false;
                    }
                    if (Reference.ResetTutorial.Value)
                    {
                        Reference.ResetTutorial.Value = false;
                        Reference.SetAllTutorials(false);
                        TutorialHelper.ComplexDialogBox box = TutorialHelper.ShowPopup("RISKYMONKEY_TUTORIAL_RESET_TITLE", "RISKYMONKEY_TUTORIAL_RESET_DESC", true);
                        box.box.AddActionButton(() => TutorialHelper.DefaultCancel(box.events), "RISKYMONKEY_TUTORIAL_RESET_ALL");
                        box.box.AddActionButton(() => { TutorialHelper.DefaultCancel(box.events); Reference.FirstRun.Value = false; Reference.SetAllTutorials(true); Reference.SetModdedTutorials(false); Reference.Config.Save(); }, "RISKYMONKEY_TUTORIAL_RESET_MODPACK");
                        box.box.AddActionButton(() => { TutorialHelper.DefaultCancel(box.events); Reference.FirstRun.Value = false; Reference.SetAllTutorials(true); Reference.Config.Save(); }, "RISKYMONKEY_TUTORIAL_RESET_NONE");
                    }
                    if (Reference.GetChangelog.Value && Reference.LastVersion.Value != Reference.Releases[0])
                    {
                        List<int> releases = new(Reference.Releases);
                        int idx = releases.IndexOf(Reference.LastVersion.Value);
                        if (idx == -1) idx = Reference.Releases.Length - 1;
                        string txt = "";
                        for (var i = idx; i >= 0; i--) if (Reference.LastVersion.Value != Reference.Releases[i]) txt += Language.GetString("RISKYMONKEY_CHANGELOG_" + Reference.Releases[i]) + "\n\n";
                        string[] lines = txt.Split('\n');
                        txt = "";
                        for (var i = 0; i < Math.Min(20, lines.Length); i++) txt += lines[i] + "\n";
                        txt += "\n\n" + Language.GetString("RISKYMONKEY_CHANGELOG_FOOTER");
                        TutorialHelper.ShowPopup("RISKYMONKEY_CHANGELOG_TITLE", txt);
                        Reference.LastVersion.Value = Reference.Releases[0];
                    }
                    if (!Reference.SeriousMode.Value && Reference.Mods("com.weliveinasociety.CustomEmotesAPI")) TutorialHelper.Tutorial(Reference.TutorialEmote, "emote");
                    if (Reference.FirstRun.Value)
                    {
                        Transform controller = MainMenuController.instance.mainMenuButtonPanel.transform.GetChild(0);
                        for (var i = 0; i < controller.childCount; i++)
                        {
                            Transform button = controller.GetChild(i);
                            if (button.name == "GenericMenuButton (Extra Game Mode)" || button.name == "GenericMenuButton (Music&More)") button.SetParent(null);
                        }
                    }
                }
            };
            Run.onClientGameOverGlobal += (run, runReport) =>
            {
                if (runReport.gameEnding.isWin) hasWonGame = true;
            };
            On.RoR2.PreGameController.Awake += (orig, self) =>
            {
                orig(self);
                if (Reference.FirstRun.Value)
                {
                    GameObject gameObject = GameObject.Find("GenericMenuButton (Loadout)");
                    if (gameObject != null) gameObject.transform.SetParent(null);
                }
                else if (Reference.TutorialLoadout.Value && !Reference.TutorialDrizzle.Value)
                {
                    Reference.TutorialDrizzle.Value = true;
                    Reference.TutorialThunderstorm.Value = true;
                    if (hasWonGame) TutorialHelper.ShowPopup("RISKYMONKEY_TUTORIAL_THUNDERSTORM_TITLE", "RISKYMONKEY_TUTORIAL_THUNDERSTORM_DESC");
                    else TutorialHelper.ShowPopup("RISKYMONKEY_TUTORIAL_DRIZZLE_TITLE", "RISKYMONKEY_TUTORIAL_DRIZZLE_DESC");
                }
                else TutorialHelper.Tutorial(Reference.TutorialLoadout, "loadout");
            };
            On.RoR2.UI.RuleBookViewer.SetData += (orig, self, choiceAvailability, ruleBook) =>
            {
                orig(self, choiceAvailability, ruleBook);
                if (!Reference.FirstRun.Value) return;
                for (var index = 0; index < RuleCatalog.categoryCount; ++index)
                {
                    if (RuleCatalog.GetCategoryDef(index).displayToken != "RULE_HEADER_ARTIFACTS") continue;
                    ReadOnlyCollection<RuleCategoryController> elements = AccessTools.FieldRefAccess<RuleBookViewer, UIElementAllocator<RuleCategoryController>>(self, "categoryElementAllocator").elements;
                    if (0 <= index && index < elements.Count) elements[index].gameObject.SetActive(false);
                }
            };
            IL.RoR2.UI.RuleCategoryController.SetData += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdarg(0), x => x.MatchLdfld<RuleCategoryController>("headerColorImages"), x => x.MatchStloc(6));
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Action<RuleCategoryController>>((self) =>
                {
                    List<RuleDef> rules = new();
                    foreach (var exp in ExpansionCatalog.expansionDefs) if (exp.nameToken != "DLC1_NAME") rules.Add(RuleCatalog.FindRuleDef("Expansions." + exp.name));
                    if (Reference.FirstRun.Value) foreach (var art in AccessTools.StaticFieldRefAccess<ArtifactDef[]>(typeof(ArtifactCatalog), "artifactDefs")) rules.Add(RuleCatalog.FindRuleDef("Artifacts." + art.cachedName));
                    List<RuleDef> rulesToDisplay = AccessTools.FieldRefAccess<RuleCategoryController, List<RuleDef>>(self, "rulesToDisplay");
                    foreach (var rule in rules) if (rulesToDisplay.Contains(rule)) rulesToDisplay.Remove(rule);
                    foreach (var art in AccessTools.StaticFieldRefAccess<ArtifactDef[]>(typeof(ArtifactCatalog), "artifactDefs"))
                    {
                        if (art.cachedName == "ArtifactOfCorruption") continue;
                        var rule = RuleCatalog.FindRuleDef("Artifacts." + art.cachedName);
                        if (self.currentCategory.displayToken == "RULE_HEADER_ARTIFACTS" && !Reference.FirstRun.Value && Reference.ShowAllArtifacts.Value && !rulesToDisplay.Contains(rule)) rulesToDisplay.Add(rule);
                    }
                    AccessTools.FieldRefAccess<RuleCategoryController, List<RuleDef>>(self, "rulesToDisplay") = rulesToDisplay;
                    List<ArtifactDef> _lockedArts = new();
                    foreach (var art in AccessTools.StaticFieldRefAccess<ArtifactDef[]>(typeof(ArtifactCatalog), "artifactDefs"))
                    {
                        if (art.cachedName == "ArtifactOfCorruption") continue;
                        var rule = RuleCatalog.FindRuleDef("Artifacts." + art.cachedName);
                        if (art.unlockableDef != null && !NetworkUser.readOnlyLocalPlayersList[0].unlockables.Contains(art.unlockableDef)) _lockedArts.Add(art);
                    }
                    if (_lockedArts.Count > 0) lockedArts = _lockedArts;
                });
            };
            On.RoR2.Language.GetLocalizedStringByToken += (orig, self, token) => // this is the nth time i hook this method in this mod alone
            {
                foreach (var art in lockedArts) if (token == art.descriptionToken && NetworkUser.readOnlyLocalPlayersList?[0]?.master?.GetBody() == null)
                {
                    string txt = "";
                    switch (Reference.ArtifactUnlockHint.Value)
                    {
                        case 4:
                            return orig(self, "ARTIFACT_UNLOCKHINT_" + art.cachedName.ToUpper() + "_4");
                        case 3:
                            txt = " / " + orig(self, "ARTIFACT_UNLOCKHINT_" + art.cachedName.ToUpper() + "_3");
                            goto case 2; // fallthrough
                        case 2:
                            txt = " / " + orig(self, "ARTIFACT_UNLOCKHINT_" + art.cachedName.ToUpper() + "_2") + txt;
                            goto case 1; // fallthrough
                        case 1:
                            txt = orig(self, "ARTIFACT_UNLOCKHINT_" + art.cachedName.ToUpper() + "_1") + txt;
                            break;
                    }
                    return txt;
                }
                return orig(self, token);
            };
            On.RoR2.CharacterBody.OnSprintStart += (orig, self) =>
            {
                orig(self);
                if (NetworkUser.readOnlyLocalPlayersList[0].GetCurrentBody() == self) Reference.TutorialRun.Value = true;
            };
            On.RoR2.Stage.Start += (orig, self) =>
            {
                orig(self);
                if (Reference.FirstRun.Value) Reference.FirstRun.Value = false;
                TutorialHelper.Tutorial(Reference.TutorialTeleporter, "teleporter");
                TutorialHelper.Tutorial(Reference.TutorialPing, "ping");
                SceneDef defForCurrentScene = SceneCatalog.GetSceneDefForCurrentScene();
                if (self.name == "bazaar") TutorialHelper.Tutorial(Reference.TutorialBazaar, "bazaar");
                else if (Run.instance.loopClearCount >= 1 && defForCurrentScene != null && defForCurrentScene.sceneType == SceneType.Stage && !defForCurrentScene.isFinalStage) TutorialHelper.Tutorial(Reference.TutorialLevel6, "level6");
                else if (defForCurrentScene.stageOrder >= 4) TutorialHelper.Tutorial(Reference.TutorialLevel4, "level4");
                else if (defForCurrentScene.stageOrder >= 2)
                {
                    TutorialHelper.Tutorial(Reference.TutorialLevel2, "level2");
                    TutorialHelper.Tutorial(Reference.TutorialRun, "run");
                }
            };
            On.EntityStates.VoidCamp.Idle.OnEnter += (orig, self) =>
            {
                orig(self);
                TutorialHelper.Tutorial(Reference.TutorialVoidField, "voidfield");
            };
            CharacterMaster.onStartGlobal += characterMaster =>
            {
                if (
                (Reference.Mods("com.Moffein.EngiAutoFire") && characterMaster.masterIndex == MasterCatalog.FindMasterIndex(RoR2Content.Survivors.Engi.bodyPrefab)) 
                || (Reference.Mods("de.userstorm.captainshotgunmodes") && characterMaster.masterIndex == MasterCatalog.FindMasterIndex(RoR2Content.Survivors.Captain.bodyPrefab))
                || (Reference.Mods("de.userstorm.banditweaponmodes") && characterMaster.masterIndex == MasterCatalog.FindMasterIndex(RoR2Content.Survivors.Bandit2.bodyPrefab)))
                    TutorialHelper.Tutorial(Reference.TutorialWheel, "wheel");
            };
        }
    }
}

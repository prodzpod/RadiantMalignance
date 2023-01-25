using BepInEx.Configuration;
using HarmonyLib;
using R2API;
using RoR2;
using RoR2.Achievements;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RiskyMonkeyBase.Achievements
{
    public class ExtraSkinAchievement
    {
        public static Dictionary<string, UnlockableDef> unlockables;
        public static void Patch()
        {
            unlockables = new();
            if (Reference.Mods("com.Borbo.LazyBastardEngineer")) MakeUnlockable("Skins.Engineer.Extra1");
            if (Reference.Mods("com.dotflare.LTT1")) MakeUnlockable("Skins.Captain.Extra1");
            if (Reference.Mods("com.eyeknow.HighFashionLoader")) MakeUnlockable("Skins.Loader.Extra1");
            if (Reference.Mods("com.Takrak.RailgunnerAltTextures")) MakeUnlockable("Skins.Railgunner.Extra1");
            AchievementManager.onAchievementsRegistered += PostPatch;
        }
        public static void PostPatch()
        {
            if (Reference.Mods("com.Borbo.LazyBastardEngineer")) AddUnlockable("LazyBastardEngineer", "Skins.Engineer.Extra1");
            if (Reference.Mods("com.dotflare.LTT1")) AddUnlockable("PCap", "Skins.Captain.Extra1");
            if (Reference.Mods("com.eyeknow.HighFashionLoader")) AddUnlockable("SpaceCadet", "Skins.Loader.Extra1");
            if (Reference.Mods("com.Takrak.RailgunnerAltTextures")) AddUnlockable("RailgunnerSkin 2", "Skins.Railgunner.Extra1");
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Extra1_Loader", "Skins.Loader.Extra1", null, null, "com.eyeknow.HighFashionLoader")] 
        public class LoaderExtra1SkinAchievement : BaseAchievement 
        { 
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("LoaderBody");
            public override void OnBodyRequirementMet()
            {
                base.OnBodyRequirementMet();
                On.RoR2.PurchaseInteraction.OnInteractionBegin += OnInteractionBegin;
            }

            public override void OnBodyRequirementBroken()
            {
                On.RoR2.PurchaseInteraction.OnInteractionBegin -= OnInteractionBegin;
                base.OnBodyRequirementBroken();
            }

            private void OnInteractionBegin(On.RoR2.PurchaseInteraction.orig_OnInteractionBegin orig, PurchaseInteraction self, Interactor activator)
            {
                if (self.name.Contains("SlumberingPedestal")) Grant();
                orig(self, activator);
            }
        }
        [RegisterModdedAchievement("RiskyMonkey_Skin_Extra1_Railgunner", "Skins.Railgunner.Extra1", null, typeof(RailgunnerExtra1SkinServerAchievement), "com.Takrak.RailgunnerAltTextures")]
        public class RailgunnerExtra1SkinAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("RailgunnerBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); SetServerTracked(true); }
            public override void OnBodyRequirementBroken() { SetServerTracked(false); base.OnBodyRequirementBroken(); }

            private class RailgunnerExtra1SkinServerAchievement : BaseServerAchievement
            {
                private ToggleAction listenForDamage;
                private ToggleAction listenForGameOver;
                private bool failed;

                public override void OnInstall()
                {
                    base.OnInstall();
                    listenForDamage = new ToggleAction(() => RoR2Application.onFixedUpdate += new Action(OnFixedUpdate), () => RoR2Application.onFixedUpdate -= new Action(OnFixedUpdate));
                    listenForGameOver = new ToggleAction(() => Run.onServerGameOver += new Action<Run, GameEndingDef>(OnServerGameOver), () => Run.onServerGameOver -= new Action<Run, GameEndingDef>(OnServerGameOver));
                    Run.onRunStartGlobal += OnRunStart;
                    Run.onRunDestroyGlobal += OnRunDestroy;
                    if (Run.instance != null) OnRunDiscovered(Run.instance);
                }

                public override void OnUninstall()
                {
                    Run.onRunDestroyGlobal -= OnRunDestroy;
                    Run.onRunStartGlobal -= OnRunStart;
                    listenForGameOver.SetActive(false);
                    listenForDamage.SetActive(false);
                    base.OnUninstall();
                }

                private bool CharacterIsAtFullHealthOrNull()
                {
                    CharacterBody currentBody = GetCurrentBody();
                    return currentBody == null || currentBody.healthComponent.health >= currentBody.healthComponent.fullHealth;
                }

                private void OnFixedUpdate() { if (!CharacterIsAtFullHealthOrNull()) Fail(); }

                private void Fail()
                {
                    failed = true;
                    listenForDamage.SetActive(false);
                    listenForGameOver.SetActive(false);
                }

                private void OnServerGameOver(Run run, GameEndingDef gameEndingDef) { if (gameEndingDef.isWin && !failed) Grant(); }

                private void OnRunStart(Run run) => OnRunDiscovered(run);

                private void OnRunDiscovered(Run _)
                {
                    listenForGameOver.SetActive(true);
                    listenForDamage.SetActive(true);
                    failed = false;
                }

                private void OnRunDestroy(Run run) => OnRunLost(run);

                private void OnRunLost(Run _)
                {
                    listenForGameOver.SetActive(false);
                    listenForDamage.SetActive(false);
                }
            }
        }
        [RegisterModdedAchievement("RiskyMonkey_Skin_Extra1_Captain", "Skins.Captain.Extra1", null, null, "com.dotflare.LTT1")]
        public class CaptainExtra1SkinAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CaptainBody");
            public override void OnBodyRequirementMet()
            {
                base.OnBodyRequirementMet();
                On.RoR2.PurchaseInteraction.OnInteractionBegin += OnInteractionBegin;
            }

            public override void OnBodyRequirementBroken()
            {
                On.RoR2.PurchaseInteraction.OnInteractionBegin -= OnInteractionBegin;
                base.OnBodyRequirementBroken();
            }

            private void OnInteractionBegin(On.RoR2.PurchaseInteraction.orig_OnInteractionBegin orig, PurchaseInteraction self, Interactor activator)
            {
                if (self.name.Contains("EquipmentDrone") && activator.GetComponent<CharacterBody>().inventory.currentEquipmentIndex == EquipmentCatalog.FindEquipmentIndex("BossHunterConsumed")) Grant();
                orig(self, activator);
            }
        }
        [RegisterModdedAchievement("RiskyMonkey_Skin_Extra1_Engineer", "Skins.Engineer.Extra1", null, null, "com.Borbo.LazyBastardEngineer")]
        public class EngineerExtra1SkinAchievement : BaseAchievement
        {
            private int skillUseCount = 0;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("EngiBody");
            public override void OnBodyRequirementMet()
            {
                base.OnBodyRequirementMet();
                Run.onRunStartGlobal += ResetSkillUseCount;
                Run.onClientGameOverGlobal += ClearCheck;
                On.RoR2.CharacterBody.OnSkillActivated += SkillCheck;
            }

            public override void OnBodyRequirementBroken()
            {
                Run.onRunStartGlobal += ResetSkillUseCount;
                Run.onClientGameOverGlobal -= ClearCheck;
                On.RoR2.CharacterBody.OnSkillActivated -= SkillCheck;
                base.OnBodyRequirementBroken();
            }

            private void SkillCheck(On.RoR2.CharacterBody.orig_OnSkillActivated orig, CharacterBody self, GenericSkill skill)
            {
                if (self.bodyIndex == LookUpRequiredBodyIndex() && self.teamComponent.teamIndex == TeamIndex.Player && skill != self.skillLocator.special)
                {
                    if (skillUseCount == 0)
                    {
                        Debug.Log("DEBUG: Lazy Bastard challenge failed.");
                        if (AccessTools.StaticFieldRefAccess<ConfigEntry<bool>>(AccessTools.TypeByName("LasyBastardEngineer.Base"), "AnnounceWhenFail").Value)
                            Chat.AddMessage("Lazy Bastard challenge failed!");
                    }
                    ++skillUseCount;
                }
                orig(self, skill);
            }

            private void ResetSkillUseCount(Run obj) => skillUseCount = 0;

            public void ClearCheck(Run run, RunReport runReport)
            {
                bool flag = skillUseCount == 0 && meetsBodyRequirement;
                skillUseCount = 0;
                if (run == null || runReport == null || !(bool)runReport.gameEnding || !runReport.gameEnding.isWin || !flag) return;
                Grant();
            }
        }

        public static void MakeUnlockable(string name)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            unlockables.Add(name, unlockableDef);
            RiskyMonkeyBase.Log.LogDebug("Registered Unlockable " + name);
        }
        public static void AddUnlockable(string skinName, string name)
        {
            SkinDef def = null;
            foreach (var skin in SkinCatalog.allSkinDefs) if (skin.name == skinName) def = skin;
            UnlockableDef unlockableDef = unlockables[name];
            RiskyMonkeyBase.Log.LogDebug("Fetched Unlockable " + name);
            unlockableDef.nameToken = def.nameToken;
            unlockableDef.achievementIcon = def.icon;
            def.unlockableDef = unlockableDef;
            AccessTools.FieldRefAccess<Sprite>(typeof(AchievementDef), "achievedIcon")(AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName)) = def.icon;
        }
    }
}

﻿using BepInEx.Configuration;
using BubbetsItems.Items.BarrierItems;
using HarmonyLib;
using R2API;
using RiskyMonkeyBase.Tweaks;
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
            if (Reference.Mods("com.Mark.Joyride")) MakeUnlockable("Skins.Bandit.Extra1");
            if (Reference.Mods("com.dotflare.LTT1")) MakeUnlockable("Skins.Captain.Extra1");
            if (Reference.Mods("com.Mark.RoninMercSkin", "prodzpod.Downpour")) MakeUnlockable("Skins.Mercenary.Extra1");
            if (Reference.Mods("com.eyeknow.HighFashionLoader", "PlasmaCore.ForgottenRelics")) MakeForgottenRelics();
            if (Reference.Mods("prodzpod.TemplarSkins")) MakeUnlockable("Skins.Templar.Extra1");
            if (Reference.Mods("com.Takrak.RailgunnerAltTextures")) MakeUnlockable("Skins.Railgunner.Extra1");
            if (Reference.Mods("com.mark.InterloperViendSkin")) MakeUnlockable("Skins.VoidFiend.Extra1");
            AchievementManager.onAchievementsRegistered += PostPatch;
        }

        public static void MakeForgottenRelics()
        {
            if (!FRCSharp.VF2ConfigManager.disableSlumberingSatellite.Value) MakeUnlockable("Skins.Loader.Extra1");
        }
        public static void PostPatch()
        {
            if (Reference.Mods("com.Mark.Joyride")) AddUnlockable("Skin Defo", "Skins.Bandit.Extra1");
            if (Reference.Mods("com.dotflare.LTT1")) AddUnlockable("PCap", "Skins.Captain.Extra1");
            if (Reference.Mods("com.Mark.RoninMercSkin", "prodzpod.Downpour")) AddUnlockable("Merc Defo", "Skins.Mercenary.Extra1");
            if (Reference.Mods("com.eyeknow.HighFashionLoader", "PlasmaCore.ForgottenRelics")) AddForgottenRelics();
            if (Reference.Mods("prodzpod.TemplarSkins")) AddUnlockable("skinTemplarGreenAlt", "Skins.Templar.Extra1");
            if (Reference.Mods("com.Takrak.RailgunnerAltTextures")) AddUnlockable("RailgunnerSkin 2", "Skins.Railgunner.Extra1");
            if (Reference.Mods("com.mark.InterloperViendSkin")) AddUnlockable("Void Skin Defo", "Skins.VoidFiend.Extra1");
        }
        public static void AddForgottenRelics()
        {
            if (!FRCSharp.VF2ConfigManager.disableSlumberingSatellite.Value) AddUnlockable("SpaceCadet", "Skins.Loader.Extra1");
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Extra1_Bandit", "Skins.Bandit.Extra1", null, null, "com.Mark.Joyride")]
        public class BanditExtra1SkinAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Bandit2Body");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); GlobalEventManager.onClientDamageNotified += OnDamage; }
            public override void OnBodyRequirementBroken() { GlobalEventManager.onClientDamageNotified -= OnDamage; base.OnBodyRequirementBroken(); }
            public void OnDamage(DamageDealtMessage damageDealtMessage) { if (damageDealtMessage.attacker == null || damageDealtMessage.attacker != localUser.cachedBody) return; if (damageDealtMessage.damage >= localUser.cachedBody.baseDamage * 100) Grant(); }
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

        [RegisterModdedAchievement("RiskyMonkey_Skin_Extra1_Mercenary", "Skins.Mercenary.Extra1", null, null, "com.Mark.RoninMercSkin", "prodzpod.Downpour")]
        public class MercenaryExtra1SkinAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MercBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); Run.onClientGameOverGlobal += OnGameOver; }
            public override void OnBodyRequirementBroken() { Run.onClientGameOverGlobal += OnGameOver; base.OnBodyRequirementBroken(); }
            public void OnGameOver(Run self, RunReport report)
            {
                if (Downpour.DownpourPlugin.DownpourList.Contains(DifficultyCatalog.GetDifficultyDef(self.selectedDifficulty)) && report.gameEnding.isWin) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Extra1_Loader", "Skins.Loader.Extra1", null, null, "com.eyeknow.HighFashionLoader", "PlasmaCore.ForgottenRelics")] 
        public class LoaderExtra1SkinAchievement : BaseAchievement 
        { 
            public static bool OnlyRegisterIf() { return !FRCSharp.VF2ConfigManager.disableSlumberingSatellite.Value; }
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

        [RegisterModdedAchievement("RiskyMonkey_Skin_Extra1_Templar", "Skins.Templar.Extra1", null, null, "prodzpod.TemplarSkins")]
        public class TemplarExtra1SkinAchievement : BaseAchievement
        {
            private bool win;
            private bool found;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); GlobalEventManager.onCharacterDeathGlobal += OnKill; Run.onClientGameOverGlobal += OnGameOver; GlobalEventManager.onServerDamageDealt += OnDamage; Stage.onStageStartGlobal += OnStart; }
            public override void OnBodyRequirementBroken() { GlobalEventManager.onCharacterDeathGlobal -= OnKill; Run.onClientGameOverGlobal += OnGameOver; GlobalEventManager.onServerDamageDealt += OnDamage; Stage.onStageStartGlobal -= OnStart; base.OnBodyRequirementBroken(); }
            public void OnStart(Stage self) { win = true; found = false; }
            public void OnKill(DamageReport report)
            {
                if (report.victimBody.name == "ClayBruiserBody") found = true;
            }
            public void OnDamage(DamageReport report)
            {
                if (report.victimBody == localUser.cachedBody && report.attackerBody?.name == "ClayBruiserBody") win = false;
            }
            public void OnGameOver(Run self, RunReport report)
            {
                if (found && win && report.gameEnding.isWin) Grant();
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

        [RegisterModdedAchievement("RiskyMonkey_Skin_Extra1_VoidFiend", "Skins.VoidFiend.Extra1", null, null, "com.mark.InterloperViendSkin")]
        public class VoidFiendExtra1SkinAchievement : BaseAchievement
        {
            private bool win;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("VoidSurvivorBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); Run.onClientGameOverGlobal += OnGameOver; Inventory.onInventoryChangedGlobal += OnPickup; Stage.onStageStartGlobal += OnStart; }
            public override void OnBodyRequirementBroken() { Run.onClientGameOverGlobal += OnGameOver; Inventory.onInventoryChangedGlobal += OnPickup; Stage.onStageStartGlobal -= OnStart; base.OnBodyRequirementBroken(); }
            public void OnStart(Stage self) { win = true; }
            public void OnPickup(Inventory inv)
            {
                if (!win) return;
                if (inv == localUser.cachedBody?.inventory && inv.itemAcquisitionOrder.Exists(x =>
                {
                    ItemTier def = ItemCatalog.GetItemDef(x)?.tier ?? ItemTier.NoTier;
                    return def == ItemTier.VoidTier1 || def == ItemTier.VoidTier2 || def == ItemTier.VoidTier3 || (Reference.Mods("bubbet.bubbetsitems") && VoidLunarTweaks.isVoidLunar(def));
                })) win = false;
            }
            public void OnGameOver(Run self, RunReport report)
            {
                if (win && report.gameEnding.isWin) Grant();
            }
        }

        public static void MakeUnlockable(string name)
        {
            if (RiskyMonkeyAchievements.achievementBlacklist.Contains(name)) return;
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            unlockables.Add(name, unlockableDef);
            RiskyMonkeyAchievements.Log("Registered Unlockable " + name);
        }
        public static void AddUnlockable(string skinName, string name)
        {
            if (RiskyMonkeyAchievements.achievementBlacklist.Contains(name)) return;
            SkinDef def = null;
            foreach (var skin in SkinCatalog.allSkinDefs) if (skin.name == skinName) def = skin;
            UnlockableDef unlockableDef = unlockables[name];
            RiskyMonkeyAchievements.Log("Fetched Unlockable " + name);
            unlockableDef.nameToken = def.nameToken;
            unlockableDef.achievementIcon = def.icon;
            def.unlockableDef = unlockableDef;
            AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName).achievedIcon = def.icon;
        }
    }
}

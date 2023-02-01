using HarmonyLib;
using R2API;
using RoR2;
using RoR2.Achievements;
using System.Collections.Generic;
using UnityEngine;

namespace RiskyMonkeyBase.Achievements
{
    public class EnemyAchievement
    {
        public static Dictionary<string, UnlockableDef> unlockables;
        public static void Patch()
        {
            unlockables = new();
            if (Reference.Mods("com.RetroInspired.Gupmando")) MakeUnlockable("Commando");
            if (Reference.Mods("com.ArtyBoi.SkullDuggery")) MakeUnlockable("Bandit");
            if (Reference.Mods("com.TailLover.DinoMulT", "com.rob.Direseeker")) MakeUnlockable("MulT");
            if (Reference.Mods("com.marklow.HellfireCaptain")) MakeUnlockable("Captain");
            if (Reference.Mods("com.FrostRay.FrostRaySkinPack", "com.Anreol.ReleasedFromTheVoid")) MakeUnlockable("Mercenary");
            if (Reference.Mods("HIFU.Inferno", "PlasmaCore.ForgottenRelics")) MakeForgottenRelics();
            if (Reference.Mods("com.Rero.MasquePack")) MakeUnlockable("Acrid");
            if (Reference.Mods("com.KrononConspirator.ScavangerLoader")) MakeUnlockable("Loader");
            if (Reference.Mods("prodzpod.TemplarSkins")) MakeUnlockable("Templar");
            if (Reference.Mods("com.KrononConspirator.Solus_RailGunner")) MakeUnlockable("Railgunner");
            if (Reference.Mods("com.TailLover.VoidJailerFiend")) MakeUnlockable("VoidFiend");
            AchievementManager.onAchievementsRegistered += PostPatch;
        }

        public static void MakeForgottenRelics()
        {
            if (!FRCSharp.VF2ConfigManager.disableFrostWisp.Value) MakeUnlockable("Artificer");
        }

        public static void PostPatch()
        {
            if (Reference.Mods("com.RetroInspired.Gupmando")) AddUnlockable("GupmandoBig", "Commando");
            if (Reference.Mods("com.ArtyBoi.SkullDuggery")) AddUnlockable("Skullduggery", "Bandit");
            if (Reference.Mods("com.TailLover.DinoMulT", "com.rob.Direseeker")) AddUnlockable("DinoMul-TSkin", "MulT");
            if (Reference.Mods("com.marklow.HellfireCaptain")) AddUnlockable("Hellfire Captain", "Captain");
            if (Reference.Mods("com.FrostRay.FrostRaySkinPack", "com.Anreol.ReleasedFromTheVoid")) AddUnlockable("MercKitsune", "Mercenary");
            if (Reference.Mods("HIFU.Inferno", "PlasmaCore.ForgottenRelics")) AddForgottenRelics();
            if (Reference.Mods("com.Rero.MasquePack")) AddUnlockable("AcridBlind", "Acrid");
            if (Reference.Mods("com.KrononConspirator.ScavangerLoader")) AddUnlockable("ScavLoaderY", "Loader");
            if (Reference.Mods("prodzpod.TemplarSkins")) AddUnlockable("skinTemplarGoldAlt", "Templar");
            if (Reference.Mods("com.KrononConspirator.Solus_RailGunner")) AddUnlockable("SolusGunner", "Railgunner");
            if (Reference.Mods("com.TailLover.VoidJailerFiend")) AddUnlockable("VoidJailerFiendSkin", "VoidFiend");
        }

        public static void AddForgottenRelics()
        {
            if (!FRCSharp.VF2ConfigManager.disableFrostWisp.Value) AddUnlockable("CArti", "Artificer");
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Enemy_Commando", "Skins.Commando.Enemy", null, typeof(CommandoEnemySkinServerAchievement), "com.RetroInspired.Gupmando")]
        public class CommandoEnemySkinAchievement : BasePerSurvivorEnemySkinAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CommandoBody");
            public class CommandoEnemySkinServerAchievement : BasePerSurvivorEnemySkinServerAchievement
            { public override BodyIndex body => BodyCatalog.FindBodyIndex("GupBody"); public override int reqKills => 25; }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Enemy_Bandit", "Skins.Bandit.Enemy", null, typeof(BanditEnemySkinServerAchievement), "com.ArtyBoi.SkullDuggery")]
        public class BanditEnemySkinAchievement : BasePerSurvivorEnemySkinAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Bandit2Body");
            public class BanditEnemySkinServerAchievement : BasePerSurvivorEnemySkinServerAchievement
            { public override BodyIndex body => BodyCatalog.FindBodyIndex("BeetleBody"); public override int reqKills => 1000; }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Enemy_MulT", "Skins.MulT.Enemy", null, typeof(MulTEnemySkinServerAchievement), "com.TailLover.DinoMulT", "com.rob.Direseeker")]
        public class MulTEnemySkinAchievement : BasePerSurvivorEnemySkinAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("ToolbotBody");
            public class MulTEnemySkinServerAchievement : BasePerSurvivorEnemySkinServerAchievement
            { public override BodyIndex body => BodyCatalog.FindBodyIndex("DireseekerBossBody"); public override int reqKills => 2; }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Enemy_Captain", "Skins.Captain.Enemy", null, typeof(CaptainEnemySkinServerAchievement), "com.marklow.HellfireCaptain")]
        public class CaptainEnemySkinAchievement : BasePerSurvivorEnemySkinAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CaptainBody");
            public class CaptainEnemySkinServerAchievement : BasePerSurvivorEnemySkinServerAchievement
            { public override BodyIndex body => BodyCatalog.FindBodyIndex("MagmaWormBody"); public override int reqKills => 10; }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Enemy_Mercenary", "Skins.Mercenary.Enemy", null, typeof(MercenaryEnemySkinServerAchievement), "com.FrostRay.FrostRaySkinPack", "com.Anreol.ReleasedFromTheVoid")]
        public class MercenaryEnemySkinAchievement : BasePerSurvivorEnemySkinAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MercBody");
            public class MercenaryEnemySkinServerAchievement : BasePerSurvivorEnemySkinServerAchievement
            { public override BodyIndex body => BodyCatalog.FindBodyIndex("Assassin2Body"); public override int reqKills => 50; }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Enemy_Artificer", "Skins.Artificer.Enemy", null, typeof(ArtificerEnemySkinServerAchievement), "HIFU.Inferno", "PlasmaCore.ForgottenRelics")]
        public class ArtificerEnemySkinAchievement : BasePerSurvivorEnemySkinAchievement
        {
            public static bool OnlyRegisterIf() { return !FRCSharp.VF2ConfigManager.disableFrostWisp.Value; }
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MageBody");
            public class ArtificerEnemySkinServerAchievement : BasePerSurvivorEnemySkinServerAchievement
            { public override BodyIndex body => BodyCatalog.FindBodyIndex("FrostWispBody"); public override int reqKills => 100; }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Enemy_Acrid", "Skins.Acrid.Enemy", null, typeof(AcridEnemySkinServerAchievement), "com.Rero.MasquePack")]
        public class AcridEnemySkinAchievement : BasePerSurvivorEnemySkinAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CrocoBody");
            public class AcridEnemySkinServerAchievement : BasePerSurvivorEnemySkinServerAchievement
            { public override BodyIndex body => BodyCatalog.FindBodyIndex("FlyingVerminBody"); public override int reqKills => 250; }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Enemy_Loader", "Skins.Loader.Enemy", null, typeof(LoaderEnemySkinServerAchievement), "com.KrononConspirator.ScavangerLoader")]
        public class LoaderEnemySkinAchievement : BasePerSurvivorEnemySkinAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("LoaderBody");
            public class LoaderEnemySkinServerAchievement : BasePerSurvivorEnemySkinServerAchievement
            { public override BodyIndex body => BodyCatalog.FindBodyIndex("ScavBody"); public override int reqKills => 10; }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Enemy_Templar", "Skins.Templar.Enemy", null, typeof(TemplarEnemySkinServerAchievement), "prodzpod.TemplarSkins")]
        public class TemplarEnemySkinAchievement : BasePerSurvivorEnemySkinAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor");
            public class TemplarEnemySkinServerAchievement : BasePerSurvivorEnemySkinServerAchievement
            { public override BodyIndex body => BodyCatalog.FindBodyIndex("TitanGoldBody"); public override int reqKills => 2; }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Enemy_Railgunner", "Skins.Railgunner.Enemy", null, typeof(RailgunnerEnemySkinServerAchievement), "com.KrononConspirator.Solus_RailGunner")]
        public class RailgunnerEnemySkinAchievement : BasePerSurvivorEnemySkinAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("RailgunnerBody");
            public class RailgunnerEnemySkinServerAchievement : BasePerSurvivorEnemySkinServerAchievement
            { public override BodyIndex body => BodyCatalog.FindBodyIndex("SuperRoboBallBossBody"); public override int reqKills => 2; }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skin_Enemy_VoidFiend", "Skins.VoidFiend.Enemy", null, typeof(VoidFiendEnemySkinServerAchievement), "com.TailLover.VoidJailerFiend")]
        public class VoidFiendEnemySkinAchievement : BasePerSurvivorEnemySkinAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("VoidSurvivorBody");
            public class VoidFiendEnemySkinServerAchievement : BasePerSurvivorEnemySkinServerAchievement
            { public override BodyIndex body => BodyCatalog.FindBodyIndex("VoidMegaCrabBody"); public override int reqKills => 5; }
        }

        public class BasePerSurvivorEnemySkinAchievement : BaseAchievement
        {

            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); SetServerTracked(true); }
            public override void OnBodyRequirementBroken() { SetServerTracked(false); base.OnBodyRequirementBroken(); }

            public class BasePerSurvivorEnemySkinServerAchievement : BaseServerAchievement
            {
                private int kills;
                public virtual BodyIndex body => BodyIndex.None;
                public virtual int reqKills => 100;

                public override void OnInstall()
                {
                    base.OnInstall();
                    Run.onRunStartGlobal += OnRunStart;
                    GlobalEventManager.onCharacterDeathGlobal += onKill;
                    if (Run.instance != null) kills = 0;
                }

                public override void OnUninstall()
                {
                    Run.onRunStartGlobal -= OnRunStart;
                    GlobalEventManager.onCharacterDeathGlobal -= onKill;
                    base.OnUninstall();
                }
                private void OnRunStart(Run _) => kills = 0;

                private void onKill(DamageReport damageReport)
                {
                    if (damageReport.victimBodyIndex == body)
                    {
                        kills++;
                        if (kills >= reqKills)
                        {
                            Grant();
                            Run.onRunStartGlobal -= OnRunStart;
                            GlobalEventManager.onCharacterDeathGlobal -= onKill;
                        }
                    }
                }
            }
        }

        public static void MakeUnlockable(string name)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Skins." + name + ".Enemy";
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
            AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName).achievedIcon = def.icon;
        }
    }
}

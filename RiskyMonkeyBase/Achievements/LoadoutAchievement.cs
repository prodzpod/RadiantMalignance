using R2API;
using RoR2;
using RoR2.Achievements;
using RoR2.Orbs;
using RoR2.Projectile;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace RiskyMonkeyBase.Achievements
{
    public class LoadoutAchievement
    {
        public static Dictionary<string, UnlockableDef> unlockables;
        public static void Patch()
        {
            unlockables = new();
            if (Reference.Mods("com.lodington.AutoShot")) MakeUnlockable("AutoShot");
            if (Reference.Mods("com.macawesone.EngiShotgun"))
            {
                MakeUnlockable("ENGIPLUS_PLASMAGRENADES_NAMETOKEN");
                MakeUnlockable("ENGIPLUS_JETPACK_NAMETOKEN");
            }
            if (Reference.Mods("xyz.yekoc.PassiveAgression"))
            {
                MakeUnlockable("PASSIVEAGRESSION_COMMANDOSTIM");
                MakeUnlockable("PASSIVEAGRESSION_BANDITSTARCH");
                MakeUnlockable("PASSIVEAGRESSION_PALADINRESOLVE");
                MakeUnlockable("PASSIVEAGRESSION_MAGEBLOODRITE");
                MakeUnlockable("PASSIVEAGRESSION_MAGEBLOODBOLT");
                MakeUnlockable("PASSIVEAGRESSION_MAGEICEARMOR");
                MakeUnlockable("PASSIVEAGRESSION_LOADERELEC");
                MakeUnlockable("PASSIVEAGRESSION_CROCSPREAD");
                MakeUnlockable("PASSIVEAGRESSION_RAILMISSILE");
                MakeUnlockable("PASSIVEAGRESSION_VIENDBUG");
                MakeUnlockable("PASSIVEAGRESSION_VIENDTEAR");
            }
            if (Reference.Mods("pseudopulse.ChaoticSkills"))
            {
                MakeUnlockable("SKILL_SFG_NAME");
                MakeUnlockable("SKILL_LockOn_NAME");
                MakeUnlockable("SKILL_IceGrenade_NAME");
                MakeUnlockable("SKILL_Destroyer_NAME");
                MakeUnlockable("SKILL_Sadism_NAME");
                MakeUnlockable("SKILL_AutoNailblast_NAME");
                MakeUnlockable("SKILL_Flamethrower_NAME");
                MakeUnlockable("SKILL_Thruster_NAME");
                MakeUnlockable("SKILL_Constructor_NAME");
                MakeUnlockable("SKILL_MedicTurret_NAME");
                MakeUnlockable("SKILL_Sniper_NAME");
                MakeUnlockable("SKILL_OffensiveMicrobots_NAME");
                if (Reference.Mods("xyz.yekoc.FetchAFriend")) MakeUnlockable("SKILL_Backup_NAME");
                MakeUnlockable("SKILL_DesignBeacon_NAME");
                MakeUnlockable("SKILL_VoidBeacon_NAME");
                MakeUnlockable("SKILL_TETHER_NAME");
                MakeUnlockable("SKILL_Shards_NAME");
                MakeUnlockable("SKILL_Pathosis_NAME");
            }
            if (Reference.Mods("com.groovesalad.GrooveSaladSpikestripContent")) MakeUnlockable("SPIKESTRIPSKILL_EMPCHARGE_NAME");
            AchievementManager.onAchievementsRegistered += PostPatch;
            if (Reference.Mods("com.groovesalad.GrooveSaladSpikestripContent", "com.plasmacore.PlasmaCoreSpikestripContent")) PatchSpikestrip();
        }

        public static void PostPatch()
        {
            if (Reference.Mods("com.lodington.AutoShot")) AddUnlockable("Auto Shot", "AutoShot");
            if (Reference.Mods("com.macawesone.EngiShotgun"))
            {
                AddUnlockable("ENGIPLUS_PLASMAGRENADES_NAMETOKEN");
                AddUnlockable("ENGIPLUS_JETPACK_NAMETOKEN");
            }
            if (Reference.Mods("xyz.yekoc.PassiveAgression"))
            {
                AddUnlockable("PASSIVEAGRESSION_COMMANDOSTIM");
                AddUnlockable("PASSIVEAGRESSION_BANDITSTARCH");
                AddUnlockable("PASSIVEAGRESSION_PALADINRESOLVE");
                AddUnlockable("PASSIVEAGRESSION_MAGEBLOODRITE");
                AddUnlockable("PASSIVEAGRESSION_MAGEBLOODBOLT");
                AddUnlockable("PASSIVEAGRESSION_MAGEICEARMOR");
                AddUnlockable("PASSIVEAGRESSION_LOADERELEC");
                AddUnlockable("PASSIVEAGRESSION_CROCSPREAD");
                AddUnlockable("PASSIVEAGRESSION_RAILMISSILE");
                AddUnlockable("PASSIVEAGRESSION_VIENDBUG");
                AddUnlockable("PASSIVEAGRESSION_VIENDTEAR");
            }
            if (Reference.Mods("pseudopulse.ChaoticSkills"))
            {
                AddUnlockable("SKILL_SFG_NAME");
                AddUnlockable("SKILL_LockOn_NAME");
                AddUnlockable("SKILL_IceGrenade_NAME");
                AddUnlockable("SKILL_Destroyer_NAME");
                AddUnlockable("SKILL_Sadism_NAME");
                AddUnlockable("SKILL_AutoNailblast_NAME");
                AddUnlockable("SKILL_Flamethrower_NAME");
                AddUnlockable("SKILL_Thruster_NAME");
                AddUnlockable("SKILL_Constructor_NAME");
                AddUnlockable("SKILL_MedicTurret_NAME");
                AddUnlockable("SKILL_Sniper_NAME");
                AddUnlockable("SKILL_OffensiveMicrobots_NAME");
                if (Reference.Mods("xyz.yekoc.FetchAFriend")) AddUnlockable("SKILL_Backup_NAME");
                AddUnlockable("SKILL_DesignBeacon_NAME");
                AddUnlockable("SKILL_VoidBeacon_NAME");
                AddUnlockable("SKILL_TETHER_NAME");
                AddUnlockable("SKILL_Shards_NAME");
                AddUnlockable("SKILL_Pathosis_NAME");
            }
            if (Reference.Mods("com.groovesalad.GrooveSaladSpikestripContent")) AddUnlockable("SPIKESTRIPSKILL_EMPCHARGE_NAME");
        }

        public static List<string> spikestripKeys;
        public static void PatchSpikestrip()
        {
            spikestripKeys = new() { "ACHIEVEMENT_SPIKESTRIPACRIDDEFEATVOID_DESCRIPTION", "ACHIEVEMENT_SPIKESTRIPARTIFICERDEFEATVOID_DESCRIPTION", "ACHIEVEMENT_SPIKESTRIPHUNTRESSDEFEATVOID_DESCRIPTION", "ACHIEVEMENT_SPIKESTRIPLOADERDEFEATVOID_DESCRIPTION", "ACHIEVEMENT_SPIKESTRIPRAILGUNNERDEFEATVOID_DESCRIPTION" };
            Dictionary<string, Type> list = new()
            {
                { "SpikestripSkills.Acrid.「Deeprot」", typeof(AcridSpeedrunPlanetariumAchievement) },
                { "SpikestripSkills.Artificer.「Choke」", typeof(ArtificerSpeedrunPlanetariumAchievement) },
                { "SpikestripSkills.Huntress.「Network」", typeof(HuntressSpeedrunPlanetariumAchievement) },
                { "SpikestripSkills.Loader.「Conduit」", typeof(LoaderSpeedrunPlanetariumAchievement) },
                { "SpikestripSkills.Railgunner.「Torpor」", typeof(RailgunnerSpeedrunPlanetariumAchievement) }
            };
            foreach (var achievement in UnlockableAPI.Achievements)
            {
                if (list.ContainsKey(achievement.unlockableRewardIdentifier)) 
                {
                    achievement.type = list[achievement.unlockableRewardIdentifier];
                    achievement.serverTrackerType = null;
                }
            }
            On.RoR2.Language.GetLocalizedStringByToken += (orig, self, token) =>
            {
                if (spikestripKeys.Contains(token)) return orig(self, token).Replace("escape the Planetarium or complete wave 50 in Simulacrum", "escape the Planetarium within 5 stages or less");
                return orig(self, token);
            };
        }

        public class AcridSpeedrunPlanetariumAchievement : BaseSpeedrunPlanetariumAchievement { public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CrocoBody"); }
        public class ArtificerSpeedrunPlanetariumAchievement : BaseSpeedrunPlanetariumAchievement { public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MageBody"); }
        public class HuntressSpeedrunPlanetariumAchievement : BaseSpeedrunPlanetariumAchievement { public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("HuntressBody"); }
        public class LoaderSpeedrunPlanetariumAchievement : BaseSpeedrunPlanetariumAchievement { public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("LoaderBody"); }
        public class RailgunnerSpeedrunPlanetariumAchievement : BaseSpeedrunPlanetariumAchievement { public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("RailgunnerBody"); }

        public abstract class BaseSpeedrunPlanetariumAchievement : BaseAchievement
        {
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); Run.onClientGameOverGlobal += OnClientGameOverGlobal; }
            public override void OnBodyRequirementBroken() { Run.onClientGameOverGlobal -= OnClientGameOverGlobal; base.OnBodyRequirementBroken(); }
            public void OnClientGameOverGlobal(Run run, RunReport runReport) { if (runReport.gameEnding && runReport.gameEnding == DLC1Content.GameEndings.VoidEnding && run.stageClearCount < 5) Grant(); }
        }

        public static void MakeUnlockable(string name)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Skills." + name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            unlockables.Add(name, unlockableDef);
            RiskyMonkeyAchievements.Log("Registered Unlockable " + name);
        }
        public static void AddUnlockable(string skillName, string name = null)
        {
            if (name == null) name = skillName;
            if (RiskyMonkeyAchievements.achievementBlacklist.Contains("Skills." + name)) return;
            SkillDef def = null;
            UnlockableDef unlockableDef = unlockables[name];
            RiskyMonkeyAchievements.Log("Fetched Unlockable " + name);
            foreach (var skill in SkillCatalog.allSkillDefs) if (skill.skillNameToken == skillName) { def = skill; break; }
            if (def == null) return;
            unlockableDef.nameToken = def.skillNameToken;
            unlockableDef.achievementIcon = def.icon;
            AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName).achievedIcon = def.icon;
            IEnumerator<SkillFamily> families = SkillCatalog.allSkillFamilies.GetEnumerator();
            while (families.MoveNext()) for (var i = 0; i < families.Current.variants.Length; i++) if (families.Current.variants[i].skillDef == def) families.Current.variants[i].unlockableDef = unlockableDef;
        }

        public static bool hasDebuffs(BuffIndex[] buffs, int count)
        {
            int _count = 0;
            for (var i = 0; i < buffs.Length; i++)
            {
                BuffDef buff = BuffCatalog.GetBuffDef(buffs[i]);
                if (buff != null && buff.isDebuff) _count++;
            }
            return _count >= count;
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_AutoShot", "Skills.AutoShot", null, null, "com.lodington.AutoShot")]
        public class AutoShotAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CommandoBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); On.RoR2.Orbs.LightningOrb.OnArrival += OnArrival; }
            public override void OnBodyRequirementBroken() { On.RoR2.Orbs.LightningOrb.OnArrival -= OnArrival; base.OnBodyRequirementBroken(); }
            public void OnArrival(On.RoR2.Orbs.LightningOrb.orig_OnArrival orig, LightningOrb self)
            {
                orig(self);
                if (localUser == null || localUser.cachedBody == null) return;
                if (self.lightningType == LightningOrb.LightningType.Ukulele && ((2 * localUser.cachedBody.inventory.GetItemCount(RoR2Content.Items.ChainLightning)) - self.bouncesRemaining >= 10)) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_ENGIPLUS_PLASMAGRENADES_NAMETOKEN", "Skills.ENGIPLUS_PLASMAGRENADES_NAMETOKEN", null, null, "com.macawesone.EngiShotgun")]
        public class ENGIPLUS_PLASMAGRENADES_NAMETOKENAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("EngiBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); GlobalEventManager.onCharacterDeathGlobal += OnCharacterDeath; }
            public override void OnBodyRequirementBroken() { GlobalEventManager.onCharacterDeathGlobal -= OnCharacterDeath; base.OnBodyRequirementBroken(); }
            public void OnCharacterDeath(DamageReport damageReport)
            {
                if (damageReport.damageInfo.attacker == null || damageReport.damageInfo.attacker != localUser.cachedBodyObject || damageReport.victimBody.name != "VagrantBody") return;
                if (damageReport.damageInfo.inflictor != null && damageReport.damageInfo.inflictor.name.Contains("VagrantNovaItem")) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_ENGIPLUS_JETPACK_NAMETOKEN", "Skills.ENGIPLUS_JETPACK_NAMETOKEN", null, null, "com.macawesone.EngiShotgun")]
        public class ENGIPLUS_JETPACK_NAMETOKENAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("EngiBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; base.OnBodyRequirementBroken(); }
            public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.moveSpeed / localUser.cachedBody.baseMoveSpeed >= 6f) Grant(); }
        }

        

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_COMMANDOSTIM", "Skills.PASSIVEAGRESSION_COMMANDOSTIM", null, null, "xyz.yekoc.PassiveAgression")]
        public class PASSIVEAGRESSION_COMMANDOSTIMAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CommandoBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; base.OnBodyRequirementBroken(); }
            public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.attackSpeed >= 6f) Grant(); }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_BANDITSTARCH", "Skills.PASSIVEAGRESSION_BANDITSTARCH", null, null, "xyz.yekoc.PassiveAgression")]
        public class PASSIVEAGRESSION_BANDITSTARCHAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Bandit2Body");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); On.RoR2.CharacterBody.AddBuff_BuffDef += AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex += AddBuff; }
            public override void OnBodyRequirementBroken() { On.RoR2.CharacterBody.AddBuff_BuffDef -= AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex -= AddBuff; base.OnBodyRequirementBroken(); }
            public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffDef orig, CharacterBody self, BuffDef buff)
            {
                if ((bool)self && self == localUser.cachedBody && self.activeBuffsList != null && hasDebuffs(self.activeBuffsList, 4)) Grant(); orig(self, buff);
            }
            public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffIndex orig, CharacterBody self, BuffIndex buff)
            {
                if ((bool)self && self == localUser.cachedBody && self.activeBuffsList != null && hasDebuffs(self.activeBuffsList, 4)) Grant(); orig(self, buff);
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_PALADINRESOLVE", "Skills.PASSIVEAGRESSION_PALADINRESOLVE", null, null, "xyz.yekoc.PassiveAgression", "com.rob.Paladin")]
        public class PASSIVEAGRESSION_PALADINRESOLVEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("RobPaladinBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; base.OnBodyRequirementBroken(); }
            public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.armor >= 200f) Grant(); }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_MAGEBLOODRITE", "Skills.PASSIVEAGRESSION_MAGEBLOODRITE", null, null, "xyz.yekoc.PassiveAgression")]
        public class PASSIVEAGRESSION_MAGEBLOODRITEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MageBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); On.RoR2.CharacterBody.AddBuff_BuffDef += AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex += AddBuff; }
            public override void OnBodyRequirementBroken() { On.RoR2.CharacterBody.AddBuff_BuffDef -= AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex -= AddBuff; base.OnBodyRequirementBroken(); }
            public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffDef orig, CharacterBody self, BuffDef buff)
            {
                if ((bool)self && self.teamComponent.teamIndex != TeamIndex.Player && self.activeBuffsList != null && self.GetBuffCount(RoR2Content.Buffs.OnFire) >= 25) Grant(); orig(self, buff);
            }
            public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffIndex orig, CharacterBody self, BuffIndex buff)
            {
                if ((bool)self && self.teamComponent.teamIndex != TeamIndex.Player && self.activeBuffsList != null && self.GetBuffCount(RoR2Content.Buffs.OnFire) >= 25) Grant(); orig(self, buff);
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_MAGEBLOODBOLT", "Skills.PASSIVEAGRESSION_MAGEBLOODBOLT", null, null, "xyz.yekoc.PassiveAgression")]
        public class PASSIVEAGRESSION_MAGEBLOODBOLTAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MageBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; base.OnBodyRequirementBroken(); }
            public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.bleedChance >= 0.99f) Grant(); }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_MAGEICEARMOR", "Skills.PASSIVEAGRESSION_MAGEICEARMOR", null, null, "xyz.yekoc.PassiveAgression")]
        public class PASSIVEAGRESSION_MAGEICEARMORAchievement : BaseAchievement
        {
            public static float stopwatch = 0;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MageBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onFixedUpdate += OnUpdate; Stage.onStageStartGlobal += OnStart; Run.onRunDestroyGlobal += OnGameOver; }
            public override void OnBodyRequirementBroken() { RoR2Application.onFixedUpdate -= OnUpdate; Stage.onStageStartGlobal -= OnStart; Run.onRunDestroyGlobal -= OnGameOver; base.OnBodyRequirementBroken(); }
            public void OnStart(Stage self) { stopwatch = 0; }
            public void OnGameOver(Run run) { stopwatch = -1; }
            public void OnUpdate()
            {
                if (stopwatch >= 0) stopwatch += Time.fixedDeltaTime;
                if (stopwatch >= 600f) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_LOADERELEC", "Skills.PASSIVEAGRESSION_LOADERELEC", null, null, "xyz.yekoc.PassiveAgression")]
        public class PASSIVEAGRESSION_LOADERELECAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("LoaderBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); Run.onClientGameOverGlobal += OnGameOver; }
            public override void OnBodyRequirementBroken() { Run.onClientGameOverGlobal -= OnGameOver; base.OnBodyRequirementBroken(); }
            public void OnGameOver(Run self, RunReport report)
            {
                if (report != null && !report.gameEnding.isWin) return;
                int count = 0;
                foreach (var art in ArtifactCatalog.artifactDefs)
                {
                    var rule = RuleCatalog.FindRuleDef("Artifacts." + art.cachedName);
                    if (report.ruleBook.IsChoiceActive(rule.FindChoice("On"))) count++;
                }
                if (count >= 5) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_CROCSPREAD", "Skills.PASSIVEAGRESSION_CROCSPREAD", null, null, "xyz.yekoc.PassiveAgression")]
        public class PASSIVEAGRESSION_CROCSPREADAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CrocoBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); TeleporterInteraction.onTeleporterChargedGlobal += OnTeleporter; }
            public override void OnBodyRequirementBroken() { TeleporterInteraction.onTeleporterChargedGlobal -= OnTeleporter; base.OnBodyRequirementBroken(); }
            public void OnTeleporter(TeleporterInteraction tp)
            {
                if (localUser == null || localUser.cachedBody == null) return;
                foreach (HurtBox hurtBox in new SphereSearch() { origin = localUser.cachedBody.footPosition, radius = 50000f, mask = LayerIndex.entityPrecise.mask }.RefreshCandidates().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes())
                {
                    CharacterBody body = hurtBox.healthComponent.body;
                    if (body.GetBuffCount(RoR2Content.Buffs.Poisoned) <= 0 && body.GetBuffCount(RoR2Content.Buffs.Blight) <= 0) return;
                }
                Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_RAILMISSILE", "Skills.PASSIVEAGRESSION_RAILMISSILE", null, null, "xyz.yekoc.PassiveAgression")]
        public class PASSIVEAGRESSION_RAILMISSILEAchievement : BaseAchievement
        {
            public static List<ProjectileController> list = new();
            public static List<GenericDamageOrb> list2 = new();
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("RailgunnerBody");
            public override void OnBodyRequirementMet() 
            { 
                base.OnBodyRequirementMet();
                On.RoR2.Stage.Start += StageStart;
                On.RoR2.Projectile.ProjectileController.OnEnable += ProjectileBegin;
                On.RoR2.Projectile.ProjectileController.OnDisable += ProjectileEnd;
                On.RoR2.Orbs.GenericDamageOrb.Begin += OrbBegin;
                On.RoR2.Orbs.GenericDamageOrb.OnArrival += OrbEnd;
            }
            public override void OnBodyRequirementBroken()
            {
                On.RoR2.Stage.Start -= StageStart;
                On.RoR2.Projectile.ProjectileController.OnEnable -= ProjectileBegin;
                On.RoR2.Projectile.ProjectileController.OnDisable -= ProjectileEnd;
                On.RoR2.Orbs.GenericDamageOrb.Begin -= OrbBegin;
                On.RoR2.Orbs.GenericDamageOrb.OnArrival -= OrbEnd;
                base.OnBodyRequirementBroken(); 
            }

            public void StageStart(On.RoR2.Stage.orig_Start orig, Stage self)
            {
                list.Clear();
                list2.Clear();
                orig(self);
            }

            public void ProjectileBegin(On.RoR2.Projectile.ProjectileController.orig_OnEnable orig, ProjectileController self)
            {
                if (localUser == null || localUser.cachedBody == null || !self.procChainMask.HasProc(ProcType.Missile) || localUser.cachedBody != self.Networkowner) return;
                list.Add(self);
                if ((list.Count + list2.Count) >= 100) Grant();
                orig(self);
            }

            public void ProjectileEnd(On.RoR2.Projectile.ProjectileController.orig_OnDisable orig, ProjectileController self)
            {
                if (list.Contains(self)) list.Remove(self);
                orig(self);
            }

            public void OrbBegin(On.RoR2.Orbs.GenericDamageOrb.orig_Begin orig, GenericDamageOrb self)
            {
                if (localUser == null || localUser.cachedBody == null || !self.procChainMask.HasProc(ProcType.Missile) || localUser.cachedBody != self.attacker) return;
                list2.Add(self);
                if ((list.Count + list2.Count) >= 100) Grant();
                orig(self);
            }

            public void OrbEnd(On.RoR2.Orbs.GenericDamageOrb.orig_OnArrival orig, GenericDamageOrb self)
            {
                if (list2.Contains(self)) list2.Remove(self);
                orig(self);
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_VIENDBUG", "Skills.PASSIVEAGRESSION_VIENDBUG", null, null, "xyz.yekoc.PassiveAgression")]
        public class PASSIVEAGRESSION_VIENDBUGAchievement : BaseAchievement
        {
            public static bool win = false;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("VoidSurvivorBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onFixedUpdate += OnUpdate; TeleporterInteraction.onTeleporterChargedGlobal += OnTeleporter; Stage.onStageStartGlobal += OnStart; Run.onRunDestroyGlobal += OnGameOver; }
            public override void OnBodyRequirementBroken() { RoR2Application.onFixedUpdate -= OnUpdate; TeleporterInteraction.onTeleporterChargedGlobal -= OnTeleporter; Stage.onStageStartGlobal -= OnStart; Run.onRunDestroyGlobal -= OnGameOver; base.OnBodyRequirementBroken(); }
            public void OnStart(Stage self) { win = true; }
            public void OnGameOver(Run self) { win = false; }
            public void OnUpdate()
            {
                if (win && localUser != null && localUser.cachedBody != null && localUser.cachedBody.GetBuffCount(DLC1Content.Buffs.VoidSurvivorCorruptMode) > 0) win = false;
            }
            public void OnTeleporter(TeleporterInteraction tp)
            {
                if (win) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_VIENDTEAR", "Skills.PASSIVEAGRESSION_VIENDTEAR", null, null, "xyz.yekoc.PassiveAgression")]
        public class PASSIVEAGRESSION_VIENDTEARAchievement : BaseAchievement
        {
            public static bool win = false;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("VoidSurvivorBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onFixedUpdate += OnUpdate; Run.onClientGameOverGlobal += OnGameOver; Run.onRunStartGlobal += OnStart; Run.onRunDestroyGlobal += OnEnd; }
            public override void OnBodyRequirementBroken() { RoR2Application.onFixedUpdate -= OnUpdate; Run.onClientGameOverGlobal -= OnGameOver; Run.onRunStartGlobal -= OnStart; Run.onRunDestroyGlobal -= OnEnd; base.OnBodyRequirementBroken(); }
            public void OnStart(Run self) { win = true; }
            public void OnEnd(Run self) { win = false; }
            public void OnUpdate()
            {
                if (!win || localUser == null || localUser.cachedBody == null) return;
                if (hasDebuffs(localUser.cachedBody.activeBuffsList, 1)) win = false;
            }
            public void OnGameOver(Run self, RunReport report)
            {
                if (win && report.gameEnding.isWin) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_SFG_NAME", "Skills.SKILL_SFG_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_SFG_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CommandoBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; base.OnBodyRequirementBroken(); }
            public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.baseDamage >= 500f) Grant(); }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_LockOn_NAME", "Skills.SKILL_LockOn_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_LockOn_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CommandoBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; base.OnBodyRequirementBroken(); }
            public void OnUpdate()
            {
                if (localUser == null || localUser.cachedBody == null || localUser.cachedBody.skillLocator.secondary.skillDef == null) return;
                GenericSkill secondary = localUser.cachedBody.skillLocator.secondary;
                if ((secondary.skillDef.baseRechargeInterval * secondary.cooldownScale - secondary.flatCooldownReduction) <= 0.5f) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_IceGrenade_NAME", "Skills.SKILL_IceGrenade_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_IceGrenade_NAMEAchievement : BaseAchievement
        {
            public static bool win = false;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CommandoBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); GlobalEventManager.onServerDamageDealt += OnServerDamageDealt; Run.onClientGameOverGlobal += OnGameOver; Run.onRunStartGlobal += OnStart; Run.onRunDestroyGlobal += OnEnd; }
            public override void OnBodyRequirementBroken() { GlobalEventManager.onServerDamageDealt -= OnServerDamageDealt; Run.onClientGameOverGlobal -= OnGameOver; Run.onRunStartGlobal -= OnStart; Run.onRunDestroyGlobal -= OnEnd; base.OnBodyRequirementBroken(); }
            public void OnStart(Run self) { win = true; }
            public void OnEnd(Run self) { win = false; }
            public void OnServerDamageDealt(DamageReport report)
            {
                if (!win || localUser == null || localUser.cachedBody == null) return;
                if (report.attacker == localUser.cachedBody && (report.dotType == DotController.DotIndex.Burn || report.dotType == DotController.DotIndex.Helfire)) win = false;
            }
            public void OnGameOver(Run self, RunReport report)
            {
                if (win && report.gameEnding.isWin) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_Destroyer_NAME", "Skills.SKILL_Destroyer_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_Destroyer_NAMEAchievement : BaseAchievement
        {
            public static bool win = false;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("HuntressBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; TeleporterInteraction.onTeleporterChargedGlobal += OnGameOver; Run.onRunStartGlobal += OnStart; Run.onRunDestroyGlobal += OnEnd; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; TeleporterInteraction.onTeleporterChargedGlobal -= OnGameOver; Run.onRunStartGlobal -= OnStart; Run.onRunDestroyGlobal -= OnEnd; base.OnBodyRequirementBroken(); }
            public void OnStart(Run self) { win = true; }
            public void OnEnd(Run self) { win = false; }
            public void OnUpdate()
            {
                if (!win || localUser == null || localUser.cachedBody == null) return;
                if (localUser.cachedBody.isSprinting) win = false;
            }
            public void OnGameOver(TeleporterInteraction teleporterInteraction)
            {
                if (win && Run.instance.stageClearCount == 0 && !isUserAlive) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_Sadism_NAME", "Skills.SKILL_Sadism_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_Sadism_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Bandit2Body");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); On.RoR2.CharacterBody.AddBuff_BuffDef += AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex += AddBuff; }
            public override void OnBodyRequirementBroken() { On.RoR2.CharacterBody.AddBuff_BuffDef -= AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex -= AddBuff; base.OnBodyRequirementBroken(); }
            public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffDef orig, CharacterBody self, BuffDef buff)
            {
                if ((bool)self && self.teamComponent.teamIndex != TeamIndex.Player && self.activeBuffsList != null && hasDebuffs(self.activeBuffsList, 6)) Grant(); orig(self, buff);
            }
            public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffIndex orig, CharacterBody self, BuffIndex buff)
            {
                if ((bool)self && self.teamComponent.teamIndex != TeamIndex.Player && self.activeBuffsList != null && hasDebuffs(self.activeBuffsList, 6)) Grant(); orig(self, buff);
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_AutoNailblast_NAME", "Skills.SKILL_AutoNailblast_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_AutoNailblast_NAMEAchievement : BaseAchievement
        {
            public static bool win = false;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("ToolbotBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; TeleporterInteraction.onTeleporterChargedGlobal += OnGameOver; Run.onRunStartGlobal += OnStart; Run.onRunDestroyGlobal += OnEnd; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; TeleporterInteraction.onTeleporterChargedGlobal -= OnGameOver; Run.onRunStartGlobal -= OnStart; Run.onRunDestroyGlobal -= OnEnd; base.OnBodyRequirementBroken(); }
            public void OnStart(Run self) { win = true; }
            public void OnEnd(Run self) { win = false; }
            public void OnUpdate()
            {
                if (!win || localUser == null || localUser.cachedBody == null) return;
                if (localUser.cachedBody.moveSpeed > 10) win = false;
            }
            public void OnGameOver(TeleporterInteraction teleporterInteraction)
            {
                if (win && Run.instance.stageClearCount == 0 && !isUserAlive) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_Flamethrower_NAME", "Skills.SKILL_Flamethrower_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_Flamethrower_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("EngiBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); GlobalEventManager.onCharacterDeathGlobal += OnCharacterDeathGlobal; }
            public override void OnBodyRequirementBroken() { GlobalEventManager.onCharacterDeathGlobal -= OnCharacterDeathGlobal; base.OnBodyRequirementBroken(); }
            public void OnCharacterDeathGlobal(DamageReport report)
            {
                if (localUser == null || localUser.cachedBody == null) return;
                if (report.victimBodyIndex == BodyCatalog.FindBodyIndex("BrotherHurtBody") && report.attacker == localUser.cachedBody && (report.dotType == DotController.DotIndex.Burn || report.dotType == DotController.DotIndex.Helfire)) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_Thruster_NAME", "Skills.SKILL_Thruster_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_Thruster_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("EngiBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); On.EntityStates.Headstompers.HeadstompersFall.DoStompExplosionAuthority += OnStompExplosionAuthority; }
            public override void OnBodyRequirementBroken() { On.EntityStates.Headstompers.HeadstompersFall.DoStompExplosionAuthority -= OnStompExplosionAuthority; base.OnBodyRequirementBroken(); }
            public void OnStompExplosionAuthority(On.EntityStates.Headstompers.HeadstompersFall.orig_DoStompExplosionAuthority orig, EntityStates.Headstompers.HeadstompersFall self)
            {
                if (localUser == null || localUser.cachedBody == null || self.body != localUser.cachedBody) return;
                if (((bool)self.body.inventory ? self.body.inventory.GetItemCount(RoR2Content.Items.FallBoots) : 1) > 0 && self.initialY - self.body.footPosition.y > EntityStates.Headstompers.HeadstompersFall.maxDistance) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_Constructor_NAME", "Skills.SKILL_Constructor_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_Constructor_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("EngiBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); MinionOwnership.onMinionGroupChangedGlobal += OnMinionGroupChangedGlobal; }
            public override void OnBodyRequirementBroken() { MinionOwnership.onMinionGroupChangedGlobal -= OnMinionGroupChangedGlobal; base.OnBodyRequirementBroken(); }
            public void OnMinionGroupChangedGlobal(MinionOwnership minion)
            {
                if (localUser?.cachedBody == null || minion.ownerMaster != localUser.cachedMaster) return;
                foreach (var group in minion.group.members)
                {
                    if (group.ownerMaster == null) continue;
                    var body = group.ownerMaster.GetBody();
                    if (body == null || body.inventory == null) continue;
                    if (body.inventory.currentEquipmentIndex == RoR2Content.Equipment.QuestVolatileBattery.equipmentIndex)
                    {
                        Grant();
                        return;
                    }
                }
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_MedicTurret_NAME", "Skills.SKILL_MedicTurret_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_MedicTurret_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("EngiBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); MinionOwnership.onMinionGroupChangedGlobal += OnMinionGroupChangedGlobal; }
            public override void OnBodyRequirementBroken() { MinionOwnership.onMinionGroupChangedGlobal -= OnMinionGroupChangedGlobal; base.OnBodyRequirementBroken(); }
            public void OnMinionGroupChangedGlobal(MinionOwnership minion)
            {
                if (localUser?.cachedBody == null || minion.ownerMaster != localUser.cachedMaster) return;
                if (minion.group.memberCount < 6) return;
                foreach (var group in minion.group.members) if (!(group?.ownerMaster?.GetBody()?.name?.Contains("Drone2") ?? false) || !(group?.ownerMaster?.GetBody()?.name?.Contains("EmergencyDrone") ?? false)) return;
                Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_Sniper_NAME", "Skills.SKILL_Sniper_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_Sniper_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("EngiBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); MinionOwnership.onMinionGroupChangedGlobal += OnMinionGroupChangedGlobal; }
            public override void OnBodyRequirementBroken() { MinionOwnership.onMinionGroupChangedGlobal -= OnMinionGroupChangedGlobal; base.OnBodyRequirementBroken(); }
            public void OnMinionGroupChangedGlobal(MinionOwnership minion)
            {
                if (localUser?.cachedBody == null || minion.ownerMaster != localUser.cachedMaster) return;
                if (minion.group.memberCount < 6) return;
                foreach (var group in minion.group.members) if (!(group?.ownerMaster?.GetBody()?.name?.Contains("MissileDrone") ?? false) || !(group?.ownerMaster?.GetBody()?.name?.Contains("FlameDrone") ?? false)) return;
                Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_OffensiveMicrobots_NAME", "Skills.SKILL_OffensiveMicrobots_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_OffensiveMicrobots_NAMEAchievement : BaseAchievement
        {
            public static bool win = false;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CaptainBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); MinionOwnership.onMinionGroupChangedGlobal += OnMinionGroupChangedGlobal; Run.onClientGameOverGlobal += OnGameOver; Run.onRunStartGlobal += OnStart; Run.onRunDestroyGlobal += OnEnd; }
            public override void OnBodyRequirementBroken() { MinionOwnership.onMinionGroupChangedGlobal -= OnMinionGroupChangedGlobal; Run.onClientGameOverGlobal -= OnGameOver; Run.onRunStartGlobal -= OnStart; Run.onRunDestroyGlobal -= OnEnd; base.OnBodyRequirementBroken(); }
            public void OnStart(Run self) { win = true; }
            public void OnEnd(Run self) { win = false; }
            public void OnMinionGroupChangedGlobal(MinionOwnership minion)
            {
                if (!win || localUser?.cachedBody == null || minion.ownerMaster != localUser.cachedMaster) return;
                if (minion.group.memberCount <= 0) return;
                foreach (var group in minion.group.members) if (!(group?.ownerMaster?.GetBody()?.name?.Contains("Drone") ?? false))
                {
                    win = false;
                    return;
                }
            }
            public void OnGameOver(Run self, RunReport report)
            {
                if (win && report.gameEnding.isWin) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_Backup_NAME", "Skills.SKILL_Backup_NAME", null, null, "pseudopulse.ChaoticSkills", "xyz.yekoc.FetchAFriend")]
        public class SKILL_Backup_NAMEAchievement : BaseAchievement
        {
            public static bool win = false;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CaptainBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); MinionOwnership.onMinionGroupChangedGlobal += OnMinionGroupChangedGlobal; Run.onClientGameOverGlobal += OnGameOver; Run.onRunStartGlobal += OnStart; Run.onRunDestroyGlobal += OnEnd; }
            public override void OnBodyRequirementBroken() { MinionOwnership.onMinionGroupChangedGlobal -= OnMinionGroupChangedGlobal; Run.onClientGameOverGlobal -= OnGameOver; Run.onRunStartGlobal -= OnStart; Run.onRunDestroyGlobal -= OnEnd; base.OnBodyRequirementBroken(); }
            public void OnStart(Run self) { win = false; }
            public void OnEnd(Run self) { win = false; }
            public void OnMinionGroupChangedGlobal(MinionOwnership minion)
            {
                if (localUser?.cachedBody == null || minion.ownerMaster != localUser.cachedMaster) return;
                if (minion.group.memberCount <= 0) return;
                foreach (var group in minion.group.members) if (!(group?.ownerMaster?.GetBody()?.name?.Contains("TreeBot") ?? false))
                {
                    win = true;
                    return;
                }
                win = false;
            }
            public void OnGameOver(Run self, RunReport report)
            {
                if (win && report.gameEnding.isWin) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_DesignBeacon_NAME", "Skills.SKILL_DesignBeacon_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_DesignBeacon_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CaptainBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); GlobalEventManager.onCharacterDeathGlobal += OnCharacterDeathGlobal; }
            public override void OnBodyRequirementBroken() { GlobalEventManager.onCharacterDeathGlobal -= OnCharacterDeathGlobal; base.OnBodyRequirementBroken(); }
            public void OnCharacterDeathGlobal(DamageReport report)
            {
                if (localUser == null || localUser.cachedBody == null) return;
                if ((report.victimBodyIndex == BodyCatalog.FindBodyIndex("VoidRaidCrabBody") || report.victimBodyIndex == BodyCatalog.FindBodyIndex("VoidRaidCrabJointBody")) && report.attacker == localUser.cachedBody && DoesDamageQualify(report)) Grant();
            }
            private static bool DoesDamageQualify(DamageReport damageReport)
            {
                GenericDisplayNameProvider component = damageReport.damageInfo.inflictor.GetComponent<GenericDisplayNameProvider>();
                return (bool)component && component.displayToken != null && component.displayToken.StartsWith("CAPTAIN_SUPPLY_");
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_VoidBeacon_NAME", "Skills.SKILL_VoidBeacon_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_VoidBeacon_NAMEAchievement : BaseAchievement
        {
            public float time = 60f;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CaptainBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; base.OnBodyRequirementBroken(); }
            public void OnUpdate()
            {
                if (localUser == null || localUser.cachedBody == null) return;
                if (localUser.cachedBody.HasBuff(RoR2Content.Buffs.VoidFogMild) || localUser.cachedBody.HasBuff(RoR2Content.Buffs.VoidFogStrong))
                {
                    time -= Time.fixedDeltaTime;
                    if (time <= 0) Grant();
                }
                else time = 60f;
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_TETHER_NAME", "Skills.SKILL_TETHER_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_TETHER_NAMEAchievement : BaseAchievement
        {
            public static bool win = false;
            public static BullseyeSearch search;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MercBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; TeleporterInteraction.onTeleporterChargedGlobal += OnEnd; TeleporterInteraction.onTeleporterBeginChargingGlobal += OnStart; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; TeleporterInteraction.onTeleporterChargedGlobal -= OnEnd; TeleporterInteraction.onTeleporterBeginChargingGlobal -= OnStart; base.OnBodyRequirementBroken(); }
            public void OnStart(TeleporterInteraction self) { win = true; if (localUser?.cachedBody != null) search = new BullseyeSearch
            {
                teamMaskFilter = TeamMask.GetEnemyTeams(TeamIndex.Player),
                viewer = localUser.cachedBody,
                maxDistanceFilter = 12
            }; }
            public void OnEnd(TeleporterInteraction self) { if (win) Grant(); win = false; search = null; }
            public void OnUpdate()
            {
                if (!win || localUser?.cachedBody == null || search == null) return;
                search.searchOrigin = localUser.cachedBody.transform.position;
                search.RefreshCandidates();
                if (search.GetResults()?.Any() ?? false) return;
                win = false;
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_Shards_NAME", "Skills.SKILL_Shards_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_Shards_NAMEAchievement : BaseAchievement
        {
            public static bool win = false;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MageBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); On.EntityStates.Missions.BrotherEncounter.PreEncounter.OnEnter += OnStart; On.EntityStates.Missions.BrotherEncounter.EncounterFinished.OnEnter += OnEnd; }
            public override void OnBodyRequirementBroken() { On.EntityStates.Missions.BrotherEncounter.PreEncounter.OnEnter -= OnStart; On.EntityStates.Missions.BrotherEncounter.EncounterFinished.OnEnter -= OnEnd; base.OnBodyRequirementBroken(); }
            public void OnSkillActivatedAuthority(GenericSkill skill)
            {
                if (localUser.cachedBody.skillLocator.primary != skill) win = false;
            }

            public void OnStart(On.EntityStates.Missions.BrotherEncounter.PreEncounter.orig_OnEnter orig, EntityStates.Missions.BrotherEncounter.PreEncounter self)
            {
                orig(self);
                if (localUser?.cachedBody != null)
                {
                    win = true;
                    localUser.cachedBody.onSkillActivatedAuthority += OnSkillActivatedAuthority;
                }
            }
            public void OnEnd(On.EntityStates.Missions.BrotherEncounter.EncounterFinished.orig_OnEnter orig, EntityStates.Missions.BrotherEncounter.EncounterFinished self)
            {
                orig(self);
                if (win && localUser?.cachedMaster != null && !localUser.cachedMaster.IsDeadAndOutOfLivesServer()) Grant();
                if (localUser?.cachedBody != null) localUser.cachedBody.onSkillActivatedAuthority -= OnSkillActivatedAuthority;
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SKILL_Pathosis_NAME", "Skills.SKILL_Pathosis_NAME", null, null, "pseudopulse.ChaoticSkills")]
        public class SKILL_Pathosis_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CrocoBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); On.RoR2.ArenaMissionController.EndRound += OnEndRound; }
            public override void OnBodyRequirementBroken() { On.RoR2.ArenaMissionController.EndRound -= OnEndRound; base.OnBodyRequirementBroken(); }
            public void OnEndRound(On.RoR2.ArenaMissionController.orig_EndRound orig, ArenaMissionController self)
            {
                if (localUser == null || localUser.cachedBody == null || !NetworkServer.active || self.currentRound < self.totalRoundsMax) return;
                if (Run.instance.stageClearCount < 5) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_SPIKESTRIPSKILL_EMPCHARGE_NAME", "Skills.SPIKESTRIPSKILL_EMPCHARGE_NAME", null, null, "com.groovesalad.GrooveSaladSpikestripContent")]
        public class SPIKESTRIPSKILL_EMPCHARGE_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("CaptainBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; base.OnBodyRequirementBroken(); }
            public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.moveSpeed >= localUser.cachedBody.baseMoveSpeed * 5) Grant(); }
        }
    }
}

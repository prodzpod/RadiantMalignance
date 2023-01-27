using HarmonyLib;
using R2API;
using RoR2;
using RoR2.Achievements;
using RoR2.Orbs;
using RoR2.Skills;
using System.Collections.Generic;
using UnityEngine;

namespace RiskyMonkeyBase.Achievements
{
    public class LoadoutAchievement
    {
        public static Dictionary<string, UnlockableDef> unlockables;
        public static void Patch()
        {
            unlockables = new();
            if (Reference.Mods("com.lodington.AutoShot")) MakeUnlockable("AutoShot");
            if (Reference.Mods("com.Moffein.BanditDynamite")) MakeUnlockable("MOFFEINBANDITDYNAMITE_SECONDARY_NAME");
            if (Reference.Mods("com.macawesone.EngiShotgun"))
            {
                MakeUnlockable("ENGIPLUS_PLASMAGRENADES_NAMETOKEN");
                MakeUnlockable("ENGIPLUS_JETPACK_NAMETOKEN");
            }
            if (Reference.Mods("com.Tymmey.Templar"))
            {
                MakeUnlockable("TEMPLAR_PRIMARY_RAILGUN_NAME");
                MakeUnlockable("TEMPLAR_PRIMARY_FLAMETHROWER_NAME");
                MakeUnlockable("TEMPLAR_PRIMARY_BAZOOKA_NAME");
                MakeUnlockable("TEMPLAR_SECONDARY_SHOTGUN_NAME");
                MakeUnlockable("TEMPLAR_UTILITY_DODGE_NAME");
                MakeUnlockable("TEMPLAR_SPECIAL_SWAP_NAME");
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
                MakeUnlockable("PASSIVEAGRESSION_VIENDBUG");
                MakeUnlockable("PASSIVEAGRESSION_VIENDTEAR");
            }
            AchievementManager.onAchievementsRegistered += PostPatch;
        }

        public static void PostPatch()
        {
            if (Reference.Mods("com.lodington.AutoShot")) AddUnlockable("Auto Shot", "AutoShot");
            if (Reference.Mods("com.Moffein.BanditDynamite")) AddUnlockable("MOFFEINBANDITDYNAMITE_SECONDARY_NAME");
            if (Reference.Mods("com.macawesone.EngiShotgun"))
            {
                AddUnlockable("ENGIPLUS_PLASMAGRENADES_NAMETOKEN");
                AddUnlockable("ENGIPLUS_JETPACK_NAMETOKEN");
            }
            if (Reference.Mods("com.Tymmey.Templar"))
            {
                AddUnlockable("TEMPLAR_PRIMARY_RAILGUN_NAME");
                AddUnlockable("TEMPLAR_PRIMARY_FLAMETHROWER_NAME");
                AddUnlockable("TEMPLAR_PRIMARY_BAZOOKA_NAME");
                AddUnlockable("TEMPLAR_SECONDARY_SHOTGUN_NAME");
                AddUnlockable("TEMPLAR_UTILITY_DODGE_NAME");
                AddUnlockable("TEMPLAR_SPECIAL_SWAP_NAME");
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
                AddUnlockable("PASSIVEAGRESSION_VIENDBUG");
                AddUnlockable("PASSIVEAGRESSION_VIENDTEAR");
            }
        }

        public static void MakeUnlockable(string name)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Skills." + name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            unlockables.Add(name, unlockableDef);
            RiskyMonkeyBase.Log.LogDebug("Registered Unlockable " + name);
        }
        public static void AddUnlockable(string skillName, string name = null)
        {
            if (name == null) name = skillName;
            SkillDef def = null;
            UnlockableDef unlockableDef = unlockables[name];
            RiskyMonkeyBase.Log.LogDebug("Fetched Unlockable " + name);
            foreach (var skill in SkillCatalog.allSkillDefs) if (skill.skillNameToken == skillName) { def = skill; break; }
            if (def == null) return;
            unlockableDef.nameToken = def.skillNameToken;
            unlockableDef.achievementIcon = def.icon;
            AccessTools.FieldRefAccess<Sprite>(typeof(AchievementDef), "achievedIcon")(AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName)) = def.icon;
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

        [RegisterModdedAchievement("RiskyMonkey_Skills_MOFFEINBANDITDYNAMITE_SECONDARY_NAME", "Skills.MOFFEINBANDITDYNAMITE_SECONDARY_NAME", null, null, "com.Moffein.BanditDynamite")]
        public class MOFFEINBANDITDYNAMITE_SECONDARY_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Bandit2Body");
            public override void OnBodyRequirementMet()  {  base.OnBodyRequirementMet(); On.RoR2.CharacterBody.AddBuff_BuffDef += AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex += AddBuff; }
            public override void OnBodyRequirementBroken() { On.RoR2.CharacterBody.AddBuff_BuffDef -= AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex -= AddBuff; base.OnBodyRequirementBroken();  }
            public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffDef orig, CharacterBody self, BuffDef buff) { 
                if ((bool)self && self.teamComponent.teamIndex != TeamIndex.Player && self.activeBuffsList != null && hasDebuffs(self.activeBuffsList, 6)) Grant(); orig(self, buff); }
            public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffIndex orig, CharacterBody self, BuffIndex buff) { 
                if ((bool)self && self.teamComponent.teamIndex != TeamIndex.Player && self.activeBuffsList != null && hasDebuffs(self.activeBuffsList, 6)) Grant(); orig(self, buff); }
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
                if (damageReport.damageInfo.inflictor != null) RiskyMonkeyBase.Log.LogDebug(damageReport.damageInfo.inflictor.name);
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

        [RegisterModdedAchievement("RiskyMonkey_Skills_TEMPLAR_PRIMARY_RAILGUN_NAME", "Skills.TEMPLAR_PRIMARY_RAILGUN_NAME", null, null, "com.Tymmey.Templar")]
        public class TEMPLAR_PRIMARY_RAILGUN_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); GlobalEventManager.onClientDamageNotified += OnDamage; }
            public override void OnBodyRequirementBroken() { GlobalEventManager.onClientDamageNotified -= OnDamage; base.OnBodyRequirementBroken(); }
            public void OnDamage(DamageDealtMessage damageDealtMessage) { if (damageDealtMessage.attacker == null || damageDealtMessage.attacker != localUser.cachedBody) return; if (damageDealtMessage.damage >= 10000) Grant(); }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_TEMPLAR_PRIMARY_FLAMETHROWER_NAME", "Skills.TEMPLAR_PRIMARY_FLAMETHROWER_NAME", null, null, "com.Tymmey.Templar")]
        public class TEMPLAR_PRIMARY_FLAMETHROWER_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; base.OnBodyRequirementBroken(); }
            public void OnUpdate()
            {
                if (localUser == null || localUser.cachedBody == null) return;
                int num = 0;
                foreach (HurtBox hurtBox in new SphereSearch() { origin = localUser.cachedBody.footPosition, radius = 50000f, mask = LayerIndex.entityPrecise.mask }.RefreshCandidates()
                    .FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(localUser.cachedBody.teamComponent.teamIndex)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes())
                {
                    CharacterBody body = hurtBox.healthComponent.body;
                    body.RecalculateStats();
                    if (body.HasBuff(RoR2Content.Buffs.ClayGoo)) ++num;
                }
                if (num >= 20) Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_TEMPLAR_PRIMARY_BAZOOKA_NAME", "Skills.TEMPLAR_PRIMARY_BAZOOKA_NAME", null, null, "com.Tymmey.Templar")]
        public class TEMPLAR_PRIMARY_BAZOOKA_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); GlobalEventManager.onCharacterDeathGlobal += OnCharacterDeath; }
            public override void OnBodyRequirementBroken() { GlobalEventManager.onCharacterDeathGlobal -= OnCharacterDeath; base.OnBodyRequirementBroken(); }
            public void OnCharacterDeath(DamageReport damageReport)
            {
                if (localUser == null || localUser.cachedBody == null || Stage.instance.sceneDef.cachedName != "goolake") return;
                foreach (HurtBox hurtBox in new SphereSearch() { origin = localUser.cachedBody.footPosition, radius = 50000f, mask = LayerIndex.entityPrecise.mask }.RefreshCandidates().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes())
                {
                    CharacterBody body = hurtBox.healthComponent.body;
                    if (body.name.Contains("ExplosivePotDestructibleBody")) return;
                }
                Grant();
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_TEMPLAR_SECONDARY_SHOTGUN_NAME", "Skills.TEMPLAR_SECONDARY_SHOTGUN_NAME", null, null, "com.Tymmey.Templar")]
        public class TEMPLAR_SECONDARY_SHOTGUN_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; base.OnBodyRequirementBroken(); }
            public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.skillLocator.primary.cooldownScale <= 0.2f) Grant(); }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_TEMPLAR_UTILITY_DODGE_NAME", "Skills.TEMPLAR_UTILITY_DODGE_NAME", null, null, "com.Tymmey.Templar")]
        public class TEMPLAR_UTILITY_DODGE_NAMEAchievement : BaseAchievement
        {
            public bool enabled = false;
            public float stopwatch = 0;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor");
            public override void OnBodyRequirementMet() 
            { 
                base.OnBodyRequirementMet();
                On.EntityStates.Missions.BrotherEncounter.Phase1.OnEnter += OnEncounterStart;
                RoR2Application.onFixedUpdate += OnUpdate;
                On.EntityStates.Missions.BrotherEncounter.EncounterFinished.OnEnter += OnEncounterFinished;
            }
            public override void OnBodyRequirementBroken()
            {
                On.EntityStates.Missions.BrotherEncounter.Phase1.OnEnter -= OnEncounterStart;
                RoR2Application.onFixedUpdate -= OnUpdate;
                On.EntityStates.Missions.BrotherEncounter.EncounterFinished.OnEnter -= OnEncounterFinished;
                base.OnBodyRequirementBroken();
            }
            public void OnUpdate()
            {
                if (localUser == null || localUser.cachedBody == null || !enabled) return;
                stopwatch += Time.deltaTime;
            }

            public void OnEncounterStart(On.EntityStates.Missions.BrotherEncounter.Phase1.orig_OnEnter orig, EntityStates.Missions.BrotherEncounter.Phase1 self)
            {
                stopwatch = 0;
                enabled = true;
                orig(self);
            }

            public void OnEncounterFinished(On.EntityStates.Missions.BrotherEncounter.EncounterFinished.orig_OnEnter orig, EntityStates.Missions.BrotherEncounter.EncounterFinished self)
            {
                if (stopwatch < 180f)
                {
                    Grant();
                    enabled = false;
                }
                orig(self);
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_TEMPLAR_SPECIAL_SWAP_NAME", "Skills.TEMPLAR_SPECIAL_SWAP_NAME", null, null, "com.Tymmey.Templar")]
        public class TEMPLAR_SPECIAL_SWAP_NAMEAchievement : BaseAchievement
        {
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); RoR2Application.onUpdate += OnUpdate; }
            public override void OnBodyRequirementBroken() { RoR2Application.onUpdate -= OnUpdate; base.OnBodyRequirementBroken(); }
            public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.healthComponent.health >= 3000) Grant(); }
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
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); On.RoR2.CharacterBody.AddBuff_BuffDef += AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex += AddBuff; }
            public override void OnBodyRequirementBroken() { On.RoR2.CharacterBody.AddBuff_BuffDef -= AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex -= AddBuff; base.OnBodyRequirementBroken(); }
            public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffDef orig, CharacterBody self, BuffDef buff)
            {
                if ((bool)self && self.teamComponent.teamIndex != TeamIndex.Player && self.activeBuffsList != null && self.GetBuffCount(RoR2Content.Buffs.Bleeding) >= 50) Grant(); orig(self, buff);
            }
            public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffIndex orig, CharacterBody self, BuffIndex buff)
            {
                if ((bool)self && self.teamComponent.teamIndex != TeamIndex.Player && self.activeBuffsList != null && self.GetBuffCount(RoR2Content.Buffs.Bleeding) >= 50) Grant(); orig(self, buff);
            }
        }

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_MAGEICEARMOR", "Skills.PASSIVEAGRESSION_MAGEICEARMOR", null, null, "xyz.yekoc.PassiveAgression")]
        public class PASSIVEAGRESSION_MAGEICEARMORAchievement : BaseAchievement
        {
            public static float stopwatch = 0;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("MageBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); On.RoR2.RoR2Application.FixedUpdate += OnUpdate; On.RoR2.Stage.Start += OnStart; Run.onRunDestroyGlobal += OnGameOver; }
            public override void OnBodyRequirementBroken() { On.RoR2.RoR2Application.FixedUpdate -= OnUpdate; On.RoR2.Stage.Start -= OnStart; Run.onRunDestroyGlobal -= OnGameOver; base.OnBodyRequirementBroken(); }
            public void OnStart(On.RoR2.Stage.orig_Start orig, Stage self) { orig(self); stopwatch = 0; }
            public void OnGameOver(Run run) { stopwatch = -1; }
            public void OnUpdate(On.RoR2.RoR2Application.orig_FixedUpdate orig, RoR2Application self)
            {
                orig(self);
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
                foreach (var art in AccessTools.StaticFieldRefAccess<ArtifactDef[]>(typeof(ArtifactCatalog), "artifactDefs"))
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

        [RegisterModdedAchievement("RiskyMonkey_Skills_PASSIVEAGRESSION_VIENDBUG", "Skills.PASSIVEAGRESSION_VIENDBUG", null, null, "xyz.yekoc.PassiveAgression")]
        public class PASSIVEAGRESSION_VIENDBUGAchievement : BaseAchievement
        {
            public static bool win = false;
            public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("VoidSurvivorBody");
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); On.RoR2.RoR2Application.FixedUpdate += OnUpdate; TeleporterInteraction.onTeleporterChargedGlobal += OnTeleporter; On.RoR2.Stage.Start += OnStart; Run.onRunDestroyGlobal += OnGameOver; }
            public override void OnBodyRequirementBroken() { On.RoR2.RoR2Application.FixedUpdate -= OnUpdate; TeleporterInteraction.onTeleporterChargedGlobal -= OnTeleporter; On.RoR2.Stage.Start -= OnStart; Run.onRunDestroyGlobal -= OnGameOver; base.OnBodyRequirementBroken(); }
            public void OnStart(On.RoR2.Stage.orig_Start orig, Stage self) { orig(self); win = true; }
            public void OnGameOver(Run self) { win = false; }
            public void OnUpdate(On.RoR2.RoR2Application.orig_FixedUpdate orig, RoR2Application self)
            {
                orig(self);
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
            public override void OnBodyRequirementMet() { base.OnBodyRequirementMet(); On.RoR2.RoR2Application.FixedUpdate += OnUpdate; Run.onClientGameOverGlobal += OnGameOver; Run.onRunStartGlobal += OnStart; Run.onRunDestroyGlobal += OnEnd; }
            public override void OnBodyRequirementBroken() { On.RoR2.RoR2Application.FixedUpdate -= OnUpdate; Run.onClientGameOverGlobal -= OnGameOver; Run.onRunStartGlobal -= OnStart; Run.onRunDestroyGlobal -= OnEnd; base.OnBodyRequirementBroken(); }
            public void OnStart(Run self) { win = true; }
            public void OnEnd(Run self) { win = false; }
            public void OnUpdate(On.RoR2.RoR2Application.orig_FixedUpdate orig, RoR2Application self)
            {
                orig(self);
                if (!win || localUser == null || localUser.cachedBody == null) return;
                if (hasDebuffs(localUser.cachedBody.activeBuffsList, 1)) win = false;
            }
            public void OnGameOver(Run self, RunReport report)
            {
                if (win && report.gameEnding.isWin) Grant();
            }
        }
    }
}

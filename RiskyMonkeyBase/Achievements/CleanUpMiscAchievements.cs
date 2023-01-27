using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
using RoR2;
using RoR2.Achievements;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RiskyMonkeyBase.Achievements
{
    public class CleanUpMiscAchievements
    {
        public static UnlockableDef templarMonsoon;
        public static UnlockableDef templarBulwark;
        public static UnlockableDef minerBulwark;
        public static void Patch()
        {
            if (Reference.Mods("prodzpod.TemplarSkins")) templarMonsoon = MakeUnlockable("Skins.Templar.Alt1");
            if (Reference.Mods("com.themysticsword.bulwarkshaunt", "prodzpod.TemplarSkins")) templarBulwark = MakeUnlockable("Skins.Templar.BulwarksHaunt_Alt");
            if (Reference.Mods("com.themysticsword.bulwarkshaunt", "com.rob.DiggerUnearthed")) minerBulwark = MakeUnlockable("Skins.Miner.BulwarksHaunt_Alt");
            if (Reference.Mods("com.rob.DiggerUnearthed")) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchOurDefs));
            AchievementManager.onAchievementsRegistered += PostPatch;
        }
        public static void PostPatch()
        {
            RiskyMonkeyBase.Log.LogDebug("Skills: " + SkillCatalog.allSkillDefs.Join(skill => skill.skillNameToken));
            if (Reference.Mods("prodzpod.TemplarSkins")) AddUnlockable("skinTemplarAlt", templarMonsoon, true);
            if (Reference.Mods("com.themysticsword.bulwarkshaunt", "prodzpod.TemplarSkins")) AddUnlockable("skinTemplarBulwarksHauntAlt", templarBulwark, true);
            if (Reference.Mods("com.themysticsword.bulwarkshaunt", "com.rob.DiggerUnearthed")) AddUnlockable("MINERBODY_TUNDRA_SKIN_NAME", minerBulwark, true);
            if (Reference.Mods("com.Wolfo.WolfoQualityOfLife")) AddUnlockable("skinMercAltNoEdit", UnlockableCatalog.GetUnlockableDef("Skins.Merc.Alt1"));
            if (Reference.Mods("com.bobblet.UltrakillV1BanditSkin")) AddUnlockable("V2BanditSkin", UnlockableCatalog.GetUnlockableDef("Skins.Bandit2.Alt1"));
            if (Reference.Mods("com.12GaugeAwayFromFace.TeamFortress2_Engineer_Engineer_Skin")) AddUnlockable("TF2EngiSkinMonsoon", UnlockableCatalog.GetUnlockableDef("Skins.Engi.Alt1"));
            if (Reference.Mods("com.Heyimnoob.BioDroneAcrid")) AddUnlockable("RedDroneAcrid", UnlockableCatalog.GetUnlockableDef("Skins.Croco.Alt1"));
            if (Reference.Mods("com.SussyBnuuy.PEPSIMANVoidFiend")) AddUnlockable("PepsimanClassic", UnlockableCatalog.GetUnlockableDef("Skins.VoidSurvivor.Alt1"));
            if (Reference.Mods("com.rob.DiggerUnearthed"))
            {
                SkillFamily.Variant[] variants = DiggerPlugin.DiggerPlugin.characterBodyPrefab.GetComponent<SkillLocator>().special.skillFamily.variants;
                for (var i = 0; i < variants.Length; i++)
                {
                    if (variants[i].skillDef.skillNameToken == "MINER_SPECIAL_TOTHESTARSCLASSIC_NAME")
                    {
                        DiggerPlugin.Unlockables.pupleUnlockableDef.achievementIcon = variants[i].skillDef.icon;
                        IEnumerator<SkillFamily> families = SkillCatalog.allSkillFamilies.GetEnumerator();
                        DiggerPlugin.DiggerPlugin.characterBodyPrefab.GetComponent<SkillLocator>().special.skillFamily.variants[i].unlockableDef = DiggerPlugin.Unlockables.pupleUnlockableDef;
                        AccessTools.FieldRefAccess<Sprite>(typeof(AchievementDef), "achievedIcon")(AchievementManager.GetAchievementDefFromUnlockable(DiggerPlugin.Unlockables.pupleUnlockableDef.cachedName)) = variants[i].skillDef.icon;
                    }
                }
                GenericSkill[] skls = DiggerPlugin.DiggerPlugin.characterBodyPrefab.GetComponents<GenericSkill>();
                for (var i = 0; i < skls.Length; i++)
                {
                    if (SkillCatalog.GetSkillFamilyName(skls[i].skillFamily.catalogIndex) == "MinerBodyPassive")
                    {
                        for (var j = 0; j < skls[i].skillFamily.variants.Length; j++)
                        {
                            if (skls[i].skillFamily.variants[j].skillDef.skillNameToken == "PASSIVEAGRESSION_DIGGERFLAME")
                            {
                                DiggerPlugin.Unlockables.blacksmithUnlockableDef.achievementIcon = skls[i].skillFamily.variants[j].skillDef.icon;
                                AccessTools.FieldRefAccess<Sprite>(typeof(AchievementDef), "achievedIcon")(AchievementManager.GetAchievementDefFromUnlockable(DiggerPlugin.Unlockables.blacksmithUnlockableDef.cachedName)) = skls[i].skillFamily.variants[j].skillDef.icon;
                            }
                        }
                    }
                }
            }
            if (Reference.Mods("xyz.yekoc.PassiveAgression"))
            {
                GenericSkill[] skls = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiBody").GetComponents<GenericSkill>();
                for (var i = 0; i < skls.Length; i++)
                {
                    if (SkillCatalog.GetSkillFamilyName(skls[i].skillFamily.catalogIndex) == "EngiBodyPassive")
                    {
                        List<SkillFamily.Variant> variants = new(skls[i].skillFamily.variants);
                        int found = -1;
                        for (var j = 0; j < variants.Count; j++)
                        {
                            if (variants[j].skillDef.skillNameToken == "PASSIVEAGRESSION_NONE") found = j;
                        }
                        if (found != -1)
                        {
                            variants.RemoveAt(found);
                            RiskyMonkeyBase.Log.LogDebug("Skills: " + variants.Join());
                            LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiBody").GetComponents<GenericSkill>()[i].skillFamily.variants = variants.ToArray();
                        }
                    }
                }
            }
            if (Reference.Mods("com.sixfears7.M1BitePlus"))
            {
                Resources.Load<GameObject>("prefabs/characterbodies/CrocoBody");
                SkillFamily skillFamily = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Croco/CrocoBody.prefab").WaitForCompletion().GetComponent<SkillLocator>().primary.skillFamily;
                for (var i = 0; i < skillFamily.variants.Length; i++) if (skillFamily.variants[i].skillDef.skillName == "CrocoBite") skillFamily.variants[i].unlockableDef = UnlockableCatalog.GetUnlockableDef("Skills.Croco.CrocoBite");
            }
            if (Reference.Mods("com.Skell.GoldenCoastPlus"))
            {
                On.RoR2.Achievements.KillGoldTitanInOneCycleAchievement.KillGoldTitanInOnePhaseServerAchievement.OnInstall += (orig, self) =>
                {
                    RoR2Application.onUpdate += () => OnUpdate(self);
                    orig(self);
                };
                On.RoR2.Achievements.KillGoldTitanInOneCycleAchievement.KillGoldTitanInOnePhaseServerAchievement.OnUninstall += (orig, self) =>
                {
                    RoR2Application.onUpdate -= () => OnUpdate(self);
                    orig(self);
                };
            }
        }

        public static void OnUpdate(KillGoldTitanInOneCycleAchievement.KillGoldTitanInOnePhaseServerAchievement self)
        {
            CharacterBody body = self.serverAchievementTracker.networkUser.GetCurrentBody();
            if (body == null) return;
            if (body.GetBuffCount(BuffCatalog.FindBuffIndex("<style=cShrine>Aurelionite's Blessing</style>")) >= 2) AccessTools.Method(typeof(BaseServerAchievement), "Grant").Invoke(self, null);
        }

        [RegisterModdedAchievement("TemplarClearGameMonsoon", "Skins.Templar.Alt1", null, null, "prodzpod.TemplarSkins")] public class TemplarClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement { public override BodyIndex LookUpRequiredBodyIndex() => BodyCatalog.FindBodyIndex("Templar_Survivor"); }

        public static UnlockableDef MakeUnlockable(string name)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            RiskyMonkeyBase.Log.LogDebug("Registered Unlockable " + name);
            return unlockableDef;
        }
        public static void AddUnlockable(string skinName, UnlockableDef unlockableDef, bool setIcon = false)
        {
            SkinDef def = null;
            foreach (var skin in SkinCatalog.allSkinDefs) if (skin.name == skinName) def = skin;
            unlockableDef.nameToken = def.nameToken;
            unlockableDef.achievementIcon = def.icon;
            def.unlockableDef = unlockableDef;
            RiskyMonkeyBase.Log.LogDebug("Fetched Unlockable " + unlockableDef.cachedName);
            if (setIcon) AccessTools.FieldRefAccess<Sprite>(typeof(AchievementDef), "achievedIcon")(AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName)) = def.icon;
        }

        [HarmonyPatch(typeof(UnlockableAPI), "AddOurDefs")]
        public class PatchOurDefs
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel) 
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchStloc(1));
                c.Index++;
                c.Emit(OpCodes.Ldloc_1);
                c.EmitDelegate<Func<AchievementDef, AchievementDef>>(def =>
                {
                    if (def.nameToken == "MINER_TUNDRAUNLOCKABLE_ACHIEVEMENT_NAME") return null;
                    return def;
                });
                c.Emit(OpCodes.Stloc_1);
            }
        }
    }
}

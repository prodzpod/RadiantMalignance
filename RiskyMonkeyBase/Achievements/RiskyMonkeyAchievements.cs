using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2BepInExPack.VanillaFixes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine.Networking;

namespace RiskyMonkeyBase.Achievements
{
    public class RiskyMonkeyAchievements
    {
        public static List<string> achievementBlacklist;
        public static StringBuilder stringBuilder;
        public static void Patch()
        {
            achievementBlacklist = new();
            foreach (var entry in Reference.AchievementBlacklist.Value.Split(',')) achievementBlacklist.Add(entry.Trim());
            AchievementManager.onAchievementsRegistered += () => RiskyMonkeyBase.Log.LogDebug("Achievements: " + AchievementManager.readOnlyAchievementIdentifiers.Join());
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchAchievementDefs));
            Run.onRunSetRuleBookGlobal += (run, ruleBook) =>
            {
                for (var i = 0; i < run.availableItems.array.Length; i++) 
                    if (run.availableItems.array[i] && ItemCatalog.GetItemDef((ItemIndex)i).unlockableDef != null && 
                    !PreGameController.AnyUserHasUnlockable(ItemCatalog.GetItemDef((ItemIndex)i).unlockableDef)) 
                        run.availableItems.array[i] = false;
                for (var i = 0; i < run.availableEquipment.array.Length; i++) 
                    if (run.availableEquipment.array[i] && EquipmentCatalog.GetEquipmentDef((EquipmentIndex)i).unlockableDef != null &&
                    !PreGameController.AnyUserHasUnlockable(EquipmentCatalog.GetEquipmentDef((EquipmentIndex)i).unlockableDef)) 
                        run.availableEquipment.array[i] = false;
                PickupDropTable.RegenerateAll(run);
            };
            On.RoR2.Artifacts.EnigmaArtifactManager.OnRunStartGlobal += (orig, run) =>
            {
                orig(run);
                if (NetworkServer.active) RoR2.Artifacts.EnigmaArtifactManager.validEquipment.RemoveAll((EquipmentIndex x) => !PreGameController.AnyUserHasUnlockable(EquipmentCatalog.GetEquipmentDef(x).unlockableDef));
            };
        }

        public static void Log(string content)
        {
            if (stringBuilder == null) stringBuilder = HG.StringBuilderPool.RentStringBuilder();
            stringBuilder.AppendLine(content);
        }

        public static void Print()
        {
            if (stringBuilder == null) return;
            RiskyMonkeyBase.Log.LogDebug(stringBuilder.ToString());
            HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
        }

        [HarmonyPatch(typeof(SaferAchievementManager), nameof(SaferAchievementManager.SaferCollectAchievementDefs))]
        public class PatchAchievementDefs
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel) 
            {
                ILCursor c = new(il);
                c.Index = 0;
                c.EmitDelegate(() => { RiskyMonkeyBase.Log.LogDebug("Hook Planted"); });
                c.GotoNext(x => x.MatchCastclass<RegisterAchievementAttribute>(), x => x.MatchStloc(11));
                c.Index += 2;
                c.Emit(OpCodes.Ldloc, 10);
                c.Emit(OpCodes.Ldloc, 11);
                c.EmitDelegate<Func<Type, RegisterAchievementAttribute, RegisterAchievementAttribute>>((type, achievementAttribute) =>
                {
                    if (achievementAttribute != null)
                    {
                        if (achievementBlacklist.Contains(achievementAttribute.unlockableRewardIdentifier)) return null;
                        if (achievementAttribute.prerequisiteAchievementIdentifier != null && achievementBlacklist.Contains(achievementAttribute.prerequisiteAchievementIdentifier)) achievementAttribute.prerequisiteAchievementIdentifier = null;
                        return achievementAttribute;    
                    }
                    RegisterModdedAchievementAttribute moddedAttribute = (RegisterModdedAchievementAttribute)Enumerable.FirstOrDefault(type.GetCustomAttributes(false), v => v is RegisterModdedAchievementAttribute);
                    if (moddedAttribute == null || !Reference.Mods(moddedAttribute.mods) || !Reference.CustomAchievements.Value || achievementBlacklist.Contains(moddedAttribute.unlockableRewardIdentifier)) return null;
                    MethodInfo m = type.GetMethod("OnlyRegisterIf");
                    if (m != null && !(bool)m.Invoke(null, null)) return null;
                    if (moddedAttribute.prerequisiteAchievementIdentifier != null && achievementBlacklist.Contains(moddedAttribute.prerequisiteAchievementIdentifier)) moddedAttribute.prerequisiteAchievementIdentifier = null;
                    return new RegisterAchievementAttribute(moddedAttribute.identifier, moddedAttribute.unlockableRewardIdentifier, moddedAttribute.prerequisiteAchievementIdentifier, moddedAttribute.serverTrackerType);
                });
                c.Emit(OpCodes.Stloc, 11);
            }
        }
    }
}

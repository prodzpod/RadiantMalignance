using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.Achievements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RiskyMonkeyBase.Achievements
{
    public class RiskyMonkeyAchievements
    {
        public static void Patch()
        {
            AchievementManager.onAchievementsRegistered += () => RiskyMonkeyBase.Log.LogDebug(AchievementManager.readOnlyAchievementIdentifiers.Join());
            RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchAchievementDefs));
        }

        [HarmonyPatch(typeof(RoR2BepInExPack.VanillaFixes.SaferAchievementManager), "SaferCollectAchievementDefs")]
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
                    if (achievementAttribute != null) return achievementAttribute;
                    RegisterModdedAchievementAttribute moddedAttribute = (RegisterModdedAchievementAttribute)Enumerable.FirstOrDefault(type.GetCustomAttributes(false), v => v is RegisterModdedAchievementAttribute);
                    if (moddedAttribute == null || !Reference.Mods(moddedAttribute.mods)) return null;
                    RiskyMonkeyBase.Log.LogDebug("Found Modded Achievement: " + moddedAttribute.identifier);
                    return new RegisterAchievementAttribute(moddedAttribute.identifier, moddedAttribute.unlockableRewardIdentifier, moddedAttribute.prerequisiteAchievementIdentifier, moddedAttribute.serverTrackerType);
                });
                c.Emit(OpCodes.Stloc, 11);
            }
        }
    }
}

using HarmonyLib;
using TPDespair.ZetArtifacts;

namespace RiskyMonkeyBase.Tweaks
{
    public class ReviveBetweenMithrixPhase
    {
        public static int State = 0;
        public static int Phase = 99;
        public static void Patch()
        {
            On.RoR2.Run.Start += (orig, self) =>
            {
                Phase = 99; // not moon
                orig(self);
            };
            On.EntityStates.Missions.BrotherEncounter.PreEncounter.OnEnter += (orig, self) =>
            {
                Phase = 1; // moon
                orig(self);
            }; 
            On.RoR2.Run.OnServerBossDefeated += (orig, self, bossGroup) =>
            {
                State = AccessTools.StaticFieldRefAccess<int>(typeof(ZetRevivifact), "State");
                if (Phase < 4)
                {
                    AccessTools.StaticFieldRefAccess<int>(typeof(ZetRevivifact), "State") = 0;
                    RiskyMonkeyBase.Log.LogDebug("No Revives!");
                }
                Phase++;
                orig(self, bossGroup);
                AccessTools.StaticFieldRefAccess<int>(typeof(ZetRevivifact), "State") = State;
            }; // its LIFO AND Janky Hack:tm:
        }
    }
}

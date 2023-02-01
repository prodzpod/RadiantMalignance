using HarmonyLib;
using TPDespair.ZetArtifacts;

namespace RiskyMonkeyBase.Tweaks
{
    public class ReviveBetweenMithrixPhase
    {
        public static int State = 0;
        public static bool Active = false;
        public static void Patch()
        {
            On.RoR2.Run.Start += (orig, self) =>
            {
                Active = false;
                orig(self);
            };
            On.EntityStates.Missions.BrotherEncounter.PreEncounter.OnEnter += (orig, self) =>
            {
                Active = true;
                orig(self);
            };
            On.EntityStates.Missions.BrotherEncounter.Phase4.OnEnter += (orig, self) =>
            {
                Active = false;
                orig(self);
            };
            On.RoR2.Run.OnServerBossDefeated += (orig, self, bossGroup) =>
            {
                State = ZetRevivifact.State;
                if (Active)
                {
                    ZetRevivifact.State = 0;
                    RiskyMonkeyBase.Log.LogDebug("No Revives!");
                }
                orig(self, bossGroup);
                ZetRevivifact.State = State;
            }; // its LIFO AND Janky Hack:tm:
        }
    }
}

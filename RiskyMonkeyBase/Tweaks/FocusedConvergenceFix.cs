using MonoMod.Cil;
using System;
using UnityEngine;

namespace RiskyMonkeyBase.Tweaks
{
    public class FocusedConvergenceFix
    {
        public static void Patch()
        {
            IL.RoR2.HoldoutZoneController.FocusConvergenceController.FixedUpdate += (il) =>
            {
                ILCursor c = new ILCursor(il);
                c.GotoNext(x => x.MatchCall<Mathf>("Min"));
                c.Index -= 3;
                c.RemoveRange(6); // remove Mathf.min code
            };
            if (Reference.FocusedConvergenceRateLimit.Value >= 0) IL.RoR2.HoldoutZoneController.FocusConvergenceController.ApplyRate += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdfld("RoR2.HoldoutZoneController/FocusConvergenceController", "currentFocusConvergenceCount"), x => x.MatchConvR4(), x => x.MatchMul(), x => x.MatchDiv());
                c.Index++;
                c.EmitDelegate<Func<int, int>>((count) => { return Mathf.Min(count, Reference.FocusedConvergenceRateLimit.Value); });
            };
            if (Reference.FocusedConvergenceRangeLimit.Value >= 0) IL.RoR2.HoldoutZoneController.FocusConvergenceController.ApplyRadius += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdfld("RoR2.HoldoutZoneController/FocusConvergenceController", "currentFocusConvergenceCount"), x => x.MatchConvR4(), x => x.MatchMul(), x => x.MatchDiv());
                c.Index++;  
                c.EmitDelegate<Func<int, int>>((count) => { return Mathf.Min(count, Reference.FocusedConvergenceRangeLimit.Value); });
            };
        }
    }
}

using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
using RoR2;
using System;
using UnityEngine;

namespace RiskyMonkeyBase.Contents
{
    public class DownpourDifficulty
    {
        public static DifficultyDef Downpour;
        public static DifficultyIndex DownpourIndex;
        public static void AddDifficulty()
        {
            Downpour = new(999f, "DOWNPOUR_TITLE", "DOWNPOUR_ICON", "DOWNPOUR_DESC", new Color32(98, 157, 230, 255), "DP", false); // 999 to sort to last
            Downpour.iconSprite = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/downpour.png");
            Downpour.foundIconSprite = true;
            DownpourIndex = DifficultyAPI.AddDifficulty(Downpour);
            IL.RoR2.Run.RecalculateDifficultyCoefficentInternal += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdarg(0), x => x.MatchLdloc(5), x => x.MatchLdloc(8), x => x.MatchLdloc(2));
                c.Emit(OpCodes.Ldloc_0);
                c.Emit(OpCodes.Ldloc_1);
                c.Emit(OpCodes.Ldloc, 6);
                c.EmitDelegate<Func<float, DifficultyDef, float, float>>((sec, difficultyDef, people) =>
                {
                    if (difficultyDef != Downpour) return 0.0506f * difficultyDef.scalingValue * people; 
                    return 0.0506f * (2f + (Reference.DownpourScaling.Value * sec / 3000f)) * people; // 0.02 per perfect, 50(1 to 0.02) * 60(sec to min)
                });
                c.Emit(OpCodes.Stloc, 7);
                c.Emit(OpCodes.Ldloc, 7);
                c.Emit(OpCodes.Stloc, 8); // why is there an identical value lmfao
            };
            On.RoR2.Language.GetLocalizedStringByToken += (orig, self, token) =>
            {
                if (token == "DOWNPOUR_DESC") return orig(self, token).Replace("{0}", Reference.DownpourScaling.Value.ToString());
                return orig(self, token);
            };
        }
    }
}

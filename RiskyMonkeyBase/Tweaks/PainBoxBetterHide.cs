using GrooveSaladSpikestripContent;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.UI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace RiskyMonkeyBase.Tweaks
{
    public class PainBoxBetterHide
    {
        public static Sprite sprite = Base.GroovyAssetBundle.LoadAsset<Sprite>("texPainBoxHealthbar.png");
        public static void Patch()
        {
            On.RoR2.UI.HealthBar.ApplyBars += (orig, self) =>
            {
                if ((self?.source?.body?.inventory?.GetItemCount(ItemCatalog.FindItemIndex("ITEM_PAINBOX")) ?? 0) > 0)
                {
                    self.barInfoCollection.ospBarInfo.enabled = false;
                    self.barInfoCollection.cullBarInfo.enabled = false;
                    self.barInfoCollection.barrierBarInfo.enabled = false;
                }
                orig(self);
            };
            IL.RoR2.UI.HealthBar.UpdateBarInfos += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchStfld<HealthBar.BarInfo>(nameof(HealthBar.BarInfo.normalizedXMax)), x => x.MatchLdloc(2));
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate(CheckPainBox);
                c.GotoNext(x => x.MatchLdloca(0), x => x.MatchCallOrCallvirt<HealthBar>("<UpdateBarInfos>g__AddBar|38_0"));
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate(CheckPainBox);
                c.GotoNext(x => x.MatchStfld<HealthBar.BarInfo>(nameof(HealthBar.BarInfo.normalizedXMax)), x => x.MatchLdloc(3));
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate(CheckPainBox);
            };
        }
        
        public static float CheckPainBox(float orig, HealthBar self)
        {
            if ((self?.source?.body?.inventory?.GetItemCount(ItemCatalog.FindItemIndex("ITEM_PAINBOX")) ?? 0) > 0) return 1;
            return orig;
        }
    }
}

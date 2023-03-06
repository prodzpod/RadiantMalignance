using EntityStates.LaserTurbine;
using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
using RoR2;
using RoR2.Items;
using RoR2.Orbs;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace RiskyMonkeyBase.Contents
{
    public class NumberChanges
    {
        public static Dictionary<string, Func<On.RoR2.Language.orig_GetLocalizedStringByToken, Language, string, string>> rules;
        public static List<Orb> lightnings;
        public static Dictionary<CharacterMotor, float> mass = new();
        public static void Patch()
        {
            rules = new();
            lightnings = new();
            if (Reference.DropletPickupRadius.Value != 1)
            {
                foreach (var source in new string[] { "HealPack", "AmmoPack", "BonusMoneyPack" }) BuffPickupRange(source);
                if (Reference.Mods("com.ContactLight.LostInTransit")) BuffMeat();
                if (Reference.Mods("com.themysticsword.mysticsitems")) RoR2Application.onLoad += BuffMysticsPickup;
            }
            // common
            if (Reference.MagazineCooldown.Value != 0)
            {
                On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
                {
                    orig(self);
                    if (self == null || self.inventory == null) return;
                    if (self.skillLocator != null && self.skillLocator.secondary != null) self.skillLocator.secondary.cooldownScale *= Mathf.Max(0, 1 - (Reference.MagazineCooldown.Value * self.inventory.GetItemCount(RoR2Content.Items.SecondarySkillMagazine)));
                };
                rules.Add("ITEM_SECONDARYSKILLMAGAZINE_DESC", (orig, self, token) =>
                {
                    return orig(self, token) + " " + orig(self, "RISKYMONKEY_ITEM_SECONDARYSKILLMAGAZINE_DESC").Replace("{0}", (Reference.MagazineCooldown.Value * 100).ToString());
                });
            }
            if (Reference.TTimeMax.Value != 1f)
            {
                IL.RoR2.HealthComponent.TakeDamage += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchLdfld<HealthComponent.ItemCounts>(nameof(HealthComponent.ItemCounts.bear)), x => x.MatchConvR4(), x => x.MatchMul(), x => x.MatchCallOrCallvirt(typeof(Util), nameof(Util.ConvertAmplificationPercentageIntoReductionPercentage)));
                    c.Index += 3;
                    c.Remove();
                    c.EmitDelegate<Func<float, float>>(bear =>
                    {
                        float count = bear / 15;
                        float coeff = 300 / ((20 * Reference.TTimeMax.Value) - 3); // should be good
                        return 100 * Reference.TTimeMax.Value * (1 - (100 / ((count * coeff) + 100)));
                    });
                };
                rules.Add("ITEM_BEAR_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_BEAR_DESC").Replace("{0}", (Reference.TTimeMax.Value * 100).ToString());
                });
            }
            if (Reference.SSpaceMax.Value != 0f)
            {
                IL.RoR2.HealthComponent.TakeDamage += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchLdloc(14));
                    c.Index += 4;
                    c.EmitDelegate<Func<float, float>>(chance =>
                    {
                        return Reference.SSpaceMax.Value + (chance / 15f * (15f - Reference.SSpaceMax.Value));
                    });
                };
                rules.Add("ITEM_BEARVOID_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_BEARVOID_DESC").Replace("{0}", Reference.SSpaceMax.Value.ToString());
                });
            }
            if (Reference.BungusKnockback.Value)
            {
                On.RoR2.CharacterMotor.ApplyForceImpulse += (On.RoR2.CharacterMotor.orig_ApplyForceImpulse orig, CharacterMotor self, ref PhysForceInfo info) =>
                {
                    if (self.body.inventory != null && self.body.GetNotMoving() && (self.body.inventory.GetItemCount(RoR2Content.Items.Mushroom) > 0 || self.body.inventory.GetItemCount(ItemCatalog.FindItemIndex("ITEM_SHARP_ANCHOR")) > 0)) return;
                    orig(self, ref info);
                };
                rules.Add("ITEM_MUSHROOM_PICKUP", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_MUSHROOM_PICKUP");
                });
                rules.Add("ITEM_MUSHROOM_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_MUSHROOM_DESC");
                });
                rules.Add("ITEM_SHARP_ANCHOR_PICKUP", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_SHARP_ANCHOR_PICKUP");
                });
                rules.Add("ITEM_SHARP_ANCHOR_DESCRIPTION", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_SHARP_ANCHOR_DESCRIPTION");
                });
            }
            if (Reference.ElixirHealth.Value != 0.25f)
            {
                IL.RoR2.HealthComponent.UpdateLastHitTime += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchLdarg(0), x => x.MatchCallOrCallvirt<HealthComponent>("get_isHealthLow"));
                    c.Index++;
                    c.Remove();
                    c.EmitDelegate<Func<HealthComponent, bool>>(self =>
                    {
                        return (self.health / self.fullHealth) < Reference.ElixirHealth.Value;
                    });
                };
                rules.Add("ITEM_HEALINGPOTION_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_HEALINGPOTION_DESC").Replace("{0}", (Reference.ElixirHealth.Value * 100).ToString()).Replace("{1}", ((1 - Reference.ElixirHealth.Value) * 100).ToString());
                });
            }
            if (Reference.GasolineArea.Value != 12 || Reference.GasolineAreaStack.Value != 4 || Reference.GasolineBlast.Value != 1.5f || Reference.GasolineBlastFlat.Value != 0 || Reference.GasolineBurn.Value != 1.5f || Reference.GasolineBurnStack.Value != 0.75f)
            {
                IL.RoR2.GlobalEventManager.ProcIgniteOnKill += (il) => {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchLdcR4(8));
                    c.Next.Operand = Reference.GasolineArea.Value - Reference.GasolineAreaStack.Value;
                    c.GotoNext(x => x.MatchLdcR4(4));
                    c.Next.Operand = Reference.GasolineAreaStack.Value;
                    c.GotoNext(x => x.MatchLdcR4(1.5f));
                    c.Next.Operand = Reference.GasolineBlast.Value;
                    c.GotoNext(x => x.MatchStloc(3));
                    c.Emit(OpCodes.Ldc_R4, Reference.GasolineBlastFlat.Value);
                    c.Emit(OpCodes.Add);
                    c.GotoNext(x => x.MatchLdarg(0), x => x.MatchLdfld<DamageReport>(nameof(DamageReport.attackerBody)), x => x.MatchCallOrCallvirt<CharacterBody>("get_damage"));
                    c.EmitDelegate<Func<float, float>>(chance =>
                    {
                        return Reference.GasolineBurn.Value + ((chance - 0.75f) / 0.75f * (Reference.GasolineBurnStack.Value - 1));
                    });
                };
                rules.Add("ITEM_IGNITEONKILL_DESC", (orig, self, token) =>
                {
                    string ret = "Killing an enemy <style=cIsDamage>ignites</style> all enemies within <style=cIsDamage>" + Reference.GasolineArea.Value + "m</style>";
                    if (Reference.GasolineAreaStack.Value != 0) ret += " <style=cStack>(+" + Reference.GasolineAreaStack.Value + "m per stack)</style>";
                    bool blast = Reference.GasolineBlast.Value != 0 || Reference.GasolineBlastFlat.Value != 0;
                    bool burn = Reference.GasolineBurn.Value != 0 || Reference.GasolineBurnStack.Value != 0;
                    if (blast)
                    {
                        ret += " for <style=cIsDamage>";
                        if (Reference.GasolineBlastFlat.Value != 0)
                        {
                            ret += Reference.GasolineBlastFlat.Value;
                            if (Reference.GasolineBlast.Value != 0) ret += " plus ";
                            else ret += "</style>";
                        }
                        if (Reference.GasolineBlast.Value != 0) ret += (Reference.GasolineBlast.Value * 100) + "%</style> base";
                        ret += " damage.";
                    }
                    if (burn)
                    {
                        if (blast) ret += " Additionally, enemies <style=cIsDamage>burn</style> for <style=cIsDamage>";
                        else ret += ", <style=cIsDamage>burning</style> enemies for <style=cIsDamage>";
                        ret += (Reference.GasolineBurn.Value * 100) + "%</style>";
                        if (Reference.GasolineBurnStack.Value != 0) ret += " <style=cStack>(+" + (Reference.GasolineBurnStack.Value * 100) + "% per stack)</style>";
                        ret += " base damage.";
                    }
                    return ret;
                });
            }
            if (Reference.FireworkProc.Value != 0.2f) LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/FireworkProjectile").GetComponent<ProjectileController>().procCoefficient = Reference.FireworkProc.Value;
            if (Reference.BroochAmount.Value != 15 || Reference.BroochStack.Value != 15)
            {
                IL.RoR2.GlobalEventManager.OnCharacterDeath += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchCallOrCallvirt<HealthComponent>(nameof(HealthComponent.AddBarrier)));
                    c.EmitDelegate<Func<float, float>>(orig =>
                    {
                        return Reference.BroochAmount.Value + ((orig - 15) * Reference.BroochStack.Value / 15);
                    });
                };
                rules.Add("ITEM_BARRIERONKILL_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_BARRIERONKILL_DESC").Replace("{0}", Reference.BroochAmount.Value.ToString()).Replace("{1}", Reference.BroochStack.Value.ToString());
                });
            }
            if (Reference.WarbannerStack.Value != 0)
            {
                RecalculateStatsAPI.GetStatCoefficients += (self, args) =>
                {
                    if (self == null || self.inventory == null) return;
                    float stack = self.inventory.GetItemCount(RoR2Content.Items.WardOnLevel) * Reference.WarbannerStack.Value + (Reference.WarbannerAmount.Value - Reference.WarbannerStack.Value);
                    if (self.HasBuff(RoR2Content.Buffs.Warbanner))
                    {
                        args.moveSpeedMultAdd += stack - 0.3f;
                        args.attackSpeedMultAdd += stack - 0.3f;
                    }
                };
                if (Reference.WarbannerSkillCooldown.Value)
                {
                    On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
                    {
                        orig(self);
                        if (self == null || self.inventory == null) return;
                        if (self.HasBuff(RoR2Content.Buffs.Warbanner) && self.skillLocator != null)
                        {
                            float scale = Mathf.Max(0, 1 - self.inventory.GetItemCount(RoR2Content.Items.WardOnLevel) * Reference.WarbannerStack.Value + (Reference.WarbannerAmount.Value - Reference.WarbannerStack.Value));
                            if (self.skillLocator.primary != null) self.skillLocator.primary.cooldownScale *= scale;
                            if (self.skillLocator.secondary != null) self.skillLocator.secondary.cooldownScale *= scale;
                            if (self.skillLocator.utility != null) self.skillLocator.utility.cooldownScale *= scale;
                            if (self.skillLocator.special != null) self.skillLocator.special.cooldownScale *= scale;
                        }
                    };
                    rules.Add("ITEM_WARDONLEVEL_PICKUP", (orig, self, token) =>
                    {
                        return orig(self, "RISKYMONKEY_ITEM_WARDONLEVEL_PICKUP");
                    });
                }
                rules.Add("ITEM_WARDONLEVEL_DESC", (orig, self, token) =>
                {
                    string ret = orig(self, "RISKYMONKEY_ITEM_WARDONLEVEL_DESC");
                    if (Reference.WarbannerSkillCooldown.Value) ret += ", <style=cIsUtility>movement speed</style> and <style=cIsUtility>cooldown reduction</style>";
                    else ret += " and <style=cIsUtility>movement speed</style>";
                    ret += " by <style=cIsDamage>" + (Reference.WarbannerAmount.Value * 100) + "%</style>";
                    if (Reference.WarbannerStack.Value != 0) ret += " <style=cStack>(+" + (Reference.WarbannerAmount.Value * 100) + "% per stack)</style>";
                    return ret + ".";
                });
            }
            if (Reference.Mogus.Value != 0.2f)
            {
                IL.RoR2.HealthComponent.TakeDamage += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchLdloc(24), x => x.MatchConvR4());
                    c.Index += 2;
                    c.Next.Operand = Reference.Mogus.Value;
                };
                rules.Add("ITEM_NEARBYDAMAGEBONUS_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_NEARBYDAMAGEBONUS_DESC").Replace("{0}", (Reference.Mogus.Value * 100).ToString());
                });
            }
            // uncommon
            if (Reference.ScytheHealAmount.Value != 4 || Reference.ScytheHealStack.Value != 4)
            {
                IL.RoR2.GlobalEventManager.OnCrit += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchLdcR4(4));
                    c.Next.Operand = Reference.ScytheHealAmount.Value;
                    c.Index++;
                    c.GotoNext(x => x.MatchLdcR4(4));
                    c.Next.Operand = Reference.ScytheHealStack.Value;
                };
                rules.Add("ITEM_HEALONCRIT_DESC", (orig, self, token) =>
                {
                    string ret = "Gain <style=cIsDamage>5% critical chance</style>. <style=cIsDamage>Critical strikes</style> <style=cIsHealing>heal</style> for <style=cIsHealing>" + Reference.ScytheHealAmount.Value + "</style>";
                    if (Reference.ScytheHealStack.Value != 0) ret += " <style=cStack>(+" + Reference.ScytheHealStack.Value + " per stack)</style>";
                    ret += " <style=cIsHealing>health</style>.";
                    return ret;
                });
            }
            if (Reference.TankAmount.Value != 3 || Reference.TankStack.Value != 3)
            {
                IL.RoR2.StrengthenBurnUtils.CheckDotForUpgrade += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchLdcI4(1), x => x.MatchLdcI4(3));
                    c.Remove(); c.Emit(OpCodes.Ldc_R4, 1 + Reference.TankAmount.Value - Reference.TankStack.Value);
                    c.Remove(); c.Emit(OpCodes.Ldc_R4, Reference.TankStack.Value);
                    c.Index++;
                    c.Emit(OpCodes.Conv_R4);
                };
                rules.Add("ITEM_STRENGTHENBURN_DESC", (orig, self, token) =>
                {
                    string ret = "Ignite effects deal <style=cIsDamage>+" + (Reference.TankAmount.Value * 100) + "%</style>";
                    if (Reference.ScytheHealStack.Value != 0) ret += " <style=cStack>(+" + (Reference.TankStack.Value * 100) + "% per stack)</style>";
                    ret += " more damage over time.";
                    return ret;
                });
            }
            if (Reference.StealthkitHealth.Value != 0.25f)
            {
                IL.RoR2.Items.PhasingBodyBehavior.FixedUpdate += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchCallOrCallvirt<HealthComponent>("get_isHealthLow"));
                    c.Index++;
                    c.Emit(OpCodes.Pop);
                    c.Emit(OpCodes.Ldarg_0);
                    c.EmitDelegate<Func<PhasingBodyBehavior, bool>>(self =>
                    {
                        return self.body.healthComponent.combinedHealthFraction < Reference.StealthkitHealth.Value;
                    });
                };
                rules.Add("ITEM_PHASING_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_PHASING_DESC").Replace("{0}", (Reference.StealthkitHealth.Value * 100).ToString());
                });
            }
            if (Reference.WireDamage.Value != 1.5f || Reference.WireProc.Value != 1)
            {
                IL.RoR2.HealthComponent.TakeDamage += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchStloc(65));
                    c.GotoNext(x => x.MatchLdcR4(1.6f));
                    c.Next.Operand = Reference.WireDamage.Value;
                    c.GotoNext(x => x.MatchLdcR4(0.5f));
                    c.Next.Operand = Reference.WireProc.Value;
                };
                rules.Add("ITEM_THORNS_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_THORNS_DESC").Replace("{0}", (Reference.WireDamage.Value * 100).ToString());
                });
            }
            if (Reference.WispProc.Value != 1) LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/WilloWispDelay").GetComponent<DelayBlast>().procCoefficient = Reference.WispProc.Value;
            if (Reference.WispDamage.Value != 3.5f || Reference.WispDamageStack.Value != 2.8f || Reference.WispArea.Value != 12 || Reference.WispAreaStack.Value != 2.4f)
            {
                IL.RoR2.GlobalEventManager.OnCharacterDeath += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchLdcR4(3.5f));
                    c.Next.Operand = Reference.WispDamage.Value;
                    c.GotoNext(x => x.MatchLdcR4(0.8f));
                    c.Next.Operand = Reference.WispDamageStack.Value / Reference.WispDamage.Value;
                    c.GotoNext(x => x.MatchLdcR4(12), x => x.MatchLdcR4(2.4f));
                    c.Next.Operand = Reference.WispArea.Value;
                    c.Next.Next.Operand = Reference.WispAreaStack.Value;
                };
                rules.Add("ITEM_EXPLODEONDEATH_DESC", (orig, self, token) =>
                {
                    string ret = "On killing an enemy, spawn a <style=cIsDamage>lava pillar</style> in a <style=cIsDamage>" + Reference.WispArea.Value + "m</style>";
                    if (Reference.WispAreaStack.Value != 0) ret += " <style=cStack>(+" + Reference.WispAreaStack.Value + "m per stack)</style>";
                    ret += " radius for <style=cIsDamage>" + Reference.WispDamage.Value + "%</style>";
                    if (Reference.WispDamageStack.Value != 0) ret += " <style=cStack>(+" + Reference.WispDamageStack.Value + "% per stack)</style>";
                    return ret + " base damage.";
                });
            }
            if (Reference.WarHornDuration.Value != 8 || Reference.WarHornDurationStack.Value != 4 || Reference.WarHornSpeed.Value != 0.7 || Reference.WarHornSpeedStack.Value != 0)
            {
                RecalculateStatsAPI.GetStatCoefficients += (self, args) =>
                {
                    if (self == null || self.inventory == null) return;
                    float stack = self.inventory.GetItemCount(RoR2Content.Items.EnergizedOnEquipmentUse) * Reference.WarHornSpeedStack.Value + (Reference.WarHornSpeed.Value - Reference.WarHornSpeedStack.Value);
                    if (self.HasBuff(RoR2Content.Buffs.Energized)) args.attackSpeedMultAdd += stack - 0.7f;
                };
                IL.RoR2.EquipmentSlot.OnEquipmentExecuted += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchLdcI4(8));
                    c.Remove();
                    c.Emit(OpCodes.Ldc_R4, Reference.WarHornDuration.Value);
                    c.Emit(OpCodes.Conv_I4);
                    c.GotoNext(x => x.MatchLdcI4(4));
                    c.Remove();
                    c.Emit(OpCodes.Ldc_R4, Reference.WarHornDurationStack.Value);
                    c.Emit(OpCodes.Conv_I4);
                };
                rules.Add("ITEM_ENERGIZEDONEQUIPMENTUSE_DESC", (orig, self, token) =>
                {
                    string ret = "Activating your Equipment gives you <style=cIsDamage>+" + (Reference.WarHornSpeed.Value * 100) + "% attack speed</style>";
                    if (Reference.WarHornSpeedStack.Value != 0) ret += " <style=cStack>(+" + (Reference.WarHornSpeedStack.Value * 100) + "% per stack)</style>";
                    ret += " for <style=cIsDamage>" + Reference.WarHornDuration.Value + "s</style>";
                    if (Reference.WarHornDurationStack.Value != 0) ret += " <style=cStack>(+" + Reference.WarHornDurationStack.Value + "s per stack)</style>";
                    return ret + ".";
                });
            }
            // legendary
            if (Reference.DiscProc.Value != 1f) {
                FireMainBeamState.mainBeamProcCoefficient *= Reference.DiscProc.Value;
                LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/LaserTurbineBomb").GetComponent<ProjectileController>().procCoefficient *= Reference.DiscProc.Value;
                LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/LaserTurbineBomb").GetComponent<ProjectileImpactExplosion>().blastProcCoefficient *= Reference.DiscProc.Value;
            }
            if (Reference.DaggerProc.Value != 1f) LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/DaggerProjectile").GetComponent<ProjectileController>().procCoefficient = Reference.DaggerProc.Value;
            if (Reference.AegisBarrierHeal.Value)
            {
                On.RoR2.HealthComponent.AddBarrier += (orig, self, amt) =>
                {
                    orig(self, amt);
                    if (self.body.inventory.GetItemCount(RoR2Content.Items.BarrierOnOverHeal) > 0 && self.health < self.fullHealth)
                        self.Heal(Mathf.Min(self.fullHealth - self.health, amt), new ProcChainMask());
                };
                rules.Add("ITEM_BARRIERONOVERHEAL_PICKUP", (orig, self, token) =>
                {
                    return orig(self, token) + " " + orig(self, "RISKYMONKEY_ITEM_BARRIERONOVERHEAL_PICKUP");
                });
                rules.Add("ITEM_BARRIERONOVERHEAL_DESC", (orig, self, token) =>
                {
                    return orig(self, token) + " " + orig(self, "RISKYMONKEY_ITEM_BARRIERONOVERHEAL_DESC");
                });
            }
            if (Reference.NkuhanaDamage.Value != 2.5f)
            {
                IL.RoR2.HealthComponent.ServerFixedUpdate += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchStloc(7));
                    c.Prev.Operand = Reference.NkuhanaDamage.Value;
                };
                rules.Add("ITEM_NOVAONHEAL_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_NOVAONHEAL_DESC").Replace("{0}", (Reference.NkuhanaDamage.Value * 100).ToString());
                });
            }
            // boss
            if (Reference.Perf1Proc.Value != 0.7f) LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/FireMeatBall").GetComponent<ProjectileImpactExplosion>().blastProcCoefficient = Reference.Perf1Proc.Value;
            if (Reference.Perf2Proc.Value != 1f) IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdcR4(1), x => x.MatchStfld<GenericDamageOrb>("procCoefficient"));
                c.Next.Operand = Reference.Perf2Proc.Value;
            };
            if (Reference.DiscipleProc.Value != 1f) On.RoR2.Orbs.DevilOrb.Begin += (orig, self) =>
            {
                orig(self);
                self.procCoefficient = Reference.DiscipleProc.Value;
            };
            if (Reference.GloopProc.Value != 1f) EntityStates.VagrantNovaItem.DetonateState.blastProcCoefficient = Reference.GloopProc.Value;
            // void
            if (Reference.SingularityBandDamage.Value != 1 || Reference.SingularityBandStack.Value != 1)
            {
                IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchLdcR4(1), x => x.MatchLdloc(97));
                    c.Remove(); c.Emit(OpCodes.Ldloc_1);
                    c.EmitDelegate<Func<CharacterBody, float>>(body =>
                    {
                        if (body.inventory == null) return 1;
                        int count = body.inventory.GetItemCount(DLC1Content.Items.ElementalRingVoid);
                        if (count > 0) return Reference.SingularityBandDamage.Value + ((count - 1) * Reference.SingularityBandStack.Value);
                        return 1;
                    });
                };
                rules.Add("ITEM_ELEMENTALRINGVOID_DESC", (orig, self, token) =>
                {
                    string ret = "Hits that deal <style=cIsDamage>more than 400% damage</style> also fire a black hole that <style=cIsUtility>draws enemies within 15m into its center</style>. Lasts <style=cIsUtility>5</style> seconds before collapsing, dealing <style=cIsDamage>" + (Reference.SingularityBandDamage.Value * 100) + "%</style>";
                    if (Reference.SingularityBandStack.Value != 0) ret += " <style=cStack>(+" + (Reference.SingularityBandStack.Value * 100) + "% per stack)</style>";
                    ret += " TOTAL damage. Recharges every <style=cIsUtility>20</style> seconds. <style=cIsVoid>Corrupts all Runald's and Kjaro's Bands</style>.";
                    return ret;
                });
            }
            // lunar
            if (Reference.TransHealth.Value != 0.25f)
            {
                RecalculateStatsAPI.GetStatCoefficients += (self, args) =>
                {
                    if (self == null || self.inventory == null) return;
                    int stack = self.inventory.GetItemCount(RoR2Content.Items.ShieldOnly);
                    if (stack > 0) args.healthMultAdd += (stack - 1 + (Reference.TransHealth.Value * 4)) / (1 + stack);
                };
                rules.Add("ITEM_SHIELDONLY_DESC", (orig, self, token) =>
                {
                    string ret = orig(self, "RISKYMONKEY_ITEM_SHIELDONLY_DESC").Replace("{0}", (Reference.TransHealth.Value * 100).ToString());
                    if (Reference.Mods("com.TPDespair.ZetAspects")) ret += " At least <style=cIsHealing>{0}%</style> of <style=cIsHealing>health regeneration</style> applies to <style=cIsHealing>shield</style>.".Replace("{0}", TranscendenceRegen().ToString());
                    return ret + ".";
                });
            }
            if (Reference.HooksDamage.Value != 8.75f)
            {
                // LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/LunarSecondaryProjectile")
                On.EntityStates.Mage.Weapon.BaseThrowBombState.OnEnter += (orig, self) =>
                {
                    if (self is EntityStates.GlobalSkills.LunarNeedle.ThrowLunarSecondary)
                    {
                        self.minDamageCoefficient = Reference.HooksDamage.Value;
                        self.maxDamageCoefficient = Reference.HooksDamage.Value;
                    }
                    orig(self);
                };
            }
            rules.Add("ITEM_LUNARSECONDARYREPLACEMENT_DESC", (orig, self, token) =>
            {
                return orig(self, "RISKYMONKEY_ITEM_LUNARSECONDARYREPLACEMENT_DESC").Replace("{0}", (Reference.HooksDamage.Value * 125).ToString()).Replace("{1}", (Reference.HooksDamage.Value * 100).ToString()) + ".";
            });
            if (Reference.EssenceDamage.Value != 3f || Reference.EssenceDamageStack.Value != 1.2f)
            {
                EntityStates.GlobalSkills.LunarDetonator.Detonate.baseDamageCoefficient = Reference.EssenceDamage.Value;
                EntityStates.GlobalSkills.LunarDetonator.Detonate.damageCoefficientPerStack = Reference.EssenceDamageStack.Value;
                rules.Add("ITEM_LUNARSPECIALREPLACEMENT_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_LUNARSPECIALREPLACEMENT_DESC").Replace("{0}", (Reference.EssenceDamage.Value * 100).ToString()).Replace("{1}", (Reference.EssenceDamageStack.Value * 100).ToString()) + ".";
                });
            }
            if (Reference.EgoSpeed.Value != 0)
            {
                RecalculateStatsAPI.GetStatCoefficients += (self, args) =>
                {
                    if (self == null || self.inventory == null) return;
                    int stack = self.inventory.GetItemCount(DLC1Content.Items.LunarSun);
                    if (stack > 0) args.moveSpeedMultAdd += Reference.EgoSpeed.Value * stack;
                };
                rules.Add("ITEM_LUNARSUN_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_ITEM_LUNARSUN_DESC").Replace("{0}", (Reference.EgoSpeed.Value * 100).ToString());
                });
            }
            // equipment
            if (Reference.WoodspriteCooldown.Value != 15) LegacyResourcesAPI.Load<EquipmentDef>("EquipmentDefs/PassiveHealing").cooldown = Reference.WoodspriteCooldown.Value;
            if (Reference.ForgiveMeCooldown.Value != 45) LegacyResourcesAPI.Load<EquipmentDef>("EquipmentDefs/DeathProjectile").cooldown = Reference.ForgiveMeCooldown.Value;
            if (Reference.VaseCooldown.Value != 45) LegacyResourcesAPI.Load<EquipmentDef>("EquipmentDefs/Gateway").cooldown = Reference.VaseCooldown.Value;
            if (Reference.BlastShowerCooldown.Value != 20) LegacyResourcesAPI.Load<EquipmentDef>("EquipmentDefs/Cleanse").cooldown = Reference.BlastShowerCooldown.Value;
            if (Reference.RecyclerCooldown.Value != 45) LegacyResourcesAPI.Load<EquipmentDef>("EquipmentDefs/Recycle").cooldown = Reference.RecyclerCooldown.Value;
            if (Reference.BackupCooldown.Value != 100) LegacyResourcesAPI.Load<EquipmentDef>("EquipmentDefs/DroneBackup").cooldown = Reference.BackupCooldown.Value;
            On.RoR2.JetpackController.Start += (orig, self) =>
            {
                self.boostCooldown = Reference.ChrysalisBoostCooldown.Value;
                self.boostSpeedMultiplier = Reference.ChrysalisBoost.Value;
                orig(self);
            };
            if (Reference.ChrysalisSpeed.Value != 0) RecalculateStatsAPI.GetStatCoefficients += (self, args) =>
            {
                if (self == null || self.inventory == null || self.equipmentSlot == null) return;
                JetpackController controller = JetpackController.FindJetpackController(self.equipmentSlot.gameObject);
                if (controller != null && controller.providingFlight) args.moveSpeedMultAdd += Reference.ChrysalisSpeed.Value;
            };
            if (Reference.ChrysalisSpeed.Value != 0.2f) rules.Add("EQUIPMENT_JETPACK_DESC", (orig, self, token) =>
            {
                string ret = orig(self, "RISKYMONKEY_EQUIPMENT_JETPACK_DESC");
                if (Reference.ChrysalisSpeed.Value != 0) ret += " Gain <style=cIsUtility>+" + (Reference.ChrysalisSpeed.Value * 100).ToString() + "% movement speed</style> for the duration.";
                return ret;
            });
            if (Reference.CrowdfunderDamage.Value != 1) EntityStates.GoldGat.GoldGatFire.damageCoefficient = Reference.CrowdfunderDamage.Value;
            if (Reference.CrowdfunderChestCost.Value != 0) IL.EntityStates.GoldGat.GoldGatFire.FireBullet += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchConvI4(), x => x.MatchStloc(2));
                c.Emit(OpCodes.Pop);
                c.EmitDelegate(() =>
                {
                    return Mathf.Pow(Run.instance.compensatedDifficultyCoefficient, 1.25f) * 25 * Reference.CrowdfunderChestCost.Value;
                });
            };
            if (Reference.CrowdfunderDamage.Value != 1 || Reference.CrowdfunderChestCost.Value != 0) rules.Add("EQUIPMENT_GOLDGAT_DESC", (orig, self, token) =>
            {
                return orig(self, "RISKYMONKEY_EQUIPMENT_GOLDGAT_DESC").Replace("{0}", (Reference.CrowdfunderDamage.Value * 100).ToString()).Replace("{1}", (Reference.CrowdfunderChestCost.Value * 25).ToString());
            });
            if (Reference.VolcanicEggPassive.Value != 5 || Reference.VolcanicEggDamage.Value != 8 || Reference.VolcanicEggArea.Value != 8)
            {
                IL.RoR2.EquipmentSlot.FireFireBallDash += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchStloc(1));
                    c.EmitDelegate<Func<GameObject, GameObject>>(ball =>
                    {
                        ball.GetComponent<FireballVehicle>().overlapDamageCoefficient = Reference.VolcanicEggPassive.Value;
                        ball.GetComponent<FireballVehicle>().blastDamageCoefficient = Reference.VolcanicEggDamage.Value;
                        ball.GetComponent<FireballVehicle>().blastRadius = Reference.VolcanicEggArea.Value;
                        return ball;
                    });
                };
                rules.Add("EQUIPMENT_FIREBALLDASH_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_EQUIPMENT_FIREBALLDASH_DESC").Replace("{0}", (Reference.VolcanicEggPassive.Value * 100).ToString()).Replace("{1}", (Reference.VolcanicEggDamage.Value * 100).ToString());
                });
            }
            if (Reference.CapacitorCooldown.Value != 20) LegacyResourcesAPI.Load<EquipmentDef>("EquipmentDefs/Lightning").cooldown = Reference.CapacitorCooldown.Value;
            if (Reference.CapacitorDamage.Value != 30)
            {
                IL.RoR2.EquipmentSlot.FireLightning += (il) =>
                {
                    ILCursor c = new(il);
                    c.GotoNext(x => x.MatchLdcR4(30));
                    c.Next.Operand = Reference.CapacitorDamage.Value;
                };
                rules.Add("EQUIPMENT_LIGHTNING_DESC", (orig, self, token) =>
                {
                    return orig(self, "RISKYMONKEY_EQUIPMENT_LIGHTNING_DESC").Replace("{0}", (Reference.CapacitorDamage.Value * 100).ToString());
                });
            }
            if (Reference.CapacitorArea.Value != 3) IL.RoR2.Orbs.LightningStrikeOrb.OnArrival += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdcR4(3));
                c.Next.Operand = Reference.CapacitorArea.Value;
            };
            // end of vanilla stuff
            if (Reference.Mods("Quickstraw.StormyItems")) PatchStormy();
            if (Reference.Mods("com.groovesalad.GrooveSaladSpikestripContent")) PatchSpikestrip();
            On.RoR2.Language.GetLocalizedStringByToken += (orig, self, token) =>
            {
                if (rules.ContainsKey(token)) return rules[token](orig, self, token);
                return orig(self, token);
            };
        }

        public static float TranscendenceRegen()
        {
            return TPDespair.ZetAspects.Configuration.TranscendenceRegen.Value * 100;
        }

        public static void PatchStormy()
        {
            if (Reference.HaloSpeed.Value != 0.25f) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchHaloSpeed));
            if (Reference.HaloSkill.Value != 0) On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
            {
                orig(self);
                if (self == null || self.inventory == null) return;
                int itemCount = self.inventory.GetItemCount(ItemCatalog.FindItemIndex("ITEM_CRACKED_HALO"));
                if (itemCount > 0 && self.characterMotor != null && !self.characterMotor.isGrounded && self.skillLocator != null)
                {
                    float scale = Mathf.Max(0, 1 - (Reference.HaloSkill.Value * itemCount));
                    if (self.skillLocator.primary != null) self.skillLocator.primary.cooldownScale *= scale;
                    if (self.skillLocator.secondary != null) self.skillLocator.secondary.cooldownScale *= scale;
                    if (self.skillLocator.utility != null) self.skillLocator.utility.cooldownScale *= scale;
                    if (self.skillLocator.special != null) self.skillLocator.special.cooldownScale *= scale;
                }
            };
            if (Reference.HaloSpeed.Value != 0.25f || Reference.HaloSkill.Value != 0)
            {
                string str = "Being in the air increases your movement speed by <style=cIsUtility>{0}%</style> <style=cStack>(+{0}% per stack)</style>".Replace("{0}", (Reference.HaloSpeed.Value * 100).ToString());
                if (Reference.HaloSkill.Value > 0) str += ". Reduce skill cooldown by <style=cIsUtility>" + (Reference.HaloSkill.Value * 100) + "%</style> <style=cStack>(+" + (Reference.HaloSkill.Value * 100).ToString() + "% per stack)</style>";
                LanguageAPI.AddOverlay("ITEM_CRACKED_HALO_DESCRIPTION", str + ".");
            }
            if (Reference.UrchinShield.Value != 0.1f) 
            {
                RecalculateStatsAPI.GetStatCoefficients += (self, args) =>
                {
                    if (self == null || self.inventory == null) return;
                    if (self.inventory.GetItemCount(ItemCatalog.FindItemIndex("ITEM_CHARGED_URCHIN")) > 0) args.baseShieldAdd += self.maxHealth * (Reference.UrchinShield.Value - 0.1f);
                };
                LanguageAPI.AddOverlay("ITEM_CHARGED_URCHIN_DESCRIPTION", "Gain a <style=cIsHealth>shield</style> equal to <style=cIsHealth>{0}%</style> of your maximum health. While you have a shield, taking damage <style=cIsDamage>shocks</style> enemies for <style=cIsDamage>80%</style> <style=cStack>(+80% per stack)</style> damage.".Replace("{0}", (Reference.UrchinShield.Value * 100).ToString()));
            }
        }

        [HarmonyPatch(typeof(StormyItems.Items.CrackedHalo), nameof(StormyItems.Items.CrackedHalo.OnGetStatCoefficients))]
        public class PatchHaloSpeed
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdcR4(0.25f));
                c.Next.Operand = Reference.HaloSpeed.Value;
            }
        }

        public static void PatchSpikestrip()
        {
            if (Reference.QuartzRabbitCooldown.Value != 100) ItemAPI.EquipmentDefinitions.ToList().Find(x => x.EquipmentDef.name == "EQUIPMENT_DOUBLEITEMS").EquipmentDef.cooldown = Reference.QuartzRabbitCooldown.Value;
        }

        // literally stole from borbo :scream
        public static void BuffPickupRange(string source)
        {
            GravitatePickup componentInChildren = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/" + source).GetComponentInChildren<GravitatePickup>();
            if (componentInChildren != null)
            {
                Collider component = componentInChildren.GetComponent<Collider>();
                if (!component.isTrigger) return;
                component.transform.localScale *= Reference.DropletPickupRadius.Value;
            }
        }

        public static void BuffMeat()
        {
            LostInTransit.Items.MeatNugget.MeatNuggetPickup.GetComponentInChildren<GravitatePickup>().GetComponent<Collider>().transform.localScale *= Reference.DropletPickupRadius.Value;
        }

        public static void BuffMysticsPickup()
        {
            if (MysticsItems.Items.StarBook.starPrefab != null) MysticsItems.Items.StarBook.starPrefab.GetComponentInChildren<GravitatePickup>().GetComponent<Collider>().transform.localScale *= Reference.DropletPickupRadius.Value;
            if (MysticsItems.Items.ExplosivePickups.gunpowderPickup != null) MysticsItems.Items.ExplosivePickups.gunpowderPickup.transform.Find("GravitationController").GetComponent<Collider>().transform.localScale *= Reference.DropletPickupRadius.Value;
        }
    }
}

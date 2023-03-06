using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static Hex3Mod.Items.Tickets;

namespace RiskyMonkeyBase.Contents
{
    public class MoreTickets
    {
        public static void PatchPotential()
        {
            On.RoR2.PurchaseInteraction.OnInteractionBegin += (orig, self, activator) =>
            {
                orig(self, activator);
                if (activator.TryGetComponent(out CharacterBody body) && body.inventory && body.inventory.GetItemCount(itemDef) > 0 && self.name.Contains("VoidTriple"))
                {
                    if (!body.GetComponent<TicketsBehavior>()) body.AddItemBehavior<TicketsBehavior>(1);
                    body.GetComponent<TicketsBehavior>().interaction = self;
                }
            };
            IL.RoR2.OptionChestBehavior.ItemDrop += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchCallOrCallvirt<PickupDropletController>(nameof(PickupDropletController.CreatePickupDroplet)));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Action<OptionChestBehavior>>((self) =>
                {
                    foreach (var behavior in UnityEngine.Object.FindObjectsOfType<TicketsBehavior>()) if (behavior != null && behavior.interaction != null && behavior.interaction == self.GetComponent<PurchaseInteraction>() && self.name.Contains("VoidTriple"))
                    {
                        behavior.item = itemDef;
                        behavior.consumedItem = consumedItemDef;
                        behavior.interaction = null;
                        behavior.ExchangeTickets();
                        PickupDropletController.CreatePickupDroplet(new GenericPickupController.CreatePickupInfo()
                        {
                            pickerOptions = PickupPickerController.GenerateOptionsFromArray(self.dropTable.GenerateUniqueDrops(self.numOptions, self.rng)),
                            prefabOverride = self.pickupPrefab,
                            position = self.dropTransform.position,
                            rotation = Quaternion.identity,
                            pickupIndex = PickupCatalog.FindPickupIndex(self.displayTier)
                        }, self.dropTransform.position, Vector3.up * self.dropUpVelocityStrength + self.dropTransform.forward * self.dropForwardVelocityStrength);
                    }
                });
            };
        }

        public static void PatchPool()
        {
            On.RoR2.PurchaseInteraction.OnInteractionBegin += (orig, self, activator) =>
            {
                orig(self, activator);
                if (activator.TryGetComponent(out CharacterBody body) && body.inventory && body.inventory.GetItemCount(itemDef) > 0 && self.name.Contains("ShrineCleanse"))
                {
                    if (!body.GetComponent<TicketsBehavior>()) body.AddItemBehavior<TicketsBehavior>(1);
                    body.GetComponent<TicketsBehavior>().interaction = self;
                }
            };
            IL.RoR2.ShopTerminalBehavior.DropPickup += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchCallOrCallvirt<PickupDropletController>(nameof(PickupDropletController.CreatePickupDroplet)));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Action<ShopTerminalBehavior>>((self) =>
                {
                    foreach (var behavior in UnityEngine.Object.FindObjectsOfType<TicketsBehavior>()) if (behavior != null && behavior.interaction != null && behavior.interaction == self.GetComponent<PurchaseInteraction>() && self.gameObject.name.Contains("ShrineCleanse"))
                    {
                        behavior.item = itemDef;
                        behavior.consumedItem = consumedItemDef;
                        behavior.interaction = null;
                        behavior.ExchangeTickets();
                        PickupDropletController.CreatePickupDroplet(self.pickupIndex, (self.dropTransform ?? self.transform).position, self.transform.TransformVector(self.dropVelocity));
                    }
                });
            };
        }

        public static void PatchShrine()
        {
            On.RoR2.PurchaseInteraction.OnInteractionBegin += (orig, self, activator) =>
            {
                orig(self, activator);
                if (activator.TryGetComponent(out CharacterBody body) && body.inventory && body.inventory.GetItemCount(itemDef) > 0 && self.name.Contains("ShrineChance"))
                {
                    if (!body.GetComponent<TicketsBehavior>()) body.AddItemBehavior<TicketsBehavior>(1);
                    body.GetComponent<TicketsBehavior>().interaction = self;
                }
            };
            IL.RoR2.ShrineChanceBehavior.AddShrineStack += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchCallOrCallvirt<PickupDropletController>(nameof(PickupDropletController.CreatePickupDroplet)));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldloc_0);
                c.EmitDelegate(TryTicketShrine);
            };
            if (Reference.Mods("Xatha.SoCRebalancePlugin")) RiskyMonkeyBase.Harmony.PatchAll(typeof(PatchSoCRebalance));
        }

        [HarmonyPatch(typeof(SoCRebalancePlugin.SoCRebalancePlugin), nameof(SoCRebalancePlugin.SoCRebalancePlugin.ShrineChanceBehaviour_AddShrineStack))]
        public class PatchSoCRebalance
        {
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel)
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchCallOrCallvirt<PickupDropletController>(nameof(PickupDropletController.CreatePickupDroplet)));
                c.Index++;
                c.Emit(OpCodes.Ldarg_2);
                c.Emit(OpCodes.Ldloc, 10);
                c.EmitDelegate<Action<ShrineChanceBehavior, PickupIndex>>(TryTicketShrine);
            }
        }

        public static void TryTicketShrine(ShrineChanceBehavior self, PickupIndex pickupIndex)
        {
            foreach (var behavior in UnityEngine.Object.FindObjectsOfType<TicketsBehavior>()) if (behavior != null && behavior.interaction != null && behavior.interaction == self.GetComponent<PurchaseInteraction>())
            {
                PickupIndex[] array = Enumerable.Where(PickupTransmutationManager.GetAvailableGroupFromPickupIndex(pickupIndex), x => x != pickupIndex).ToArray();
                if (array == null || array.Length == 0) return;
                behavior.item = itemDef;
                behavior.consumedItem = consumedItemDef;
                behavior.interaction = null;
                behavior.ExchangeTickets();
                PickupDropletController.CreatePickupDroplet(Run.instance.treasureRng.NextElementUniform(array), self.dropletOrigin.position, self.dropletOrigin.forward * 20f);
            }
        }

        public static void PatchLocked()
        {
            On.RoR2.PurchaseInteraction.OnInteractionBegin += (orig, self, activator) =>
            {
                orig(self, activator);
                if (activator.TryGetComponent(out CharacterBody body) && body.inventory && body.inventory.GetItemCount(itemDef) > 0 && self.name.Contains("Lockbox"))
                {
                    if (!body.GetComponent<TicketsBehavior>()) body.AddItemBehavior<TicketsBehavior>(1);
                    body.GetComponent<TicketsBehavior>().interaction = self;
                }
            };
            IL.RoR2.OptionChestBehavior.ItemDrop += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchCallOrCallvirt<PickupDropletController>(nameof(PickupDropletController.CreatePickupDroplet)));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Action<OptionChestBehavior>>((self) =>
                {
                    foreach (var behavior in UnityEngine.Object.FindObjectsOfType<TicketsBehavior>()) if (behavior != null && behavior.interaction != null && behavior.interaction == self.GetComponent<PurchaseInteraction>() && self.name.Contains("LockboxVoid"))
                    {
                        behavior.item = itemDef;
                        behavior.consumedItem = consumedItemDef;
                        behavior.interaction = null;
                        behavior.ExchangeTickets();
                        PickupDropletController.CreatePickupDroplet(new GenericPickupController.CreatePickupInfo()
                        {
                            pickerOptions = PickupPickerController.GenerateOptionsFromArray(self.dropTable.GenerateUniqueDrops(self.numOptions, self.rng)),
                            prefabOverride = self.pickupPrefab,
                            position = self.dropTransform.position,
                            rotation = Quaternion.identity,
                            pickupIndex = PickupCatalog.FindPickupIndex(self.displayTier)
                        }, self.dropTransform.position, Vector3.up * self.dropUpVelocityStrength + self.dropTransform.forward * self.dropForwardVelocityStrength);
                    }
                });
            };
            IL.RoR2.ChestBehavior.ItemDrop += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchCallOrCallvirt<PickupDropletController>(nameof(PickupDropletController.CreatePickupDroplet)));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Action<ChestBehavior>>((self) =>
                {
                    foreach (var behavior in UnityEngine.Object.FindObjectsOfType<TicketsBehavior>()) if (behavior != null && behavior.interaction != null && behavior.interaction == self.GetComponent<PurchaseInteraction>() && self.name.Contains("Lockbox"))
                    {
                        behavior.item = itemDef;
                        behavior.consumedItem = consumedItemDef;
                        behavior.interaction = null;
                        behavior.ExchangeTickets();
                        PickupDropletController.CreatePickupDroplet(self.dropTable.GenerateDrop(self.rng), self.dropTransform.position + Vector3.up * 1.5f, Vector3.up * self.dropUpVelocityStrength + self.dropTransform.forward * self.dropForwardVelocityStrength);
                    }
                });
            };
        }
    }
}

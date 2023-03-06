using BepInEx.Configuration;
using RiskyMonkeyBase.Tweaks;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using static RoR2.PingerController;

namespace RiskyMonkeyBase.Tutorials
{
    public class TutorialSeen
    {
        public static void Patch()
        {
            On.RoR2.PingerController.OnSyncCurrentPing += (orig, self, newPingInfo) =>
            {
                orig(self, newPingInfo);
                // also maybe do smth for tablets
                string[] scene = new string[] { "golemplains2", "blackbeach", "snowyforest", "foggyswamp", "goolake", "FBLScene", "frozenwall", "wispgraveyard", "shipgraveyard", "arena", "dampcavesimple", "rootjungle", "skymeadow", "slumberingsatellite", "artifactworld", "bazaar", "forgottenhaven", "goldshores", "moon2", "voidraid", "voidstage", "mysteryspace", "BulwarksHaunt_GhostWave", "itmoon" };
                Vector3[] pos = new Vector3[] { new Vector3(-14.032445f, 59.56197f, 569.5622f), new Vector3(83.161835f, -100.376f, -32.45949f), new Vector3(220.91655f, 70.28304f, -261.43035f), new Vector3(167.91045f, 65.02085f, 266.70285f), new Vector3(56.296895f, -212.7365f, -179.3394f), new Vector3(-87.87186f, -174.22905f, -414.34755f), new Vector3(74.25433f, -76.92122f, -186.29525f), new Vector3(-25.054475f, 205.40805f, 528.1014f), new Vector3(-782.29475f, 29.520135f, 145.8164f), new Vector3(-415.32515f, 146.3342f, 185.1537f), new Vector3(68.06669f, 20.061355f, -156.04855f), new Vector3(91.22834f, 2.4940275f, 232.7996f), new Vector3(-93.55012f, -54.278035f, -129.51055f), new Vector3(-28.624875f, -23.479195f, 85.733655f), new Vector3(-122.65935f, -103.48755f, -236.6111f), new Vector3(-27.81225f, -47.540095f, 43.497655f), new Vector3(13.181515f, 86.85408f, 271.0321f), new Vector3(-128.13835f, -20.54223f, -52.843725f), new Vector3(159.73835f, 6.9063085f, 211.47225f), new Vector3(-37.810325f, -12.45223f, -65.75679f), new Vector3(-617.293f, -5.7893735f, -0.77086f), new Vector3(70.2657f, 15.90943f, -4087.9405f), new Vector3(79.156895f, -44.768835f, -57.43729f), new Vector3(373.20385f, -152.463f, 215.4269f), new Vector3(-12.962905f, 70.89496f, 124.3398f), new Vector3(7.043066f, -57.9691f, -3.4158f) };
                float[] radius = new float[] { 0.8788758008f, 6.201456859f, 44.46078282f, 49.95978365f, 4.963403463f, 5.768813397f, 12.70869243f, 37.05681462f, 11.58983935f, 22.84601126f, 7.082158281f, 6.532256176f, 6.566980653f, 8.982244098f, 7.14822476f, 17.53991801f, 28.39404245f, 4.384242254f, 6.685704262f, 8.171190281f, 29.02755259f, 8.381215094f, 10.47946421f, 6.341096193f, 4.48672252f, 68.55571185f };
                for (var i = 0; i < scene.Length; i++) if (Stage.instance.name == scene[i] && (newPingInfo.origin - pos[i]).magnitude <= radius[i]) TutorialHelper.Tutorial(Reference.TutorialTablet, "tablet");
                if (Stage.instance.name == "dampcavesimple" && (Reference.Mods("com.rob.Direseeker") || Reference.Mods("com.rob.DiggerUnearthed")))
                { // button handling here using newPingInfo.origin
                    foreach (var pos2 in new Vector3[] { new Vector3(2f, -140.5f, -439f), new Vector3(110f, -179.2f, -150f), new Vector3(203f, -75f, -195.5f), new Vector3(51f, -86f, -200.6f), new Vector3(-154f, -153f, -103.6f), new Vector3(-0.5f, -135.5f, -12.5f), new Vector3(-37.5f, -127f, -287.8f) }) 
                        if ((newPingInfo.origin - pos2).magnitude <= 3) TutorialHelper.Tutorial(Reference.TutorialButton, "button");
                }
                if (newPingInfo.targetGameObject == null) return;
                if (newPingInfo.targetGameObject.name.StartsWith("GenericPickup")) // item
                {
                    GenericPickupController[] pickups = Object.FindObjectsOfType<GenericPickupController>();
                    foreach (GenericPickupController pickupController in pickups)
                    {
                        if (pickupController.gameObject != newPingInfo.targetGameObject) continue;
                        PickupDef pickup = PickupCatalog.GetPickupDef(pickupController.pickupIndex);
                        if (pickup.itemIndex != ItemIndex.None)
                        {
                            switch (pickup.itemTier)
                            {
                                case ItemTier.Tier1:
                                case ItemTier.Tier2:
                                case ItemTier.Tier3:
                                case ItemTier.Boss:
                                    TutorialHelper.Tutorial(Reference.TutorialRarity, "rarity");
                                    break;
                                case ItemTier.VoidTier1:
                                case ItemTier.VoidTier2:
                                case ItemTier.VoidTier3:
                                case ItemTier.VoidBoss:
                                    TutorialHelper.Tutorial(Reference.TutorialVoidItem, "voiditem");
                                    break;
                                case ItemTier.Lunar:
                                    TutorialHelper.Tutorial(Reference.TutorialLunarItem, "lunaritem");
                                    break;
                            }
                            if (Reference.Mods("bubbet.bubbetsitems") && VoidLunarTweaks.isVoidLunar(pickup.itemTier))
                            {
                                TutorialHelper.Tutorial(Reference.TutorialLunarItem, "lunaritem");
                                TutorialHelper.Tutorial(Reference.TutorialVoidItem, "voiditem");
                                TutorialHelper.Tutorial(Reference.TutorialVoidLunarItem, "voidlunaritem");
                            }
                        }
                        else if (pickup.equipmentIndex != EquipmentIndex.None)
                        {
                            if (pickup.isLunar) TutorialHelper.Tutorial(Reference.TutorialLunarItem, "lunaritem");
                            TutorialHelper.Tutorial(Reference.TutorialEquipment, "equipment");
                        }
                        else if (pickup.internalName.StartsWith("LunarCoin")) TutorialHelper.Tutorial(Reference.TutorialLunarCoin, "lunarcoin");
                    }
                }
                checkSeen(newPingInfo, "Chest", Reference.TutorialRarity, "rarity"); // no need to check for lunar btw since e0 is already lunar & you wouldve seen lunar tuto then
                checkSeen(newPingInfo, "VoidTriple", Reference.TutorialRarity, "rarity");
                checkSeen(newPingInfo, "Duplicator", Reference.TutorialRarity, "rarity");
                checkSeen(newPingInfo, "MultiShopTerminal", Reference.TutorialRarity, "rarity");
                checkSeen(newPingInfo, "MultiShopLargeTerminal", Reference.TutorialRarity, "rarity");
                checkSeen(newPingInfo, "Cauldron", Reference.TutorialRarity, "rarity");

                checkSeen(newPingInfo, "EquipmentBarrel", Reference.TutorialEquipment, "equipment");
                checkSeen(newPingInfo, "MultiShopEquipmentTerminal", Reference.TutorialEquipment, "equipment");
                checkSeen(newPingInfo, "TimedChest", Reference.TutorialEquipment, "equipment");

                checkSeen(newPingInfo, "Duplicator", Reference.TutorialPrinter, "printer");
                checkSeen(newPingInfo, "Scrapper", Reference.TutorialScrapper, "scrapper");
                checkSeen(newPingInfo, "MultiShop", Reference.TutorialShop, "shop");
                checkSeen(newPingInfo, "CasinoChest", Reference.TutorialSlot, "slot");
                checkSeen(newPingInfo, "Cauldron", Reference.TutorialCauldron, "cauldron");

                checkSeen(newPingInfo, "Chest2", Reference.TutorialChestVariant, "chestvariant");
                checkSeen(newPingInfo, "GoldChest", Reference.TutorialChestVariant, "chestvariant");
                checkSeen(newPingInfo, "CategoryChest", Reference.TutorialChestThemed, "chestthemed");

                checkSeen(newPingInfo, "VoidChest", Reference.TutorialVoidItem, "voiditem");
                checkSeen(newPingInfo, "LunarChest", Reference.TutorialLunarItem, "lunaritem");
                checkSeen(newPingInfo, "LunarShopTerminal", Reference.TutorialLunarItem, "lunaritem");
                if (newPingInfo.targetGameObject.name.Contains("LunarShopTerminal") && Reference.Mods("bubbet.bubbetsitems")
                    && VoidLunarTweaks.isVoidLunar(PickupCatalog.GetPickupDef(newPingInfo.targetGameObject.GetComponent<ShopTerminalBehavior>().CurrentPickupIndex()).itemTier)) // voidlunar conversions!
                {
                    TutorialHelper.Tutorial(Reference.TutorialVoidItem, "voiditem");
                    TutorialHelper.Tutorial(Reference.TutorialVoidLunarItem, "voidlunaritem");
                }

                checkSeen(newPingInfo, "ShrineChance", Reference.TutorialShrineChance, "shrinechance");
                checkSeen(newPingInfo, "ShrineRestack", Reference.TutorialShrineOrder, "shrineorder");
                checkSeen(newPingInfo, "ShrineCombat", Reference.TutorialShrineCombat, "shrinecombat");
                checkSeen(newPingInfo, "ShrineBlood", Reference.TutorialShrineBlood, "shrineblood");
                checkSeen(newPingInfo, "ShrineHealing", Reference.TutorialShrineHealing, "shrinehealing");
                checkSeen(newPingInfo, "ShrineRepair", Reference.TutorialShrineRepair, "shrinerepair");
                checkSeen(newPingInfo, "VoidShrine", Reference.TutorialShrineVoid, "shrinevoid");

                checkSeen(newPingInfo, "Turret", Reference.TutorialTurret, "turret");
                checkSeen(newPingInfo, "Drone", Reference.TutorialDrone, "drone");
                checkSeen(newPingInfo, "EquipmentDrone", Reference.TutorialEquipmentDrone, "equipmentdrone");
                checkSeen(newPingInfo, "EmergencyDrone", Reference.TutorialEmergencyDrone, "emergencydrone");

                checkSeen(newPingInfo, "ShrineCleanse", Reference.TutorialCleansingPool, "cleansingpool");
                checkSeen(newPingInfo, "ShopkeeperBody", Reference.TutorialNewt, "newt");
                checkSeen(newPingInfo, "SeerStation", Reference.TutorialDream, "dream");

                checkSeen(newPingInfo, "TimedChest", Reference.TutorialTimedChest, "timedchest");
                checkSeen(newPingInfo, "ShrineGoldshoresAccess", Reference.TutorialAltar, "altar");
                checkSeen(newPingInfo, "GLPressurePlate", Reference.TutorialPressurePlate, "pressureplate");
                checkSeen(newPingInfo, "VultureEggBody", Reference.TutorialEgg, "egg");
                checkSeen(newPingInfo, "TreebotUnlockInteractable", Reference.TutorialRex, "rex");
                checkSeen(newPingInfo, "LunarTeleporter", Reference.TutorialPrimordialTeleporter, "primordialteleporter");
                checkSeen(newPingInfo, "PortalDialer", Reference.TutorialTabletInput, "tabletinput");
                checkSeen(newPingInfo, "Obelisk", Reference.TutorialObelisk, "obelisk");
                checkSeen(newPingInfo, "FrogInteractable", Reference.TutorialFrog, "frog");

                if (Reference.Mods("PlasmaCore.ForgottenRelics"))
                {
                    checkSeen(newPingInfo, "RelicOfEnergy", Reference.TutorialRelicEnergy, "relicenergy");
                    checkSeen(newPingInfo, "ShatteredTeleporter", Reference.TutorialShatteredTeleporter, "shatteredteleporter");
                    checkSeen(newPingInfo, "SlumberingPedestal", Reference.TutorialSlumberingAltar, "slumberingaltar");
                }
            };
            On.RoR2.CharacterMasterNotificationQueue.PushNotification += (orig, self, notificationInfo, duration) =>
            {
                orig(self, notificationInfo, duration);
                if (notificationInfo.data is ItemDef itemDef)
                {
                    switch (itemDef.tier)
                    {
                        case ItemTier.Tier1:
                        case ItemTier.Tier2:
                        case ItemTier.Tier3:
                        case ItemTier.Boss:
                            TutorialHelper.Tutorial(Reference.TutorialRarity, "rarity");
                            break;
                        case ItemTier.VoidTier1:
                        case ItemTier.VoidTier2:
                        case ItemTier.VoidTier3:
                        case ItemTier.VoidBoss:
                            TutorialHelper.Tutorial(Reference.TutorialVoidItem, "voiditem");
                            break;
                        case ItemTier.Lunar:
                            TutorialHelper.Tutorial(Reference.TutorialLunarItem, "lunaritem");
                            break;
                    }
                    if (Reference.Mods("bubbet.bubbetsitems") && VoidLunarTweaks.isVoidLunar(itemDef.tier))
                    {
                        TutorialHelper.Tutorial(Reference.TutorialLunarItem, "lunaritem");
                        TutorialHelper.Tutorial(Reference.TutorialVoidItem, "voiditem");
                        TutorialHelper.Tutorial(Reference.TutorialVoidLunarItem, "voidlunaritem");
                    }
                    if (Reference.TutorialStacking.Value) TutorialHelper.Tutorial(Reference.TutorialRebalance, "rebalance");
                    else Reference.TutorialRebalance.Value = true;
                    if (self.master.inventory.GetItemCount(itemDef) >= 2) TutorialHelper.Tutorial(Reference.TutorialStacking, "stacking");
                    if (new List<string>(new string[] { "ItemDefVoidJunkToScrapTier1", "RegeneratingScrap", "RegeneratingScrapConsumed", "ScrapGreen", "ScrapRed", "ScrapWhite", "ScrapYellow" }).Contains(itemDef.name))
                        TutorialHelper.Tutorial(Reference.TutorialScrapper, "scrapper");
                    else if (itemDef.name == "RelicOfEnergy") TutorialHelper.Tutorial(Reference.TutorialRelicEnergy, "relicenergy");
                }
                else if (notificationInfo.data is EquipmentDef equipmentDef)
                {
                    TutorialHelper.Tutorial(Reference.TutorialEquipment, "equipment");
                    if (equipmentDef.isLunar) TutorialHelper.Tutorial(Reference.TutorialLunarItem, "lunaritem");
                }
            };
            On.RoR2.PurchaseInteraction.OnInteractionBegin += (orig, self, activator) =>
            {
                orig(self, activator);
                checkInteracted(self, "Duplicator", Reference.TutorialPrinter, "printer");
                checkInteracted(self, "Scrapper", Reference.TutorialScrapper, "scrapper");
                checkInteracted(self, "MultiShop", Reference.TutorialShop, "shop");
                checkInteracted(self, "CasinoChest", Reference.TutorialSlot, "slot");
                checkInteracted(self, "Cauldron", Reference.TutorialCauldron, "cauldron");
                checkInteracted(self, "Chest2", Reference.TutorialChestVariant, "chestvariant");
                checkInteracted(self, "GoldChest", Reference.TutorialChestVariant, "chestvariant");
                checkInteracted(self, "CategoryChest", Reference.TutorialChestThemed, "chestthemed");
                checkInteracted(self, "ShrineChance", Reference.TutorialShrineChance, "shrinechance");
                checkInteracted(self, "ShrineRestack", Reference.TutorialShrineOrder, "shrineorder");
                checkInteracted(self, "ShrineCombat", Reference.TutorialShrineCombat, "shrinecombat");
                checkInteracted(self, "ShrineBlood", Reference.TutorialShrineBlood, "shrineblood");
                checkInteracted(self, "ShrineHealing", Reference.TutorialShrineHealing, "shrinehealing");
                checkInteracted(self, "ShrineRepair", Reference.TutorialShrineRepair, "shrinerepair");
                checkInteracted(self, "VoidShrine", Reference.TutorialShrineVoid, "shrinevoid");
                checkInteracted(self, "Turret", Reference.TutorialTurret, "turret");
                checkInteracted(self, "Drone", Reference.TutorialDrone, "drone");
                checkInteracted(self, "EquipmentDrone", Reference.TutorialEquipmentDrone, "equipmentdrone");
                checkInteracted(self, "EmergencyDrone", Reference.TutorialEmergencyDrone, "emergencydrone");
                checkInteracted(self, "ShrineCleanse", Reference.TutorialCleansingPool, "cleansingpool");
                checkInteracted(self, "SeerStation", Reference.TutorialDream, "dream");
                checkInteracted(self, "ShrineGoldshoresAccess", Reference.TutorialAltar, "altar");
                checkInteracted(self, "GLPressurePlate", Reference.TutorialPressurePlate, "pressureplate");
                checkInteracted(self, "PortalDialer", Reference.TutorialTabletInput, "tabletinput");
            };
            GlobalEventManager.onCharacterDeathGlobal += (damageReport) =>
            {
                if (damageReport.victimBody.gameObject.name.Contains("Egg")) TutorialHelper.Tutorial(Reference.TutorialEgg, "egg");
            };
            On.RoR2.GenericPickupController.OnInteractionBegin += (orig, self, activator) =>
            {
                orig(self, activator);
                if (PickupCatalog.GetPickupDef(self.pickupIndex).internalName.StartsWith("LunarCoin")) TutorialHelper.Tutorial(Reference.TutorialLunarCoin, "lunarcoin");
            };
        }

        public static void checkSeen(PingInfo info, string name, ConfigEntry<bool> entry, string token)
        {
            if (info.targetGameObject.name.Contains(name)) TutorialHelper.Tutorial(entry, token);
        }
        public static void checkInteracted(PurchaseInteraction self, string name, ConfigEntry<bool> entry, string token)
        {
            if (self.name.Contains(name)) TutorialHelper.Tutorial(entry, token);
        }
    }
}

﻿using BepInEx.Bootstrap;
using BepInEx.Configuration;
using System.IO;

namespace RiskyMonkeyBase
{
    public class Reference
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "prodzpod";
        public const string PluginName = "RiskyMonkeyBase";
        public const string PluginDisplayName = "Radiant Malignance";
        public const string PluginVersion = "0.9.3";
        public static int[] Releases = { 93, 92, 91, 90 }; // prepend new releases
        public static ConfigFile Config;
        public static ConfigEntry<bool> RadiantMalignance;
        public static ConfigEntry<bool> UseFullDescForPickup;
        public static ConfigEntry<bool> FixPaladinAchievementNames;
        public static ConfigEntry<string> GrandDifficulty;
        public static ConfigEntry<bool> EnablePaladinBulwark;
        public static ConfigEntry<string> DifficultiesToRemove;
        public static ConfigEntry<bool> EnableDamageNumbers;
        public static ConfigEntry<bool> TossingAlt;
        public static ConfigEntry<float> SAAltChance;
        public static ConfigEntry<bool> ReviveBetweenMithrixPhase;
        public static ConfigEntry<float> VoidLunarOnBazaar;
        public static ConfigEntry<bool> CleansingPoolVoidLunar;
        public static ConfigEntry<bool> HEADSTChanges;
        public static ConfigEntry<string> RiskOfOptionsHideList;
        public static ConfigEntry<string> SkinsToReorder;
        public static ConfigEntry<string> MemeSkins;
        public static ConfigEntry<string> SkinsToRemove;
        public static ConfigEntry<string> SurvivorsOrder;
        public static ConfigEntry<string> ChallengesToReorder;
        public static ConfigEntry<bool> CustomAchievements;
        public static ConfigEntry<int> FocusedConvergenceRateLimit;
        public static ConfigEntry<int> FocusedConvergenceRangeLimit;
        public static ConfigEntry<float> DownpourScaling;
        public static ConfigEntry<bool> ShowAllArtifacts;
        public static ConfigEntry<int> ArtifactUnlockHint;
        public static ConfigEntry<bool> RFTVDisableVoidCoin;
        public static ConfigEntry<bool> RFTVDisableVoidSuppressor;
        public static ConfigEntry<bool> RFTVDisableItemEnable;
        public static ConfigEntry<bool> RFTVDisableCommandoSkin;
        public static ConfigEntry<bool> RFTVDisableLocusTweaks;
        public static ConfigEntry<bool> ReprogrammerActivate;
        public static ConfigEntry<int> ReprogrammerCooldown;
        public static ConfigEntry<float> ReprogrammerRefresh;
        public static ConfigEntry<bool> ReprogrammerRespectTier;
        public static ConfigEntry<float> ReprogrammerRepairChance;

        public static ConfigEntry<bool> SeriousMode;

        public static ConfigEntry<bool> ResetTutorial;
        public static ConfigEntry<bool> GetChangelog;
        public static ConfigEntry<int> LastVersion;
        public static ConfigEntry<bool> TutorialTeleporter;
        public static ConfigEntry<bool> TutorialTeleporterZone;
        public static ConfigEntry<bool> TutorialTeleporterPost;
        public static ConfigEntry<bool> TutorialPing;
        public static ConfigEntry<bool> TutorialEmote;
        public static ConfigEntry<bool> TutorialRun;
        public static ConfigEntry<bool> TutorialOSP;
        public static ConfigEntry<bool> TutorialPause;
        public static ConfigEntry<bool> TutorialWheel;
        public static ConfigEntry<bool> TutorialInfo;
        public static ConfigEntry<bool> TutorialDrop;
        public static ConfigEntry<bool> TutorialAchievements;
        public static ConfigEntry<bool> TutorialRarity;
        public static ConfigEntry<bool> TutorialRebalance;
        public static ConfigEntry<bool> TutorialStacking;
        public static ConfigEntry<bool> TutorialEquipment;
        public static ConfigEntry<bool> TutorialVoidItem;
        public static ConfigEntry<bool> TutorialLunarItem;
        public static ConfigEntry<bool> TutorialVoidLunarItem;
        public static ConfigEntry<bool> TutorialElite;
        public static ConfigEntry<bool> TutorialLunarCoin;
        public static ConfigEntry<bool> TutorialBazaar;
        public static ConfigEntry<bool> TutorialNewt;
        public static ConfigEntry<bool> TutorialDream;
        public static ConfigEntry<bool> TutorialCleansingPool;
        public static ConfigEntry<bool> TutorialLevel2;
        public static ConfigEntry<bool> TutorialPressurePlate;
        public static ConfigEntry<bool> TutorialTimedChest;
        public static ConfigEntry<bool> TutorialLevel4;
        public static ConfigEntry<bool> TutorialEgg;
        public static ConfigEntry<bool> TutorialButton;
        public static ConfigEntry<bool> TutorialPrimordialTeleporter;
        public static ConfigEntry<bool> TutorialTablet;
        public static ConfigEntry<bool> TutorialTabletInput;
        public static ConfigEntry<bool> TutorialFrog;
        public static ConfigEntry<bool> TutorialLevel6;
        public static ConfigEntry<bool> TutorialObelisk;
        public static ConfigEntry<bool> TutorialRex;
        public static ConfigEntry<bool> TutorialAltar;
        public static ConfigEntry<bool> TutorialRelicEnergy;
        public static ConfigEntry<bool> TutorialShatteredTeleporter;
        public static ConfigEntry<bool> TutorialSlumberingAltar;
        public static ConfigEntry<bool> FirstRun;
        public static ConfigEntry<bool> TutorialLoadout;
        public static ConfigEntry<bool> TutorialDrizzle;
        public static ConfigEntry<bool> TutorialThunderstorm;
        public static ConfigEntry<bool> TutorialChestVariant;
        public static ConfigEntry<bool> TutorialChestThemed;
        public static ConfigEntry<bool> TutorialShop;
        public static ConfigEntry<bool> TutorialSlot;
        public static ConfigEntry<bool> TutorialCauldron;
        public static ConfigEntry<bool> TutorialPrinter;
        public static ConfigEntry<bool> TutorialScrapper;
        public static ConfigEntry<bool> TutorialShrineChance;
        public static ConfigEntry<bool> TutorialShrineCombat;
        public static ConfigEntry<bool> TutorialShrineOrder;
        public static ConfigEntry<bool> TutorialShrineBlood;
        public static ConfigEntry<bool> TutorialShrineHealing;
        public static ConfigEntry<bool> TutorialShrineVoid;
        public static ConfigEntry<bool> TutorialShrineRepair;
        public static ConfigEntry<bool> TutorialVoidField;
        public static ConfigEntry<bool> TutorialDrone;
        public static ConfigEntry<bool> TutorialTurret;
        public static ConfigEntry<bool> TutorialEmergencyDrone;
        public static ConfigEntry<bool> TutorialEquipmentDrone;

        // public static List<ConfigEntry<bool>> allTutorials;
        // public static List<ConfigEntry<bool>> moddedTutorials;

        public static void InitConfig(string configPath)
        {
            Config = new ConfigFile(Path.Combine(configPath, PluginGUID + ".cfg"), true);
            Config.SaveOnConfigSet = true;
            // allTutorials = new();
            // moddedTutorials = new();

            RadiantMalignance = Config.Bind("General", "Modpack Mode", false, "Set to false when used outside RM.");

            UseFullDescForPickup = Config.Bind("General", "Better Descriptions", true, "Makes vanilla items more descriptive.");
            FixPaladinAchievementNames = Config.Bind("General", "Fix Paladin Achievement Names", true, "Puts “Paladin: ” in front of the achievement names.");
            GrandDifficulty = Config.Bind("General", "Grand Mastery Difficulty", "INFERNO_NAME", "Difficulty to replace Typhoon with for achievement description. Accepts nameTokens. HIFU-Inferno by default until Starstorm2 SoTV.");
            EnablePaladinBulwark = Config.Bind("General", "The Grand 3 Mod Cross Compatibility", true, "Paladin, Thy_Providence, BulwarksHaunt.");
            DifficultiesToRemove = Config.Bind("General", "Remove Difficulties", "SUNNY_NAME, CALYPSO_NAME, TEMPEST_NAME, SCYLLA_NAME", "accepts nameTokens, split by commas.");
            EnableDamageNumbers = Config.Bind("General", "Enable Damage Numbers", false, "force enable damage numbers, vanillavoid issue perhaps?");
            TossingAlt = Config.Bind("General", "Artifact of Tossing Scrap Alt", true, "Set to false to scrap with just a right click.");
            SAAltChance = Config.Bind("General", "Stage Aesthetics Alt Chance", 0.25f, "Chance for alternate aesthetics compared to vanilla ones. Will be ignored if vanilla is disabled.");
            ReviveBetweenMithrixPhase = Config.Bind("General", "Revive Between Mithrix Phase", false, "Set to true to revive everyone between Mithrix Phases (except 5) with Artifact of Revive on.");
            VoidLunarOnBazaar = Config.Bind("General", "Void Lunar on Bazaar Chance", 0.33f, "Chance for lunar item to convert into void version in bazaar. set to 0 to disable.");
            CleansingPoolVoidLunar = Config.Bind("General", "Cleansing Pool Void Lunar", true, "Set to false to not accept void lunar at in Cleansing Pools.");
            HEADSTChanges = Config.Bind("General", "H3AD-5T v2 Rebalance", true, "Removes cooldown, and increase damage on stacks. Also affects Empyrean Braces.");
            RiskOfOptionsHideList = Config.Bind("General", "Risk Of Options Hide List", "", "List of categories to hide in Mod Options. separated by commas. See log for list of IDs.");
            SkinsToReorder = Config.Bind("General", "Skin Reorder List", "", "List of skin names to reorder, separated by commas. Ones not on the list will appear at the front.");
            MemeSkins = Config.Bind("General", "Meme Skins", "", "List of skin names to hide when serious mode is off, separated by commas.");
            SkinsToRemove = Config.Bind("General", "Skins Hide List", "", "List of skins to hide, separated by commas.");
            SurvivorsOrder = Config.Bind("General", "Survivor Order List", "Commando - 1, Huntress - 2, Bandit2 - 3, Toolbot - 4, Engi - 5, Mage - 6, Merc - 7, Treebot - 8, Loader - 9, Croco - 10, Captain - 11, Heretic - 13, Railgunner - 14, VoidSurvivor - 15", "List of Survivors to reorder, seperated by commas. Ones not on the list will have its desiredSortPosition unmodified.");
            ChallengesToReorder = Config.Bind("General", "Challenge Reorder List", "", "List of achievement INTERNAL names to reorder, separated by commas. Ones not on the list will have its sortScore unmodified. see logs for list.");
            CustomAchievements = Config.Bind("General", "Enable Custom Achievements", true, "Set to false to unlock modded items/skins/loadout by default.");
            FocusedConvergenceRateLimit = Config.Bind("General", "Focused Convergence Rate Max Stack", -1, "Set to -1 for infinite.");
            FocusedConvergenceRangeLimit = Config.Bind("General", "Focused Convergence Range Max Stack", 3, "Set to -1 for infinite.");
            DownpourScaling = Config.Bind("General", "Downpour Scaling Per Minute", 5f, "in percents. set to 0 to disable.");
            ShowAllArtifacts = Config.Bind("General", "Unlock All Artifacts", false, "Set to true to access all artifacts.");
            ArtifactUnlockHint = Config.Bind("General", "Artifact Unlock Hint", 3, "Displays artifact hints from the wiki in number of lines, 0 to disable, 4 to give direct answers");

            RFTVDisableVoidCoin = Config.Bind("RFTV Modules", "Disable Void Coins", false, "Set to true to disable Void Coins.");
            RFTVDisableVoidSuppressor = Config.Bind("RFTV Modules", "Disable Void Suppressors", false, "Set to true to disable Void Suppressors.");
            RFTVDisableItemEnable = Config.Bind("RFTV Modules", "Disable Item Enable", false, "Set to true to disable Lunar Portal Item.");
            RFTVDisableCommandoSkin = Config.Bind("RFTV Modules", "Disable Commando Skin", false, "Set to true to disable Helot skin.");
            RFTVDisableLocusTweaks = Config.Bind("RFTV Modules", "Disable Void Locus Tweaks", false, "Set to true to disable Void Locus Tweaks.");

            ReprogrammerActivate = Config.Bind("Reprogrammer", "Reprogrammer Activate", true, "Set to false to disable Reprogrammer.");
            ReprogrammerCooldown = Config.Bind("Reprogrammer", "Reprogrammer Cooldown", 60, "Cooldown for Reprogrammer in seconds.");
            ReprogrammerRefresh = Config.Bind("Reprogrammer", "Reprogrammer HUD Refresh", 0.2f, "every N seconds reprogrammer will check for new target. helps with lag but may desync HUD. does not affect gameplay.");
            ReprogrammerRepairChance = Config.Bind("Reprogrammer", "Reprogrammer Shrine Chance", 0.25f, "Chance for Reprogrammer to turn printers into Shrine of Repair. Will be ignored if Shrine of Repair is not installed.");

            /* END USER OPTIONS */ SeriousMode = Config.Bind("Settings", "Serious Mode", true, "Set to false to Monkey.");
            /* END USER OPTIONS */ ResetTutorial = Config.Bind("Settings", "Reset Tutorial", true, "Set to true to reprompt the first tutorial prompt.");
            /* END USER OPTIONS */ GetChangelog = Config.Bind("Settings", "Get Changelog", true, "Set to false to no longer recieve changelog on pack update.");
            LastVersion = Config.Bind("Settings", "Last Version", Releases[0], "For changelog prompt. ignored if getChangelog is false.");
            // main menu

            // first stage encounters
            TutorialTeleporter = RegisterTutorial("Teleporter Tutorial Seen"); // start of first run, objectives, also talk about how enemies will regen forever
            TutorialTeleporterZone = RegisterTutorial("Teleporter Zone Tutorial Seen"); // when teleporter activated, boss and zone, money converts to xp in the end
            /* MODPACK TUTORIAL */ TutorialTeleporterPost = RegisterTutorial("Teleporter Post Tutorial Seen", true); // when boss killed, kill monster to accelerate
            /* MODPACK TUTORIAL */ TutorialPing = RegisterTutorial("Ping Tutorial Seen", true); // start of first run, ping to see info about it! ("seen" is pinged from now on)

            /* MODPACK TUTORIAL */ TutorialEmote = RegisterTutorial("Emote Tutorial Seen", true); // serious mode off (use mod settings)
            TutorialRun = RegisterTutorial("Running Tutorial Seen"); // not pressing run for like 30 seconds (please run)
            TutorialOSP = RegisterTutorial("One Shot Protection Tutorial Seen"); // take fall damage OR trigger OSP (you cant fall and die btw lol)
            /* MODPACK TUTORIAL */ TutorialPause = RegisterTutorial("Pause Tutorial Seen", true); // not pause for like 3 minutes (you can pause! yay!)
            /* MODPACK TUTORIAL */ TutorialWheel = RegisterTutorial("Wheel Tutorial Seen", true); // start character with wheel functions
            TutorialInfo = RegisterTutorial("Inventory Tutorial Seen"); // not pressing tab for like a minute (hover to view item)
            /* MODPACK TUTORIAL */ TutorialDrop = RegisterTutorial("Drop Tutorial Seen", true); // not pressing drop for like 2 minute (only when dropifact is 2)
            TutorialAchievements = RegisterTutorial("Achievements Tutorial Seen"); // achievement gained (unlock things by achieving, also check logbook)
            TutorialElite = RegisterTutorial("Elite Tutorial Seen"); // elite first killed

            TutorialRarity = RegisterTutorial("Rarity Tutorial Seen"); // shop, printer or item seen or obtained
            /* MODPACK TUTORIAL */ TutorialRebalance = RegisterTutorial("Rebalance Tutorial Seen", true); // item obtained but tutorialstacking is true, disabled on item pickup
            TutorialStacking = RegisterTutorial("Stacking Tutorial Seen"); // same item obtained, press tab to see stacked stat
            TutorialEquipment = RegisterTutorial("Equipment Tutorial Seen"); // equipment chest, shop or item seen or obtained
            TutorialVoidItem = RegisterTutorial("Void Item Tutorial Seen"); // void cradle or item seen or obtained
            TutorialLunarItem = RegisterTutorial("Lunar Item Tutorial Seen");  // lunar pod or item seen or obtained
            /* MODPACK TUTORIAL */ TutorialVoidLunarItem = RegisterTutorial("Void Lunar Item Tutorial Seen", true);  // void lunar item seen or obtained

            // interactibles
            TutorialChestVariant = RegisterTutorial("Chest Variant Tutorial Seen"); // big/golden chest seen
            TutorialChestThemed = RegisterTutorial("Themed Chest Tutorial Seen"); // themed chest seen
            TutorialShop = RegisterTutorial("Shop Tutorial Seen"); // multishop terminal seen
            TutorialSlot = RegisterTutorial("Slot Tutorial Seen"); // slot terminal seen
            TutorialCauldron = RegisterTutorial("Cauldron Tutorial Seen"); // cauldron tutorial seen

            TutorialPrinter = RegisterTutorial("Printer Tutorial Seen"); // when printer first seen
            TutorialScrapper = RegisterTutorial("Scrapper Tutorial Seen"); // scrapper first seen OR obtained scrap
            /* MODPACK TUTORIAL */ TutorialShrineChance = RegisterTutorial("Shrine of Chance Tutorial Seen", true); // shrine first seen, misfortune is now rewarded...
            TutorialShrineCombat = RegisterTutorial("Shrine of Combat Tutorial Seen"); // shrine first seen
            /* MODPACK TUTORIAL */ TutorialShrineOrder = RegisterTutorial("Shrine of Order Tutorial Seen", true); // shrine first seen, it's like a d4...
            TutorialShrineBlood = RegisterTutorial("Shrine of Blood Tutorial Seen"); // shrine first seen
            TutorialShrineHealing = RegisterTutorial("Shrine of the Woods Tutorial Seen"); // shrine first seen
            /* MODPACK TUTORIAL */ TutorialShrineVoid = RegisterTutorial("Shattered Monolith Tutorial Seen", true); // shrine first seen
            /* MODPACK TUTORIAL */ TutorialShrineRepair = RegisterTutorial("Shrine of Repair Tutorial Seen", true); // shrine first seen
            TutorialVoidField = RegisterTutorial("Void Field Tutorial Seen"); // void field first seen

            /* MODPACK TUTORIAL */ TutorialDrone = RegisterTutorial("Drone Tutorial Seen"); // not-special drone first seen
            TutorialTurret = RegisterTutorial("Turret Tutorial Seen"); // turret first seen
            TutorialEmergencyDrone = RegisterTutorial("Emergency Drone Tutorial Seen"); // drone first seen
            TutorialEquipmentDrone = RegisterTutorial("Equipment Drone Tutorial Seen"); // drone first seen

            // Bazaar
            TutorialLunarCoin = RegisterTutorial("Lunar Coin Tutorial Seen"); // lunar coin obtained
            TutorialBazaar = RegisterTutorial("Bazaar Tutorial Seen"); // bazaar first entered, also talk about newt altar
            TutorialNewt = RegisterTutorial("Newt `Tutorial` Seen"); // not tutorial, "you may want to not hit them lol"
            TutorialDream = RegisterTutorial("Dream Tutorial Seen"); // dream seen
            TutorialCleansingPool = RegisterTutorial("Cleansing Pool Tutorial Seen"); // cleansing pool seen, "it takes lunar..."

            // Subsequent levels (vague hints)
            TutorialLevel2 = RegisterTutorial("Level 2+ Tutorial Seen"); // on level 2, "most levels have hidden things you can utilize in runs"
            TutorialPressurePlate = RegisterTutorial("Desert Pressure Plate `Tutorial` Seen"); // button seen, "opens doors..."
            TutorialTimedChest = RegisterTutorial("Timed Chest `Tutorial` Seen"); // timed out timed chest seen, "seems like you're late..."

            TutorialLevel4 = RegisterTutorial("Level 4+ Tutorial Seen"); // teleporter know how, "seek particles, get a feel for where it spawns relative to you"
            TutorialEgg = RegisterTutorial("Egg `Tutorial` Seen"); // egg seen, "destroying it seems to anger the vultures..."
            TutorialRex = RegisterTutorial("Rex `Tutorial` Seen"); // rex seen, "Its fuel seems depleted."
            /* MODPACK TUTORIAL */ TutorialButton = RegisterTutorial("Button `Tutorial` Seen", true); // button seen, "seems to be a seal..."

            TutorialPrimordialTeleporter = RegisterTutorial("Primordial Teleporter `Tutorial` Seen"); // primordial teleporter seen, "you can change the destination..."
            TutorialTablet = RegisterTutorial("Tablet `Tutorial` Seen"); // tablet seen, "write it down somewhere..."
            TutorialTabletInput = RegisterTutorial("Tablet Input `Tutorial` Seen"); // button seen, "where have i seen something like this..."
            TutorialFrog = RegisterTutorial("Frog `Tutorial` Seen"); // frog seen, "You feel drawn to pet it..."

            TutorialLevel6 = RegisterTutorial("Loop Tutorial Seen"); // loop!
            TutorialObelisk = RegisterTutorial("Obelisk `Tutorial` Seen"); // obelisk seen, "It obliterates you, But..."
            /* MODPACK TUTORIAL */ TutorialAltar = RegisterTutorial("Altar of Gold `Tutorial` Seen"); // Altar of gold seen

            /* MODPACK TUTORIAL */ TutorialRelicEnergy = RegisterTutorial("Relic of Energy Tutorial Seen");
            /* MODPACK TUTORIAL */ TutorialShatteredTeleporter = RegisterTutorial("Shattered Teleporter Tutorial Seen");
            /* MODPACK TUTORIAL */ TutorialSlumberingAltar = RegisterTutorial("Slumbering Pedestal Tutorial Seen");

            // Subsequent Runs
            FirstRun = Config.Bind("Tutorial", "First Run", true, "--PACK DEVS DONT TOUCH THIS--"); // is first run?
            TutorialLoadout = RegisterTutorial("Loadout Tutorial Seen"); // second run charselect, "press loadouts to change moveset", disables on loadout change
            TutorialDrizzle = RegisterTutorial("Drizzle Tutorial Seen"); // died 5 time in a row, "try drizzle for easier challenge", disables on diff change
            TutorialThunderstorm = RegisterTutorial("Thunderstorm Tutorial Seen"); // won within 5 tries, "try thunderstorm for harder challenge", disables on diff change
        }

        private static ConfigEntry<bool> RegisterTutorial(string title, bool isModded = false)
        {
            ConfigEntry<bool> ret = Config.Bind("Tutorial", title, false, "--PACK DEVS DONT TOUCH THIS--");
            // allTutorials.Add(ret);
            // if (isModded) moddedTutorials.Add(ret);
            return ret;
        }

        public static bool Mods(params string[] arr)
        {
            for (int i = 0; i < arr.Length; i++) if (!Chainloader.PluginInfos.ContainsKey(arr[i])) return false;
            return true;
        }

        public static void SetAllTutorials(bool value)
        {
            RiskyMonkeyBase.Log.LogDebug("Setting all tutorials to " + value);
            TutorialTeleporter.Value = value;
            TutorialTeleporterZone.Value = value;
            TutorialTeleporterPost.Value = value;
            TutorialPing.Value = value;
            TutorialEmote.Value = value;
            TutorialRun.Value = value;
            TutorialOSP.Value = value;
            TutorialPause.Value = value;
            TutorialWheel.Value = value;
            TutorialInfo.Value = value;
            TutorialDrop.Value = value;
            TutorialAchievements.Value = value;
            TutorialRarity.Value = value;
            TutorialRebalance.Value = value;
            TutorialStacking.Value = value;
            TutorialEquipment.Value = value;
            TutorialVoidItem.Value = value;
            TutorialLunarItem.Value = value;
            TutorialVoidLunarItem.Value = value;
            TutorialElite.Value = value;
            TutorialLunarCoin.Value = value;
            TutorialChestVariant.Value = value;
            TutorialChestThemed.Value = value;
            TutorialShop.Value = value;
            TutorialSlot.Value = value;
            TutorialCauldron.Value = value;
            TutorialPrinter.Value = value;
            TutorialScrapper.Value = value;
            TutorialShrineChance.Value = value;
            TutorialShrineCombat.Value = value;
            TutorialShrineOrder.Value = value;
            TutorialShrineBlood.Value = value;
            TutorialShrineHealing.Value = value;
            TutorialShrineRepair.Value = value;
            TutorialShrineVoid.Value = value;
            TutorialVoidField.Value = value;
            TutorialDrone.Value = value;
            TutorialTurret.Value = value;
            TutorialEmergencyDrone.Value = value;
            TutorialEquipmentDrone.Value = value;
            TutorialBazaar.Value = value;
            TutorialNewt.Value = value;
            TutorialDream.Value = value;
            TutorialCleansingPool.Value = value;
            TutorialLevel2.Value = value;
            TutorialPressurePlate.Value = value;
            TutorialTimedChest.Value = value;
            TutorialLevel4.Value = value;
            TutorialEgg.Value = value;
            TutorialButton.Value = value;
            TutorialPrimordialTeleporter.Value = value;
            TutorialTablet.Value = value;
            TutorialTabletInput.Value = value;
            TutorialFrog.Value = value;
            TutorialObelisk.Value = value;
            TutorialRex.Value = value;
            TutorialLoadout.Value = value;
            TutorialDrizzle.Value = value;
            TutorialThunderstorm.Value = value;
            TutorialAltar.Value = value;
            TutorialLevel6.Value = value;
            TutorialRelicEnergy.Value = value;
            TutorialShatteredTeleporter.Value = value;
            TutorialSlumberingAltar.Value = value;

            FirstRun.Value = !value;
        }

        public static void SetModdedTutorials(bool value)
        {
            RiskyMonkeyBase.Log.LogDebug("Setting modded tutorials to " + value);
            TutorialTeleporterPost.Value = value;
            TutorialPing.Value = value;
            TutorialEmote.Value = value;
            TutorialPause.Value = value;
            TutorialWheel.Value = value;
            TutorialDrop.Value = value;
            if (Mods("Hayaku.VanillaRebalance")) TutorialRebalance.Value = value;
            TutorialVoidLunarItem.Value = value;
            if (Mods("Xatha.SoCRebalancePlugin")) TutorialShrineChance.Value = value;
            if (Mods("TJT.HarderCombatShrines")) TutorialShrineCombat.Value = value;
            if (Mods("cbrl.ShrineOfDisorder")) TutorialShrineOrder.Value = value;
            TutorialShrineRepair.Value = value;
            TutorialShrineVoid.Value = value;
            TutorialButton.Value = value;
            if (Mods("com.Skell.GoldenCoastPlus")) TutorialAltar.Value = value;
            if (Mods("der10pm.chargeinhalf")) TutorialLevel6.Value = value;
            TutorialRelicEnergy.Value = value;
            TutorialShatteredTeleporter.Value = value;
            TutorialSlumberingAltar.Value = value;
            TutorialDrone.Value = value;

            FirstRun.Value = !value;
        }
    }
}

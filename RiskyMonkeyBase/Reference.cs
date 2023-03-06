using BepInEx.Bootstrap;
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
        public const string PluginVersion = "0.10.0";
        public static int[] Releases = { 100, 97, 96, 95, 94, 93, 92, 91, 90 }; // prepend new releases
        public static ConfigFile Config;
        public static ConfigFile ConfigTutorial;
        public static ConfigEntry<bool> RadiantMalignance;
        public static ConfigEntry<bool> FixPaladinAchievementNames;
        public static ConfigEntry<string> GrandDifficulty;
        public static ConfigEntry<string> DifficultiesToRemove;
        public static ConfigEntry<float> SAAltChance;
        public static ConfigEntry<bool> HitListDisable;
        public static ConfigEntry<bool> ReviveBetweenMithrixPhase;
        public static ConfigEntry<float> VoidLunarOnBazaar;
        public static ConfigEntry<bool> CleansingPoolVoidLunar;
        public static ConfigEntry<bool> HEADSTChanges;
        public static ConfigEntry<string> RiskOfOptionsHideList;
        public static ConfigEntry<string> ScenesToRemove;
        public static ConfigEntry<string> SkinsToReorder;
        public static ConfigEntry<string> MemeSkins;
        public static ConfigEntry<string> SurvivorsOrder;
        public static ConfigEntry<string> ChallengesToReorder;
        public static ConfigEntry<bool> CustomAchievements;
        public static ConfigEntry<string> AchievementBlacklist;
        public static ConfigEntry<string> SkillsToReorder;
        public static ConfigEntry<float> ScytheHealAmount;
        public static ConfigEntry<float> ScytheHealStack;
        public static ConfigEntry<float> TankAmount;
        public static ConfigEntry<float> TankStack;
        public static ConfigEntry<float> StealthkitHealth;
        public static ConfigEntry<float> SingularityBandDamage;
        public static ConfigEntry<float> SingularityBandStack;
        public static ConfigEntry<float> WispProc;
        public static ConfigEntry<float> WispArea;
        public static ConfigEntry<float> WispAreaStack; 
        public static ConfigEntry<float> WispDamage;
        public static ConfigEntry<float> WispDamageStack;
        public static ConfigEntry<float> GasolineBlast;
        public static ConfigEntry<float> GasolineBlastFlat;
        public static ConfigEntry<float> GasolineBurn;
        public static ConfigEntry<float> GasolineBurnStack;
        public static ConfigEntry<float> GasolineArea;
        public static ConfigEntry<float> GasolineAreaStack;
        public static ConfigEntry<float> Perf1Proc;
        public static ConfigEntry<bool> ShowAllArtifacts;
        public static ConfigEntry<int> ArtifactUnlockHint;
        public static ConfigEntry<int> ScrapperStackAtOnce;
        public static ConfigEntry<float> Perf2Proc;
        public static ConfigEntry<float> DiscProc;
        public static ConfigEntry<float> DiscipleProc;
        public static ConfigEntry<float> GloopProc;
        public static ConfigEntry<float> DaggerProc;
        public static ConfigEntry<float> NkuhanaDamage;
        public static ConfigEntry<float> WireProc;
        public static ConfigEntry<float> WireDamage;
        public static ConfigEntry<float> FireworkProc;
        public static ConfigEntry<float> WoodspriteCooldown;
        public static ConfigEntry<float> BackupCooldown;
        public static ConfigEntry<float> ForgiveMeCooldown;
        public static ConfigEntry<float> ChrysalisSpeed;
        public static ConfigEntry<float> ChrysalisBoostCooldown;
        public static ConfigEntry<float> ChrysalisBoost;
        public static ConfigEntry<float> CapacitorDamage;
        public static ConfigEntry<float> CapacitorCooldown;
        public static ConfigEntry<float> CapacitorArea;
        public static ConfigEntry<float> CrowdfunderDamage;
        public static ConfigEntry<float> CrowdfunderChestCost;
        public static ConfigEntry<float> VolcanicEggPassive;
        public static ConfigEntry<float> VolcanicEggDamage;
        public static ConfigEntry<float> WarHornSpeedStack;
        public static ConfigEntry<float> WarHornDuration;
        public static ConfigEntry<float> WarHornDurationStack;
        public static ConfigEntry<float> FireRingDamage;
        public static ConfigEntry<float> IceRingDamage;
        public static ConfigEntry<float> EgoSpeed;
        public static ConfigEntry<float> HooksDamage;
        public static ConfigEntry<float> EssenceDamage;
        public static ConfigEntry<float> EssenceDamageStack;
        public static ConfigEntry<float> UrchinShield;
        public static ConfigEntry<float> QuartzRabbitCooldown;
        public static ConfigEntry<float> TransHealth;
        public static ConfigEntry<float> HaloSpeed;
        public static ConfigEntry<float> HaloSkill;
        public static ConfigEntry<float> VolcanicEggArea;
        public static ConfigEntry<float> VaseCooldown;
        public static ConfigEntry<float> BlastShowerCooldown;
        public static ConfigEntry<float> RecyclerCooldown;
        public static ConfigEntry<float> WarHornSpeed;
        public static ConfigEntry<bool> InvulnerableInUI;
        public static ConfigEntry<bool> GayMarrige;
        public static ConfigEntry<bool> WarbannerSkillCooldown;
        public static ConfigEntry<bool> WhiteGuillotine;
        public static ConfigEntry<int> RecyclerMaxUses;
        public static ConfigEntry<bool> BetterEclipseRex;
        public static ConfigEntry<bool> BetterEclipseCorpsebloom;
        public static ConfigEntry<bool> WorseEclipseShield;
        public static ConfigEntry<bool> SimulacrumItemChanges;
        public static ConfigEntry<bool> AegisBarrierHeal;
        public static ConfigEntry<bool> BetterEngiAchievement;
        public static ConfigEntry<bool> PainBoxBetterHide;
        public static ConfigEntry<bool> BungusKnockback;
        public static ConfigEntry<bool> TicketShrine;
        public static ConfigEntry<bool> TicketPotential;
        public static ConfigEntry<bool> TicketLocked;
        public static ConfigEntry<bool> TicketPool;
        public static ConfigEntry<float> DropletPickupRadius;
        public static ConfigEntry<float> WarbannerAmount;
        public static ConfigEntry<float> WarbannerStack;
        public static ConfigEntry<float> MagazineCooldown;
        public static ConfigEntry<float> BroochAmount;
        public static ConfigEntry<float> BroochStack;
        public static ConfigEntry<float> Mogus;
        public static ConfigEntry<float> TTimeMax;
        public static ConfigEntry<float> SSpaceMax;
        public static ConfigEntry<float> ElixirHealth;

        // public static ConfigEntry<bool> RFTVDisableVoidCoin;
        // public static ConfigEntry<bool> RFTVDisableVoidSuppressor;
        public static ConfigEntry<bool> RFTVDisableItemEnable;
        public static ConfigEntry<bool> RFTVIotaConstructFix;
        public static ConfigEntry<bool> RFTVIotaConstructBuff;
        public static ConfigEntry<bool> RFTVIotaConstructNerf;
        public static ConfigEntry<bool> RFTVAssassinNerf;
        // public static ConfigEntry<bool> RFTVDisableCommandoSkin;
        // public static ConfigEntry<bool> RFTVDisableLocusTweaks;

        public static ConfigEntry<bool> ReprogrammerActivate;
        public static ConfigEntry<int> ReprogrammerCooldown;
        public static ConfigEntry<float> ReprogrammerRefresh;
        public static ConfigEntry<bool> ReprogrammerRespectTier;
        public static ConfigEntry<float> ReprogrammerRepairChance;

        public static ConfigEntry<bool> SeriousMode;
        public static ConfigEntry<bool> EnableEmotes;
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
            int idx = configPath.IndexOf("RiskOfRain2"); // cfg so powerful it goes outside the profile page
            if (idx != -1) ConfigTutorial = new ConfigFile(Path.Combine(configPath.Substring(0, idx + "RiskOfRain2".Length), "RadiantMalignancePersistent.cfg"), true);
            else ConfigTutorial = Config;
            ConfigTutorial.SaveOnConfigSet = true;
            // allTutorials = new();
            // moddedTutorials = new();

            RadiantMalignance = Config.Bind("1. Radiance HK Died of Ligma", "Modpack Mode", false, "Set to false when used outside RM.");
            CustomAchievements = Config.Bind("1. Radiance HK Died of Ligma", "Enable Custom Achievements", true, "Set to false to unlock modded items/skins/loadout by default.");

            ReprogrammerActivate = Config.Bind("2. Reprog is Repog", "Reprogrammer Activate", true, "Set to false to disable Reprogrammer.");
            ReprogrammerCooldown = Config.Bind("2. Reprog is Repog", "Reprogrammer Cooldown", 60, "Cooldown for Reprogrammer in seconds.");
            ReprogrammerRefresh = Config.Bind("2. Reprog is Repog", "Reprogrammer HUD Refresh", 0.2f, "every N seconds reprogrammer will check for new target. helps with lag but may desync HUD. does not affect gameplay.");
            ReprogrammerRepairChance = Config.Bind("2. Reprog is Repog", "Reprogrammer Shrine Chance", 0.25f, "Chance for Reprogrammer to turn printers into Shrine of Repair. Will be ignored if Shrine of Repair is not installed.");
            
            SurvivorsOrder = Config.Bind("3. Content Disabler V2", "Survivor Order List", "Commando - 1, Huntress - 2, Bandit2 - 3, Toolbot - 4, Engi - 5, Mage - 6, Merc - 7, Treebot - 8, Loader - 9, Croco - 10, Captain - 11, Heretic - 13, Railgunner - 14, VoidSurvivor - 15", "List of Survivors to reorder, seperated by commas. Ones not on the list will have its desiredSortPosition unmodified.");
            DifficultiesToRemove = Config.Bind("3. Content Disabler V2", "Remove Difficulties", "", "accepts nameTokens, split by commas.");
            ChallengesToReorder = Config.Bind("3. Content Disabler V2", "Challenge Reorder List", "", "List of achievement INTERNAL names to reorder, separated by commas. Ones not on the list will have its sortScore unmodified. see logs for list.");
            AchievementBlacklist = Config.Bind("3. Content Disabler V2", "Challenge Hide List", "", "List of achievements to Not Load, unlockable name, seperated by commas.");
            SkillsToReorder = Config.Bind("3. Content Disabler V2", "Skill Reorder List", "", "List of skill names to reorder, separated by commas. Ones not on the list will appear at the front.");
            SkinsToReorder = Config.Bind("3. Content Disabler V2", "Skin Reorder List", "", "List of skin names to reorder, separated by commas. Ones not on the list will appear at the front.");
            MemeSkins = Config.Bind("3. Content Disabler V2", "Meme Skins", "", "List of skin names to hide when serious mode is off, separated by commas.");
            RiskOfOptionsHideList = Config.Bind("3. Content Disabler V2", "Risk Of Options Hide List", "", "List of categories to hide in Mod Options. separated by commas. See log for list of IDs.");
            ScenesToRemove = Config.Bind("3. Content Disabler V2", "Scenes Hide List", "", "List of scenes to disable, separated by commas.");

            FixPaladinAchievementNames = Config.Bind("4. FTFY: The Mod", "Fix Paladin Achievement Names", true, "Puts “Paladin: ” in front of the achievement names.");
            ReviveBetweenMithrixPhase = Config.Bind("4. FTFY: The Mod", "Revive Between Mithrix Phase", false, "Set to true to revive everyone between Mithrix Phases (except 5) with Artifact of Revive on.");
            GrandDifficulty = Config.Bind("4. FTFY: The Mod", "Grand Mastery Difficulty", "INFERNO_NAME", "Difficulty to replace Typhoon with for achievement description. Accepts nameTokens. HIFU-Inferno by default until Starstorm2 SoTV.");
            SAAltChance = Config.Bind("4. FTFY: The Mod", "Stage Aesthetics Alt Chance", 0.25f, "Chance for alternate aesthetics compared to vanilla ones. Will be ignored if vanilla is disabled.");
            HitListDisable = Config.Bind("4. FTFY: The Mod", "Hit List Disabled", false, "I LOVE MOONSTORM!!!");

            // RFTVDisableVoidCoin = Config.Bind("5. RFTV but Epic", "Disable Void Coins", false, "Set to true to disable Void Coins.");
            // RFTVDisableVoidSuppressor = Config.Bind("5. RFTV but Epic", "Disable Void Suppressors", false, "Set to true to disable Void Suppressors.");
            RFTVDisableItemEnable = Config.Bind("5. RFTV but Epic", "Disable Item Enable", false, "Set to true to disable Lunar Portal Item.");
            // RFTVDisableCommandoSkin = Config.Bind("5. RFTV but Epic", "Disable Commando Skin", false, "Set to true to disable Helot skin.");
            // RFTVDisableLocusTweaks = Config.Bind("5. RFTV but Epic", "Disable Void Locus Tweaks", false, "Set to true to disable Void Locus Tweaks.");
            RFTVIotaConstructFix = Config.Bind("5. RFTV but Epic", "Iota Construct Fix", true, "Modded stage compat & Void seed fix");
            RFTVIotaConstructBuff = Config.Bind("5. RFTV but Epic", "Iota Construct Buff", true, "Make it occasionally spawn Sigma Constructs");
            RFTVIotaConstructNerf = Config.Bind("5. RFTV but Epic", "Iota Construct Nerf", true, "Makes it unable to snipe you from 6000 miles away");
            RFTVAssassinNerf = Config.Bind("5. RFTV but Epic", "Assassin Nerf", true, "Makes it unable to literally one hit you");  

            ShowAllArtifacts = Config.Bind("6. ProdQualityOfLife", "Show All Artifacts", true, "Set to false to hide artifacts like vanilla. DOES NOT AFFECT UNLOCKS");
            ArtifactUnlockHint = Config.Bind("6. ProdQualityOfLife", "Artifact Unlock Hint", 3, "Displays artifact hints from the wiki in number of lines, 0 to disable, 4 to give direct answers");
            InvulnerableInUI = Config.Bind("6. ProdQualityOfLife", "Invulnerable in UI", true, "Imagine dying gettign items in Simulacrum");

            WhiteGuillotine = Config.Bind("7. :smirk_cat:", "White Guillotine", true, "Real");
            TicketShrine = Config.Bind("7. :smirk_cat:", "400 Tickets Works on Shrine of Chance", false, "yeah");
            TicketPotential = Config.Bind("7. :smirk_cat:", "400 Tickets Works on Void Potential", true, "yeah");
            TicketLocked = Config.Bind("7. :smirk_cat:", "400 Tickets Works on (encrusted) Lockboxes", true, "yeah");
            TicketPool = Config.Bind("7. :smirk_cat:", "400 Tickets Works on Cleansing Pool", true, "yeah");
            GayMarrige = Config.Bind("7. :smirk_cat:", "Enable Gay Marrige", false, "Having both bands double effect.");
            WarbannerSkillCooldown = Config.Bind("7. :smirk_cat:", "Warbanner Also Increases Skill Speed", false, "too much attack speed buffs tbh");
            HEADSTChanges = Config.Bind("7. :smirk_cat:", "H3AD-5T v2 Rebalance", false, "Removes cooldown, and increase damage on stacks. Lowers max damage so its useful without quail abuse.");
            VoidLunarOnBazaar = Config.Bind("7. :smirk_cat:", "Void Lunar on Bazaar Chance", 0.33f, "Chance for lunar item to convert into void version in bazaar. set to 0 to disable.");
            BetterEclipseRex = Config.Bind("7. :smirk_cat:", "Better Eclipse REX", true, "his skills is not affected on 5 and 8 (considered fall damage-esque)");
            BetterEclipseCorpsebloom = Config.Bind("7. :smirk_cat:", "Better Eclipse Corpsebloom", true, "Fixes the hobo james moment for Corpsebloom");
            WorseEclipseShield = Config.Bind("7. :smirk_cat:", "Worse Eclipse Shield", true, "Shield recharge also gets doubled on Eclipse 5, no more transc cheese hopefully");
            SimulacrumItemChanges = Config.Bind("7. :smirk_cat:", "Simulacrum Item Changes", true, "makes various items work better in simulacrum.");
            AegisBarrierHeal = Config.Bind("7. :smirk_cat:", "Aegis Barrier Gain Heals", false, "barrier gain heals you as well");
            BetterEngiAchievement = Config.Bind("7. :smirk_cat:", "Better Engi Achievement", false, "literally steals it off squid polyp");
            PainBoxBetterHide = Config.Bind("7. :smirk_cat:", "Box of Agony Better Hide", true, "makes using it actually a challenge");
            BungusKnockback = Config.Bind("7. :smirk_cat:", "Bustling Fungus Removes Knockback", false, "cobalt shield!!! (also applies to Sharp Anchor)");

            DropletPickupRadius = Config.Bind("8. Literally Risk of Rain 3", "Droplet Pickup Radius Multiplier", 1f, "Vanilla: 1.");
            WarbannerAmount = Config.Bind("8. Literally Risk of Rain 3", "Warbanner Stat Increase", 0.3f, "Vanilla: 0.3.");
            WarbannerStack = Config.Bind("8. Literally Risk of Rain 3", "Warbanner Stat Increase per Stack", 0f, "Vanilla: 0."); // Yeah
            MagazineCooldown = Config.Bind("8. Literally Risk of Rain 3", "Backup Magazine Secondary Cooldown Reduction", 0f, "Vanilla: 0.");
            BroochAmount = Config.Bind("8. Literally Risk of Rain 3", "Topaz Brooch Barrier Amount", 15f, "Vanilla: 15.");
            BroochStack = Config.Bind("8. Literally Risk of Rain 3", "Topaz Brooch Barrier Amount per Stack", 15f, "Vanilla: 15.");
            Mogus = Config.Bind("8. Literally Risk of Rain 3", "Focus Crystal Damage", 0.15f, "Vanilla: 0.2.");
            TTimeMax = Config.Bind("8. Literally Risk of Rain 3", "Tougher Times Max Block", 1f, "Vanilla: 1.");
            SSpaceMax = Config.Bind("8. Literally Risk of Rain 3", "Safer Spaces Min Cooldown", 0.5f, "Vanilla: 0.");
            ElixirHealth = Config.Bind("8. Literally Risk of Rain 3", "Power Elixir Health Threshold", 0.25f, "Vanilla: 0.25.");
            GasolineBlast = Config.Bind("8. Literally Risk of Rain 3", "Gasoline Blast Damage", 1.5f, "Vanilla: 1.5.");
            GasolineBlastFlat = Config.Bind("8. Literally Risk of Rain 3", "Gasoline Blast Flat Damage", 0f, "Vanilla: 0."); // reasonably kills fodders on early game
            GasolineBurn = Config.Bind("8. Literally Risk of Rain 3", "Gasoline Burn Damage", 1.5f, "Vanilla: 1.5.");
            GasolineBurnStack = Config.Bind("8. Literally Risk of Rain 3", "Gasoline Burn Damage per Stack", 0.75f, "Vanilla: 0.75."); // kinda weak, but trying to give edge to void counterpart, fire has synergy
            GasolineArea = Config.Bind("8. Literally Risk of Rain 3", "Gasoline Blast Radius", 12f, "Vanilla: 12.");
            GasolineAreaStack = Config.Bind("8. Literally Risk of Rain 3", "Gasoline Blast Radius per Stack", 4f, "Vanilla: 4.");
            FireworkProc = Config.Bind("8. Literally Risk of Rain 3", "Firework Bundle Proc Coefficient", 0.2f, "Vanilla: 0.2."); // trying to give edge over void counterpart (easy option)
            ScytheHealAmount = Config.Bind("8. Literally Risk of Rain 3", "Harvester`s Scythe Heal Amount", 4f, "Vanilla: 8, VanillaRebalance: 4.");
            ScytheHealStack = Config.Bind("8. Literally Risk of Rain 3", "Harvester`s Scythe Heal Amount per Stack", 4f, "Vanilla: 4."); // RM has 1897 ways of gaining crit you get 50% in stage 2 LOL
            TankAmount = Config.Bind("8. Literally Risk of Rain 3", "Ignition Tank Heal Amount", 3f, "Vanilla: 3.");
            TankStack = Config.Bind("8. Literally Risk of Rain 3", "Ignition Tank Heal Amount per Stack", 3f, "Vanilla: 3.");
            StealthkitHealth = Config.Bind("8. Literally Risk of Rain 3", "Old War Stealthkit Threshold", 0.25f, "Vanilla: 0.25.");
            WireProc = Config.Bind("8. Literally Risk of Rain 3", "Razorwire Proc Coefficient", 1f, "Vanilla: 1.0."); // honestly the most important change
            WireDamage = Config.Bind("8. Literally Risk of Rain 3", "Razorwire Damage", 1.5f, "Vanilla: 1.5."); // no 1s delay
            WispProc = Config.Bind("8. Literally Risk of Rain 3", "Will o` the Wisp Proc Coefficient", 1f, "Vanilla: 1.0.");
            WispArea = Config.Bind("8. Literally Risk of Rain 3", "Will o` the Wisp Area", 12f, "Vanilla: 12.");
            WispAreaStack = Config.Bind("8. Literally Risk of Rain 3", "Will o` the Wisp Area per Stack", 2.4f, "Vanilla: 2.4.");
            WispDamage = Config.Bind("8. Literally Risk of Rain 3", "Will o` the Wisp Damage", 3.5f, "Vanilla: 3.5.");
            WispDamageStack = Config.Bind("8. Literally Risk of Rain 3", "Will o` the Wisp Damage per Stack", 2.8f, "Vanilla: 2.8.");
            WarHornSpeed = Config.Bind("8. Literally Risk of Rain 3", "War Horn Attack Speed", 0.7f, "Vanilla: 0.7.");
            WarHornSpeedStack = Config.Bind("8. Literally Risk of Rain 3", "War Horn Attack Speed per Stack", 0f, "Vanilla: 0.");
            WarHornDuration = Config.Bind("8. Literally Risk of Rain 3", "War Horn Duration", 8f, "Vanilla: 8.");
            WarHornDurationStack = Config.Bind("8. Literally Risk of Rain 3", "War Horn Duration per Stack", 4f, "Vanilla: 4."); // makes it more synergy-ish
            FireRingDamage = Config.Bind("8. Literally Risk of Rain 3", "Kjaro`s Band Damage", 3f, "Vanilla: 3."); // accounting for gaymarrige
            IceRingDamage = Config.Bind("8. Literally Risk of Rain 3", "Runald`s Band Damage", 2.5f, "Vanilla: 2.5.");
            DiscProc = Config.Bind("8. Literally Risk of Rain 3", "Resonant Disc Proc Coefficient Multiplier", 1f, "Vanilla: 1.0.");
            DaggerProc = Config.Bind("8. Literally Risk of Rain 3", "Ceremonial Dagger Proc Coefficient", 1f, "Vanilla: 1.0.");
            NkuhanaDamage = Config.Bind("8. Literally Risk of Rain 3", "N`kuhana`s Opinion Damage", 2.5f, "Vanilla: 2.5.");
            Perf1Proc = Config.Bind("8. Literally Risk of Rain 3", "Molten Perforator Proc Coefficient", 0.7f, "Vanilla: 0.7.");
            Perf2Proc = Config.Bind("8. Literally Risk of Rain 3", "Charged Perforator Proc Coefficient", 1f, "Vanilla: 1.0."); // its 1 hit instead of 3 so it should be ok right :smile
            DiscipleProc = Config.Bind("8. Literally Risk of Rain 3", "Little Disciple Proc Coefficient", 1f, "Vanilla: 1.0.");
            GloopProc = Config.Bind("8. Literally Risk of Rain 3", "Genesis Loop Proc Coefficient", 1f, "Vanilla: 1.0.");
            SingularityBandDamage = Config.Bind("8. Literally Risk of Rain 3", "Singularity Band Damage", 1f, "Vanilla: 1."); // taking gay marrige into account, should be better early game
            SingularityBandStack = Config.Bind("8. Literally Risk of Rain 3", "Singularity Band Damage per Stack", 1f, "Vanilla: 1.");
            TransHealth = Config.Bind("8. Literally Risk of Rain 3", "Transcendence Health Bonus", 0.5f, "Vanilla: 0.5."); // still useful i think lol
            EgoSpeed = Config.Bind("8. Literally Risk of Rain 3", "Egocentrism Speed Boost per Stack", 0f, "Vanilla: 0.");
            HooksDamage = Config.Bind("8. Literally Risk of Rain 3", "Hooks of Heresy Damage", 7f, "Vanilla: 7. Charge: Value * 1.25.");
            EssenceDamage = Config.Bind("8. Literally Risk of Rain 3", "Essence of Heresy Damage", 3f, "Vanilla: 3.");
            EssenceDamageStack = Config.Bind("8. Literally Risk of Rain 3", "Essence of Heresy Damage per Stack", 1.2f, "Vanilla: 1.2.");
            WoodspriteCooldown = Config.Bind("8. Literally Risk of Rain 3", "Gnarled Woodsprite Cooldown", 15f, "Vanilla: 15.");
            BackupCooldown = Config.Bind("8. Literally Risk of Rain 3", "The Back-up Cooldown", 100f, "Vanilla: 100.");
            ForgiveMeCooldown = Config.Bind("8. Literally Risk of Rain 3", "Forgive Me Please Cooldown", 45f, "Vanilla: 45.");
            VaseCooldown = Config.Bind("8. Literally Risk of Rain 3", "Eccentric Vase Cooldown", 45f, "Vanilla: 45."); // 100% uptime by default (technically)
            BlastShowerCooldown = Config.Bind("8. Literally Risk of Rain 3", "Blast Shower Cooldown", 20f, "Vanilla: 20."); // indirect warhorn / bottled chaos buff, people don't run this often anyways
            RecyclerCooldown = Config.Bind("8. Literally Risk of Rain 3", "Recycler Cooldown", 45f, "Vanilla: 45."); // "balance" for inf recycle so 100% uptime is harder to achieve
            ChrysalisSpeed = Config.Bind("8. Literally Risk of Rain 3", "Milky Chrysalis Base Speed Boost", 0f, "Vanilla: 0.");
            ChrysalisBoostCooldown = Config.Bind("8. Literally Risk of Rain 3", "Milky Chrysalis Boost Cooldown", 0.5f, "Vanilla: 0.5.");
            ChrysalisBoost = Config.Bind("8. Literally Risk of Rain 3", "Milky Chrysalis Boost Speed", 3f, "Vanilla: 3.");
            CrowdfunderDamage = Config.Bind("8. Literally Risk of Rain 3", "Crowdfunder Damage", 1f, "Vanilla: 1."); // hire a mando
            CrowdfunderChestCost = Config.Bind("8. Literally Risk of Rain 3", "Crowdfunder Cost proportional to chest", 0.04f, "Vanilla: 0."); // actually scaling
            VolcanicEggPassive = Config.Bind("8. Literally Risk of Rain 3", "Volcanic Egg Ram Damage", 5f, "Vanilla: 5.");
            VolcanicEggDamage = Config.Bind("8. Literally Risk of Rain 3", "Volcanic Egg End Damage", 8f, "Vanilla: 8.");
            VolcanicEggArea = Config.Bind("8. Literally Risk of Rain 3", "Volcanic Egg Area", 8f, "Vanilla: 8."); // it literally explodes, this is Not Balance i just think it should be stronger
            CapacitorCooldown = Config.Bind("8. Literally Risk of Rain 3", "Royal Capacitor Cooldown", 20f, "Vanilla: 20.");
            CapacitorDamage = Config.Bind("8. Literally Risk of Rain 3", "Royal Capacitor Damage", 30f, "Vanilla: 30."); // literally captain m2 now lol
            CapacitorArea = Config.Bind("8. Literally Risk of Rain 3", "Royal Capacitor Stun Area", 3f, "Vanilla: 3.");
            HaloSpeed = Config.Bind("8. Literally Risk of Rain 3", "Cracked Halo Speed Boost", 0.25f, "Vanilla?: 0.25.");
            HaloSkill = Config.Bind("8. Literally Risk of Rain 3", "Cracked Halo Skill Speed", 0f, "Vanilla?: 0.");
            UrchinShield = Config.Bind("8. Literally Risk of Rain 3", "Charged Urchin Shield %", 0.1f, "Vanilla?: 0.1."); // fixes something 
            QuartzRabbitCooldown = Config.Bind("8. Literally Risk of Rain 3", "Quartz Rabbit Cooldown", 100f, "Vanilla?: 100.");

            /* END USER OPTIONS */
            SeriousMode = ConfigTutorial.Bind("Settings", "Serious Mode", true, "Set to false to Monkey.");
            /* END USER OPTIONS */ EnableEmotes = ConfigTutorial.Bind("Settings", "Enable Emotes", false, "Separated from Serious Mode, contains memes.");
            /* END USER OPTIONS */ ResetTutorial = ConfigTutorial.Bind("Settings", "Reset Tutorial", true, "Set to true to reprompt the first tutorial prompt.");
            /* END USER OPTIONS */ GetChangelog = ConfigTutorial.Bind("Settings", "Get Changelog", true, "Set to false to no longer recieve changelog on pack update.");
            LastVersion = ConfigTutorial.Bind("Settings", "Last Version", Releases[0], "For changelog prompt. ignored if getChangelog is false.");

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
            FirstRun = ConfigTutorial.Bind("Tutorial", "First Run", true, "--PACK DEVS DONT TOUCH THIS--"); // is first run?
            TutorialLoadout = RegisterTutorial("Loadout Tutorial Seen"); // second run charselect, "press loadouts to change moveset", disables on loadout change
            TutorialDrizzle = RegisterTutorial("Drizzle Tutorial Seen"); // died 5 time in a row, "try drizzle for easier challenge", disables on diff change
            TutorialThunderstorm = RegisterTutorial("Thunderstorm Tutorial Seen"); // won within 5 tries, "try thunderstorm for harder challenge", disables on diff change
        }

        private static ConfigEntry<bool> RegisterTutorial(string title, bool isModded = false)
        {
            ConfigEntry<bool> ret = ConfigTutorial.Bind("Tutorial", title, false, "--PACK DEVS DONT TOUCH THIS--");
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

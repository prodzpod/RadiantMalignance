# Radiant Malignance - Base
![logo image](https://prodzpod.github.io/RadiantMalignance/logo.png)  
This is a base mod for Radiance Malignance. Issues related to the modpack and the basemod should be directed here.
**DO NOT REPORT ERRORS YOU ENCOUNTER IN THE MODPACK TO THE MOD DEV** unless you know what you're doing. Cross-mod interactions are supposed to be messy and it should be reported here instead.

Refer to the [official page](https://prodzpod.github.io/RadiantMalignance/index.html) for credits and changelog.

## Note: Optimization
Yes I know the mod is wildly unoptimized. This was my first RoR2 project and the first C# project of this scale. Adjusting to the API took some time (I don't even think I know all the helper functions yet), and there were definitely large obstacles. Feel free to mess with the code, push a PR, whatever. go nuts. Thank you for visiting this.

## Changes - Major

### Achievements
Add unlocks for:
- Survivors
  - SotV + [Forgotten Relics](https://thunderstore.io/package/PlasmaCore3/Forgotten_Relics/): Railgunner (Forgotten Haven)
  - [PlayableTemplar](https://thunderstore.io/package/TemplarBoyz/PlayableTemplar/): Templar (Golden Coast)
  - SotV: Void Fiend required changed (removed simulacrum for (see Skins))
- Items
  - [Bubbet's Items](https://thunderstore.io/package/Bubbet/BubbetsItems/):
    - Gem Carapace
    - Eternal Slug
    - Bunny Foot
    - Escape Plan
    - Wildlife Camera
  - [HolyCrapForkisBack](https://thunderstore.io/package/BiggerUkulele/HolyCrapForkIsBack/):
    - Knife
    - Sharpened Chopsticks
- Skills
  - [Auto Shot](https://thunderstore.io/package/Lodington/AutoShot/): Auto shot
  - [Bandit Dynamite Toss](https://thunderstore.io/package/Moffein/Bandit_Dynamite_Toss/): Dynamite Toss
  - [EngineerWithAShotgunREDUX](https://thunderstore.io/package/macawesone/EngineerWithAShotgunREDUX/):
    - Plasma Grenades
    - Jetpack
  - [PlayableTemplar](https://thunderstore.io/package/TemplarBoyz/PlayableTemplar/):
    - Railgun
    - Flamethrower
    - Bazooka Mk. 2
    - Blunderbuss
    - Sidestep
    - Weapon Swap
  - [PassiveAgression](https://thunderstore.io/package/RandomlyAwesome/PassiveAgression/):
    - Stim Shot
    - Starch Bomb
    - Carrier Pathogen
    - Power Pack Discharge
    - Unorthodox Rituals
    - Excise
    - Snowsculpt
    - Incessant Infestation
    - 「T?ea?r】
    - Steel Resolve
    - Heart of the Forge
- Skins (Skin renames are Modpack Mode only)
  - Add skin categories similar to Mastery
    - Nemesis: Kill n enemies in a run (subject to change)
    - Mastery: Monsoon
    - [Spiritual Success](https://thunderstore.io/package/TheMysticSword/BulwarksHaunt/): fills in modded character skins
    - Simulated: reach LV50 in [Inferno](https://thunderstore.io/package/HIFU/Inferno/) Simulacrum
    - Providence: E8 OR [Artifact of Eclipse](https://thunderstore.io/package/William758/ZetArtifacts) (subject to change?), mostly from [MV](https://thunderstore.io/package/KrononConspirator/MadVeteran_Skinpack/)
    - [Survival](https://thunderstore.io/package/HIFU/Inferno/): Fills in missing skins for now, mostly from [LTT](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/)
  - Commando
    - Nemesis (Gup): [Giga-Gupmando](https://thunderstore.io/package/RetroInspired/Gupmando/), renamed to "Friend Shaped"
    - Simulated: [Dressed To Kill](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/)
    - Providence: [Refined Junkie](https://thunderstore.io/package/KrononConspirator/MadVeteran_Skinpack/)
    - No Achievement: [Among Us](https://thunderstore.io/package/bobblet/AmongUsCommando/), renamed to "Suspicious"
  - Huntress
    - Simulated: [Serpentine](https://thunderstore.io/package/FrostRay/Frost_Ray_Skin_Pack/)
    - Providence: [Kindred](https://thunderstore.io/package/Arty_Boyos/KindredHuntress/)
    - Survival: [Ghost Shikari](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/)
    - No Achievement: [Casual](https://thunderstore.io/package/KrononConspirator/MadVeteran_Skinpack/), renamed to "Orange Box"
  - Bandit
    - Nemesis (Beetle): [Skullduggery](https://thunderstore.io/package/Arty_Boyos/SkullDuggery/)
    - Simulated: [Highwayman](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/)
    - Providence: [Pinkerton](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/)
    - No Achievement: [V1, V2](https://thunderstore.io/package/bobblet/UltrakillV1_Bandit_Skin/), renamed to "Supreme" and "SSadistic"
  - Mul-T
    - Nemesis ([Direseeker](https://thunderstore.io/package/EnforcerGang/Direseeker)): [Dino](https://thunderstore.io/package/TailLover/DinoMulT/), renamed to "Cretaceous"
    - Simulated: [Crying Golem](https://thunderstore.io/package/Arty_Boyos/CryingGolem/), renamed to "Glitched"
    - Providence: [War Tank](https://thunderstore.io/package/KrononConspirator/MadVeteran_Skinpack/), renamed to "Mechanized"
    - Survival: [Grad](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/), renamed to "Prototype"
  - Engineer
    - [Power Armor Mk2](https://thunderstore.io/package/duckduckgreyduck/LazyBastardEngineer/): Fixed achievement (ModdedUnlockable -> RoR2), renamed to "Power Armor"
    - Simulated: [Null](https://thunderstore.io/package/prodzpod/TemplarSkins/)
    - Providence: [Agent Bungus](https://thunderstore.io/package/KrononConspirator/MadVeteran_Skinpack/), renamed to "Fungal"
    - Survival: [Moloch](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/)
    - No Achievement: [Hard Hat, Hard Hat (Classic)](https://thunderstore.io/package/12GaugeAwayFromFace/TF2_Engineer_Skin/), renamed to "Stock" and "Zagged"
  - Captain
    - Special Achievement: [Steelbeard](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/)
    - Nemesis (Magma Worm): [Hellfire Captain](https://thunderstore.io/package/marklow/HellfireCaptain), Renamed to "Hellfire"
    - Simulated: [Underglow](https://thunderstore.io/package/MAVRI/CaptainUnderglowDrip), no decal renamed to "Underglow (No Decal)"
    - Providence: [Lieutenant](https://thunderstore.io/package/KrononConspirator/MadVeteran_Skinpack/)
    - No Achievement: [Captain Latch](https://thunderstore.io/package/TailLover/CaptainLatch/), Renamed to "Funky"
  - Mercenary
    - Nemesis ([Assassin](https://thunderstore.io/package/Anreol/ReleasedFromTheVoid)): [Kitsune](https://thunderstore.io/package/FrostRay/Frost_Ray_Skin_Pack/)
    - [Oni Traditional](https://thunderstore.io/package/Wolfo/WolfoQualityOfLife/): Makes it also unlocked by Mastery, renamed to "Oni (Traditional)"
    - Simulated: [Yin Yang](https://thunderstore.io/package/Arty_Boyos/YinYang/), renamed to "Shinobi"
    - Providence: [Sandswept Shaitan](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/)
    - No Achievement: [DynamicHollowKnight](https://thunderstore.io/package/UndyingDuck/HollowKnightMerc/), renamed to "Vessel"
  - [Paladin](https://thunderstore.io/package/Paladin_Alliance/PaladinMod/)
    - Spiritual Success: [Solar](https://thunderstore.io/package/KrononConspirator/Thy_Providence/)
    - Simulated: Specter
    - Providence: Aphelian
    - No Achievement: Drip, renamed to "Vine"
    - No Achievement: Minecraft, renamed to "Aether"
  - Artificer
    - Nemesis ([Frost Wisp](https://thunderstore.io/package/PlasmaCore3/Forgotten_Relics/)): [Crystallized](https://thunderstore.io/package/HIFU/Inferno/) (Moved from Survival), renamed to "Frozen"
    - Simulated: [Mech](https://thunderstore.io/package/LexLamb/MechArtificer/), renamed to "Advanced"
    - Providence: [Star Witch](https://thunderstore.io/package/KrononConspirator/MadVeteran_Skinpack/), renamed to "Hex"
    - Survival: [Light Dreamer](https://thunderstore.io/package/Dreams/LightDreamerSkin/), renamed to "Radiant"
    - No Achievement: [Tasque](https://thunderstore.io/package/TheBreadMan/TasqueManager/), renamed to "Rental Due"
  - Acrid
    - Nemesis (Blind Pest): [Blind](https://thunderstore.io/package/RetroInspired/Ratcrid/)
    - Simulated: [Kindred's Lizard](https://thunderstore.io/package/Arty_Boyos/KindredsLizard/), renamed to "[Abyssal](https://thunderstore.io/package/RetroInspired/Retros_Skinpack/)"
    - Providence: [Pale Rat](https://thunderstore.io/package/KrononConspirator/MadVeteran_Skinpack/), renamed to "Fossilized"
    - Survival: [Prehistoric](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/), renamed to "Restored"
    - No Achievement: [DRONE_001, DRONE_002](https://thunderstore.io/package/Heyimnoob/BioDroneAcrid/), renamed to "Adam" and "Eve"
  - REX (why is there like No Skins)
    - Simulated: [LunarVoid_REX](https://thunderstore.io/package/hilliurn/LunarVoid_REX/), renamed to "Flooded"
    - Providence: [Synergized](https://thunderstore.io/package/KrononConspirator/MadVeteran_Skinpack/)
    - Survival: [Lilly](https://thunderstore.io/package/Wolfo/LittleGameplayTweaks/)
  - Loader
    - [Special Achievement](https://thunderstore.io/package/PlasmaCore3/Forgotten_Relics/): [Space Cadet](https://thunderstore.io/package/eyeknow/High_Fashion_with_Loader/)
    - Nemesis (Scavenger): [Scavenged](https://thunderstore.io/package/KrononConspirator/Scavanger_Loader/), renamed to "KrilKril" like the other skin
    - Simulated: [Plum](https://thunderstore.io/package/eyeknow/High_Fashion_with_Loader/)
    - Providence: [Runestone](https://thunderstore.io/package/KrononConspirator/MadVeteran_Skinpack/)
    - Survival: [Lady Clockwork](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/)
    - No Achievement: [EzGreen](https://thunderstore.io/package/KrononConspirator/SirEz_Miner/), renamed to "Figurine"
  - Miner
    - Spiritual Success: Tundra (moved from special achievement), renamed to "Ghoul"
    - Simulated: Puple (moved from special achievement)
    - Providence: Blacksmith (moved from special achievement)
  - [Templar](https://thunderstore.io/package/TemplarBoyz/PlayableTemplar/)
    - Nemesis (Aurellionite): [Gilded](https://thunderstore.io/package/prodzpod/TemplarSkins/)
    - Mastery: [Forgotten](https://thunderstore.io/package/prodzpod/TemplarSkins/)
    - Spiritual Success: [Siphoned](https://thunderstore.io/package/prodzpod/TemplarSkins/)
    - Simulated: [Simulacrum](https://thunderstore.io/package/prodzpod/TemplarSkins/)
    - Providence: [Perfected](https://thunderstore.io/package/prodzpod/TemplarSkins/)
    - Survival: [Scorched](https://thunderstore.io/package/prodzpod/TemplarSkins/)
  - Railgunner
    - Special Achievement: [Invis](https://thunderstore.io/package/Takrak/RailgunnerAltTextures/), renamed to "Cloaked"
    - Nemesis (Alloy Worship Unit): [Solus](https://thunderstore.io/package/KrononConspirator/Solus_RailGunner/)
    - Simulated: [Night Ops](https://thunderstore.io/package/RetroInspired/Railgunner_Skins/)
    - Providence: [Aviator](https://thunderstore.io/package/KrononConspirator/MadVeteran_Skinpack/)
    - No Achievement: [White Death](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/), renamed to "[Rallypoint](https://thunderstore.io/package/RetroInspired/Railgunner_Skins/)"
  - Void Fiend
    - Nemesis (Void Devastator): [Void Jailer](https://thunderstore.io/package/TailLover/VoidJailerFiend/), renamed to "Plasma"
    - Simulated: [Exalted](https://thunderstore.io/package/KrononConspirator/MadVeteran_Skinpack/)
    - Providence: [Demon from the Deep](https://thunderstore.io/package/dotflare/LostThroughTimeSkinPack/), renamed to "Submerged"
    - Survival: [Void Dreamer](https://thunderstore.io/package/Dreams/VoidDreamerVFSKIN/), renamed to "Malignance"
    - No Achievement: [PEPSI MAN, PEPSI MAN (Classic)](https://thunderstore.io/package/SussyBnuuy/PEPSI_MAN_Void_Fiend/), renamed to "Caffeinated" and "Crystal"
- Artifacts: Added trials and codes for all artifacts in:
  - [Artifact of Dissimilarity](https://thunderstore.io/package/Wolfo/ArtifactOfDissimilarity/)
  - [ZetArtifacts](https://thunderstore.io/package/William758/ZetArtifacts/)
  - `●◆●` [Artifact of Potential](https://thunderstore.io/package/zombieseatflesh7/Artifact_of_Potential/)
  - `●■■` [Artifact of Blindness](https://thunderstore.io/package/HIFU/ArtifactOfBlindness/)
  - `■■■` [Artifact of Chosen](https://thunderstore.io/package/DogeTeam/ArtifactOfChosen/)
  - You are expected to have [CoolerLocus](https://thunderstore.io/package/Nuxlar/CoolerLocus/), [Forgotten Relics](https://thunderstore.io/package/PlasmaCore3/Forgotten_Relics/) and [Fogbound Lagoon](https://thunderstore.io/package/JaceDaDorito/FogboundLagoon/) to see all codes. EVERY stage now contains a code.

### Contents
- Reprogrammer: Equipment. 60s cooldown, toggle/configurable. Currently a reskin of Recycler. Transform a 3D Printer into a different one. Chance to transform it into a [Shrine of Repair](https://thunderstore.io/package/viliger/ShrineOfRepair/) instead. comes with an unlock achievement.
- Downpour: Experimental Difficulty. global difficulty also scales with time so it's like squared I think, meant to be an alternate to Monsoon. also toggle/configurable.
- Tutorials for new players / new players to the modpack. currently utilizes popups but will shift to less distracting methods if possible. (Modpack Mode Only)
- Changes main menu. 3 versions. (Modpack Mode Only)
- Displays modpack changelog. (Modpack Mode Only)

### Tweaks
- Toggle option to use full description in item pickup text
- [Paladin](https://thunderstore.io/package/Paladin_Alliance/PaladinMod/), [Miner](https://thunderstore.io/package/EnforcerGang/MinerUnearthed/), [Sniper](https://thunderstore.io/package/EnforcerGang/SniperClassic/) and [Enforcer](https://thunderstore.io/package/EnforcerGang/Enforcer/): Option to change grand mastery requirement to any difficulty. Changes "Grand Mastery" to "Survival" if [Inferno](https://thunderstore.io/package/HIFU/Inferno/) is selected (default).
- [Paladin](https://thunderstore.io/package/Paladin_Alliance/PaladinMod/): Fixed achievement names to be consistent with other survivors. (puts "Paladin: " in front of achievement names
- [Eggs Skills](https://thunderstore.io/package/egpimp/EggsSkills/): Fixes puncuations and capitalizations on achievements.
- [HuntressMomentum](https://thunderstore.io/package/TeamGoose/HuntressMomentum/): Fixes puncuations and capitalizations on skill.
- [PlayableTemplar](https://thunderstore.io/package/TemplarBoyz/PlayableTemplar/): Fixes puncuations and capitalizations on skill, and replaces survivor logbook icon with a HQ one.
- [ZetArtifacts](https://thunderstore.io/package/William758/ZetArtifacts/): More detailed description for Artifact of Tossing.
- [ZetArtifacts](https://thunderstore.io/package/William758/ZetArtifacts/): togglable config for Artifact of Revival to disable reviving between mithrix phases.
- [StageAesthetics](https://thunderstore.io/package/HIFU/StageAesthetic/): configurable chance for vanilla variant to appear instead of an alternate one.
- [Bubbet's Items](https://thunderstore.io/package/Bubbet/BubbetsItems/): Ability for void lunar items to be used in cleansing pool, chance for void lunar items to appear on shop.
- [Bubbet's Items](https://thunderstore.io/package/Bubbet/BubbetsItems/): Fixed Submerging Cistern's item display.
- [QuickRestart](https://thunderstore.io/package/AceOfShades/QuickRestart/) & [Photo Mode](https://thunderstore.io/package/Dragonyck/PhotoMode/): Makes button order more sensible.
- [Risk of Options](https://thunderstore.io/package/Rune580/Risk_Of_Options/): Ability to hide ROO categories. for modpack development.
- [Bulwark's Haunt](https://thunderstore.io/package/TheMysticSword/BulwarksHaunt/): "Fixes" Void Fiend achievement name.
- [Released From the Void](https://thunderstore.io/package/Anreol/ReleasedFromTheVoid/): Ability to disable modules of the mod.
- Customizable Focused Convergence stacking. configurable max range stack and max rate stack.
- Ability to reorder survivors, logbook challenges and skins. Ability to hide skins.
- [Forgotten Relics](https://thunderstore.io/package/PlasmaCore3/Forgotten_Relics/): Disables the auto-Bell Tower disable.
- [Forgotten Relics](https://thunderstore.io/package/PlasmaCore3/Forgotten_Relics/): Removes Sanctum Wisp from logbook (until it's implemented).
- [BossAntiSoftlock](https://thunderstore.io/package/JustDerb/BossAntiSoftlock/): Disables the text that appears every time a boss appears.
- [DiscordRichPresence](https://thunderstore.io/package/Cuno/DiscordRichPresence/): Removes log spam.
- togglable tweak to H3AD-5T v2, removes cooldown and stack increases damage instead. also affects [Empyrean Braces](https://thunderstore.io/package/PlasmaCore3/Forgotten_Relics/).
- Ability to see all artifacts, as well as display hints. (Vanilla artifact hints from [RoR2 Wiki](https://riskofrain2.fandom.com/wiki/Artifact_Hints)). Hint level configurable.
- [PassiveAgression](https://thunderstore.io/package/RandomlyAwesome/PassiveAgression/): Fixes puncuations and capitalizations on skill, and removes "None" for Engineer's passive (why would you Not have a buff).

### Logbook
Added logbook entries for missing things. may change as mods gets updates.
- Vanilla
  - Trophy Hunter's Tricorn (Consumed)
  - Fuel Cells
  - Lunar Chimera (with [WolfosQualityOfLife](https://thunderstore.io/package/Wolfo/WolfoQualityOfLife/))
  - Malachite Urchin
  - Geep
  - Gip
  - Newt (kinda)
- [Bubbet's Items](https://thunderstore.io/package/Bubbet/BubbetsItems/)
  - Bone Visor
  - Gem Carapace
  - Eternal Slug
  - Mechanical Snail
  - Acid Soaked Blindfold
  - Bunny Foot
  - Escape Plan
  - Abundant Hourglass
  - Jellied Soles
  - Scintillating Jet
  - Shifted Quartz
  - Submerging Cistern
  - Deficient Clepsydra
  - Adrenalline Sprout
  - Lost Seers Tragedy
  - Zealotry Embrace
  - Abstracted Locus
  - Deluged Circlet
  - Clumped Sand
  - Deep Descent
  - Hydrophily
  - Imperfection
  - Orb Of Falsity
  - Seeping Ocean
  - Tarnished
  - Broken Clock
  - Gyroscopic Whisk
  - Holographic Donkey
- [StormyItems](https://thunderstore.io/package/Quickstraw/StormyItems/)
  - Sharp Anchor
  - Cracked Halo
  - Charged Urchin
  - Illegal Drone Coolant
  - Abyssal Talon
- [Forgotten Relics](https://thunderstore.io/package/PlasmaCore3/Forgotten_Relics/)
  - Relic of Energy
  - Empyrean Braces
  - Frost Wisp
  - Slumbering Satellite
  - Forgotten Haven
- [Archaic Wisp](https://thunderstore.io/package/Moffein/Archaic_Wisp/)
- [Released From the Void](https://thunderstore.io/package/Anreol/ReleasedFromTheVoid/): Iota Construct
- [PlayableTemplar](https://thunderstore.io/package/TemplarBoyz/PlayableTemplar/): Templar

### Korean Support (BETA, INCOMPLETE)
currently implemented:
- [QuickRestart](https://thunderstore.io/package/AceOfShades/QuickRestart/)
- [Photo Mode](https://thunderstore.io/package/Dragonyck/PhotoMode/)
- [BetterUI](https://thunderstore.io/package/XoXFaby/BetterUI/)
- [Artifact of Chosen](https://thunderstore.io/package/DogeTeam/ArtifactOfChosen/)
- [Shrine of Repair](https://thunderstore.io/package/viliger/ShrineOfRepair/) (Modpack Mode Only)
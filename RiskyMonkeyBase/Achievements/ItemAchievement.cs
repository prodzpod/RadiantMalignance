using HarmonyLib;
using R2API;
using RiskyMonkeyBase.Contents;
using RiskyMonkeyBase.Tweaks;
using RoR2;
using RoR2.Achievements;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RiskyMonkeyBase.Achievements
{
    public class ItemAchievement
    {
        public static Dictionary<string, UnlockableDef> unlockables;
        public static void Patch()
        {
            unlockables = new();
            MakeUnlockable("LunarSun");
            MakeUnlockable("RandomEquipmentTrigger");
            if (Reference.Mods("com.Ner0ls.HolyCrapForkIsBack")) MakeHCFB();
            if (Reference.Mods("bubbet.bubbetsitems")) MakeBubbets();
            if (Reference.Mods("com.Viliger.ShrineOfRepair") && Reference.ReprogrammerActivate.Value) MakeUnlockable("RM_reprogrammer"); // tfw you hack your own item !!! why
            if (Reference.Mods("com.Hex3.Hex3Mod")) MakeHex3();
            if (Reference.Mods("com.groovesalad.GrooveSaladSpikestripContent", "com.plasmacore.PlasmaCoreSpikestripContent", "com.heyimnoob.NoopSpikestripContent")) MakeSpikestrip();
            if (Reference.Mods("com.ContactLight.LostInTransit")) MakeLostInTransit();
            if (Reference.Mods("com.themysticsword.mysticsitems")) MakeMystics();
            AchievementManager.onAchievementsRegistered += PostPatch;
        }

        public static void MakeHCFB()
        {
            if (HasHCFBType(typeof(HolyCrapForkIsBack.Items.Knife))) MakeUnlockable("HCFB_ITEM_KNIFE");
            if (HasHCFBType(typeof(HolyCrapForkIsBack.Items.Chopsticks))) MakeUnlockable("HCFB_ITEM_CHOPSTICKS");
            if (HasHCFBType(typeof(HolyCrapForkIsBack.Items.MoltenFork))) MakeUnlockable("HCFB_ITEM_MOLTEN_FORK");
        }

        public static bool HasHCFBType(Type type)
        {
            return ((HolyCrapForkIsBack.Main)HolyCrapForkIsBack.Main.PInfo.Instance).Items.Exists(item => item.GetType() == type);
        }

        public static void MakeBubbets()
        {
            if (HasBubbetsType(typeof(BubbetsItems.Items.BarrierItems.GemCarapace))) MakeUnlockable("ItemDefGemCarapace");
            if (HasBubbetsType(typeof(BubbetsItems.Items.BarrierItems.EternalSlug))) MakeUnlockable("ItemDefEternalSlug");
            if (HasBubbetsType(typeof(BubbetsItems.Items.BunnyFoot))) MakeUnlockable("ItemDefBunnyFoot");
            if (HasBubbetsType(typeof(BubbetsItems.Items.EscapePlan))) MakeUnlockable("ItemDefEscapePlan");
            foreach (var equipment in BubbetsItems.EquipmentBase.Equipments) if (equipment is BubbetsItems.Equipments.WildlifeCamera && equipment.Enabled.Value) MakeUnlockable("EquipmentDefWildlifeCamera");
        }

        public static bool HasBubbetsType(Type type)
        {
            foreach (var item in BubbetsItems.ItemBase.Items) if (item.GetType() == type) return item.Enabled.Value;
            return false;
        }

        public static void MakeHex3()
        {
            if (Hex3Mod.Main.HopooEgg_Enable.Value && Hex3Mod.Main.HopooEgg_Load.Value) MakeUnlockable("HopooEgg");
            if (Hex3Mod.Main.ScavengersPack_Enable.Value && Hex3Mod.Main.ScavengersPack_Load.Value) MakeUnlockable("ScavengersPouch");
        }

        public static void MakeSpikestrip()
        {
            if (HasSpikestrip("ITEM_DOTPROCS")) MakeUnlockable("ITEM_DOTPROCS");
            if (HasSpikestrip("ITEM_SINGULARITY")) MakeUnlockable("ITEM_SINGULARITY");
            if (HasSpikestrip("ITEM_PAINBOX") && Reference.Mods("com.themysticsword.bulwarkshaunt")) MakeUnlockable("ITEM_PAINBOX");
            if (HasSpikestrip("ITEM_SHIELDREMOVAL")) MakeUnlockable("ITEM_SHIELDREMOVAL");
            if (ItemAPI.EquipmentDefinitions.ToList().Exists(x => x.EquipmentDef.name == "EQUIPMENT_DOUBLEITEMS")) MakeUnlockable("EQUIPMENT_DOUBLEITEMS");
        }

        public static bool HasSpikestrip(string name)
        {
            return ItemAPI.ItemDefinitions.ToList().Exists(x => x.ItemDef.name == name);
        }

        public static void MakeLostInTransit()
        {
            if (!Reference.HitListDisable.Value) MakeUnlockable("HitList");
        }

        public static void MakeMystics()
        {
            if (HasMystics("MysticsItems_MarwanAsh1")) MakeUnlockable("MysticsItems_MarwanAsh1");
            if (HasMystics("MysticsItems_ScratchTicket")) MakeUnlockable("MysticsItems_ScratchTicket");
            if (HasMystics("MysticsItems_ExtraShrineUse")) MakeUnlockable("MysticsItems_ExtraShrineUse");
            if (!MysticsItems.ConfigManager.General.disabledEquipment.Keys.Any(x => EquipmentCatalog.GetEquipmentDef(x).name == "MysticsItems_FragileMask")) MakeUnlockable("MysticsItems_FragileMask");
        }

        public static bool HasMystics(string name)
        {
            return !MysticsItems.ConfigManager.General.disabledItems.Keys.Any(x => ItemCatalog.GetItemDef(x).name == name);
        }

        public static void PostPatch()
        {
            AddUnlockable("LunarSun");
            AddUnlockable("RandomEquipmentTrigger");
            if (Reference.Mods("com.Ner0ls.HolyCrapForkIsBack"))
            {
                if (unlockables.ContainsKey("HCFB_ITEM_KNIFE")) AddUnlockable("HCFB_ITEM_KNIFE");
                if (unlockables.ContainsKey("HCFB_ITEM_CHOPSTICKS")) AddUnlockable("HCFB_ITEM_CHOPSTICKS");
                if (unlockables.ContainsKey("HCFB_ITEM_MOLTEN_FORK")) AddUnlockable("HCFB_ITEM_MOLTEN_FORK");
            }
            if (Reference.Mods("bubbet.bubbetsitems"))
            {
                if (unlockables.ContainsKey("ItemDefGemCarapace")) AddUnlockable("ItemDefGemCarapace");
                if (unlockables.ContainsKey("ItemDefEternalSlug")) AddUnlockable("ItemDefEternalSlug");
                if (unlockables.ContainsKey("ItemDefBunnyFoot")) AddUnlockable("ItemDefBunnyFoot");
                if (unlockables.ContainsKey("ItemDefEscapePlan")) AddUnlockable("ItemDefEscapePlan");
                if (unlockables.ContainsKey("EquipmentDefWildlifeCamera")) AddUnlockableEquipment("EquipmentDefWildlifeCamera");
            }
            if (Reference.Mods("com.Viliger.ShrineOfRepair") && Reference.ReprogrammerActivate.Value) AddUnlockableEquipment("RM_reprogrammer");
            if (Reference.Mods("com.Hex3.Hex3Mod"))
            {
                if (unlockables.ContainsKey("HopooEgg")) AddUnlockable("HopooEgg");
                if (unlockables.ContainsKey("ScavengersPouch")) AddUnlockable("ScavengersPouch");
            }
            if (Reference.Mods("com.groovesalad.GrooveSaladSpikestripContent", "com.plasmacore.PlasmaCoreSpikestripContent", "com.heyimnoob.NoopSpikestripContent"))
            {
                if (unlockables.ContainsKey("ITEM_DOTPROCS")) AddUnlockable("ITEM_DOTPROCS");
                if (unlockables.ContainsKey("ITEM_SINGULARITY")) AddUnlockable("ITEM_SINGULARITY");
                if (unlockables.ContainsKey("ITEM_PAINBOX")) AddUnlockable("ITEM_PAINBOX");
                if (unlockables.ContainsKey("ITEM_SHIELDREMOVAL")) AddUnlockable("ITEM_SHIELDREMOVAL");
                if (unlockables.ContainsKey("EQUIPMENT_DOUBLEITEMS")) AddUnlockableEquipment("EQUIPMENT_DOUBLEITEMS");
            }
            if (Reference.Mods("com.ContactLight.LostInTransit") && unlockables.ContainsKey("HitList")) AddUnlockable("HitList");
            if (Reference.Mods("com.themysticsword.mysticsitems"))
            {
                if (unlockables.ContainsKey("MysticsItems_MarwanAsh1")) 
                {
                    AddUnlockable("MysticsItems_MarwanAsh1");
                    ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("MysticsItems_MarwanAsh2")).unlockableDef = unlockables["MysticsItems_MarwanAsh1"];
                    PickupCatalog.GetPickupDef(PickupCatalog.FindPickupIndex(ItemCatalog.FindItemIndex("MysticsItems_MarwanAsh2"))).unlockableDef = unlockables["MysticsItems_MarwanAsh1"];
                    ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("MysticsItems_MarwanAsh3")).unlockableDef = unlockables["MysticsItems_MarwanAsh1"];
                    PickupCatalog.GetPickupDef(PickupCatalog.FindPickupIndex(ItemCatalog.FindItemIndex("MysticsItems_MarwanAsh3"))).unlockableDef = unlockables["MysticsItems_MarwanAsh1"];
                }
                if (unlockables.ContainsKey("MysticsItems_ScratchTicket")) AddUnlockable("MysticsItems_ScratchTicket");
                if (unlockables.ContainsKey("MysticsItems_ExtraShrineUse")) AddUnlockable("MysticsItems_ExtraShrineUse");
                if (unlockables.ContainsKey("MysticsItems_FragileMask")) AddUnlockableEquipment("MysticsItems_FragileMask");
                Sprite icon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/unlocks/texItemMysticsItems_GateChalice.png");
                UnlockableDef def = PickupCatalog.GetPickupDef(PickupCatalog.FindPickupIndex(ItemCatalog.FindItemIndex("MysticsItems_RiftLens"))).unlockableDef;
                AchievementManager.GetAchievementDefFromUnlockable(def.cachedName).achievedIcon = icon;
                def.achievementIcon = icon;
                EquipmentCatalog.GetEquipmentDef(EquipmentCatalog.FindEquipmentIndex("MysticsItems_GateChalice")).unlockableDef = def;
                PickupCatalog.GetPickupDef(PickupCatalog.FindPickupIndex(EquipmentCatalog.FindEquipmentIndex("MysticsItems_GateChalice"))).unlockableDef = def;
            }
        }

        public static void MakeUnlockable(string name)
        {
            if (RiskyMonkeyAchievements.achievementBlacklist.Contains("Items." + name)) return;
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Items." + name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            unlockables.Add(name, unlockableDef);
            RiskyMonkeyAchievements.Log("Registered Unlockable " + name);
        }

        public static void AddUnlockable(string itemName)
        {
            if (RiskyMonkeyAchievements.achievementBlacklist.Contains("Items." + itemName)) return;
            Sprite icon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/unlocks/texItem" + itemName + ".png");
            ItemDef def = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(itemName));
            UnlockableDef unlockableDef = unlockables[itemName];
            RiskyMonkeyAchievements.Log("Fetched Unlockable " + itemName);
            unlockableDef.nameToken = def.nameToken;
            unlockableDef.achievementIcon = icon;
            def.unlockableDef = unlockableDef;
            PickupCatalog.GetPickupDef(PickupCatalog.FindPickupIndex(def.itemIndex)).unlockableDef = unlockableDef;
            AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName).achievedIcon = icon;
        }

        public static void AddUnlockableEquipment(string equipmentName)
        {
            if (RiskyMonkeyAchievements.achievementBlacklist.Contains("Items." + equipmentName)) return;
            Sprite icon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/unlocks/texItem" + equipmentName + ".png");
            EquipmentDef def = EquipmentCatalog.GetEquipmentDef(EquipmentCatalog.FindEquipmentIndex(equipmentName));
            UnlockableDef unlockableDef = unlockables[equipmentName];
            RiskyMonkeyBase.Log.LogDebug("Fetched Unlockable " + equipmentName);
            unlockableDef.nameToken = def.nameToken;
            unlockableDef.achievementIcon = icon;
            def.unlockableDef = unlockableDef;
            PickupCatalog.GetPickupDef(PickupCatalog.FindPickupIndex(def.equipmentIndex)).unlockableDef = unlockableDef;
            AchievementIndex idx = AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName).index;
            AchievementManager.achievementDefs[idx.intValue].achievedIcon = icon;
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_LunarSun", "Items.LunarSun", null, null)]
    public class LunarSunAchievement : BaseAchievement
    {
        public override void OnInstall() { base.OnInstall(); Inventory.onInventoryChangedGlobal += OnInventoryChangedGlobal; }
        public override void OnUninstall() { Inventory.onInventoryChangedGlobal -= OnInventoryChangedGlobal; base.OnUninstall(); }
        public void OnInventoryChangedGlobal(Inventory inv) { if (localUser.cachedBody != null && localUser.cachedBody.inventory != null && inv == localUser.cachedBody.inventory && inv.itemStacks.Length != 0 && inv.itemStacks.Where((_, i) => Run.instance.IsItemAvailable((ItemIndex)i)).Max() >= 50) Grant(); }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_RandomEquipmentTrigger", "Items.RandomEquipmentTrigger", null, null)]
    public class RandomEquipmentTriggerAchievement : BaseAchievement
    {
        public override void OnInstall() { base.OnInstall(); Inventory.onInventoryChangedGlobal += OnInventoryChangedGlobal; }
        public override void OnUninstall() { Inventory.onInventoryChangedGlobal -= OnInventoryChangedGlobal; base.OnUninstall(); }
        public void OnInventoryChangedGlobal(Inventory inv) { if (localUser.cachedBody != null && localUser.cachedBody.inventory != null && !localUser.cachedBody.inventory.currentEquipmentState.Equals(EquipmentState.empty) && localUser.cachedBody.inventory.currentEquipmentState.equipmentDef != null && localUser.cachedBody.inventory.currentEquipmentState.equipmentDef.cooldown * localUser.cachedBody.inventory.CalculateEquipmentCooldownScale() <= 0.5f) Grant(); }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_HCFB_ITEM_KNIFE", "Items.HCFB_ITEM_KNIFE", null, null, "com.Ner0ls.HolyCrapForkIsBack")]
    public class HCFB_ITEM_KNIFEAchievement : BaseAchievement
    {
        public static float crit = 0;
        public static bool OnlyRegisterIf() { return ItemAchievement.HasHCFBType(typeof(HolyCrapForkIsBack.Items.Knife)); }
        public override void OnInstall() { base.OnInstall(); Run.onClientGameOverGlobal += OnClientGameOverGlobal; On.RoR2.CharacterBody.RecalculateStats += RecalculateStats; }
        public override void OnUninstall() { Run.onClientGameOverGlobal -= OnClientGameOverGlobal; On.RoR2.CharacterBody.RecalculateStats -= RecalculateStats; base.OnUninstall(); }
        public void RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self) { orig(self); if (localUser != null && localUser.cachedBody != null && localUser.cachedBody == self) crit = self.baseCrit; }
        public void OnClientGameOverGlobal(Run run, RunReport runReport) { if ((bool)runReport.gameEnding && runReport.gameEnding.isWin && crit <= 0) Grant(); }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_HCFB_ITEM_CHOPSTICKS", "Items.HCFB_ITEM_CHOPSTICKS", null, null, "com.Ner0ls.HolyCrapForkIsBack")]
    public class HCFB_ITEM_CHOPSTICKSAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasHCFBType(typeof(HolyCrapForkIsBack.Items.Chopsticks)); }
        public override void OnInstall() { base.OnInstall(); RoR2Application.onUpdate += OnUpdate; }
        public override void OnUninstall() { RoR2Application.onUpdate -= OnUpdate; base.OnUninstall(); }
        public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.critMultiplier >= 4f) Grant(); }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_HCFB_ITEM_MOLTEN_FORK", "Items.HCFB_ITEM_MOLTEN_FORK", null, null, "com.Ner0ls.HolyCrapForkIsBack")]
    public class HCFB_ITEM_MOLTEN_FORKAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasHCFBType(typeof(HolyCrapForkIsBack.Items.MoltenFork)); }
        public override void OnInstall() { base.OnInstall(); On.RoR2.CharacterBody.AddBuff_BuffDef += AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex += AddBuff; }
        public override void OnUninstall() { On.RoR2.CharacterBody.AddBuff_BuffDef -= AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex -= AddBuff; base.OnUninstall(); }
        public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffDef orig, CharacterBody self, BuffDef buff)
        {
            if ((bool)self && self.teamComponent.teamIndex != TeamIndex.Player && self.activeBuffsList != null && self.GetBuffCount(RoR2Content.Buffs.OnFire) >= 50) Grant(); orig(self, buff);
        }
        public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffIndex orig, CharacterBody self, BuffIndex buff)
        {
            if ((bool)self && self.teamComponent.teamIndex != TeamIndex.Player && self.activeBuffsList != null && self.GetBuffCount(RoR2Content.Buffs.OnFire) >= 50) Grant(); orig(self, buff);
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_ItemDefGemCarapace", "Items.ItemDefGemCarapace", null, null, "bubbet.bubbetsitems")]
    public class ItemDefGemCarapaceAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasBubbetsType(typeof(BubbetsItems.Items.BarrierItems.GemCarapace)); }
        public override void OnInstall() { base.OnInstall(); RoR2Application.onUpdate += OnUpdate; } public override void OnUninstall() { RoR2Application.onUpdate -= OnUpdate; base.OnUninstall(); }
        public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (!localUser.cachedBody.healthComponent.godMode && localUser.cachedBody.healthComponent.barrier >= localUser.cachedBody.maxBarrier) Grant(); }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_ItemDefEternalSlug", "Items.ItemDefEternalSlug", null, null, "bubbet.bubbetsitems")]
    public class ItemDefEternalSlugAchievement : BaseAchievement
    {
        public float time = 60f;
        public static bool OnlyRegisterIf() { return ItemAchievement.HasBubbetsType(typeof(BubbetsItems.Items.BarrierItems.EternalSlug)); }
        public override void OnInstall() { base.OnInstall(); RoR2Application.onUpdate += OnUpdate; } public override void OnUninstall() { RoR2Application.onUpdate -= OnUpdate; base.OnUninstall(); }
        public void OnUpdate() 
        { 
            if (localUser == null || localUser.cachedBody == null) return;
            if (localUser.cachedBody.healthComponent.barrier > 0)
            {
                time -= Time.fixedDeltaTime;
                if (time <= 0) Grant();
            }
            else time = 60f;
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_ItemDefBunnyFoot", "Items.ItemDefBunnyFoot", null, null, "bubbet.bubbetsitems")]
    public class ItemDefBunnyFootAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasBubbetsType(typeof(BubbetsItems.Items.BunnyFoot)); }
        public override void OnInstall() { base.OnInstall(); RoR2Application.onUpdate += OnUpdate; } public override void OnUninstall() { RoR2Application.onUpdate -= OnUpdate; base.OnUninstall(); }
        public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.maxJumpCount >= 5f) Grant(); }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_ItemDefEscapePlan", "Items.ItemDefEscapePlan", null, null, "bubbet.bubbetsitems")]
    public class ItemDefEscapePlanAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasBubbetsType(typeof(BubbetsItems.Items.EscapePlan)); }
        public override void OnInstall() { base.OnInstall(); On.RoR2.HealthComponent.TakeDamage += OnDamageTaken; } public override void OnUninstall() { On.RoR2.HealthComponent.TakeDamage -= OnDamageTaken; base.OnUninstall(); }
        public void OnDamageTaken(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo info)
        {
            orig(self, info);
            if (localUser != null && localUser.cachedBody != null && self == localUser.cachedBody.healthComponent)
                if ((info.damageType & DamageType.NonLethal) == 0 && 0 < self.combinedHealthFraction && self.combinedHealthFraction <= 0.01f) Grant();
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_EquipmentDefWildlifeCamera", "Items.EquipmentDefWildlifeCamera", null, null, "bubbet.bubbetsitems")]
    public class EquipmentDefWildlifeCameraAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { foreach (var equipment in BubbetsItems.EquipmentBase.Equipments) if (equipment is BubbetsItems.Equipments.WildlifeCamera) return equipment.Enabled.Value; return false; }
        public override void OnInstall() { base.OnInstall(); RoR2Application.onUpdate += OnUpdate; } public override void OnUninstall() { RoR2Application.onUpdate -= OnUpdate; base.OnUninstall(); }
        public void OnUpdate() { if (Input.GetKeyDown(KeyCode.F12)) Grant(); }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_RM_reprogrammer", "Items.RM_reprogrammer", null, null, "com.Viliger.ShrineOfRepair")]
    public class EquipmentDefRM_reprogrammer : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return Reference.ReprogrammerActivate.Value; }
        public override void OnInstall() { base.OnInstall(); ShrineOfRepairHook.onRepair += OnRepair; } public override void OnUninstall() { ShrineOfRepairHook.onRepair -= OnRepair; base.OnUninstall(); }
        public void OnRepair(Interactor interactor, PickupIndex idx) 
        { 
            if (PickupCatalog.GetPickupDef(idx).itemIndex != ItemIndex.None 
                && (Reference.Mods("prodzpod.LimitedInteractables") && IsStackAtOnceLimited(PickupCatalog.GetPickupDef(idx).itemIndex))
                && interactor.GetComponent<CharacterBody>() == localUser.cachedBody 
                && localUser.cachedBody.inventory.GetItemCount(PickupCatalog.GetPickupDef(idx).itemIndex) >= 10) 
                Grant(); 
        }

        public static bool IsStackAtOnceLimited(ItemIndex idx)
        {
            return !LimitedInteractables.ShrineRepair.repairList.Contains(ItemCatalog.GetItemDef(idx).name) || LimitedInteractables.Main.RepairStackAtOnce.Value >= 10;
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_HopooEgg", "Items.HopooEgg", null, null, "com.Hex3.Hex3Mod")]
    public class HopooEggAchievement : BaseAchievement
    {   
        public static bool OnlyRegisterIf() { return Hex3Mod.Main.HopooEgg_Enable.Value && Hex3Mod.Main.HopooEgg_Load.Value; }
        public override void OnInstall() { base.OnInstall(); On.RoR2.TeleportHelper.OnTeleport += OnTeleport; }
        public override void OnUninstall() { On.RoR2.TeleportHelper.OnTeleport -= OnTeleport; base.OnUninstall(); }
        public void OnTeleport(On.RoR2.TeleportHelper.orig_OnTeleport orig, GameObject gameObject, Vector3 newPosition, Vector3 delta)
        {
            if (localUser != null && localUser.cachedBody != null && localUser.cachedBodyObject == gameObject && delta.y <= -100) Grant();
            orig(gameObject, newPosition, delta);
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_ScavengersPouch", "Items.ScavengersPouch", null, null, "com.Hex3.Hex3Mod")]
    public class ScavengersPouchAchievement : BaseAchievement
    {
        public static List<string> nonstandardConsumeds = new() { "HCFB_ITEM_BROKEN_CHOPSTICKS", "VV_ITEM_BROKEN_MESS", "VV_ITEM_EMPTY_VIALS", "MysticsItems_GhostAppleWeak", "MysticsItems_LimitedArmorBroken" };
        public static List<string> consumedEquips = new() { "BossHunterConsumed" };
        public static bool OnlyRegisterIf() { return Hex3Mod.Main.ScavengersPack_Enable.Value && Hex3Mod.Main.ScavengersPack_Load.Value; }
        public override void OnInstall() { base.OnInstall(); Inventory.onInventoryChangedGlobal += OnInventoryChangedGlobal; }
        public override void OnUninstall() { Inventory.onInventoryChangedGlobal -= OnInventoryChangedGlobal; base.OnUninstall(); }
        public void OnInventoryChangedGlobal(Inventory inv)
        {
            if (localUser?.cachedBody?.inventory != inv) return;
            int ret = 0;
            for (var i = 0; i < inv.itemStacks.Length; i++)
            {
                string name = ItemCatalog.GetItemDef((ItemIndex)i).name;
                if (name.ToUpper().Contains("CONSUMED") || nonstandardConsumeds.Contains(name)) ret += inv.itemStacks[i];
            }
            for (var i = 0; i < inv.equipmentStateSlots.Length; i++)
            {
                if (inv.equipmentStateSlots[i].equipmentIndex != EquipmentIndex.None && consumedEquips.Contains(inv.equipmentStateSlots[i].equipmentDef.name)) ret++;
            }
            if (ret >= 20) Grant();
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_ITEM_DOTPROCS", "Items.ITEM_DOTPROCS", null, null, "com.groovesalad.GrooveSaladSpikestripContent", "com.plasmacore.PlasmaCoreSpikestripContent", "com.heyimnoob.NoopSpikestripContent")]
    public class ITEM_DOTPROCSAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasSpikestrip("ITEM_DOTPROCS"); }
        public override void OnInstall() { base.OnInstall(); On.RoR2.CharacterBody.AddBuff_BuffDef += AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex += AddBuff; }
        public override void OnUninstall() { On.RoR2.CharacterBody.AddBuff_BuffDef -= AddBuff; On.RoR2.CharacterBody.AddBuff_BuffIndex -= AddBuff; base.OnUninstall(); }
        public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffDef orig, CharacterBody self, BuffDef buff)
        {
            if ((bool)self && self.teamComponent.teamIndex != TeamIndex.Player && self.activeBuffsList != null && self.GetBuffCount(RoR2Content.Buffs.Bleeding) >= 10 && self.GetBuffCount(DLC1Content.Buffs.Fracture) >= 10) Grant(); orig(self, buff);
        }
        public void AddBuff(On.RoR2.CharacterBody.orig_AddBuff_BuffIndex orig, CharacterBody self, BuffIndex buff)
        {
            if ((bool)self && self.teamComponent.teamIndex != TeamIndex.Player && self.activeBuffsList != null && self.GetBuffCount(RoR2Content.Buffs.Bleeding) >= 10 && self.GetBuffCount(DLC1Content.Buffs.Fracture) >= 10) Grant(); orig(self, buff);
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_ITEM_SINGULARITY", "Items.ITEM_SINGULARITY", null, null, "com.groovesalad.GrooveSaladSpikestripContent", "com.plasmacore.PlasmaCoreSpikestripContent", "com.heyimnoob.NoopSpikestripContent")]
    public class ITEM_SINGULARITYAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasSpikestrip("ITEM_SINGULARITY"); }
        public override void OnInstall() { base.OnInstall(); Run.onClientGameOverGlobal += OnGameOver; }
        public override void OnUninstall() { Run.onClientGameOverGlobal -= OnGameOver; base.OnUninstall(); }
        public void OnGameOver(Run self, RunReport report) { if (report != null && report.gameEnding.isWin && self.GetRunStopwatch() < 1200f) Grant(); }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_ITEM_PAINBOX", "Items.ITEM_PAINBOX", null, null, "com.groovesalad.GrooveSaladSpikestripContent", "com.plasmacore.PlasmaCoreSpikestripContent", "com.heyimnoob.NoopSpikestripContent", "com.themysticsword.bulwarkshaunt")]
    public class ITEM_PAINBOXAchievement : BaseAchievement
    {
        public static bool win = false;
        public static bool OnlyRegisterIf() { return ItemAchievement.HasSpikestrip("ITEM_PAINBOX"); }
        public override void OnInstall() { base.OnInstall(); RoR2Application.onFixedUpdate += OnUpdate; Run.onClientGameOverGlobal += OnClientGameOverGlobal; Stage.onStageStartGlobal += OnStart; }
        public override void OnUninstall() { RoR2Application.onFixedUpdate -= OnUpdate; Run.onClientGameOverGlobal -= OnClientGameOverGlobal; Stage.onStageStartGlobal -= OnStart; base.OnUninstall(); }
        public void OnStart(Stage self) { if (self.sceneDef.cachedName == "BulwarksHaunt_GhostWave") win = true; }
        public void OnGameOver(Run self) { win = false; }
        public void OnUpdate()
        {
            if (win && localUser != null && localUser.cachedBody != null && localUser.cachedBody.healthComponent.combinedHealthFraction < 1) win = false;
        }
        public void OnClientGameOverGlobal(Run run, RunReport report)
        {
            if (report.gameEnding.isWin && win) Grant();
            win = false;
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_ITEM_SHIELDREMOVAL", "Items.ITEM_SHIELDREMOVAL", null, null, "com.groovesalad.GrooveSaladSpikestripContent", "com.plasmacore.PlasmaCoreSpikestripContent", "com.heyimnoob.NoopSpikestripContent")]
    public class ITEM_SHIELDREMOVALAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasSpikestrip("ITEM_SHIELDREMOVAL"); }
        public override void OnInstall() { base.OnInstall(); RoR2Application.onUpdate += OnUpdate; }
        public override void OnUninstall() { RoR2Application.onUpdate -= OnUpdate; base.OnUninstall(); }
        public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.maxHealth >= 2f && localUser.cachedBody.maxShield >= 2000) Grant(); }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_EQUIPMENT_DOUBLEITEMS", "Items.EQUIPMENT_DOUBLEITEMS", null, null, "com.groovesalad.GrooveSaladSpikestripContent", "com.plasmacore.PlasmaCoreSpikestripContent", "com.heyimnoob.NoopSpikestripContent")]
    public class EQUIPMENT_DOUBLEITEMSAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAPI.EquipmentDefinitions.ToList().Exists(x => x.EquipmentDef.name == "EQUIPMENT_DOUBLEITEMS"); }
        public override void OnInstall() { base.OnInstall(); Inventory.onInventoryChangedGlobal += OnInventoryChangedGlobal; }
        public override void OnUninstall() { Inventory.onInventoryChangedGlobal -= OnInventoryChangedGlobal; base.OnUninstall(); }
        public void OnInventoryChangedGlobal(Inventory inv)
        {
            if (localUser?.cachedBody?.inventory != inv) return;
            for (var i = 0; i < inv.itemStacks.Length; i++)
            {
                ItemDef def = ItemCatalog.GetItemDef((ItemIndex)i);
                if (!Run.instance.availableTier1DropList.Contains(PickupCatalog.FindPickupIndex(def.itemIndex))) continue;
                if (inv.itemStacks[i] == 0) 
                {
                    bool found = false;
                    foreach (var pair in ItemCatalog.itemRelationships[DLC1Content.ItemRelationshipTypes.ContagiousItem].Where(x => x.itemDef1.itemIndex == (ItemIndex)i))
                    {
                        if (inv.GetItemCount(pair.itemDef2) > 0)
                        {
                            found = true; 
                            break;
                        }
                    }
                    if (!found) return;
                }
            }
            Grant();
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_HitList", "Items.HitList", null, null, "com.ContactLight.LostInTransit")]
    public class HitListAchievement : BaseAchievement
    {
        public static bool win = false;
        public static bool OnlyRegisterIf() { return !Reference.HitListDisable.Value; }
        public override void OnInstall() { base.OnInstall(); GlobalEventManager.onCharacterDeathGlobal += OnCharacterDeathGlobal; TeleporterInteraction.onTeleporterChargedGlobal += OnTeleporter; Stage.onStageStartGlobal += OnStart; Run.onRunDestroyGlobal += OnGameOver; }
        public override void OnUninstall() { GlobalEventManager.onCharacterDeathGlobal -= OnCharacterDeathGlobal; TeleporterInteraction.onTeleporterChargedGlobal -= OnTeleporter; Stage.onStageStartGlobal -= OnStart; Run.onRunDestroyGlobal -= OnGameOver; base.OnUninstall(); }
        public void OnStart(Stage self) { win = true; }
        public void OnGameOver(Run self) { win = false; }
        public void OnCharacterDeathGlobal(DamageReport report)
        {
            if (report.attackerMaster != null && report.attackerMaster == localUser.cachedMaster && !report.victimIsBoss) win = false;
        }
        public void OnTeleporter(TeleporterInteraction tp)
        {
            if (win) Grant();
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_MysticsItems_MarwanAsh1", "Items.MysticsItems_MarwanAsh1", null, null, "com.themysticsword.mysticsitems")]
    public class MysticsItems_MarwanAsh1Achievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasMystics("MysticsItems_MarwanAsh1"); }
        public override void OnInstall() { base.OnInstall(); RoR2Application.onUpdate += OnUpdate; }
        public override void OnUninstall() { RoR2Application.onUpdate -= OnUpdate; base.OnUninstall(); }
        public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.level >= 30) Grant(); }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_MysticsItems_ScratchTicket", "Items.MysticsItems_ScratchTicket", null, null, "com.themysticsword.mysticsitems")]
    public class MysticsItems_ScratchTicketAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasMystics("MysticsItems_ScratchTicket"); }
        public override void OnInstall() { base.OnInstall(); RoR2Application.onUpdate += OnUpdate; }
        public override void OnUninstall() { RoR2Application.onUpdate -= OnUpdate; base.OnUninstall(); }
        public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.bleedChance >= 99) Grant(); }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_MysticsItems_ExtraShrineUse", "Items.MysticsItems_ExtraShrineUse", null, null, "com.themysticsword.mysticsitems")]
    public class MysticsItems_ExtraShrineUseAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasMystics("MysticsItems_ExtraShrineUse"); }
        public override void OnInstall() { base.OnInstall(); On.RoR2.RouletteChestController.EjectPickupServer += OnEjectPickupServer; }
        public override void OnUninstall() { On.RoR2.RouletteChestController.EjectPickupServer -= OnEjectPickupServer; base.OnUninstall(); }
        public void OnEjectPickupServer(On.RoR2.RouletteChestController.orig_EjectPickupServer orig, RouletteChestController self, PickupIndex pickupIndex)
        {
            if (pickupIndex.itemIndex != ItemIndex.None && ItemCatalog.GetItemDef(pickupIndex.itemIndex).tier == ItemTier.Tier3) Grant();
            orig(self, pickupIndex);
        }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_MysticsItems_FragileMask", "Items.MysticsItems_FragileMask", null, null, "com.themysticsword.mysticsitems")]
    public class MysticsItems_FragileMaskAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return !MysticsItems.ConfigManager.General.disabledEquipment.Keys.Any(x => EquipmentCatalog.GetEquipmentDef(x).name == "MysticsItems_FragileMask"); }
        public override void OnInstall() { base.OnInstall(); RoR2Application.onUpdate += OnUpdate; }
        public override void OnUninstall() { RoR2Application.onUpdate -= OnUpdate; base.OnUninstall(); }
        public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (Run.instance.ruleBook.IsChoiceActive(RuleCatalog.FindRuleDef("Artifacts.Glass").FindChoice("On")) && localUser.cachedBody.healthComponent.fullCombinedHealth >= (localUser.cachedBody.baseMaxHealth + localUser.cachedBody.levelMaxHealth) * 10) Grant(); }

    }
}
    
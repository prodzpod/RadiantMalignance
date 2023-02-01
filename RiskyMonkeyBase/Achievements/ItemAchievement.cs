using HarmonyLib;
using R2API;
using RiskyMonkeyBase.Tweaks;
using RoR2;
using RoR2.Achievements;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RiskyMonkeyBase.Achievements
{
    public class ItemAchievement
    {
        public static Dictionary<string, UnlockableDef> unlockables;
        public static void Patch()
        {
            unlockables = new();
            if (Reference.Mods("com.Ner0ls.HolyCrapForkIsBack")) MakeHCFB();
            if (Reference.Mods("bubbet.bubbetsitems")) MakeBubbets();
            if (Reference.Mods("com.Viliger.ShrineOfRepair") && Reference.ReprogrammerActivate.Value) MakeUnlockable("RM_reprogrammer"); // tfw you hack your own item !!! why
            AchievementManager.onAchievementsRegistered += PostPatch;
        }

        public static void MakeHCFB()
        {
            if (HasHCFBType(typeof(HolyCrapForkIsBack.Items.Knife))) MakeUnlockable("HCFB_ITEM_KNIFE");
            if (HasHCFBType(typeof(HolyCrapForkIsBack.Items.Chopsticks))) MakeUnlockable("HCFB_ITEM_CHOPSTICKS");
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

        public static void PostPatch()
        {
            if (Reference.Mods("com.Ner0ls.HolyCrapForkIsBack"))
            {
                if (ItemCatalog.FindItemIndex("HCFB_ITEM_KNIFE") != ItemIndex.None) AddUnlockable("HCFB_ITEM_KNIFE");
                if (ItemCatalog.FindItemIndex("HCFB_ITEM_CHOPSTICKS") != ItemIndex.None) AddUnlockable("HCFB_ITEM_CHOPSTICKS");
            }
            if (Reference.Mods("bubbet.bubbetsitems"))
            {
                if (ItemCatalog.FindItemIndex("ItemDefGemCarapace") != ItemIndex.None) AddUnlockable("ItemDefGemCarapace");
                if (ItemCatalog.FindItemIndex("ItemDefEternalSlug") != ItemIndex.None) AddUnlockable("ItemDefEternalSlug");
                if (ItemCatalog.FindItemIndex("ItemDefBunnyFoot") != ItemIndex.None) AddUnlockable("ItemDefBunnyFoot");
                if (ItemCatalog.FindItemIndex("ItemDefEscapePlan") != ItemIndex.None) AddUnlockable("ItemDefEscapePlan");
                if (EquipmentCatalog.FindEquipmentIndex("EquipmentDefWildlifeCamera") != EquipmentIndex.None)
                {
                    Sprite icon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/unlocks/texItemEquipmentDefWildlifeCamera.png");
                    EquipmentDef def = EquipmentCatalog.GetEquipmentDef(EquipmentCatalog.FindEquipmentIndex("EquipmentDefWildlifeCamera"));
                    UnlockableDef unlockableDef = unlockables["EquipmentDefWildlifeCamera"];
                    RiskyMonkeyBase.Log.LogDebug("Fetched Unlockable EquipmentDefWildlifeCamera");
                    unlockableDef.nameToken = def.nameToken;
                    unlockableDef.achievementIcon = icon;
                    def.unlockableDef = unlockableDef;
                    PickupCatalog.GetPickupDef(PickupCatalog.FindPickupIndex(def.equipmentIndex)).unlockableDef = unlockableDef;
                    AchievementIndex idx = AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName).index;
                    AchievementManager.achievementDefs[idx.intValue].achievedIcon = icon;
                }
            }
            if (Reference.Mods("com.Viliger.ShrineOfRepair") && Reference.ReprogrammerActivate.Value)
            {
                Sprite icon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/unlocks/texItemReprogrammer.png");
                EquipmentDef def = EquipmentCatalog.GetEquipmentDef(EquipmentCatalog.FindEquipmentIndex("RM_reprogrammer"));
                UnlockableDef unlockableDef = unlockables["RM_reprogrammer"];
                RiskyMonkeyBase.Log.LogDebug("Fetched Unlockable RM_reprogrammer");
                unlockableDef.nameToken = def.nameToken;
                unlockableDef.achievementIcon = icon;
                def.unlockableDef = unlockableDef;
                PickupCatalog.GetPickupDef(PickupCatalog.FindPickupIndex(def.equipmentIndex)).unlockableDef = unlockableDef;
                AchievementIndex idx = AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName).index;
                AchievementManager.achievementDefs[idx.intValue].achievedIcon = icon;
            }
        }

        public static void MakeUnlockable(string name)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = "Items." + name;
            ContentAddition.AddUnlockableDef(unlockableDef);
            unlockables.Add(name, unlockableDef);
            RiskyMonkeyBase.Log.LogDebug("Registered Unlockable " + name);
        }
        public static void AddUnlockable(string itemName)
        {
            Sprite icon = RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/unlocks/texItem" + itemName + ".png");
            ItemDef def = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(itemName));
            UnlockableDef unlockableDef = unlockables[itemName];
            RiskyMonkeyBase.Log.LogDebug("Fetched Unlockable " + itemName);
            unlockableDef.nameToken = def.nameToken;
            unlockableDef.achievementIcon = icon;
            def.unlockableDef = unlockableDef;
            PickupCatalog.GetPickupDef(PickupCatalog.FindPickupIndex(def.itemIndex)).unlockableDef = unlockableDef;
            AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName).achievedIcon = icon;
        }
    }
    
    [RegisterModdedAchievement("RiskyMonkey_Items_HCFB_ITEM_KNIFE", "Items.HCFB_ITEM_KNIFE", null, null, "com.Ner0ls.HolyCrapForkIsBack")]
    public class HCFB_ITEM_KNIFEAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasHCFBType(typeof(HolyCrapForkIsBack.Items.Knife)); }
        public override void OnInstall() { base.OnInstall(); Run.onClientGameOverGlobal += OnClientGameOverGlobal; } public override void OnUninstall() { Run.onClientGameOverGlobal -= OnClientGameOverGlobal; base.OnUninstall(); }
        public void OnClientGameOverGlobal(Run run, RunReport runReport) { if ((bool)runReport.gameEnding && runReport.gameEnding.isWin && localUser.cachedBody.crit <= 0) Grant(); }
    }

    [RegisterModdedAchievement("RiskyMonkey_Items_HCFB_ITEM_CHOPSTICKS", "Items.HCFB_ITEM_CHOPSTICKS", null, null, "com.Ner0ls.HolyCrapForkIsBack")]
    public class HCFB_ITEM_CHOPSTICKSAchievement : BaseAchievement
    {
        public static bool OnlyRegisterIf() { return ItemAchievement.HasHCFBType(typeof(HolyCrapForkIsBack.Items.Chopsticks)); }
        public override void OnInstall() { base.OnInstall(); RoR2Application.onUpdate += OnUpdate; } public override void OnUninstall() { RoR2Application.onUpdate -= OnUpdate; base.OnUninstall(); }
        public void OnUpdate() { if (localUser == null || localUser.cachedBody == null) return; if (localUser.cachedBody.critMultiplier >= 4f) Grant(); }
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
                if ((info.damageType & DamageType.NonLethal) == 0 && self.combinedHealthFraction <= 0.01f) Grant();
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
                && (!RepairTweaks.repairList.Contains(ItemCatalog.GetItemDef(PickupCatalog.GetPickupDef(idx).itemIndex).name) || Reference.RepairStackAtOnce.Value >= 10) 
                && interactor.GetComponent<CharacterBody>() == localUser.cachedBody 
                && localUser.cachedBody.inventory.GetItemCount(PickupCatalog.GetPickupDef(idx).itemIndex) >= 10) 
                Grant(); 
        }
    }
}
    
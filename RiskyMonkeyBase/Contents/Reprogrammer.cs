using HarmonyLib;
using R2API;
using RoR2;
using RoR2.DirectionalSearch;
using ShrineOfRepair.Modules.Interactables;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RiskyMonkeyBase.Contents
{
    public class Reprogrammer
    {
        public static EquipmentDef reprogrammer;
        public static GameObject reprogrammerBig;
        public static Dictionary<EquipmentSlot, PurchaseInteraction> targets;
        public static GameObject hover;
        public static float frame = 0;
        public static void Patch()
        {
            targets = new();
            hover = LegacyResourcesAPI.Load<GameObject>("Prefabs/RecyclerIndicator");
            GameObject display = RiskyMonkeyBase.AssetBundle.LoadAsset<GameObject>("Assets/DisplayReprogrammer.fbx");
            display.transform.GetChild(0).localScale = new Vector3(100, 100, 100); // try big
            ItemDisplayRuleDict rules = new ItemDisplayRuleDict(); // Yes i am Copying Recycler Manually
            rules.Add("mdlCommandoDualies", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "Chest",
                    localPos = new Vector3(0, 0, -0.256f),
                    localAngles = new Vector3(0, 90, 348.7988f),
                    localScale = new Vector3(0.1f, 0.1f, 0.1f)
                }
            });
            rules.Add("mdlHuntress", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "Pelvis",
                    localPos = new Vector3(-0.0183f, 0.1247f, 0.2044f),
                    localAngles = new Vector3(0, 90, 177.358f),
                    localScale = new Vector3(0.1f, 0.1f, 0.1f)
                }
            });
            rules.Add("mdlBandit2", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "Chest",
                    localPos = new Vector3(0, -0.087f, -0.204f),
                    localAngles = new Vector3(0, 90, 348.7988f),
                    localScale = new Vector3(0.1f, 0.1f, 0.1f)
                }
            });
            rules.Add("mdlToolbot", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "Hip",
                    localPos = new Vector3(-1.642f, 1.177f, 0),
                    localAngles = new Vector3(0, 90, 180),
                    localScale = new Vector3(0.6462f, 0.6462f, 0.6462f)
                }
            });
            rules.Add("mdlEngi", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "CannonHeadL",
                    localPos = new Vector3(0.2037f, 0.1072f, -0.0098f),
                    localAngles = new Vector3(52.5723f, 269.3417f, 269.0913f),
                    localScale = new Vector3(0.1f, 0.1f, 0.1f)
                }
            });
            rules.Add("mdlCaptain", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "Stomach",
                    localPos = new Vector3(-0.1005f, -0.076f, 0.1862f),
                    localAngles = new Vector3(0, 252.3484f, 0),
                    localScale = new Vector3(0.0551f, 0.0551f, 0.0551f)
                }
            });
            rules.Add("mdlMerc", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "Stomach",
                    localPos = new Vector3(-0.1005f, -0.076f, 0.1862f),
                    localAngles = new Vector3(0, 252.3484f, 0),
                    localScale = new Vector3(0.0551f, 0.0551f, 0.0551f)
                }
            });
            rules.Add("mdlPaladin", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "Chest",
                    localPos = new Vector3(-0.2011f, 0.098f, -0.3147f),
                    localAngles = new Vector3(0, 117.8714f, 0),
                    localScale = new Vector3(0.1008f, 0.1008f, 0.1008f)
                }
            });
            rules.Add("mdlMage", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "Chest",
                    localPos = new Vector3(0, 0.201f, -0.378f),
                    localAngles = new Vector3(0, 90, 23.3119f),
                    localScale = new Vector3(0.1276f, 0.1276f, 0.1276f)
                }
            });
            rules.Add("mdlTreebot", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "FlowerBase",
                    localPos = new Vector3(-0.651f, 0.487f, -0.669f),
                    localAngles = new Vector3(0, 131.7342f, 0),
                    localScale = new Vector3(0.1333f, 0.1333f, 0.1333f)
                }
            });
            rules.Add("mdlLoader", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "Chest",
                    localPos = new Vector3(0.2673f, 0.0405f, 0.1688f),
                    localAngles = new Vector3(0, 0, 0),
                    localScale = new Vector3(0.0579f, 0.0579f, 0.0579f)
                }
            });
            rules.Add("mdlCroco", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "Hip",
                    localPos = new Vector3(1.98f, 2.553f, 0.541f),
                    localAngles = new Vector3(34.9679f, 126.7749f, 162.9854f),
                    localScale = new Vector3(0.5f, 0.5f, 0.5f)
                }
            });
            rules.Add("mdlRailGunner", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "TopRail",
                    localPos = new Vector3(-0.001f, 0.497f, 0.059f),
                    localAngles = new Vector3(90, 270, 0),
                    localScale = new Vector3(0.045f, 0.045f, 0.045f)
                }
            });
            rules.Add("mdlVoidSurvivor", new ItemDisplayRule[]{
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = display,
                    childName = "ForearmR",
                    localPos = new Vector3(0.1788f, 0.2191f, 0.1402f),
                    localAngles = new Vector3(55.8532f, 198.9949f, 166.9621f),
                    localScale = new Vector3(0.1f, 0.1f, 0.1f)
                }
            });
            GameObject model = RiskyMonkeyBase.AssetBundle.LoadAsset<GameObject>("Assets/pickupReprogrammer.fbx");
            ModelPanelParameters logbookCompensation = model.AddComponent<ModelPanelParameters>();
            for (var i = 0; i < model.transform.GetChild(0).childCount; i++)
            {
                Transform tr = model.transform.GetChild(0).GetChild(i);
                if (tr.gameObject.name.Contains("CameraPosition")) logbookCompensation.cameraPositionTransform = tr;
                else if (tr.gameObject.name.Contains("FocusPoint")) logbookCompensation.focusPointTransform = tr;
            }
            logbookCompensation.cameraPositionTransform.Translate(0, 0, 0.05f);
            On.RoR2.UI.ModelPanel.BuildModelInstance += (orig, self) =>
            {
                if (self.modelPrefab != null && self.enabled && self.modelPrefab.name.Contains("PickupReprogrammer")) self.modelPrefab.transform.localScale = new Vector3(200, 200, 200);
                orig(self);
            };
            CustomEquipment eq = new CustomEquipment("RM_reprogrammer", 
                "RISKYMONKEY_ITEM_REPROGRAMMER_NAME", 
                "RISKYMONKEY_ITEM_REPROGRAMMER_DESC", 
                "RISKYMONKEY_ITEM_REPROGRAMMER_LORE", 
                Reference.UseFullDescForPickup.Value ? "RISKYMONKEY_ITEM_REPROGRAMMER_DESC" : "RISKYMONKEY_ITEM_REPROGRAMMER_PICKUP", 
                RiskyMonkeyBase.AssetBundle.LoadAsset<Sprite>("Assets/texReprogrammer.png"), model, 
                Reference.ReprogrammerCooldown.Value, true, true, false, false, null, null, ColorCatalog.ColorIndex.Equipment, true, true, rules);
            ItemAPI.Add(eq);
            reprogrammer = eq.EquipmentDef;
            On.RoR2.EquipmentSlot.PerformEquipmentAction += (orig, self, equipmentDef) =>
            {
                bool ret = orig(self, equipmentDef);
                if (!ret && equipmentDef == reprogrammer) return Reprogram(self);
                return ret;
            };
            On.RoR2.EquipmentSlot.UpdateTargets += (orig, self, equipmentIndex, _) =>
            {
                if (equipmentIndex != reprogrammer.equipmentIndex) orig(self, equipmentIndex, _);
                else
                {
                    frame -= Time.deltaTime;
                    if (frame > 0) return;
                    frame = Reference.ReprogrammerRefresh.Value;
                    Ray aimRay = (Ray)AccessTools.Method(typeof(EquipmentSlot), "GetAimRay").Invoke(self, null);
                    PurchaseInteraction target = FindPurchaseInteraction(self, aimRay, 10f, 30f, true, "Duplicator");
                    ref EquipmentSlot.UserTargetInfo currentTarget = ref AccessTools.FieldRefAccess<EquipmentSlot, EquipmentSlot.UserTargetInfo>("currentTarget")(self);
                    if (target != null)
                    {
                        currentTarget = new EquipmentSlot.UserTargetInfo();
                        currentTarget.rootObject = target.gameObject;
                        currentTarget.transformToIndicateAt = target.transform;
                        if (!targets.ContainsKey(self)) targets.Add(self, target);
                        else targets[self] = target;
                    }
                    ref Indicator targetIndicator = ref AccessTools.FieldRefAccess<EquipmentSlot, Indicator>(self, "targetIndicator");
                    targetIndicator.visualizerPrefab = hover;
                    targetIndicator.active = (bool)target;
                    targetIndicator.targetTransform = (bool)target ? target.transform : null;
                }
            };
            On.RoR2.Language.GetLocalizedStringByToken += (orig, self, token) =>
            {
                if (token == "RISKYMONKEY_ITEM_REPROGRAMMER_DESC") return orig(self, token).Replace("{0}", (Reference.ReprogrammerRepairChance.Value * 100).ToString());
                return orig(self, token);
            };
        }

        public static bool Reprogram(EquipmentSlot self)
        {
            frame = 0;
            AccessTools.Method(typeof(EquipmentSlot), "UpdateTargets").Invoke(self, new object[] { reprogrammer.equipmentIndex, false });
            if (!targets.ContainsKey(self) || targets[self] == null) return false;
            PurchaseInteraction target = targets[self];
            PickupIndex initialPickupIndex = target.GetComponent<ShopTerminalBehavior>().NetworkpickupIndex;
            AccessTools.FieldRefAccess<EquipmentSlot, float>(self, "subcooldownTimer") = 0.2f;
            PickupIndex[] array = Enumerable.Where(PickupTransmutationManager.GetAvailableGroupFromPickupIndex(initialPickupIndex), pickupIndex => pickupIndex != initialPickupIndex).ToArray();
            if (array == null || array.Length == 0 || Run.instance.treasureRng.RangeFloat(0, 1) < Reference.ReprogrammerRepairChance.Value) // f
            {
                DirectorPlacementRule directorPlacementRule = new DirectorPlacementRule();
                directorPlacementRule.placementMode = 0;
                GameObject spawnedInstance = ShrineOfRepairPicker.shrineSpawnCard.DoSpawn(target.transform.position, Quaternion.identity, new DirectorSpawnRequest(ShrineOfRepairPicker.shrineSpawnCard, directorPlacementRule, Run.instance.runRNG)).spawnedInstance;
                spawnedInstance.transform.eulerAngles = target.transform.eulerAngles;
                NetworkServer.Spawn(spawnedInstance);
                target.gameObject.SetActive(false);
            } else target.GetComponent<ShopTerminalBehavior>().NetworkpickupIndex = Run.instance.treasureRng.NextElementUniform(array);
            EffectManager.SimpleEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OmniEffect/OmniRecycleEffect"), target.transform.position, Quaternion.identity, true);
            AccessTools.Method(typeof(EquipmentSlot), "InvalidateCurrentTarget").Invoke(self, null);
            targets.Remove(self);
            return true;
        }

        private static PurchaseInteraction FindPurchaseInteraction(
          EquipmentSlot self,
          Ray aimRay,
          float maxAngle,
          float maxDistance,
          bool requireLoS,
          string name)
        {
            InteractableSearch interactableSearch = new();
            float extraRaycastDistance;
            aimRay = CameraRigController.ModifyAimRayIfApplicable(aimRay, self.gameObject, out extraRaycastDistance);
            interactableSearch.searchOrigin = aimRay.origin;
            interactableSearch.searchDirection = aimRay.direction;
            interactableSearch.minAngleFilter = 0.0f;
            interactableSearch.maxAngleFilter = maxAngle;
            interactableSearch.minDistanceFilter = 0.0f;
            interactableSearch.maxDistanceFilter = maxDistance + extraRaycastDistance;
            interactableSearch.filterByDistinctEntity = false;
            interactableSearch.filterByLoS = requireLoS;
            interactableSearch.sortMode = SortMode.DistanceAndAngle;
            interactableSearch.name = name;
            return interactableSearch.SearchCandidatesForSingleTarget(InstanceTracker.GetInstancesList<PurchaseInteraction>());
        }

        public class InteractableSearch : BaseDirectionalSearch<PurchaseInteraction, InteractableSearchSelector, InteractableSearchFilter>
        {
            public string name
            {
                get => candidateFilter.name;
                set => candidateFilter.name = value;
            }

            public InteractableSearch() : base(new InteractableSearchSelector(), new InteractableSearchFilter())
            {
            }

            public InteractableSearch(InteractableSearchSelector selector, InteractableSearchFilter candidateFilter) : base(selector, candidateFilter)
            {
            }
        }

        [StructLayout(LayoutKind.Sequential, Size = 1)]
        public struct InteractableSearchSelector : IGenericWorldSearchSelector<PurchaseInteraction>
        {
            public Transform GetTransform(PurchaseInteraction source) => source.transform;
            public GameObject GetRootObject(PurchaseInteraction source) => source.gameObject;
        }

        public struct InteractableSearchFilter : IGenericDirectionalSearchFilter<PurchaseInteraction>
        {
            public string name;
            public bool PassesFilter(PurchaseInteraction purchaseInteraction) => purchaseInteraction.gameObject.activeInHierarchy && (name == null || purchaseInteraction.name.Contains(name));
        }
    }
}

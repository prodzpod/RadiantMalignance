using EntityStates.Assassin2;
using HarmonyLib;
using HG;
using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace RiskyMonkeyBase.Tweaks
{
    public class RFTVIotaConstructFix
    {
        public static void Patch()
        {
            DirectorCard majorConstructDC = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC1/MajorAndMinorConstruct/cscMajorConstruct.asset").WaitForCompletion(),
                selectionWeight = 1,
                spawnDistance = 0,
                minimumStageCompletions = 3,
                preventOverhead = true
            };
            On.RoR2.ClassicStageInfo.RebuildCards += (orig, self) => // add it wherever Xi Construct is loaded
            {
                SceneDef mostRecentSceneDef = SceneCatalog.mostRecentSceneDef;
                if (self.monsterDccsPool) HandleDccsPool(self.monsterDccsPool);
                if (self.monsterCategories) HandleDccs(self.monsterCategories);
                orig(self);
            };
            On.EntityStates.GenericCharacterDeath.FixedUpdate += (orig, self) =>
            {
                orig(self);
                if (self is EntityStates.MajorConstruct.Death)
                {
                    if (self.fixedAge <= 5f || !NetworkServer.active) return;
                    self.DestroyBodyAsapServer();
                }
            };

            void HandleDccsPool(DccsPool dccsPool)
            {
                for (int i = 0; i < dccsPool.poolCategories.Length; i++)
                {
                    DccsPool.Category category = dccsPool.poolCategories[i];
                    HandleDccsPoolEntries(category.alwaysIncluded);
                    HandleDccsPoolEntries(category.includedIfConditionsMet);
                    HandleDccsPoolEntries(category.includedIfNoConditionsMet);
                }
            }
            void HandleDccsPoolEntries(DccsPool.PoolEntry[] entries) { foreach (DccsPool.PoolEntry poolEntry in entries) { HandleDccs(poolEntry.dccs); } }
            void HandleDccs(DirectorCardCategorySelection dccs)
            {
                for (int i = 0; i < dccs.categories.Length; i++)
                {
                    ref DirectorCardCategorySelection.Category cat = ref dccs.categories[i];
                    if (Array.Exists(cat.cards, x => x.spawnCard.prefab.name.Contains("MegaConstruct")))
                    {
                        ArrayUtils.ArrayAppend(ref cat.cards, majorConstructDC);
                        RiskyMonkeyBase.Log.LogDebug("Added Iota Construct in " + cat.name + " of " + dccs.name);
                    }
                }
            }
        }

        public static void IotaNerf()
        {
            On.EntityStates.MajorConstruct.Weapon.FireLaser.OnEnter += (orig, self) =>
            {
                orig(self);
                if (self.characterBody.name.Contains("MajorConstruct")) self.maxDistance = 150f;
            };
        }

        public const int IotaConstructSpawnCount = 3;
        public static Dictionary<CharacterBody, List<GameObject>> spawnedConstructs = new();
        public static void IotaBuff()
        {
            CharacterSpawnCard card = null;
            RoR2Application.onLoad += () => card = PlasmaCoreSpikestripContent.Content.Monsters.SigmaConstruct.instance.CharacterSpawnCard;
            On.EntityStates.MajorConstruct.Stance.LoweredToRaised.OnEnter += (orig, self) =>
            {
                orig(self);
                if (!NetworkServer.active) return;
                foreach (HurtBox hurtBox in new SphereSearch() { origin = self.gameObject.transform.position, radius = 37.5f, mask = LayerIndex.entityPrecise.mask }.RefreshCandidates().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes())
                {
                    CharacterBody body = hurtBox.healthComponent.body;
                    if (body && body.name == "SigmaConstructBody") return;
                }
                Quaternion rot = Quaternion.AngleAxis(120f, Vector3.up);
                Vector3 cur = self.transform.forward * 16f;
                if (!spawnedConstructs.ContainsKey(self.characterBody)) spawnedConstructs.Add(self.characterBody, new());
                for (int i = 0; i < IotaConstructSpawnCount; i++)
                {
                    Vector3 pos = cur + self.transform.position;
                    GameObject obj = card.DoSpawn(pos, Quaternion.Euler(self.transform.forward), new(card, new() { position = pos, placementMode = DirectorPlacementRule.PlacementMode.Direct }, Run.instance.spawnRng) { teamIndexOverride = self.characterBody.teamComponent.teamIndex }).spawnedInstance;
                    NetworkServer.Spawn(obj);
                    spawnedConstructs[self.characterBody].Add(obj);
                    cur = rot * cur;
                }
            };
            GlobalEventManager.onCharacterDeathGlobal += report =>
            {
                if (spawnedConstructs.ContainsKey(report.victimBody)) foreach (var body in spawnedConstructs[report.victimBody])
                        body?.GetComponent<CharacterMaster>()?.bodyInstanceObject?.GetComponent<HealthComponent>()?.Suicide(report.attackerBody.gameObject);
                spawnedConstructs.Remove(report.victimBody);
            };
        }

        public static void AssassinNerf()
        {
            On.EntityStates.Assassin2.DashStrike.OnEnter += (orig, self) =>
            {
                orig(self);
                if (self.slashCount == 1) self.slashCount = 2;
            };
        }
    }
}

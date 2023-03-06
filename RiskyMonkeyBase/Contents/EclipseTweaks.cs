using EntityStates.Treebot.TreebotFlower;
using EntityStates.Treebot.Weapon;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.Projectile;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace RiskyMonkeyBase.Contents
{
    public class EclipseTweaks
    {
        public static float _heal;
        public static void PatchREX()
        {
            Run.onRunStartGlobal += (run) =>
            {
                if (DifficultyIndex.Eclipse5 <= run.selectedDifficulty && run.selectedDifficulty <= DifficultyIndex.Eclipse8)
                {
                    LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/SyringeProjectileHealing").GetComponent<ProjectileHealOwnerOnDamageInflicted>().fractionOfDamage *= 2;
                    FirePlantSonicBoom.healthFractionPerHit *= 2;
                    Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Treebot/TreebotFruitPack.prefab").WaitForCompletion().transform.GetChild(2).GetComponent<HealthPickup>().fractionalHealing *= 2;
                    TreebotFlower2Projectile.healthFractionYieldPerHit *= 2;
                }
            };
            Run.onRunDestroyGlobal += (run) =>
            {
                if (DifficultyIndex.Eclipse5 <= run.selectedDifficulty && run.selectedDifficulty <= DifficultyIndex.Eclipse8)
                {
                    LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/SyringeProjectileHealing").GetComponent<ProjectileHealOwnerOnDamageInflicted>().fractionOfDamage /= 2;
                    FirePlantSonicBoom.healthFractionPerHit /= 2;
                    Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Treebot/TreebotFruitPack.prefab").WaitForCompletion().transform.GetChild(2).GetComponent<HealthPickup>().fractionalHealing /= 2;
                    TreebotFlower2Projectile.healthFractionYieldPerHit /= 2;
                }
            };
        }

        public static void PatchCorpsebloom()
        {
            IL.RoR2.HealthComponent.Heal += (il) =>
            {
                ILCursor c = new(il);
                c.GotoNext(x => x.MatchLdarg(0));
                c.Emit(OpCodes.Ldarg_1);
                c.EmitDelegate<Action<float>>(amount => _heal = amount);
                c.GotoNext(x => x.MatchLdarg(1), x => x.MatchStloc(2));
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldarg_2);
                c.Emit(OpCodes.Ldarg_3);
                c.EmitDelegate<Func<float, HealthComponent, ProcChainMask, bool, float>>((amount, self, procChainMask, nonRegen) => 
                {
                    if (nonRegen && self.repeatHealComponent != null && procChainMask.HasProc(ProcType.RepeatHeal)) return _heal;
                    return amount;
                });
            };
        }

        public static void PatchShield()
        {
            Run.onRunStartGlobal += (run) =>
            {
                if (DifficultyIndex.Eclipse5 <= run.selectedDifficulty && run.selectedDifficulty <= DifficultyIndex.Eclipse8)
                    IL.RoR2.CharacterBody.FixedUpdate += PatchShieldFRFR;
            };
            Run.onRunDestroyGlobal += (run) =>
            {
                if (DifficultyIndex.Eclipse5 <= run.selectedDifficulty && run.selectedDifficulty <= DifficultyIndex.Eclipse8)
                    IL.RoR2.CharacterBody.FixedUpdate -= PatchShieldFRFR;
            };
        }

        public static void PatchShieldFRFR(ILContext il)
        {
            ILCursor c = new(il);
            c.GotoNext(x => x.MatchLdcR4(7));
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, 14);
        }

        public static void PatchSimulacrum()
        {
            InfiniteTowerRun.onWaveInitialized += (self) =>
            {
                if (NetworkServer.active)
                {
                    CharacterBody body = LocalUserManager.GetFirstLocalUser().currentNetworkUser.GetCurrentBody();
                    if (body == null || body.inventory == null) return;
                    int itemCount = body.inventory.GetItemCount(RoR2Content.Items.WardOnLevel);
                    if (itemCount > 0)
                    {
                        GameObject gameObject = UnityEngine.Object.Instantiate(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/WarbannerWard"), body.transform.position, Quaternion.identity);
                        gameObject.GetComponent<TeamFilter>().teamIndex = body.teamComponent.teamIndex;
                        gameObject.GetComponent<BuffWard>().Networkradius = 8f + 8f * itemCount;
                        NetworkServer.Spawn(gameObject);
                    }
                }
            };
            On.RoR2.CharacterBody.OnInventoryChanged += (orig, self) =>
            {
                if (Run.instance is InfiniteTowerRun && NetworkServer.active)
                {
                    self.AddItemBehavior<DroneCoolantBehaviour>(self.inventory.GetItemCount(ItemCatalog.FindItemIndex("ITEM_ILLEGAL_DRONE_COOLANT") + self.inventory.GetItemCount(ItemCatalog.FindItemIndex("MysticsItems_DroneWires"))));
                }
                orig(self);
            };
            NumberChanges.rules.Add("ITEM_ILLEGAL_DRONE_COOLANT_PICKUP", GainDroneText);
            NumberChanges.rules.Add("ITEM_ILLEGAL_DRONE_COOLANT_DESC", GainDroneText);
            NumberChanges.rules.Add("ITEM_MYSTICSITEMS_DRONEWIRES_PICKUP", GainDroneText);
            NumberChanges.rules.Add("ITEM_MYSTICSITEMS_DRONEWIRES_DESC", GainDroneText);
        }

        public static string GainDroneText(On.RoR2.Language.orig_GetLocalizedStringByToken orig, Language self, string token)
        {
            if (Run.instance != null && Run.instance is InfiniteTowerRun)
                return "<style=cIsUtility>Gain a random drone.</style> " + orig(self, token);
            return orig(self, token);
        }

        public class DroneCoolantBehaviour : CharacterBody.ItemBehavior
        {
            private int previousStack;
            private CharacterSpawnCard droneSpawnCard1;
            private CharacterSpawnCard droneSpawnCard2;
            private Xoroshiro128Plus rng;
            private DirectorPlacementRule placementRule;
            private int hasSpawnedDrone = 0;
            private float spawnDelay;

            private void OnEnable()
            {
                hasSpawnedDrone = 0;
                rng = new Xoroshiro128Plus(Run.instance.seed ^ (ulong)Run.instance.stageClearCount);
                droneSpawnCard1 = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscDrone1");
                droneSpawnCard2 = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscDrone2");
                placementRule = new DirectorPlacementRule()
                {
                    placementMode = DirectorPlacementRule.PlacementMode.Approximate,
                    minDistance = 3f,
                    maxDistance = 40f,
                    spawnOnTarget = transform
                };
                UpdateAllMinions(stack);
            }
            private void OnDisable() { UpdateAllMinions(0); }

            private void FixedUpdate()
            {
                if (previousStack != stack) UpdateAllMinions(stack);
                spawnDelay -= Time.fixedDeltaTime;
                if (hasSpawnedDrone >= stack || body == null || spawnDelay > 0.0) return;
                TrySpawnDrone();
            }

            private void UpdateAllMinions(int newStack)
            {
                if (body == null || body.master == null) return;
                previousStack = newStack;
            }

            private void TrySpawnDrone()
            {
                if (body.master.IsDeployableLimited(DeployableSlot.DroneWeaponsDrone)) return;
                spawnDelay = 1f;
                DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(rng.nextBool ? droneSpawnCard1 : droneSpawnCard2, placementRule, rng)
                {
                    summonerBodyObject = gameObject,
                    onSpawnedServer = new Action<SpawnCard.SpawnResult>(OnMasterSpawned)
                });
            }

            private void OnMasterSpawned(SpawnCard.SpawnResult spawnResult)
            {
                hasSpawnedDrone++;
                GameObject spawnedInstance = spawnResult.spawnedInstance;
                if (spawnedInstance == null) return;
                CharacterMaster component1 = spawnedInstance.GetComponent<CharacterMaster>();
                if (component1 == null) return;
                Deployable component2 = component1.GetComponent<Deployable>();
                if (component2 == null) return;
                body.master.AddDeployable(component2, DeployableSlot.DroneWeaponsDrone);
            }
        }
    }
}

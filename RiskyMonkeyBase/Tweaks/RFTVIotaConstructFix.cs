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
    }
}

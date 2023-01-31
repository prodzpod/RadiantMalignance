using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace RiskyMonkeyBase.Tweaks
{
    public class LunarBudTweaks
    {
        public static Dictionary<string, Vector3> pos = new();
        public static void Patch()
        {
            pos.Add("goolake", new Vector3(285.1561f, -61.79442f, -193.2947f));
            pos.Add("ancientloft", new Vector3(-70.1913f, 81.07519f, 221.0971f));
            pos.Add("foggyswamp", new Vector3(-121.8996f, -126.0044f, -235.4447f));
            On.RoR2.Stage.Start += (orig, self) =>
            {
                orig(self);
                if (pos.ContainsKey(self.sceneDef.cachedName))
                {
                    DirectorPlacementRule directorPlacementRule = new DirectorPlacementRule();
                    directorPlacementRule.placementMode = 0;
                    SpawnCard spawnCard = Resources.Load<SpawnCard>("SpawnCards/InteractableSpawnCard/iscLunarChest");
                    GameObject spawnedInstance = spawnCard.DoSpawn(pos[self.sceneDef.cachedName], Quaternion.identity, new DirectorSpawnRequest(spawnCard, directorPlacementRule, Run.instance.runRNG)).spawnedInstance;
                    spawnedInstance.transform.eulerAngles = new Vector3();
                    NetworkServer.Spawn(spawnedInstance);
                }
            };
        }
    }
}

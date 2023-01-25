using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using RoR2.ExpansionManagement;
using UnityEngine.AddressableAssets;

namespace RiskyMonkeyBase.Tweaks
{
    public class ReorderSkins
    {
        public static List<string> skinOrder;
        public static List<string> memeSkins;
        public static List<string> skinRemoval;
        public static void Patch()
        {
            skinOrder = new();
            memeSkins = new();
            skinRemoval = new();
            foreach (var entry in Reference.SkinsToReorder.Value.Split(',')) skinOrder.Add(entry.Trim());
            foreach (var entry in Reference.MemeSkins.Value.Split(',')) memeSkins.Add(entry.Trim());
            foreach (var entry in Reference.SkinsToRemove.Value.Split(',')) skinRemoval.Add(entry.Trim());
            RiskyMonkeyBase.Log.LogDebug("Skin Order: " + skinOrder.Join());
            RiskyMonkeyBase.Log.LogDebug("Meme Skins: " + memeSkins.Join());
            RiskyMonkeyBase.Log.LogDebug("Skin Removal: " + skinRemoval.Join());
            RoR2Application.onLoad += () =>
            {
                ExpansionDef DLC1 = Addressables.LoadAssetAsync<ExpansionDef>("RoR2/DLC1/Common/DLC1.asset").WaitForCompletion();
                try
                {
                    IEnumerable<GameObject> bodyPrefabs = BodyCatalog.allBodyPrefabs;
                    foreach (var bodyPrefab in bodyPrefabs)
                    {
                        if (bodyPrefab == null) continue;
                        BodyIndex idx = BodyCatalog.FindBodyIndex(bodyPrefab);
                        SkinDef[] skins = BodyCatalog.GetBodySkins(idx);
                        if (skins.Length == 0) continue;
                        List<SkinDef> skinsList = new List<SkinDef>(skins);
                        bool modified = false;
                        for (var i = 0; i < skins.Length; i++) if (skinRemoval.Contains(skins[i].name) || (memeSkins.Contains(skins[i].name) && Reference.SeriousMode.Value))
                        {
                            skinsList.Remove(skins[i]);
                            modified = true;
                        }
                        foreach (var name in skinOrder)
                        {
                            SkinDef skin = skinsList.Find((skin) => skin.name == name);
                            if (skin == null) continue;
                            skinsList.Remove(skin);
                            skinsList.Add(skin);
                            modified = true;
                        }
                        List<string> names = new();
                        foreach (var skin in skinsList) names.Add(skin.name);
                        RiskyMonkeyBase.Log.LogDebug("Skins for " + BodyCatalog.GetBodyName(idx) + ": " + names.Join());
                        if (modified)
                        {
                            BodyCatalog.skins[(int)idx] = skinsList.ToArray();
                            SkinCatalog.skinsByBody[(int)idx] = skinsList.ToArray();
                            bodyPrefab.GetComponent<ModelLocator>().modelTransform.gameObject.GetComponent<ModelSkinController>().skins = skinsList.ToArray(); // ??
                        }
                    }
                }
                catch (Exception ex) { RiskyMonkeyBase.Log.LogError(ex); }
            };
        }
    }
}

using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using RoR2.ExpansionManagement;
using UnityEngine.AddressableAssets;
using System.Text;

namespace RiskyMonkeyBase.Tweaks
{
    public class ReorderSkins
    {
        public static List<string> skinOrder;
        public static List<string> memeSkins;
        public static void Patch()
        {
            skinOrder = new();
            memeSkins = new();
            foreach (var entry in Reference.SkinsToReorder.Value.Split(',')) skinOrder.Add(entry.Trim());
            foreach (var entry in Reference.MemeSkins.Value.Split(',')) memeSkins.Add(entry.Trim());
            RiskyMonkeyBase.Log.LogDebug("Skin Order: " + skinOrder.Join());
            RiskyMonkeyBase.Log.LogDebug("Meme Skins: " + memeSkins.Join());
            RoR2Application.onLoad += () =>
            {
                ExpansionDef DLC1 = Addressables.LoadAssetAsync<ExpansionDef>("RoR2/DLC1/Common/DLC1.asset").WaitForCompletion();
                StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
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
                        for (var i = 0; i < skins.Length; i++) if (memeSkins.Contains(skins[i].name) && Reference.SeriousMode.Value)
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
                        stringBuilder.AppendLine("Skins for " + BodyCatalog.GetBodyName(idx) + ": " + names.Join());
                        if (modified)
                        {
                            BodyCatalog.skins[(int)idx] = skinsList.ToArray();
                            SkinCatalog.skinsByBody[(int)idx] = skinsList.ToArray();
                            bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponent<ModelSkinController>().skins = skinsList.ToArray(); // ??
                        }
                    }
                }
                catch (Exception ex) { RiskyMonkeyBase.Log.LogError(ex); }
                RiskyMonkeyBase.Log.LogDebug(stringBuilder.ToString());
                HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
            };
        }
    }
}

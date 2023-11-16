using System;
using System.IO;
using Il2Cpp;
using Il2CppInterop.Runtime;
using LuckyPlorts;
using MelonLoader;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using Object = UnityEngine.Object;
[assembly: MelonInfo(typeof(EntryPoint), "LuckyPlorts", "1.0.4", "KomiksPL", "https://www.nexusmods.com/slimerancher2/mods/13")]

namespace LuckyPlorts
{
    public class EntryPoint : MelonMod
    {
        public static IdentifiableType LuckyPlort;
        public static AssetBundle luckyPlort;

        public static Sprite ConvertTextureToSprite(Texture2D texture2D)
        {
            return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100f);
        }

        public static Color32 GetAverageColorFromTexture(Texture2D tex)
        {
            Color32[] pixels = tex.GetPixels32();
            int numPixels = pixels.Length;
            int totalR = 0, totalG = 0, totalB = 0;

            foreach (Color32 pixel in pixels)
            {
                totalR += pixel.r;
                totalG += pixel.g;
                totalB += pixel.b;
            }

            byte averageR = (byte)(totalR / numPixels);
            byte averageG = (byte)(totalG / numPixels);
            byte averageB = (byte)(totalB / numPixels);

            return new Color32(averageR, averageG, averageB, 0);
        }

        public override void OnInitializeMelon()
        {
            foreach (var patchedMethod in HarmonyInstance.GetPatchedMethods())
            {
                MelonLogger.Msg(patchedMethod.ToString());
            }
            LuckyPlort = ScriptableObject.CreateInstance<IdentifiableType>();
            LuckyPlort.hideFlags |= HideFlags.HideAndDontSave;
            LuckyPlort.name = "LuckyPlort";
            LuckyPlort.localizationSuffix = "lucky_plort";

            // Load the icon texture
            using (Stream iconStream = MelonAssembly.Assembly.GetManifestResourceStream("LuckyPlorts.plortLucky.png"))
            {
                Texture2D iconTexture = new Texture2D(1, 1);
                byte[] iconBytes = new byte[iconStream.Length];
                iconStream.Read(iconBytes, 0, iconBytes.Length);
                ImageConversion.LoadImage(iconTexture, iconBytes);
                LuckyPlort.icon = ConvertTextureToSprite(iconTexture);
                Color iconColor = GetAverageColorFromTexture(iconTexture);
                iconColor.a = 1f;
                LuckyPlort.color = iconColor;
            }

            // Load the asset bundle
            using (Stream bundleStream = MelonAssembly.Assembly.GetManifestResourceStream("LuckyPlorts.plortLuckyObj"))
            {
                byte[] bundleBytes = new byte[bundleStream.Length];
                bundleStream.Read(bundleBytes, 0, bundleBytes.Length);
                luckyPlort = AssetBundle.LoadFromMemory(bundleBytes);
            }
        }
        public static T Get<T>(string name) where T : UnityEngine.Object
        {
            return Resources.FindObjectsOfTypeAll<T>().FirstOrDefault((T x) => x.name == name);
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (!sceneName.Equals("GameCore")) return;
            IdentifiableType identifiableType = Get<IdentifiableType>("TabbyPlort");
            SlimeDefinition slimeDefinition = Get<SlimeDefinition>("Lucky");
            slimeDefinition.Diet.EatMap.Add(new SlimeDiet.EatMapEntry
            {
                Driver = SlimeEmotions.Emotion.HUNGER,
                EatsIdent = Get<IdentifiableType>("StonyHen"),
                IsFavorite = true,
                FavoriteProductionCount = 2,
                ProducesIdent = EntryPoint.LuckyPlort
            });
            GameObject gameObject = PrefabUtils.CopyPrefab(identifiableType.prefab);
            gameObject.name = "plortLucky";
            Color color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
            Color color2 = new Color32(200, 200, 200, byte.MaxValue);
            Color color3 = new Color32(160, 160, 160, byte.MaxValue);
            gameObject.GetComponent<IdentifiableActor>().identType = EntryPoint.LuckyPlort;
            gameObject.GetComponent<MeshRenderer>().material = Object.Instantiate(gameObject.GetComponent<MeshRenderer>().material);
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_TopColor", color);
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_MiddleColor", color2);
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_BottomColor", color3);
            EntryPoint.LuckyPlort.prefab = gameObject;
            EntryPoint.LuckyPlort.IsPlort = true;
            GameObject gameObject2 = new GameObject("plortLuckyCoin");
            Mesh mesh = EntryPoint.luckyPlort.LoadAllAssets(Il2CppType.Of<Mesh>()).First().Cast<Mesh>();
            gameObject2.AddComponent<MeshFilter>().mesh = mesh;
            gameObject2.AddComponent<MeshRenderer>().sharedMaterial = Get<SlimeAppearance>("LuckyDefault").Structures[2].DefaultMaterials[0];
            gameObject2.transform.parent = gameObject.transform;
            gameObject2.transform.localScale *= 0.45f;
            StringTable stringTable = SRSingleton<SystemContext>.Instance.LocalizationDirector.Tables["Actor"];
            StringTableEntry stringTableEntry = stringTable.AddEntry("l.lucky_plort", "Lucky Plort");
            EntryPoint.LuckyPlort.localizedName = new LocalizedString(stringTable.SharedData.TableCollectionNameGuid, stringTableEntry.SharedEntry.Id);
        }
    }
}

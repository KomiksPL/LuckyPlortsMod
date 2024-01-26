using System.Collections;
using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher.UI.Localization;
using MelonLoader;
using UnityEngine;

namespace LuckyPlortsMod.Patches
{
    [HarmonyPatch(typeof(LocalizationDirector))]
    public static class Patch_LocalizationDirector
    {
        [HarmonyPatch(nameof(LoadTables)), HarmonyPostfix]
        public static void LoadTables(LocalizationDirector __instance)
        {
            MelonCoroutines.Start(LoadTable(__instance));
        }

        private static IEnumerator LoadTable(LocalizationDirector director)
        {
            WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(0.01f);
            yield return waitForSecondsRealtime;

            director.Tables["Actor"].AddEntry("l.lucky_plort", "Lucky Plort");
        }
    }
}
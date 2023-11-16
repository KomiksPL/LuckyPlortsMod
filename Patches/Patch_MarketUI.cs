using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher.UI;
using CollectionExtensions = System.Collections.Generic.CollectionExtensions;

namespace LuckyPlorts.Patches;

[HarmonyPatch(typeof(MarketUI))]
public static class Patch_MarketUI
{
    [HarmonyPatch(nameof(Start)), HarmonyPrefix]
    public static void Start(MarketUI __instance)
    {
        __instance.plorts = __instance.plorts.AddItem(new MarketUI.PlortEntry
        {
            identType = EntryPoint.LuckyPlort
        }).ToArray();
    }
}
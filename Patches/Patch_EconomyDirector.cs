using HarmonyLib;
using Il2Cpp;

namespace LuckyPlortsMod.Patches;


[HarmonyPatch(typeof(EconomyDirector))]
public class Patch_EconomyDirector
{
    [HarmonyPatch(nameof(InitModel)), HarmonyPrefix]
    public static void InitModel(EconomyDirector __instance)
    {
        __instance.BaseValueMap = __instance.BaseValueMap.AddItem(new EconomyDirector.ValueMap
        {
            Accept = EntryPoint.LuckyPlort.prefab.GetComponent<IdentifiableActor>(),
            Value = 250f,
            FullSaturation = 125f
        }).ToArray();
    }
}
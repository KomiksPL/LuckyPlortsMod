using HarmonyLib;
using Il2Cpp;
using UnityEngine;

namespace LuckyPlortsMod.Patches;

[HarmonyPatch(typeof(LookupDirector))]
public class Patch_LookupDirector
{
    [HarmonyPatch(nameof(Awake)), HarmonyPrefix]
    public static void Awake(LookupDirector __instance)
    {
        
        IdentifiableTypeGroup identifiableTypeGroup = Resources.FindObjectsOfTypeAll<IdentifiableTypeGroup>().FirstOrDefault(x => x.name == "PlortGroup");
        if (identifiableTypeGroup != null)
        {
            identifiableTypeGroup._memberTypes.Add(EntryPoint.LuckyPlort);
        }

        var instanceAutoSaveDirector = SRSingleton<GameContext>.Instance.AutoSaveDirector;
        // instanceAutoSaveDirector.identifiableTypes._runtimeObject = null;
        instanceAutoSaveDirector.identifiableTypes._memberTypes.Add(EntryPoint.LuckyPlort);
        // instanceAutoSaveDirector.identifiableTypes.GetRuntimeObject();
    }
}
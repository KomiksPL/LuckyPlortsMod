using HarmonyLib;
using Il2Cpp;
using UnityEngine;

namespace LuckyPlorts.Patches;

[HarmonyPatch(typeof(AutoSaveDirector))]
public class Patch_AutoSaveDirector
{
    [HarmonyPatch(nameof(Awake)), HarmonyPrefix]
    public static void Awake(AutoSaveDirector __instance)
    {
        IdentifiableTypeGroup identifiableTypeGroup = Resources.FindObjectsOfTypeAll<IdentifiableTypeGroup>().FirstOrDefault((IdentifiableTypeGroup x) => x.name == "PlortGroup");
        if (identifiableTypeGroup != null)
        {
            identifiableTypeGroup.memberTypes.Add(EntryPoint.LuckyPlort);
        }
        __instance.identifiableTypes.memberTypes.Add(EntryPoint.LuckyPlort);
    }
}
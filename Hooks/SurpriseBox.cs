using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using GbHapticsIntegration.Managers;
using System.Reflection;
using MelonLoader;

namespace GbHapticsIntegration.Hooks
{
    internal static class H_SurpriseBox
    {
        private static AccessTools.FieldRef<SurpriseBox, bool> SurpriseBox_haveSpawned = null;

        internal static void Initialize()
        {
            Debug.LogFieldRefGet("SurpriseBox.haveSpawned");
            SurpriseBox_haveSpawned = AccessTools.FieldRefAccess<SurpriseBox, bool>("haveSpawned");
            Debug.LogFieldRefFound("SurpriseBox.haveSpawned", SurpriseBox_haveSpawned);

            Debug.LogPatchInit("SurpriseBox.SpawnSurprise");
            GbHapticsIntegration.ModHarmony.Patch(AccessTools.Method(typeof(SurpriseBox), "SpawnSurprise"),
                AccessTools.Method(typeof(H_SurpriseBox), "SpawnSurprise_Prefix").ToNewHarmonyMethod());
        }

        private static void SpawnSurprise_Prefix(SurpriseBox __instance)
        {

        }
    }
}
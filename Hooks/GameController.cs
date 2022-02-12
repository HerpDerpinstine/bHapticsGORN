using System;
using GbHapticsIntegration.Managers;
using HarmonyLib;
using MelonLoader;

namespace GbHapticsIntegration.Hooks
{
    internal static class H_GameController
    {
        internal static void Initialize()
        {
            Type Type_GameController = typeof(GameController);
            Type Type_Patch_GameController = typeof(H_GameController);

            Debug.LogPatchInit("GameController.SpawnPlayerWeapons");
            GbHapticsIntegration.ModHarmony.Patch(
                AccessTools.Method(Type_GameController, "SpawnPlayerWeapons"),
                AccessTools.Method(Type_Patch_GameController, "SpawnPlayerWeapons_PostFix").ToNewHarmonyMethod());
        }

        private static void SpawnPlayerWeapons_PostFix(bool __0)
        {
            if (!__0)
                return;
            M_Tact.surprise.Play();
        }
    }
}

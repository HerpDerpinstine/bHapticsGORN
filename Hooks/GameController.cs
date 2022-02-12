using HarmonyLib;
using UnityEngine;
using MelonLoader;
using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Hooks
{
    internal static class H_GameController
    {
        internal static void Initialize()
        {
            Debug.LogPatchInit("GameController.SetupLevel");
            GbHapticsIntegration.ModHarmony.Patch(
                AccessTools.Method(typeof(GameController), "SetupLevel"),
                AccessTools.Method(typeof(H_GameController), "SetupLevel_Prefix").ToNewHarmonyMethod());
        }

        private static void SetupLevel_Prefix(GameController __instance)
        {
            if ((__instance.player == null)
                || (__instance.player.damageRelay == null))
                return;

            FakeParent oldparent = __instance.player.damageRelay.gameObject.GetComponent<FakeParent>();

            PlayerFakeParent newparent = __instance.player.damageRelay.gameObject.AddComponent<PlayerFakeParent>();
            newparent.fakeParentTo = oldparent.fakeParentTo;
            GameObject.DestroyImmediate(oldparent);
        }
    }
}

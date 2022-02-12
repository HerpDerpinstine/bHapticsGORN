using HarmonyLib;
using UnityEngine;
using MelonLoader;
using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Hooks
{
    internal static class H_FakeParent
    {
        internal static void Initialize()
        {
            Debug.LogPatchInit("FakeParent.Update");
            GbHapticsIntegration.ModHarmony.Patch(
                AccessTools.Method(typeof(FakeParent), "Update"),
                AccessTools.Method(typeof(H_FakeParent), "Update_Prefix").ToNewHarmonyMethod());
        }

        private static GameObject damageRelayGameObject;
        private static bool Update_Prefix(FakeParent __instance)
        {
            if (damageRelayGameObject == null)
                damageRelayGameObject = GameController.Player?.damageRelay?.gameObject;
            if ((damageRelayGameObject == null)
                || (__instance.transform.gameObject != damageRelayGameObject))
                return true;

            PlayerFakeParent newparent = __instance.transform.gameObject.AddComponent<PlayerFakeParent>();
            newparent.fakeParentTo = __instance.fakeParentTo;
            GameObject.DestroyImmediate(__instance);

            return false;
        }
    }
}

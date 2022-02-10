using GbHapticsIntegration.Managers;
using HarmonyLib;
using MelonLoader;

namespace GbHapticsIntegration.Hooks
{
    internal static class H_Gun
    {
        internal static void Initialize()
        {
            Debug.LogPatchInit("Gun.Fire");
            GbHapticsIntegration.ModHarmony.Patch(
                AccessTools.Method(typeof(Gun), "Fire"),
                null,
                AccessTools.Method(typeof(H_Gun), "Fire_PostFix").ToNewHarmonyMethod());
        }

        private static void Fire_PostFix(Gun __instance)
        {
            if (!__instance.wieldedByPlayer)
                return;
            M_Tact.WeaponsByType[WeaponType.Gun]?.OnShoot(__instance.grabbedByHand.ownerFist.left);
        }
    }
}

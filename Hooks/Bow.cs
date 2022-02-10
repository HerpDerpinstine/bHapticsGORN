using System;
using HarmonyLib;
using GbHapticsIntegration.Managers;
using MelonLoader;
using UnityEngine;

namespace GbHapticsIntegration.Hooks
{
    internal static class H_Bow
    {
        private static AccessTools.FieldRef<Bow, Arrow> Bow_nockedArrow = null;

        internal static void Initialize()
        {
            Debug.LogFieldRefGet("Bow.nockedArrow");
            Bow_nockedArrow = AccessTools.FieldRefAccess<Bow, Arrow>("nockedArrow");
            Debug.LogFieldRefFound("Bow.nockedArrow", Bow_nockedArrow);

            Type Type_Bow = typeof(Bow);
            Type Type_Patch_Bow = typeof(H_Bow);

            Debug.LogPatchInit("Bow.FireArrow");
            GbHapticsIntegration.ModHarmony.Patch(
                AccessTools.Method(Type_Bow, "FireArrow"),
                AccessTools.Method(Type_Patch_Bow, "FireArrow_Prefix").ToNewHarmonyMethod());

            Debug.LogPatchInit("Bow.RunSound");
            GbHapticsIntegration.ModHarmony.Patch(
                AccessTools.Method(Type_Bow, "RunSound"),
                AccessTools.Method(Type_Patch_Bow, "RunSound_Prefix").ToNewHarmonyMethod());
        }

        private static void FireArrow_Prefix(Bow __instance)
        {
            if ((__instance.grabbedBy == null)
                || (Bow_nockedArrow(__instance) == null)
                || !__instance.grabbedBy.isPlayer)
                return;

            M_Tact.WeaponsByType[WeaponType.Bow]?.OnShootString(__instance.grabbedBy.ownerFist.left);
        }

        private static void RunSound_Prefix(Bow __instance)
        {
            if ((__instance.grabbedBy == null)
                || (Bow_nockedArrow(__instance) == null)
                || !__instance.grabbedBy.isPlayer)
                return;

            Vector3 vector = __instance.nockRestPoint.position - Bow_nockedArrow(__instance).transform.position;
            __instance.nockPoint.transform.localPosition = (__instance.bowStringAttachPoints[1].localPosition + __instance.bowStringAttachPoints[0].localPosition) * 0.5f;
            float magnitude = Vector3.Distance(Bow_nockedArrow(__instance).transform.position, __instance.nockRestPoint.position);
            if (vector.magnitude < Vector3.Distance(__instance.nockPoint.transform.position, __instance.nockRestPoint.transform.position))
                magnitude = 0f;

            M_Tact.WeaponsByType[WeaponType.Bow]?.OnDrawString(magnitude, !__instance.grabbedBy.ownerFist.left);
        }
    }
}

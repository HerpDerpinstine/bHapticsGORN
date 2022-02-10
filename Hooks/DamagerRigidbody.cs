using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using GbHapticsIntegration.Managers;
using HarmonyLib;
using UnityEngine;

namespace GbHapticsIntegration.Hooks
{
    internal static class H_DamagerRigidbody
    {
        private static MethodInfo GrabHand_HitSomething = null;
        internal static FieldInfo DamagerRigidbody_velocity = null;

        internal static void Initialize()
        {
            Type Type_GrabHand = typeof(GrabHand);
            Type Type_DamagerRigidbody = typeof(DamagerRigidbody);

            Debug.LogFieldRefGet("DamagerRigidbody.velocity");
            DamagerRigidbody_velocity = AccessTools.Field(Type_DamagerRigidbody, "velocity");
            Debug.LogFieldRefFound("DamagerRigidbody.velocity", DamagerRigidbody_velocity);

            Debug.LogMethodGet("GrabHand.HitSomething");
            GrabHand_HitSomething = AccessTools.Method(Type_GrabHand, "HitSomething");
            Debug.LogMethodFound("GrabHand.HitSomething", GrabHand_HitSomething);

            Debug.LogPatchInit("DamagerRigidbody.OnCollisionEnter");
            GbHapticsIntegration.ModHarmony.Patch(AccessTools.Method(Type_DamagerRigidbody, "OnCollisionEnter"),
                new HarmonyMethod(AccessTools.Method(typeof(H_DamagerRigidbody), "OnCollisionEnter_Prefix")),
                new HarmonyMethod(AccessTools.Method(typeof(H_DamagerRigidbody), "OnCollisionEnter_PostFix")),
                new HarmonyMethod(AccessTools.Method(typeof(H_DamagerRigidbody), "OnCollisionEnter_Transpiler")));
        }
        
        private static Dictionary<DamagerRigidbody, bool> was_damaging = new Dictionary<DamagerRigidbody, bool>();
        private static void OnCollisionEnter_Prefix(DamagerRigidbody __instance) => was_damaging[__instance] = false;
        private static void OnCollisionEnter_WasDamaged(DamagerRigidbody damager) => was_damaging[damager] = true;
        private static void OnCollisionEnter_PostFix(DamagerRigidbody __instance, Collision collision)
        {
            if (was_damaging[__instance])
                return;
            M_Player.HitSomething(__instance, collision);
        }
        private static IEnumerable<CodeInstruction> OnCollisionEnter_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newinstructions = new List<CodeInstruction>();
            foreach (CodeInstruction inst in instructions)
            {
                newinstructions.Add(inst);

                if ((inst.opcode == OpCodes.Callvirt)
                    && ((MethodInfo)inst.operand) == H_DamageRelay.DamageRelay_Damage)
                {
                    Debug.LogPatchInit("DamageRelay.Damage Call");
                    newinstructions.AddRange(new CodeInstruction[]{
                        new CodeInstruction(OpCodes.Ldarg_0),
                        new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(H_DamagerRigidbody), "OnCollisionEnter_WasDamaged")),
                    });
                }
            }
            return newinstructions;
        }
    }
}

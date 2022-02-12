using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using GbHapticsIntegration.Managers;
using System.Reflection;
using UnityEngine;
using MelonLoader;

namespace GbHapticsIntegration.Hooks
{
    internal static class H_PlayerDamageRelay
    {
        private static MethodInfo BloodController_CreateImpact = null;

        internal static void Initialize()
        {
            Type Type_PlayerDamageRelay = typeof(PlayerDamageRelay);

            Debug.LogMethodGet("BloodController.CreateImpact");
            BloodController_CreateImpact = AccessTools.Method(typeof(BloodController), "CreateImpact");
            Debug.LogMethodFound("BloodController.CreateImpact", BloodController_CreateImpact);

            Debug.LogPatchInit("PlayerDamageRelay.Update");
            GbHapticsIntegration.ModHarmony.Patch(AccessTools.Method(Type_PlayerDamageRelay, "Update"), 
                null,
                null,
                AccessTools.Method(typeof(H_PlayerDamageRelay), "Update_Transpiler").ToNewHarmonyMethod());

            Debug.LogPatchInit("PlayerDamageRelay.Damage");
            GbHapticsIntegration.ModHarmony.Patch(AccessTools.Method(Type_PlayerDamageRelay, "Damage"),
                null,
                null,
                AccessTools.Method(typeof(H_PlayerDamageRelay), "Damage_Transpiler").ToNewHarmonyMethod());
        }

        private static IEnumerable<CodeInstruction> Update_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newinstructions = new List<CodeInstruction>();
            foreach (CodeInstruction inst in instructions)
            {
                if ((inst.opcode == OpCodes.Ldstr)
                    && (inst.operand != null)
                    && !string.IsNullOrEmpty((string)inst.operand)
                    && ((string)inst.operand).Equals("HeartBeat"))
                {
                    Debug.LogPatchInit("PlaySound HeartBeat Call");
                    newinstructions.Add(CodeInstruction.Call(() => M_Player.OnHeartBeat()));
                }
                newinstructions.Add(inst);
            }
            return newinstructions;
        }

        private static IEnumerable<CodeInstruction> Damage_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool should_add = true;
            List<CodeInstruction> newinstructions = new List<CodeInstruction>();
            foreach (CodeInstruction inst in instructions)
            {
                newinstructions.Add(inst);

                if (should_add
                    && (inst.opcode == OpCodes.Call)
                    && ((MethodInfo)inst.operand) == BloodController_CreateImpact)
                {
                    should_add = false;
                    Debug.LogPatchInit("BloodController.CreateImpact Call");
                    newinstructions.AddRange(new CodeInstruction[]{
                        new CodeInstruction(OpCodes.Ldarg_0),
                        new CodeInstruction(OpCodes.Ldarg_1),
                        new CodeInstruction(OpCodes.Ldarg_S, 4),
                        new CodeInstruction(OpCodes.Ldarg_S, 5),
                        new CodeInstruction(OpCodes.Ldarg_S, 7),
                        new CodeInstruction(OpCodes.Ldarg_S, 8),
                        new CodeInstruction(OpCodes.Ldarg_S, 9),
                        new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(H_PlayerDamageRelay), "Damage")),
                    });
                }
            }
            return newinstructions;
        }

        private static void Damage(PlayerDamageRelay __instance, 
            DamageType damageType,
            Vector3 pos,
            GameObject sender, 
            Collision collision,
            AITargetable responsibleEntity,
            bool isFloor)
        {
            // Check for Valid DamagerRigidbody
            DamagerRigidbody damager = sender.GetComponent<DamagerRigidbody>();
            if (damager == null)
                return;

            // If Damager Is Floor
            if (isFloor)
                return;

            // Play
            M_Tact.playerDamage.Play(pos, (collision.relativeVelocity - H_DamageRelay.DamagerRigidbody_velocity(damager)));
        }
    }
}
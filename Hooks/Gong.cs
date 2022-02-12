using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using MelonLoader;
using GbHapticsIntegration.Managers;
using GbHapticsIntegration.Setup.Effects;

namespace GbHapticsIntegration.Hooks
{
    internal static class H_Gong
    {
        internal static void Initialize()
        {
            Type Type_Gong = typeof(Gong);

            Debug.LogPatchInit("Gong.RingInternal");
            GbHapticsIntegration.ModHarmony.Patch(AccessTools.Method(Type_Gong, "RingInternal"),
                null,
                AccessTools.Method(typeof(H_Gong), "RingInternal_PostFix").ToNewHarmonyMethod());

            /*
            Debug.LogPatchInit("Gong.OnCollisionEnter");
            GbHapticsIntegration.ModHarmony.Patch(AccessTools.Method(Type_Gong, "OnCollisionEnter"),
                null,
                null,
                AccessTools.Method(typeof(H_Gong), "OnCollisionEnter_Transpiler").ToNewHarmonyMethod());
            */
        }

        private static void Play(E_Gong.Strength strength)
            => M_Tact.gong.Play(E_Gong.Strength.Hard);
        private static void RingInternal_PostFix()
            => Play(E_Gong.Strength.Hard);

        private static IEnumerable<CodeInstruction> OnCollisionEnter_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newinstructions = new List<CodeInstruction>();
            foreach (CodeInstruction inst in instructions)
            {
                if ((inst.opcode == OpCodes.Ldstr)
                    && (inst.operand != null)
                    && !string.IsNullOrEmpty((string)inst.operand))
                {
                    if (((string)inst.operand).Equals("GongSoft"))
                        newinstructions.Add(CodeInstruction.Call(() => Play(E_Gong.Strength.Soft)));
                    else if (((string)inst.operand).Equals("GongMed"))
                        newinstructions.Add(CodeInstruction.Call(() => Play(E_Gong.Strength.Medium)));
                    else if (((string)inst.operand).Equals("GongHard"))
                        newinstructions.Add(CodeInstruction.Call(() => Play(E_Gong.Strength.Hard)));
                }
                newinstructions.Add(inst);
            }
            return newinstructions;
        }
    }
}
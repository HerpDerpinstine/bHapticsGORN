using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using GbHapticsIntegration.Managers;
using System.Reflection;

namespace GbHapticsIntegration.Hooks
{
    internal static class H_SurpriseBox
    {
        private static MethodInfo GameController_SpawnSurpriseBoxItem = null;

        internal static void Initialize()
        {
            Debug.LogMethodGet("GameController.SpawnSurpriseBoxItem");
            GameController_SpawnSurpriseBoxItem = AccessTools.Method(typeof(GameController), "SpawnSurpriseBoxItem");
            Debug.LogMethodFound("GameController.SpawnSurpriseBoxItem", GameController_SpawnSurpriseBoxItem);

            Debug.LogPatchInit("SurpriseBox.SpawnSurprise");
            GbHapticsIntegration.ModHarmony.Patch(AccessTools.Method(typeof(SurpriseBox), "SpawnSurprise"),
                null,
                null,
                new HarmonyMethod(AccessTools.Method(typeof(H_SurpriseBox), "SpawnSurprise_Transpiler")));
        }

        private static IEnumerable<CodeInstruction> SpawnSurprise_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newinstructions = new List<CodeInstruction>();
            foreach (CodeInstruction inst in instructions)
            {
                if ((inst.opcode == OpCodes.Ldstr)
                    && (inst.operand != null)
                    && (inst.opcode == OpCodes.Call)
                    && ((MethodInfo)inst.operand) == GameController_SpawnSurpriseBoxItem)
                {
                    Debug.LogPatchInit("GameController.SpawnSurpriseBoxItem Call");
                    newinstructions.Add(CodeInstruction.Call(() => M_Tact.surprise.Play()));
                }
                newinstructions.Add(inst);
            }
            return newinstructions;
        }
    }
}
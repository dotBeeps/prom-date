using Harmony;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;


[HarmonyPatch(typeof(EventManager))]
[HarmonyPatch("CalculateAndGetSuitableEvent")]
public static class EventManager_CalculateAndGetSuitableEvent_Patch
{
    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        for (int i = codes.Count-1; i > 0; i--)
        {
            if (codes[i].opcode == OpCodes.Ldloc_3)
            {
                codes.Insert(i, new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Modloader), "Instance")));
                codes.Insert(i + 2, new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(Modloader), "CheckModEvents")));
                break;
            }
        }
        return codes.AsEnumerable();
    }
}
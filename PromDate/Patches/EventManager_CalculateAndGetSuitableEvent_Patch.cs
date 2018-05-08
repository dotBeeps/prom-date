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

    public static bool Prefix(ref int __result)
    {
        int modEventToTakeControl = EventHelper.Instance.CheckModEvents();
        if (modEventToTakeControl != -1)
        {
            __result = modEventToTakeControl;
            return false;
        }
        return true;
    }

}
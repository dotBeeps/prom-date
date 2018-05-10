using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[HarmonyPatch(typeof(EventManager))]
[HarmonyPatch("StartEvent")]
[HarmonyPatch(new Type[] { typeof(int) })]
class EventManager_StartEvent_Patch
{
    static void Prefix(ref int aEventIndex)
    {
        EventHelper.EventId = aEventIndex;
    }
}


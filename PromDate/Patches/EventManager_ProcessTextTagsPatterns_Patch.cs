using Harmony;
using PromDate.EventLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[HarmonyPatch(typeof(EventManager))]
[HarmonyPatch("ProcessTextTagsPatterns")]
class EventManager_ProcessTextTagsPatterns_Patch
{
    static void Prefix(ref string aText)
    {
        CustomChatEffects.ApplyChatEffects(ref aText);
    }
}

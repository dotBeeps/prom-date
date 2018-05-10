using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;

[HarmonyPatch(typeof(EventManager))]
[HarmonyPatch("L_AdvanceStepCurrentEvent")]
public static class EventManager_L_AdvanceStepCurrentEvent_Patch
{
    /*static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        for (int i = codes.Count - 1; i > 0; i--)
        {
            if (codes[i].opcode == OpCodes.Br)
            {
                codes.Insert(i + 1, new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(SpriteLoader), "Instance")));
                codes.Insert(i + 2, new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(EventManager), "WhoSpeaksSticker")));
                codes.Insert(i + 3, new CodeInstruction(OpCodes.Ldloc_S, 12));
                codes.Insert(i + 4, new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(SpriteLoader), "ModTalkingSprite", new Type[] { typeof(string), typeof(UnityEngine.Sprite) } )));
                codes.Insert(i + 5, new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(UI2DSprite), "set_sprite2D", new Type[] { typeof(UnityEngine.Sprite) } )));
                break;
            }
        }
        return codes.AsEnumerable();
    }*/
    static void Postfix()
    {
        FieldInfo stickerField = typeof(EventManager).GetField("WhoSpeaksSticker");
        if (EventHelper.CurrentSpeaker != null && EventHelper.CurrentSpeaker != "")
        {
            if (!ModConstants.VANILLA_CHARACTERS.Contains(EventHelper.CurrentSpeaker.ToUpper()))
            {
                UI2DSprite stickerSprite2d = (UI2DSprite)stickerField.GetValue(EventManager.Instance);
                Sprite sprite = SpriteLoader.Instance.ModTalkingSprite(EventHelper.CurrentSpeaker);
                stickerSprite2d.sprite2D = sprite;
            }
        }
    }
}
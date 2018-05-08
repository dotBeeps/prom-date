using Harmony;
using NGameConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[HarmonyPatch(typeof(EventManager))]
[HarmonyPatch("L_OnEventEnd")]
class Event_Manager_L_OnEventEnd_Patch
{
    static bool Prefix(ref int aEventEnded)
    {
        if (EndingHelper.Instance.IsModEnding(aEventEnded))
        {
            AudioController.Play("Game_SuccessEnding");
            EndingHelper.FinalSceneInfo info = EndingHelper.Instance.GetFinalSceneInfo(aEventEnded);
            if (info.AudioClip != null && info.AudioClip != "")
            {
                AudioController.Play(info.AudioClip);
            }
            Sprite illustrationPlayer = null;
            switch (GameManager.Instance.CurrentPlayerColor)
            {
                case EPlayerColor.Blue:
                    illustrationPlayer = GameManager.Instance.Event_Ending_Success_BLUE;
                    break;
                case EPlayerColor.Green:
                    illustrationPlayer = GameManager.Instance.Event_Ending_Success_GREEN;
                    break;
                case EPlayerColor.Red:
                    illustrationPlayer = GameManager.Instance.Event_Ending_Success_RED;
                    break;
                case EPlayerColor.Yellow:
                    illustrationPlayer = GameManager.Instance.Event_Ending_Success_YELLOW;
                    break;
            }
            for (int i = 0; i < EventManager.Instance.Events[info.SceneId].EventScenes.Length; i++)
            {
                EventManager.Instance.Events[info.SceneId].EventScenes[i].Illustration_Player = illustrationPlayer;
            }
            EventManager.Instance.StartEvent(info.SceneId);
            return false;
        }
        return true;
    }
}


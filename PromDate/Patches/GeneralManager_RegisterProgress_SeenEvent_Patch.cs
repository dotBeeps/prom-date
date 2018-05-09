using Harmony;
using System;
using System.Linq;

[HarmonyPatch(typeof(GeneralManager))]
[HarmonyPatch("RegisterProgress_SeenEvent")]
class GeneralManager_RegisterProgress_SeenEvent_Patch
{
    public static bool Prefix(string aEventName, int aEventChoiceEnum)
    {
        if (EventManager.Instance.Events.First(ev => ev.EventName.Contains(aEventName)).ArgumentTags.Any(tag => tag == "MOD"))
        {
            int eventIndex = ProgressTracker.ModEventsSeenAllTime.Select((value, index) => new { value, index = index + 1 }).Where(pair => pair.value.Contains(aEventName)).Select(pair => pair.index).FirstOrDefault() - 1;
            if (eventIndex == -1)
            {
                GameManager.Instance.RegisterThisRunProgress_NewEvent();
                ProgressTracker.ModEventsSeenAllTime.Add(EventManager.Instance.Events.First(ev => ev.EventName.Contains(aEventName)).EventName.Split(new string[] { ": " }, StringSplitOptions.None)[1]);
                ProgressTracker.ModEventChoicesSeenAllTime.Add(new bool[4]);
                if (aEventChoiceEnum >= 0 && aEventChoiceEnum <= 3)
                {
                    ProgressTracker.ModEventChoicesSeenAllTime.Last()[aEventChoiceEnum] = true;
                    GameManager.Instance.RegisterThisRunProgress_NewOutcomes(1);
                }
            }
            else
            {
                if (aEventChoiceEnum >= 0 && aEventChoiceEnum <= 3)
                {
                    if (!ProgressTracker.ModEventChoicesSeenAllTime[eventIndex][aEventChoiceEnum])
                        GameManager.Instance.RegisterThisRunProgress_NewOutcomes(1);
                    ProgressTracker.ModEventChoicesSeenAllTime[eventIndex][aEventChoiceEnum] = true;
                }
            }
            return false;
        }
        return true;
    }
}
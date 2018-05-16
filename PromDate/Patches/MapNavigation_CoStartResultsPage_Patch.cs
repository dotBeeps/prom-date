using Harmony;
using PromDate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[HarmonyPatch(typeof(MapNavigation))]
[HarmonyPatch("CoStartResultsPage")]
class MapNavigation_CoStartResultsPage_Patch
{
    public static void Prefix()
    {
        GeneralManager.Instance.Progress_SecretEndingsSeenAllTime_EventName = GeneralManager.Instance.Progress_SecretEndingsSeenAllTime_EventName.Concat(ProgressTracker.ModEndingsSeenAllTime).ToList();
        GeneralManager.Instance.Progress_EventsSeenAllTime_EventName = GeneralManager.Instance.Progress_EventsSeenAllTime_EventName.Concat(ProgressTracker.ModEventsSeenAllTime).ToList();
        GeneralManager.Instance.Progress_EventsSeenAllTime_Choices = GeneralManager.Instance.Progress_EventsSeenAllTime_Choices.Concat(ProgressTracker.ModEventChoicesSeenAllTime).ToList();
    }
}

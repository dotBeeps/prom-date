using Harmony;
using System.Collections.Generic;
using System.Linq;

[HarmonyPatch(typeof(SaveDataManager))]
[HarmonyPatch("SaveProgress")]
class SaveDataManager_SaveProgress_Patch
{
    public static void Prefix()
    {
        if (GeneralManager.Instance.Progress_SecretEndingsSeenAllTime_EventName == null)
            GeneralManager.Instance.Progress_SecretEndingsSeenAllTime_EventName = new List<string>();
        if (GeneralManager.Instance.Progress_EventsSeenAllTime_EventName == null)
            GeneralManager.Instance.Progress_EventsSeenAllTime_EventName = new List<string>();
        if (GeneralManager.Instance.Progress_EventsSeenAllTime_Choices == null)
            GeneralManager.Instance.Progress_EventsSeenAllTime_Choices = new List<bool[]>();

        GeneralManager.Instance.Progress_SecretEndingsSeenAllTime_EventName = GeneralManager.Instance.Progress_SecretEndingsSeenAllTime_EventName.Except(ProgressTracker.ModEndingsSeenAllTime).ToList();
        GeneralManager.Instance.Progress_EventsSeenAllTime_EventName = GeneralManager.Instance.Progress_EventsSeenAllTime_EventName.Except(ProgressTracker.ModEventsSeenAllTime).ToList();
        GeneralManager.Instance.Progress_EventsSeenAllTime_Choices = GeneralManager.Instance.Progress_EventsSeenAllTime_Choices.Except(ProgressTracker.ModEventChoicesSeenAllTime).ToList();
        ProgressTracker.SaveModProgress();
    }
}

using Harmony;
using System;
using System.Linq;

[HarmonyPatch(typeof(GeneralManager))]
[HarmonyPatch("RegisterProgress_SecretEnding")]
class GeneralManager_RegisterProgress_SecretEnding_Patch
{
    public static bool Prefix(string aEventName, string aNpc)
    {
        if (EventManager.Instance.Events.First(ev => ev.EventName.Contains(aEventName)).ArgumentTags.Any(tag => tag == "MOD"))
        {
            int eventIndex = ProgressTracker.ModEndingsSeenAllTime.Select((value, index) => new { value, index = index + 1 }).Where(pair => pair.value.Contains(aEventName)).Select(pair => pair.index).FirstOrDefault() - 1;
            if (eventIndex == -1)
            {
                GameManager.Instance.RegisterThisRunProgress_NewSecretEnding();
                ProgressTracker.ModEndingsSeenAllTime.Add(EventManager.Instance.Events.First(ev => ev.EventName.Contains(aEventName)).EventName.Split(new string[] { ": " }, StringSplitOptions.None)[1]);
                ProgressTracker.ModEndingsSeenCount.Add(1);
            } else
            {
                ProgressTracker.ModEndingsSeenCount[eventIndex]++;
            }
            return false;
        }
        return true;
    }
}

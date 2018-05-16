using Harmony;
using PromDate;

[HarmonyPatch(typeof(SaveDataManager))]
[HarmonyPatch("LoadProgress")]
class SaveDataManager_LoadProgress_Patch
{
    public static void Prefix(bool aIncrementalData)
    {
        ProgressTracker.LoadModProgress();
    }
}

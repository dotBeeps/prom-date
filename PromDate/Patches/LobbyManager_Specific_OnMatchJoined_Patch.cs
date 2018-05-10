using Harmony;
using UnityEngine.Networking.Match;

[HarmonyPatch(typeof(LobbyManager_Specific))]
[HarmonyPatch("OnMatchJoined")]
class LobbyManager_Specific_OnMatchJoined_Patch
{
    public static bool Prefix(LobbyManager_Specific __instance, bool success, ref string extendedInfo, MatchInfo matchInfo)
    {
        if (!success)
        {
            if (PopupOnlineHelper.Instance != null)
            {
                PopupOnlineHelper.Instance.ShowPopup("Could not join " + ReflectionHelper.GetPrivateField<string>(typeof(LobbyManager_Specific), __instance, "mLastMatchSearched") + "\nLikely due to a mod mismatch.");
            }
            typeof(LobbyManager_Specific).GetMethod("CancelClientOperation").Invoke(__instance, new object[0]);
            return false;
        }
        return true;
    }
}

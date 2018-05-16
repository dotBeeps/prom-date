using Harmony;
using PromDate;
using System;
using System.Reflection;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

[HarmonyPatch(typeof(NetworkMatch))]
[HarmonyPatch("JoinMatch")]
[HarmonyPatch(new Type[] { typeof(NetworkID), typeof(string), typeof(string), typeof(string), typeof(int), typeof(int), typeof(NetworkMatch.DataResponseDelegate<MatchInfo>) })]
class NetworkManager_JoinMatch_Patch
{
    public static void Prefix(NetworkID netId, ref string matchPassword, string publicClientAddress, string privateClientAddress, int eloScoreForClient, int requestDomain, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
    {
        matchPassword += Modloader.Instance.GenerateMultiplayerPassword();
    }
}
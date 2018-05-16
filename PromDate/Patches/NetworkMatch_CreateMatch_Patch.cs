using Harmony;
using PromDate;
using System;
using System.Reflection;
using UnityEngine.Networking.Match;

[HarmonyPatch(typeof(NetworkMatch))]
[HarmonyPatch("CreateMatch")]
[HarmonyPatch(new Type[] { typeof(string), typeof(uint), typeof(bool), typeof(string), typeof(string), typeof(string), typeof(int), typeof(int), typeof(NetworkMatch.DataResponseDelegate<MatchInfo>) })]
class NetworkManager_CreateMatch_Patch
{
    public static void Prefix(string matchName, uint matchSize, bool matchAdvertise, ref string matchPassword, string publicClientAddress, string privateClientAddress, int eloScoreForMatch, int requestDomain, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
    {
        matchPassword += Modloader.Instance.GenerateMultiplayerPassword();
    }
}

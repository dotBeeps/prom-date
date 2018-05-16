using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace MonsterPromDiscordRPC
{
    public class MonsterPromDRPC : IMod
    {
        private bool _init;
        private bool inLobby;
        private string lobbyId;
        public static readonly DiscordRpc.RichPresence Presence = new DiscordRpc.RichPresence();
        private const string DiscordAppID = "445781699540942848";

        public string Name { get { return "DiscordRPC"; } }

        public string Author { get { return "FoolishDave"; } }

        public Version Version { get; } = new Version(0, 0, 3);

        public void Awake()
        {
            if (_init) return;
            _init = true;
            GeneralManager.Instance.LogToFileOrConsole("Discord RPC Loading In.");
            var discordHandlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize(DiscordAppID, ref discordHandlers, false, string.Empty);
        }

        public void Update()
        {
            DiscordRpc.RunCallbacks();
            if (inLobby && LobbyManager_Specific.Instance != null)
            {
                string id = typeof(LobbyManager_Specific).GetField("mCreatedMatchAtHost", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(LobbyManager_Specific.Instance) as string;
                if (id.Contains('#'))
                    id = id.Replace("MonsterProm_", string.Empty).Replace("#", string.Empty);
                if (lobbyId != id)
                {
                    lobbyId = id;
                    Presence.state = lobbyId;
                    DiscordRpc.UpdatePresence(Presence);
                }
            }
        }

        public void ApplicationQuit()
        {
            DiscordRpc.Shutdown();
        }

        public void SceneLoaded(Scene loaded)
        {
            switch (loaded.buildIndex)
            {
                case 1:
                    inLobby = false;
                    Presence.details = "Main Menu";
                    Presence.state = string.Empty;
                    Presence.startTimestamp = default(long);
                    Presence.largeImageKey = "default";
                    Presence.largeImageText = "Monster Prom";
                    DiscordRpc.UpdatePresence(Presence);
                    break;
                case 4:
                    inLobby = false;
                    Presence.details = "Taking a pop quiz!";
                    Presence.state = string.Empty;
                    Presence.startTimestamp = default(long);
                    Presence.largeImageKey = "default";
                    Presence.largeImageText = "Monster Prom";
                    Presence.smallImageKey = "smart";
                    Presence.smallImageText = "Personality Quiz";
                    DiscordRpc.UpdatePresence(Presence);
                    break;
                case 6:
                    inLobby = false;
                    Presence.details = "In Game";
                    Presence.state = "Getting that monster booty.";
                    Presence.startTimestamp = default(long);
                    Presence.largeImageKey = "default";
                    Presence.largeImageText = "Monster Prom";
                    Presence.smallImageKey = "bold";
                    Presence.smallImageText = "In Game";
                    DiscordRpc.UpdatePresence(Presence);
                    break;
                case 7:
                    inLobby = true;
                    Presence.details = "In Online Lobby";
                    Presence.state = string.Empty;
                    Presence.startTimestamp = default(long);
                    Presence.largeImageKey = "default";
                    Presence.largeImageText = "Monster Prom";
                    DiscordRpc.UpdatePresence(Presence);
                    break;
                default:
                    inLobby = false;
                    break;
            }
        }

        public void OnDisable()
        {
            DiscordRpc.Shutdown();
        }

        public void OnEnable()
        {
            var discordHandlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize(DiscordAppID, ref discordHandlers, false, string.Empty);
        }

        public void Start()
        {

        }

        public void FixedUpdate()
        {

        }

        public void LateUpdate()
        {

        }

        
    }
}

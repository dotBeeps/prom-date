using Harmony;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Modloader : MonoBehaviour
{
    public static Modloader Instance;
    public Dictionary<EventArgs, bool> potentialEvents;
    private List<string> loadedDllMods = new List<string>();

    public struct EventArgs
    {
        public string name;
        public NGameConstants.ETurnType turnType;
    }

    public Modloader()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            gameObject.AddComponent(typeof(SpriteLoader));
            gameObject.AddComponent(typeof(EventHelper));

            StartCoroutine(LoadAudioFiles());

            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading in the special sauce.", false, false);
            potentialEvents = new Dictionary<EventArgs, bool>();
            var harmony = HarmonyInstance.Create("com.foolishdave.monsterprom.promdate");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loaded the special sauce ( ͡° ͜ʖ ͡°)");

            SceneManager.activeSceneChanged += this.OnSceneLoaded;
        }
        
    }

    public void Start()
    {
        FileInfo[] files = (new DirectoryInfo(Application.dataPath + "/Mods")).GetFiles("*.dll");
        foreach (FileInfo file in files)
        {
            if (file.Name == "PromDate.dll") continue;
            try
            {
                LoadModDll(file);
            }
            catch
            {
                GeneralManager.Instance.LogToFileOrConsole("[PromDate] Failed to load in " + file);
            }
        }
    }

    public int CheckModEvents(int currentChoice)
    {
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Checking if any mod events want to take control.");
        List<EventArgs> wantToPlay = new List<EventArgs>();
        int idToPlay = currentChoice;
        foreach (KeyValuePair<EventArgs, bool> pair in Modloader.Instance.potentialEvents)
        {
            if (pair.Value && GameManager.Instance.GetCurrentTurnType() == pair.Key.turnType)
            {
                GeneralManager.Instance.LogToFileOrConsole("[PromDate] Getting id for event: " + pair.Key);
                wantToPlay.Add(pair.Key);
            }
        }
        if (wantToPlay.Count > 0)
        {
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Selecting random mod event.");
            EventArgs eventToPlay = wantToPlay[UnityEngine.Random.Range(0, wantToPlay.Count - 1)];
            int eventId = EventManager.Instance.Events.TakeWhile(ev => !ev.EventName.Contains(eventToPlay.name)).Count();
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Selected event: " + eventToPlay + " with id: " + eventId);
            Modloader.Instance.potentialEvents[eventToPlay] = false;
            idToPlay = eventId;
        }
        return idToPlay;
    }

    public void AddToModEvents(EventArgs ev)
    {
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Added new event: " + ev.name);
        potentialEvents.Add(ev, false);
    }

    public void RequestModEvent(EventArgs ev)
    {
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Event requested: " + ev);
        potentialEvents[ev] = true;
    }

    IEnumerator LoadAudioFiles()
    {
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading in audio files.");
        FileInfo[] files = (new DirectoryInfo(Application.dataPath + "/Mods/Audio")).GetFiles("*.*").Where(f => f.Name.ToLower().EndsWith(".ogg") || f.Name.ToLower().EndsWith(".mp3") || f.Name.ToLower().EndsWith(".wav") || f.Name.ToLower().EndsWith(".xm") || f.Name.ToLower().EndsWith(".it") || f.Name.ToLower().EndsWith(".mod") || f.Name.ToLower().EndsWith(".s3m")).ToArray();
        foreach (FileInfo file in files)
        {
            WWW www = new WWW("file:///" + file.FullName);
            yield return www;
            if (www.error != null)
            {
                GeneralManager.Instance.LogToFileOrConsole(www.error);
            }
            AudioClip clip = www.GetAudioClip(false, false);
            clip.LoadAudioData();
            clip.name = file.Name.Split('.')[0];
            while (clip.loadState == AudioDataLoadState.Loading || clip.loadState == AudioDataLoadState.Unloaded)
                yield return 0;
            string category;
            if (clip.name.Split('_').Length > 1)
            {
                category = clip.name.Split('_')[0].ToUpper();
            } else
            {
                category = "VOICE";
            }
            AudioItem audioItem = AudioController.AddToCategory(AudioController.GetCategory(category), clip, clip.name);
            ((Dictionary<string, AudioItem>)typeof(AudioController).GetField("_audioItems", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(AudioController.Instance)).Add(audioItem.Name, audioItem);
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene oldScene, UnityEngine.SceneManagement.Scene newScene)
    {
        if (GeneralManager.Instance.IsInOnlineMode)
        {
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Hope all of you have the mod installed, online support is basically nonexistent at the moment!");
        }
        if (newScene.name == "InGame_School")
        {
            LoadNewEvents();
        }
    }

    private void LoadModDll(FileInfo file)
    {
        var assembly = Assembly.LoadFrom(file.FullName);
        var modType = assembly.GetType("Mod");
        gameObject.AddComponent(modType);
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loaded dll mod: " + file.Name);
    }

    private void LoadNewEvents()
    {
        string path = Application.dataPath + "/Mods/Events/";
        string[] files = Directory.GetFiles(path, "*.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(EventContainer));
        List<EventManager.CEventFlow> list = new List<EventManager.CEventFlow>();
        int num = 0;
        foreach (string text in files)
        {
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading event from " + text, false, false);
            EventContainer eventContainer = EventContainer.Load(text);
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] File has " + eventContainer.Events.Count + " events.");
            foreach (Event eve in eventContainer.Events)
            {
                GeneralManager.Instance.LogToFileOrConsole("\t[PromDate] Loading event " + eve.Name, false, false);
                list.Add(EventContainer.eventToFlow(eve, EventManager.Instance.Events.Length + num));
                num++;
            }
        }
        EventManager.Instance.Events = EventManager.Instance.Events.Concat(list.ToArray()).ToArray<EventManager.CEventFlow>();
    }
}

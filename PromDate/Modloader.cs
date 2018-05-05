using Harmony;
using System;
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
    public delegate string WantsToPlayEvent();
    public List<WantsToPlayEvent> potentialEvents = new List<WantsToPlayEvent>();
    private List<string> loadedDllMods = new List<string>();

    public Modloader()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading in the special sauce.", false, false);
        var harmony = HarmonyInstance.Create("com.foolishdave.monsterprom.promdate");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loaded the special sauce ( ͡° ͜ʖ ͡°)");

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

        SceneManager.activeSceneChanged += this.OnSceneLoaded;
    }

    

    public void Update()
    {
        
    }

    public int CheckModEvents(int currentChoice)
    {
        int eventToPlay = currentChoice;
        List<int> wantToPlay = new List<int>();
        foreach (WantsToPlayEvent del in potentialEvents)
        {
            string evName = del.Invoke();
            int evIndex = EventManager.Instance.Events.Select((value, index) => new { value, index = index + 1 })
                .Where(eve => eve.value.EventName.Contains(evName))
                .Select(pair => pair.index)
                .FirstOrDefault() - 1;
            if (evIndex != -1) wantToPlay.Add(evIndex);
        }
        if (wantToPlay.Count > 0)
            eventToPlay = wantToPlay[UnityEngine.Random.Range(0, wantToPlay.Count - 1)];
        return eventToPlay;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene oldScene, UnityEngine.SceneManagement.Scene newScene)
    {
        if (GeneralManager.Instance.IsInOnlineMode)
        {
            GeneralManager.Instance.LogToFileOrConsole("[Prom Date] Hope all of you have the mod installed, online support is basically nonexistent at the moment!", false, false);
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
            GeneralManager.Instance.LogToFileOrConsole("[Prom Date] Loading event from " + text, false, false);
            EventContainer eventContainer = EventContainer.Load(text);
            GeneralManager.Instance.LogToFileOrConsole("[Prom Date] File has " + eventContainer.Events.Count + " events.");
            foreach (Event eve in eventContainer.Events)
            {
                GeneralManager.Instance.LogToFileOrConsole("\t[Prom Date] Loading event " + eve.Name, false, false);
                list.Add(EventContainer.eventToFlow(eve, EventManager.Instance.Events.Length + num));
                num++;
            }
        }
        EventManager.Instance.Events = EventManager.Instance.Events.Concat(list.ToArray()).ToArray<EventManager.CEventFlow>();
    }
}

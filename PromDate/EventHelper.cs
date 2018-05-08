using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using NGameConstants;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

class EventHelper : MonoBehaviour
{

    private EventManager.CEventFlow cEvent;
    private int eId;

    public static EventHelper Instance;
    public Dictionary<EventArgs, bool> potentialEvents;

    public struct EventArgs
    {
        public string name;
        public NGameConstants.ETurnType turnType;
    }

    public EventManager.CEventFlow CurrentEvent
    {
        get { return cEvent; }
        set { cEvent = value;
            eId = EventManager.Instance.Events.TakeWhile(ev => ev != value).Count();
        }
    }
    public int EventId
    {
        get { return eId; }
        set { eId = value;
            cEvent = EventManager.Instance.Events[value];
        }
    }

    public int SceneIndex
    {
        get { return (int)typeof(EventManager).GetField("mEventSceneCounter", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(EventManager.Instance); }
    }

    public string CurrentSpeaker
    {
        get {
            if (CurrentEvent != null)
                if (SceneIndex < cEvent.EventScenes.Length)
                    return cEvent.EventScenes[SceneIndex].WhoSpeaksNPC;
            return null;
        }
    }

    public EventHelper()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            potentialEvents = new Dictionary<EventArgs, bool>();
        }
    }

    public void AddCharacter(string name, bool lovable = false, bool minor = false, bool speaker = true)
    {
        if (lovable && !GameConstants.LovableNpcs.Contains(name.ToUpper())) GameConstants.LovableNpcs = GameConstants.LovableNpcs.Concat(new string[] { name.ToUpper() }).ToArray();
        if (minor && !GameConstants.LovableNpcs.Contains(name.ToUpper())) GameConstants.MinorNpcs = GameConstants.MinorNpcs.Concat(new string[] { name.ToUpper() }).ToArray();
        if (speaker && !GameConstants.LovableNpcs.Contains(name.ToUpper())) GameConstants.NpcSpeakers = GameConstants.NpcSpeakers.Concat(new string[] { name.ToUpper() }).ToArray();
    }

    public void LoadNewEvents()
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

    public int CheckModEvents(int currentChoice)
    {
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Checking if any mod events want to take control.");
        List<EventArgs> wantToPlay = new List<EventArgs>();
        int idToPlay = currentChoice;
        foreach (KeyValuePair<EventArgs, bool> pair in EventHelper.Instance.potentialEvents)
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
            EventHelper.Instance.potentialEvents[eventToPlay] = false;
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
}

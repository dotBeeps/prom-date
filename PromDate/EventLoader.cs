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
using static EventManager;

public class EventLoader : MonoBehaviour
{
    public static EventLoader Instance;
    public Dictionary<EventArgs, bool> potentialEvents = new Dictionary<EventArgs, bool>();
    public List<CustomEventMod> customEventMods = new List<CustomEventMod>();
    public List<CEventFlow> customEvents = new List<CEventFlow>();

    public struct EventArgs
    {
        public string name;
        public NGameConstants.ETurnType turnType;
    }

    public EventLoader()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            LoadNewEvents();
        }
    }

    private void LoadNewEvents()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(EventContainer));
        int num = 0;
        string[] directories = Directory.GetDirectories(Application.dataPath + "/Mods");
        foreach (string directory in directories)
        {
            string modInfo = Directory.GetFiles(directory, "*.xml").FirstOrDefault();
            if (string.IsNullOrEmpty(modInfo)) continue;
            CustomEventMod mod = CustomEventMod.Load(modInfo);
            customEventMods.Add(mod);
            string[] files = Directory.GetFiles(directory + "/Events", "*.xml");
            foreach (string text in files)
            {
                GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading event from " + text, false, false);
                EventContainer eventContainer = EventContainer.Load(text);
                GeneralManager.Instance.LogToFileOrConsole("[PromDate] File has " + eventContainer.Events.Count + " events.");
                foreach (Event eve in eventContainer.Events)
                {
                    GeneralManager.Instance.LogToFileOrConsole("\t[PromDate] Loading event " + eve.Name, false, false);
                    CEventFlow newCustomEvent = EventContainer.eventToFlow(eve);
                    newCustomEvent.EventName = EventHelper.AppendModName(newCustomEvent.EventName, mod);
                    customEvents.Add(newCustomEvent);
                    num++;
                }
            }
        }
    }

    public void AddEventsToGame()
    {
        CEventFlow[] eventsToLoad = customEvents.ToArray();
        int index = EventManager.Instance.Events.Length;
        foreach (CEventFlow eve in eventsToLoad)
        {
            eve.EventName = index + ": " + eve.EventName;
            eve.ContinuityData.Option1Success_ContinuityIndex = eve.ContinuityData.Option1Success_ContinuityIndex + index;
            eve.ContinuityData.Option1Failure_ContinuityIndex = eve.ContinuityData.Option1Failure_ContinuityIndex + index;
            eve.ContinuityData.Option2Success_ContinuityIndex = eve.ContinuityData.Option2Success_ContinuityIndex + index;
            eve.ContinuityData.Option2Failure_ContinuityIndex = eve.ContinuityData.Option2Failure_ContinuityIndex + index;
        }

        EventManager.Instance.Events = EventManager.Instance.Events.Concat(eventsToLoad).ToArray();
    }

    public int CheckModEvents()
    {
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Checking if any mod events want to take control.");
        List<EventArgs> wantToPlay = new List<EventArgs>();
        int idToPlay = -1;
        foreach (KeyValuePair<EventArgs, bool> pair in EventLoader.Instance.potentialEvents)
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
            EventLoader.Instance.potentialEvents[eventToPlay] = false;
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

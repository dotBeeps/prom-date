using NGameConstants;
using PromDate.EventLoader;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static PromDate.EventLoader.EventLoader;

public static class EventHelper
{
    private static EventManager.CEventFlow cEvent;
    private static int eId;
    private static Dictionary<EventArgs, bool> potentialEvents = new Dictionary<EventArgs, bool>();

    public static EventManager.CEventFlow CurrentEvent
    {
        get { return cEvent; }
        set
        {
            cEvent = value;
            eId = EventManager.Instance.Events.TakeWhile(ev => ev != value).Count();
        }
    }
    public static int EventId
    {
        get { return eId; }
        set
        {
            eId = value;
            cEvent = EventManager.Instance.Events[value];
        }
    }

    public static int SceneIndex
    {
        get { return (int)typeof(EventManager).GetField("mEventSceneCounter", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(EventManager.Instance); }
    }

    public static string CurrentSpeaker
    {
        get
        {
            if (CurrentEvent != null)
                if (SceneIndex < cEvent.EventScenes.Length)
                    return cEvent.EventScenes[SceneIndex].WhoSpeaksNPC;
            return null;
        }
    }

    public static string AppendModName(string from, CustomEventMod mod)
    {
        return from + "_" + mod.Name.Replace(' ', '_');
    }

    public static void AddCharacter(string name, bool lovable = false, bool minor = false, bool speaker = true)
    {
        if (lovable && !GameConstants.LovableNpcs.Contains(name.ToUpper())) GameConstants.LovableNpcs = GameConstants.LovableNpcs.Concat(new string[] { name.ToUpper() }).ToArray();
        if (minor && !GameConstants.LovableNpcs.Contains(name.ToUpper())) GameConstants.MinorNpcs = GameConstants.MinorNpcs.Concat(new string[] { name.ToUpper() }).ToArray();
        if (speaker && !GameConstants.LovableNpcs.Contains(name.ToUpper())) GameConstants.NpcSpeakers = GameConstants.NpcSpeakers.Concat(new string[] { name.ToUpper() }).ToArray();
    }

    public static int CheckModEvents()
    {
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Checking if any mod events want to take control.");
        List<EventArgs> wantToPlay = new List<EventArgs>();
        int idToPlay = -1;
        foreach (KeyValuePair<EventArgs, bool> pair in potentialEvents)
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
            potentialEvents[eventToPlay] = false;
            idToPlay = eventId;
        }
        return idToPlay;
    }

    public static void AddToModEvents(EventArgs ev)
    {
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Added new event: " + ev.name);
        potentialEvents.Add(ev, false);
    }

    public static void RequestModEvent(EventArgs ev)
    {
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Event requested: " + ev);
        potentialEvents[ev] = true;
    }
}

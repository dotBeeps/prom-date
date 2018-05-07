using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using NGameConstants;

class EventHelper : MonoBehaviour
{

    private EventManager.CEventFlow cEvent;
    private int eId;

    public static EventHelper Instance;

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
        }
    }

    public void AddCharacter(string name, bool lovable = false, bool minor = false, bool speaker = true)
    {
        if (lovable && !GameConstants.LovableNpcs.Contains(name.ToUpper())) GameConstants.LovableNpcs = GameConstants.LovableNpcs.Concat(new string[] { name.ToUpper() }).ToArray();
        if (minor && !GameConstants.LovableNpcs.Contains(name.ToUpper())) GameConstants.MinorNpcs = GameConstants.MinorNpcs.Concat(new string[] { name.ToUpper() }).ToArray();
        if (speaker && !GameConstants.LovableNpcs.Contains(name.ToUpper())) GameConstants.NpcSpeakers = GameConstants.NpcSpeakers.Concat(new string[] { name.ToUpper() }).ToArray();
    }
}

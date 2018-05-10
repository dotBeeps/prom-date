using NGameConstants;
using System.Linq;
using System.Reflection;

public static class EventHelper
{
    private static EventManager.CEventFlow cEvent;
    private static int eId;

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
}

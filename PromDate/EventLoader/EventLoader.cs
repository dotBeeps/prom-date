using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using static EventManager;

namespace PromDate.EventLoader
{
    public static class EventLoader
    {
        public static List<CustomEventMod> CustomEventMods = new List<CustomEventMod>();
        private static List<CEventFlow> customEvents = new List<CEventFlow>();

        public struct EventArgs
        {
            public string name;
            public NGameConstants.ETurnType turnType;
        }

        public static void LoadNewEvents(DirectoryInfo dir, CustomEventMod mod)
        {
            int num = 0;
            FileInfo[] files = new DirectoryInfo(dir.FullName + "/Events").GetFiles("*.xml");
            foreach (FileInfo file in files)
            {
                EventContainer eventContainer = EventContainer.Load(file.FullName);
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

        public static void AddEventsToGame()
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
    }
}

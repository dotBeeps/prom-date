//using Harmony;
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

        public Modloader()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            GeneralManager.Instance.LogToFileOrConsole("[Prom Date] Loading in the special sauce.", false, false);
            var harmony = HarmonyInstance.Create("com.foolishdave.monsterprom.promdate");
            //harmony.PatchAll(Assembly.GetExecutingAssembly());
            GeneralManager.Instance.LogToFileOrConsole("[Prom Date] Loaded the special sauce ( ͡° ͜ʖ ͡°)");

            SceneManager.activeSceneChanged += this.OnSceneLoaded;
        }


        public int CheckModEvents(int currentChoice)
        {
            return 711;
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

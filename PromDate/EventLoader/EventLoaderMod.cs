using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PromDate.EventLoader
{
    public class EventLoaderMod : IMod
    {
        public string Name { get { return "EventLoader"; } }

        public string Author { get { return "FoolishDave"; } }

        public Version Version { get; } = new Version(0, 0, 4);

        public void Start()
        {
            InitCustomEvents();
            CustomChatEffects.AddDefaultEffects();
        }

        public void Awake()
        {
            new GameObject().AddComponent<AudioHelper>();
        }

        public void ApplicationQuit()
        {

        }

        public void SceneLoaded(Scene loaded)
        {
            if (loaded.name == "InGame_School")
            {
                LoadCustomEvents();
            }
        }

        public void Update()
        {

        }

        public void FixedUpdate()
        {

        }

        public void LateUpdate()
        {

        }

        public void OnDisable()
        {

        }

        public void OnEnable()
        {

        }

        private void InitCustomEvents()
        {
            DirectoryInfo[] modDirectories = (new DirectoryInfo(ModConstants.MODS_LOCATION)).GetDirectories();
            foreach (DirectoryInfo dir in modDirectories)
            {
                string modPath = dir.GetFiles("*.xml").FirstOrDefault().FullName;
                if (String.IsNullOrEmpty(modPath)) continue;
                CustomEventMod customEvent = CustomEventMod.Load(modPath);
                AudioHelper.Instance.LoadAudio(dir);
                SpriteHelper.LoadSprites(dir);
                EventLoader.LoadNewEvents(dir, customEvent);
                ShopHelper.LoadItemsFromFile(dir, customEvent);
                ProgressTracker.SaveEventModStarted(customEvent);
                EventLoader.CustomEvents.Add(customEvent);
            }
        }

        private void LoadCustomEvents()
        {
            EventLoader.AddEventsToGame();
            EndingHelper.LoadModEndings();
            ShopHelper.LoadItemsIntoShop();
        }
    }
}

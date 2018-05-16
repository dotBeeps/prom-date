using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace PromDate.EventLoader
{
    class EventLoaderMod : IMod
    {
        public string Name { get { return "EventLoader"; } }

        public string Author { get { return "FoolishDave"; } }

        public Version Version { get; } = new Version(0, 0, 4);

        public void Start()
        {
            AudioHelper.LoadAudioFiles();
            SpriteHelper.LoadSprites();
            EndingHelper.LoadModEndings();
            ShopHelper.LoadItemsFromFile();
            EventLoader.LoadNewEvents();
        }

        public void Awake()
        {

        }

        public void ApplicationQuit()
        {

        }

        public void SceneLoaded(Scene loaded)
        {
            if (loaded.name == "InGame_School")
            {
                EventLoader.AddEventsToGame();
                EndingHelper.LoadModEndings();
                ShopHelper.LoadItemsIntoShop();
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

class EventLoaderMod : Mod
{
    public override string Name { get { return "EventLoader"; } }

    public override string Author { get { return "FoolishDave"; } }

    public override Version Version { get { return version; } }

    public override string DisplayName { get { return "Event Loader"; } }

    private Version version = new Version(0, 0, 3);

    public void Start()
    {
        gameObject.AddComponent(typeof(AudioHelper));
        gameObject.AddComponent(typeof(SpriteLoader));
        gameObject.AddComponent(typeof(EndingHelper));
        gameObject.AddComponent(typeof(ShopHelper));
        gameObject.AddComponent(typeof(EventLoader));
        SceneManager.activeSceneChanged += this.OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene oldScene, UnityEngine.SceneManagement.Scene newScene)
    {
        if (newScene.name == "InGame_School")
        {
            EventLoader.Instance.AddEventsToGame();
            EndingHelper.Instance.LoadModEndings();
        }
    }
}

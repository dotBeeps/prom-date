using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mod : MonoBehaviour
{
    private float narratorTalking;
    private bool trackNarrator = false;
    private Modloader.EventArgs narratorEvent;

    public Mod()
    {
        narratorEvent = new Modloader.EventArgs();
        narratorEvent.name = "NarratorNotice";
        narratorEvent.turnType = NGameConstants.ETurnType.School;
        Modloader.Instance.AddToModEvents(narratorEvent);
        SceneManager.activeSceneChanged += this.OnSceneLoaded;
        GeneralManager.Instance.LogToFileOrConsole("[NarratorDate] Loaded in.");
    }

    public void Update()
    {
        if (EventManager.Instance != null)
        {
            int currentEvent = (int)EventManager.Instance.GetType().GetField("mEventIndexActive", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(EventManager.Instance);
            if (trackNarrator && EventManager.Instance.GetCurrentActiveEventScene() > 0)
            {
                if (EventManager.Instance.GetAllSpeakersForEvent(currentEvent).Contains("NARRATOR"))
                {
                    narratorTalking += Time.deltaTime;
                }

                if (narratorTalking > 30f)
                {
                    Modloader.Instance.RequestModEvent(narratorEvent);
                    trackNarrator = false;
                }
            }
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene oldScene, UnityEngine.SceneManagement.Scene newScene)
    {
        if (newScene.name == "InGame_School")
        {
            narratorTalking = 0f;
            trackNarrator = true;
        }
    }
}

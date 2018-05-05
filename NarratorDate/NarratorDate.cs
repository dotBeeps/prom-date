using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NarratorDate
{
    public class Mod : MonoBehaviour
    {
        private float narratorTalking;
        private bool trackNarrator = false;

        public Mod()
        {
            Modloader.Instance.potentialEvents.Add(new Modloader.WantsToPlayEvent(NarratorEvent));
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
                        trackNarrator = false;
                        narratorTalking = 0f;
                    }
                }
            }
        }

        public string NarratorEvent()
        {
            bool inLunch = GameManager.Instance.GetCurrentTurnType() == NGameConstants.ETurnType.Cafeteria;
            if (!inLunch && narratorTalking > 30f) return "NarratorNotice";
            else return null;
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ButtToCloud
{
    public class ButtToCloudMod : IMod
    {
        public string Name { get { return "ButtToCloud"; } }

        public string Author { get { return "FoolishDave"; } }

        public Version Version { get; } = new Version(0, 0, 1);

        private string[] wordsForButt = new string[] { "butt.", "butt ", "booty", "posterior", " ass ", "Ass ", " ass.", "tush", "tokus", "arse", "fanny" };
        private string[] pluralWordsForButt = new string[] { "butts.", "butts ", "buttocks", "booties", "posteriors", " asses ", " asses.", "Asses ", "tushes", "tokuses", "arses", "fannies" };

        public void ApplicationQuit()
        {

        }

        public void Awake()
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

        public void SceneLoaded(UnityEngine.SceneManagement.Scene loaded)
        {
            if (loaded.name == "InGame_School")
            {
                int numberOfButts = 0;
                foreach (EventManager.CEventFlow flow in EventManager.Instance.Events)
                {
                    foreach (EventManager.CEventFlow.CEventScene scene in flow.EventScenes)
                    {
                        foreach (string buttWord in wordsForButt)
                        {
                            if (scene.Text[NGameConstants.ELanguage.English].Contains(buttWord))
                                numberOfButts++;
                            scene.Text[NGameConstants.ELanguage.English] = scene.Text[NGameConstants.ELanguage.English].Replace(buttWord, "cloud");
                        }
                        foreach (string pButtWord in pluralWordsForButt)
                        {
                            if (scene.Text[NGameConstants.ELanguage.English].Contains(pButtWord))
                                numberOfButts++;
                            scene.Text[NGameConstants.ELanguage.English] = scene.Text[NGameConstants.ELanguage.English].Replace(pButtWord, "clouds");
                        }
                        if (scene.IsOptionScene)
                        {
                            foreach (string buttWord in wordsForButt)
                            {
                                if (scene.Text[NGameConstants.ELanguage.English].Contains(buttWord))
                                    numberOfButts++;
                                scene.TextOption2[NGameConstants.ELanguage.English] = scene.TextOption2[NGameConstants.ELanguage.English].Replace(buttWord, "cloud");
                            }
                            foreach (string pButtWord in pluralWordsForButt)
                            {
                                if (scene.Text[NGameConstants.ELanguage.English].Contains(pButtWord))
                                    numberOfButts++;
                                scene.TextOption2[NGameConstants.ELanguage.English] = scene.TextOption2[NGameConstants.ELanguage.English].Replace(pButtWord, "clouds");
                                numberOfButts++;
                            }
                        }
                    }
                }
                GeneralManager.Instance.LogToFileOrConsole("[B2C] Replaced " + numberOfButts + " butts with clouds.");
            }
        }

        public void Start()
        {

        }

        public void Update()
        {

        }
    }
}

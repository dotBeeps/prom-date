using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

class EndingHelper : MonoBehaviour
{
    public struct FinalSceneInfo
    {
        public int FromPromScene;
        public int SceneId;
        public string AudioClip;
    }

    public static EndingHelper Instance;

    public List<EventManager.CEventFlow> modEndings = new List<EventManager.CEventFlow>();
    public List<FinalSceneInfo> finalScenes = new List<FinalSceneInfo>();

    public EndingHelper()
    {
        if (Instance != null)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    public bool IsModEnding(int endingIndex)
    {
        return modEndings.Contains(EventManager.Instance.Events[endingIndex]);
    }

    public FinalSceneInfo GetFinalSceneInfo(int endingIndex)
    {
        return finalScenes.First(sce => sce.FromPromScene == endingIndex);
    }

    public void LoadNewEndings()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(EndingContainer));
        List<EventManager.CSecretEndingConditions> endings = new List<EventManager.CSecretEndingConditions>();
        string[] directories = Directory.GetDirectories(Application.dataPath + "/Mods");
        foreach (string directory in directories)
        {
            string[] files = Directory.GetFiles(directory + "/EndingConditions", "*.xml");
            foreach (string file in files)
            {
                GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading ending conditions from " + file);
                EndingContainer endingContainer = EndingContainer.Load(file);
                foreach (Ending ending in endingContainer.Endings)
                {
                    GeneralManager.Instance.LogToFileOrConsole("\t[PromDate] Loading ending " + ending.SecretEndingName);
                    EventManager.CSecretEndingConditions endCond = EndingContainer.convertEndingConditions(ending);
                    endings.Add(endCond);
                    modEndings.Add(EventManager.Instance.Events[endCond.cSecretEndingIndex]);
                    FinalSceneInfo sceneInfo = new FinalSceneInfo() { FromPromScene = endCond.cSecretEndingIndex, SceneId = endCond.cSecretEndingIndex + 1 };
                    if (EventManager.Instance.Events[endCond.cSecretEndingIndex].ArgumentTags.Any(arg => arg.Contains("SFX")))
                    {
                        sceneInfo.AudioClip = EventManager.Instance.Events[endCond.cSecretEndingIndex].ArgumentTags.First(arg => arg.Contains("SFX")).Split('_')[1];
                    }
                    finalScenes.Add(sceneInfo);
                }
            }
        }
        EventManager.Instance.SecretEndingsConditions = EventManager.Instance.SecretEndingsConditions.Concat(endings.ToArray()).ToArray();
    }
}


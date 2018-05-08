using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

class AudioHelper : MonoBehaviour
{
    public static AudioHelper Instance;

    public AudioHelper()
    {
        if (Instance != null)
        {
            Destroy(this);
        } else
        {
            Instance = this;
            StartCoroutine(LoadAudioFiles());
        }
    }

    IEnumerator LoadAudioFiles()
    {
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading in audio files.");
        string[] directories = Directory.GetDirectories(Application.dataPath + "/Mods");
        foreach (string directory in directories)
        {
            FileInfo[] files = (new DirectoryInfo(directory + "/Audio")).GetFiles("*.*").Where(f => f.Name.ToLower().EndsWith(".ogg") || f.Name.ToLower().EndsWith(".mp3") || f.Name.ToLower().EndsWith(".wav") || f.Name.ToLower().EndsWith(".xm") || f.Name.ToLower().EndsWith(".it") || f.Name.ToLower().EndsWith(".mod") || f.Name.ToLower().EndsWith(".s3m")).ToArray();
            foreach (FileInfo file in files)
            {
                WWW www = new WWW("file:///" + file.FullName);
                yield return www;
                if (www.error != null)
                {
                    GeneralManager.Instance.LogToFileOrConsole(www.error);
                }
                AudioClip clip = www.GetAudioClip(false, false);
                clip.LoadAudioData();
                clip.name = file.Name.Split('.')[0];
                while (clip.loadState == AudioDataLoadState.Loading || clip.loadState == AudioDataLoadState.Unloaded)
                    yield return 0;
                string category;
                if (clip.name.Split('_').Length > 1)
                {
                    category = clip.name.Split('_')[0].ToUpper();
                }
                else
                {
                    category = "VOICE";
                }
                AudioItem audioItem = AudioController.AddToCategory(AudioController.GetCategory(category), clip, clip.name);
                ((Dictionary<string, AudioItem>)typeof(AudioController).GetField("_audioItems", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(AudioController.Instance)).Add(audioItem.Name, audioItem);
            }
        }
    }
}

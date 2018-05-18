using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace PromDate.EventLoader
{
    public static class AudioLoader
    {
        public static IEnumerator LoadAudioFiles(DirectoryInfo dir)
        {
            FileInfo[] files = new DirectoryInfo(dir + "/Audio").GetFiles("*.wav").ToArray();
            foreach (FileInfo file in files)
            {
                GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading audio file: " + file.Name);
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
                AudioCategory category = AudioController.GetCategory(clip.name.Split('_')[0].ToUpper());
                if (category == null)
                {
                    category = AudioController.GetCategory("VOICE");
                }
                AudioItem audioItem = AudioController.AddToCategory(category, clip, clip.name);
                ((Dictionary<string, AudioItem>)typeof(AudioController).GetField("_audioItems", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(AudioController.Instance)).Add(audioItem.Name, audioItem);
                GeneralManager.Instance.LogToFileOrConsole("[PromDate] Finished loading audio: " + file.Name);
            }
        }
    }
}
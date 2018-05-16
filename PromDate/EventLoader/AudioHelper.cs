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
    public static class AudioHelper
    {
        public static IEnumerator LoadAudioFiles()
        {
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading in audio files.");
            string[] directories = Directory.GetDirectories(ModConstants.MODS_LOCATION);
            foreach (string directory in directories)
            {
                FileInfo[] files = (new DirectoryInfo(directory + "/Audio")).GetFiles("*.wav").ToArray();
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
                    AudioCategory category = AudioController.GetCategory(clip.name.Split('_')[0].ToUpper());
                    if (category == null)
                    {
                        category = AudioController.GetCategory("VOICE");
                    }
                    AudioItem audioItem = AudioController.AddToCategory(category, clip, clip.name);
                    ((Dictionary<string, AudioItem>)typeof(AudioController).GetField("_audioItems", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(AudioController.Instance)).Add(audioItem.Name, audioItem);
                }
            }
        }
    }
}
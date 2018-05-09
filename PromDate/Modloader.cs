using Harmony;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Modloader : MonoBehaviour
{
    public static Modloader Instance;
    private List<string> loadedDllMods = new List<string>();

    public Modloader()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading in the special sauce.");

            gameObject.AddComponent(typeof(AudioHelper));
            gameObject.AddComponent(typeof(SpriteLoader));
            gameObject.AddComponent(typeof(EventHelper));
            gameObject.AddComponent(typeof(EndingHelper));
            gameObject.AddComponent(typeof(ShopHelper));

            var harmony = HarmonyInstance.Create("com.foolishdave.monsterprom.promdate");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            LoadOtherMods();
            SceneManager.activeSceneChanged += this.OnSceneLoaded;
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loaded the special sauce ( ͡° ͜ʖ ͡°)");
        }
    }

    private void LoadOtherMods()
    {
        FileInfo[] files = (new DirectoryInfo(Application.dataPath + "/Mods")).GetFiles("*.dll");
        foreach (FileInfo file in files)
        {
            if (file.Name == "PromDate.dll") continue;
            try
            {
                LoadModDll(file);
            }
            catch
            {
                GeneralManager.Instance.LogToFileOrConsole("[PromDate] Failed to load in " + file);
            }
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene oldScene, UnityEngine.SceneManagement.Scene newScene)
    {
        if (GeneralManager.Instance.IsInOnlineMode)
        {
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Hope all of you have the mod installed, online support is basically nonexistent at the moment!");
        }
        if (newScene.name == "InGame_School")
        {
            EventHelper.Instance.LoadNewEvents();
            EndingHelper.Instance.LoadNewEndings();
        }
    }

    private void LoadModDll(FileInfo file)
    {
        if (SteamManager.Initialized)
        {
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Turning off steam achievements as mod dll is installed.");
            AccessTools.TypeByName("SteamAPI").GetMethod("Shutdown").Invoke(null, new object[0]);
            SteamManager smInst = (SteamManager)typeof(SteamManager).GetField("Instance", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            typeof(SteamManager).GetField("m_bInitialized", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(smInst, false);
        }
        var assembly = Assembly.LoadFrom(file.FullName);
        var modType = assembly.GetType("Mod");
        gameObject.AddComponent(modType);
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loaded dll mod: " + file.Name);
    }

    public void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            ProgressTracker.SaveModProgress();
        }
    }
}

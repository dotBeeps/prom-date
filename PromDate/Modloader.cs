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
    private List<Mod> loadedMods = new List<Mod>();

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
            var harmony = HarmonyInstance.Create("com.foolishdave.monsterprom.promdate");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            AddMod(typeof(EventLoaderMod));
            LoadMods();
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loaded the special sauce ( ͡° ͜ʖ ͡°)");
        }
    }

    public string GenerateMultiplayerPassword()
    {
        string password = "moddedGame-";
        password = string.Concat(password, string.Join("", loadedMods.Select(mod => mod.Name).ToArray())) + "-";
        password = string.Concat(password, string.Join("", EventLoader.Instance.customEventMods.Select(mod => mod.Name).ToArray()));

        return "modded";
    }

    private void LoadMods()
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
                GeneralManager.Instance.LogToFileOrConsole("[PromDate] Failed to load in " + file.FullName);
            }
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
        var modType = assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(Mod))).FirstOrDefault();
        if (modType == null)
        {
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Failed to load dll mod: " + file.Name + " - no class extending Mod.");
        }
        else
        {
            AddMod(modType);
        }
    }

    private void AddMod(Type mod)
    {
        var component = gameObject.AddComponent(mod);
        Mod newMod = (Mod)GetComponent(mod);

        if (string.IsNullOrEmpty(newMod.Name))
        {
            Destroy(component);
        }
        else
        {
            loadedMods.Add(newMod);
        }
    }
}

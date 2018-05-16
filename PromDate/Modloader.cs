using Harmony;
using PromDate.EventLoader;
using PromDate.Mod;
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
    private CombinationMod mods;

    public Modloader()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading in the special sauce. (Patching)");
            var harmony = HarmonyInstance.Create("com.foolishdave.monsterprom.promdate");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loaded the special sauce ( ͡° ͜ʖ ͡°) (Patched)");
        }
    }

    public string GenerateMultiplayerPassword()
    {
        //Todo: Come up with better way to do this
        return "modded";
    }

    void Awake()
    {
        SceneManager.activeSceneChanged += SceneChanged;
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading mods.");
        ModManager.Mods.Add(new EventLoaderMod());
        mods = new CombinationMod(ModManager.Mods);
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loaded!");
        mods.Awake();
    }

    void Start()
    {
        mods.Start();
    }

    void Update()
    {
        mods.Update();
    }

    void FixedUpdate()
    {
        mods.FixedUpdate();
    }

    void LateUpdate()
    {
        mods.LateUpdate();
    }

    void ApplicationQuit()
    {
        mods.ApplicationQuit();
    }

    void SceneChanged(Scene oldScene, Scene newScene)
    {
        mods.SceneLoaded(newScene);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace PromDate.Mod
{
    public static class ModManager
    {
        private static List<IMod> _Mods = new List<IMod>();
        private static ModPreferences prefs;

        public static IEnumerable<IMod> Mods
        {
            get
            {
                return _Mods;
            }
        }

        public static void AddInternalMod(IMod mod)
        {
            _Mods.Add(mod);
        }

        public static bool IsModDisabled(IMod mod)
        {
            return prefs.Disabled.Contains(mod);
        }

        public static void EnableMod(string modName)
        {
            IMod mod = Mods.FirstOrDefault(m => m.Name.ToLower() == modName.ToLower());
            if (mod != null)
                EnableMod(mod);
        }

        public static void EnableMod(IMod mod)
        {
            prefs.Disabled.Remove(mod);
            mod.OnEnable();
            prefs.Save(ModConstants.MODLOADER_PREFS_PATH);
        }

        public static void DisableMod(string modName)
        {
            IMod mod = Mods.FirstOrDefault(m => m.Name.ToLower() == modName.ToLower());
            if (mod != null)
                DisableMod(mod);
        }

        public static void DisableMod(IMod mod)
        {
            prefs.Disabled.Add(mod);
            mod.OnDisable();
            prefs.Save(ModConstants.MODLOADER_PREFS_PATH);
        }

        public static void LoadMods()
        {
            List<IMod> mods = new List<IMod>();

            FileInfo[] files = (new DirectoryInfo(ModConstants.MODS_LOCATION)).GetFiles("*.dll");
            foreach (FileInfo file in files)
            {
                if (file.Name == ModConstants.MODLOADER_DLL_NAME) continue;
                try
                {
                    mods.AddRange(LoadModsFromFile(file.FullName));
                }
                catch (Exception e)
                {
                    GeneralManager.Instance.LogToFileOrConsole("[PromDate] Failed to load in " + file.Name + " - Threw: " + e);
                }
            }
            if (!File.Exists(ModConstants.MODLOADER_PREFS_PATH))
            {
                prefs = new ModPreferences();
            } else
            {
                prefs = ModPreferences.Load(ModConstants.MODLOADER_PREFS_PATH);
            }
            _Mods.AddRange(mods);
        }

        private static IEnumerable<IMod> LoadModsFromFile(string path)
        {
            List<IMod> fileMods = new List<IMod>();
            var assembly = Assembly.LoadFrom(path);
            var modTypes = assembly.GetTypes().Where(type => type.GetInterface("IMod") != null);
            foreach (Type t in modTypes)
            {
                try
                {
                    IMod modInst = (IMod)Activator.CreateInstance(t);
                    fileMods.Add(modInst);
                } catch (Exception e)
                {
                    GeneralManager.Instance.LogToFileOrConsole("[PromDate] Failed to load mod of type " + t.Name + ". Threw: " + e);
                }
            }
            return fileMods;
        }
    }

    public class ModPreferences
    {
        public List<IMod> Disabled = new List<IMod>();

        public static ModPreferences Load(string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream f = File.Open(path, FileMode.Open);
            ModPreferences prefs = (ModPreferences)bf.Deserialize(f);
            f.Close();
            return prefs;
        }

        public void Save(string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream f = File.Open(path, FileMode.Create);
            bf.Serialize(f, this);
            f.Close();
        }
    }
}

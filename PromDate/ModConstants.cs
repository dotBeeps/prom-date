using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PromDate
{
    class ModConstants
    {
        public static readonly string[] VANILLA_CHARACTERS = { "NARRATOR", "POLLY", "VERA", "LIAM", "DAMIEN", "SCOTT", "MIRANDA", "SHOPKEEPER", "COACH", "COVEN", "HUNTER", "PRINCE", "WOLFPACK", "BLOBERT", "CALCULESTER", "FAITH", "HOPE", "JOY" };
        public static readonly string MOD_PROGRESS_PATH = Application.dataPath + "/../UserData/ModProgressData.xml";
        public static readonly string MODS_LOCATION = Application.dataPath + "/../Mods/";
        public static readonly string MODLOADER_DLL_NAME = "PromDate.dll";
        public static readonly string MODLOADER_PREFS_PATH = Application.dataPath + "/../PromDate/prefs.modprefs";
    }
}


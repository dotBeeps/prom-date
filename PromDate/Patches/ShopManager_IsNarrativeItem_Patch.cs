using Harmony;
using PromDate.EventLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[HarmonyPatch(typeof(ShopManager))]
[HarmonyPatch("IsNarrativeItem")]
class ShopManager_IsNarrativeItem_Patch
{
    static bool Prefix(ref bool __result, ref string aItemName)
    {
        string iName = aItemName.ToUpper();
        if (ShopHelper.ModItems.Any(item => item.itemName == iName))
        {
            __result = ShopHelper.NarrativeItems.Any(item => item.itemName == iName);
            return false;
        }
        return true;
    }
}


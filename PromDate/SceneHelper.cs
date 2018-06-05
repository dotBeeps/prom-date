using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace PromDate
{
    public static class SceneHelper
    {
        public static Transform GetCharacterSprite(int i)
        {
            FieldInfo[] fields = typeof(EventManager).GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(f => f.Name.StartsWith("UISprite_NPC_")).ToArray();
            int nonNullCount = 0;
            foreach (FieldInfo field in fields)
            {
                var value = field.GetValue(EventManager.Instance);
                if (value != null)
                {
                    nonNullCount++;
                    if (nonNullCount == i)
                    {
                        return (value as EventManager.CNPCSprite).uiSprite.transform;
                    }
                }
            }
            return null;
        }
    }
}

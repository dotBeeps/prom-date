using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PromDate.EventLoader
{
    class CustomChatEffects : MonoBehaviour
    {
        public delegate string ChatEffect();
        private static Dictionary<string, ChatEffect> chatEffects = new Dictionary<string, ChatEffect>();

        public static void AddChatEffect(string trigger, ChatEffect effect)
        {
            chatEffects.Add(trigger, effect);
        }

        public static void ApplyChatEffects(ref string str)
        {
            string modified = "";
            char[] charArr = str.ToCharArray();
            int i = 0;
            while (i < str.Length)
            {
                if (charArr[i] == '%')
                {
                    int endIndex = str.IndexOf('%', i + 1);
                    string effect = str.Substring(i + 1, endIndex - i - 1);
                    ChatEffect del;
                    if (chatEffects.TryGetValue(effect, out del))
                    {
                        modified += del.Invoke();
                    } else
                    {
                        modified += "%" + effect + "%";
                    }
                    i = endIndex;
                } else
                {
                    modified += charArr[i];
                }
                i++;
            }
            str = modified;
        }

        public static void AddDefaultEffects()
        {
            AddChatEffect("SYSTEMUSERNAME", ActualUsername);
        }

        private static string ActualUsername()
        {
            return "[F44242]" + Environment.UserName + "[-]";
        }
    }
}

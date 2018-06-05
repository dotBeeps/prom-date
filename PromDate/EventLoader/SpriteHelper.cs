using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace PromDate.EventLoader
{
    public static class SpriteHelper
    {

        public struct SpriteInfo
        {
            public string CharName;
            public int Outfit;
            public string Mood;
        }

        private static List<Sprite> customSprites = new List<Sprite>();
        private static Dictionary<SpriteInfo, Sprite> customNpcSprites = new Dictionary<SpriteInfo, Sprite>();
        private static EventManagerEditor_Helper eManHelper = new EventManagerEditor_Helper();
        private static MethodInfo getNpcSpritePath = eManHelper.GetType().GetMethod("GetNpcSpritePath", BindingFlags.NonPublic | BindingFlags.Instance);
        private static MethodInfo getLocationSpritePath = eManHelper.GetType().GetMethod("GetLocationSpritesPath", BindingFlags.NonPublic | BindingFlags.Instance);

        // For forcing talking sprites that aren't currently present in the base game;
        public static Sprite ModTalkingSprite(string character)
        {
            if (!string.IsNullOrEmpty(character))
            {
                if (!ModConstants.VANILLA_CHARACTERS.Contains(character.ToUpper()))
                {
                    return customNpcSprites[new SpriteInfo() { CharName = character.ToUpper(), Mood = "sticker", Outfit = 0 }];
                }
            }
            return null;
        }

        // For custom events to lookup sprites, either from mods or vanilla.
        public static Sprite LookupNpcSprite(string character, int outfit, string mood)
        {
            Sprite sprite = null;
            if (ModConstants.VANILLA_CHARACTERS.Contains(character.ToUpper()))
            {
                sprite = (Sprite)Resources.Load((string)getNpcSpritePath.Invoke(eManHelper, new object[] { character.ToUpper(), outfit, mood }), typeof(Sprite));
            }
            else
            {
                sprite = customNpcSprites[new SpriteInfo() { CharName = character, Outfit = outfit, Mood = mood }];
            }
            return sprite;
        }

        // Lookup any sprite that is present in the mods Images directory.
        public static Sprite LookupCustomSprite(string spriteName)
        {
            return customSprites.First(s => s.name.ToLower() == spriteName.ToLower());
        }

        public static Sprite LookupBG(string spriteName, ModEvent ev)
        {
            //Todo: Actually lookup all bgs and not just endings.
            Sprite sprite = LookupCustomSprite(spriteName);
            if (sprite == null)
            {
                object[] parameters = new object[] { ev.Name, ev.Location, null, null, true };
                object results = getLocationSpritePath.Invoke(eManHelper, parameters);
                sprite = (Sprite)Resources.Load((string)parameters[2], typeof(Sprite));
            }
            return sprite;
        }

        public static Sprite LookupFG(string spriteName, ModEvent ev)
        {
            //Todo: Actually lookup all bgs and not just endings.
            Sprite sprite = LookupCustomSprite(spriteName);
            if (sprite == null)
            {
                object[] parameters = new object[] { ev.Name, ev.Location, null, null, true };
                object results = getLocationSpritePath.Invoke(eManHelper, parameters);
                sprite = (Sprite)Resources.Load((string)parameters[3], typeof(Sprite));
            }
            return sprite;
        }

        public static void LoadSprites(DirectoryInfo dir)
        {
            FileInfo[] charFiles = new DirectoryInfo(dir.FullName + "/Images/Characters").GetFiles("*.*", SearchOption.AllDirectories).Where(file => file.Name.ToLower().EndsWith("jpg") || file.Name.ToLower().EndsWith("png")).ToArray();
            foreach (FileInfo file in charFiles)
            {
                string[] spriteName = file.Name.Split('.')[0].Split('_');
                SpriteInfo spriteInfo = new SpriteInfo();
                spriteInfo.CharName = spriteName[0].ToUpper();
                Sprite sprite;
                if (spriteName[1].ToLower().Contains("speaking"))
                {
                    spriteInfo.Outfit = 0;
                    spriteInfo.Mood = "sticker";
                    sprite = LoadStickerFromFile(file.FullName, file.Name.Split('.')[0]);
                }
                else
                {
                    spriteInfo.Outfit = int.Parse(spriteName[1]);
                    spriteInfo.Mood = spriteName[2].ToLower();
                    sprite = LoadSpriteFromFile(file.FullName, file.Name.Split('.')[0]);
                }
                customNpcSprites.Add(spriteInfo, sprite);
            }

            FileInfo[] files = new DirectoryInfo(dir.FullName + "/Images").GetFiles("*.*", SearchOption.AllDirectories)
                .Where(file => file.Name.ToLower().EndsWith("jpg") || file.Name.ToLower().EndsWith("png")).ToArray()
                .Except(charFiles).ToArray();
            foreach (FileInfo file in files)
            {
                string spriteName = Path.GetFileNameWithoutExtension(file.Name);
                Sprite sprite = LoadSpriteFromFile(file.FullName, spriteName);
                customSprites.Add(sprite);
            }
        }

        private static Sprite LoadStickerFromFile(string path, string sprName, float ppu = 100f)
        {
            Texture2D sprTex = LoadTextureFromFile(path);
            Sprite sprite = Sprite.Create(sprTex, new Rect(0, 0, sprTex.width, sprTex.height), new Vector2(sprTex.width / 2, sprTex.height / 2), ppu);
            sprite.name = sprName;
            return sprite;
        }

        public static Sprite LoadSpriteFromFile(string path, string sprName, float ppu = 100f)
        {
            Texture2D sprTex = LoadTextureFromFile(path);
            Sprite sprite = Sprite.Create(sprTex, new Rect(0, 0, sprTex.width, sprTex.height), Vector2.zero, ppu);
            sprite.name = sprName;
            return sprite;
        }

        private static Texture2D LoadTextureFromFile(string path)
        {
            Texture2D tex;
            byte[] data;
            if (File.Exists(path))
            {
                data = File.ReadAllBytes(path);
                tex = new Texture2D(2, 2);
                if (tex.LoadImage(data))
                    return tex;
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

class SpriteLoader : MonoBehaviour
{

    public struct SpriteInfo
    {
        public string CharName;
        public int Outfit;
        public string Mood;
    }

    public static SpriteLoader Instance;
    public Dictionary<SpriteInfo, Sprite> customSprites = new Dictionary<SpriteInfo, Sprite>();
    private EventManagerEditor_Helper eManHelper = new EventManagerEditor_Helper();
    private MethodInfo getNpcSpritePath;

    public SpriteLoader()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            getNpcSpritePath = eManHelper.GetType().GetMethod("GetNpcSpritePath", BindingFlags.NonPublic | BindingFlags.Instance);
            LoadSprites();
        }
    }

    public Sprite ModTalkingSprite(string character)
    {
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Sticker requested for character " + character);
        if (character != "" && character != null)
        {
            if (!ModConstants.VANILLA_CHARACTERS.Contains(character.ToUpper()))
            {
                return customSprites[new SpriteInfo() { CharName = character.ToUpper(), Mood = "sticker", Outfit = 0 }];
            }
        }
        return null;
    }

    public Sprite LookupSprite(string character, int outfit, string mood)
    {
        
        Sprite sprite = null;
        if (ModConstants.VANILLA_CHARACTERS.Contains(character.ToUpper()))
        {
            sprite = (Sprite)Resources.Load((string)getNpcSpritePath.Invoke(eManHelper, new object[] { character.ToUpper(), outfit, mood }), typeof(Sprite));
        } else
        {
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Looking up custom sprite for " + character);
            sprite = customSprites[new SpriteInfo() { CharName = character, Outfit = outfit, Mood = mood }];
        }

        return sprite;
    }

    private void LoadSprites()
    {
        FileInfo[] files = (new DirectoryInfo(Application.dataPath + "/Mods/Characters")).GetFiles("*.*", SearchOption.AllDirectories).Where(file => file.Name.ToLower().EndsWith("jpg") || file.Name.ToLower().EndsWith("png")).ToArray();
        foreach (FileInfo file in files)
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
            customSprites.Add(spriteInfo, sprite);
        }
    }

    private Sprite LoadStickerFromFile(string path, string sprName, float ppu = 100f)
    {
        Texture2D sprTex = LoadTextureFromFile(path);
        Sprite sprite = Sprite.Create(sprTex, new Rect(0, 0, 512, 512), new Vector2(256, 256), ppu);
        sprite.name = sprName;
        return sprite;
    }

    private Sprite LoadSpriteFromFile(string path, string sprName, float ppu = 100f)
    {
        GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loaded sprite from " + path);
        Texture2D sprTex = LoadTextureFromFile(path);
        Sprite sprite = Sprite.Create(sprTex, new Rect(0, 0, sprTex.width, sprTex.height), Vector2.zero, ppu);
        sprite.name = sprName;
        return sprite;
    }

    private Texture2D LoadTextureFromFile(string path)
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


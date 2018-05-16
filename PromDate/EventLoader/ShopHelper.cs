using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PromDate.EventLoader
{
    public static class ShopHelper
    {
        public static List<ShopManager.CItemSprites> ModItems = new List<ShopManager.CItemSprites>();
        public static List<ShopManager.CItemSprites> NarrativeItems = new List<ShopManager.CItemSprites>();

        public static void LoadItemsFromFile()
        {
            string[] directories = Directory.GetDirectories(Application.dataPath + "/Mods");
            foreach (string directory in directories)
            {
                string[] files = Directory.GetFiles(directory + "/Items", "*.xml");
                foreach (string file in files)
                {
                    GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading items from " + file);
                    ItemContainer itemContainer = ItemContainer.Load(file);
                    foreach (Item item in itemContainer.Items)
                    {
                        AddShopItem(item.Name, item.Price, item.DescriptionTitle, item.Description, item.ShopkeeperMood, SpriteHelper.LookupCustomSprite(item.SmallSprite), SpriteHelper.LookupCustomSprite(item.LargeSprite), item.EventItem);
                    }
                }
            }
        }

        public static void LoadItemsIntoShop()
        {
            ShopManager.Instance.Items = ShopManager.Instance.Items.Concat(ModItems.ToArray()).ToArray();
        }

        public static void AddShopItem(string itemName, int itemPrice, string descTitle, string desc, string shopkeepMood, Sprite smallSprite, Sprite largeSprite, bool narrative = false)
        {
            ShopManager.CItemSprites shopItem = new ShopManager.CItemSprites();
            shopItem.itemName = itemName.ToUpper();
            shopItem.price = itemPrice;
            shopItem.narrativeName = new NGameConstants.TextLangDictionary();
            shopItem.narrativeName.Add(NGameConstants.ELanguage.English, descTitle);
            shopItem.narrariveDescription = new NGameConstants.TextLangDictionary();
            shopItem.narrariveDescription.Add(NGameConstants.ELanguage.English, desc);
            shopItem.shopkeeperMood = shopkeepMood;
            shopItem.smallSprite = smallSprite;
            shopItem.bigSprite = largeSprite;
            ModItems.Add(shopItem);
            if (narrative)
                NarrativeItems.Add(shopItem);
        }
    }
}


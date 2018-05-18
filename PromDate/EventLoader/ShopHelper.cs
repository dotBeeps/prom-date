using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public static void LoadItemsFromFile(DirectoryInfo dir, CustomEventMod customEvent)
        {
            string[] files = Directory.GetFiles(dir.FullName + "/Items", "*.xml");
            foreach (string file in files)
            {
                GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading items from " + file);
                ItemContainer itemContainer = ItemContainer.Load(file);
                foreach (Item item in itemContainer.Items)
                {
                    GeneralManager.Instance.LogToFileOrConsole("[PromDate] Loading item: " + item.Name);
                    AddShopItem(item.Name, item.Price, item.DescriptionTitle, item.Description, item.ShopkeeperMood, SpriteHelper.LookupCustomSprite(item.SmallSprite), SpriteHelper.LookupCustomSprite(item.LargeSprite), item.EventItem);
                    if (!string.IsNullOrEmpty(item.UnlockDescription))
                    {
                        if (ProgressTracker.HasEventModBeenLoadedBefore(customEvent)) return;
                        GeneralManager.CUnlockableConditions cond = new GeneralManager.CUnlockableConditions(item.Name, NGameConstants.EUnlockableType.Item, "", new GeneralManager.CUnlockableRequirement[0], SpriteHelper.LookupCustomSprite(item.LargeSprite), item.UnlockDescription);
                        (typeof(GeneralManager).GetField("mUnlockedThisRun", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(GeneralManager.Instance) as List<GeneralManager.CUnlockableConditions>).Add(cond);
                    }
                }
            }
            GeneralManager.Instance.CheckMoreUnlockablesThisRun();
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


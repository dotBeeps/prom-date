using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

[XmlRoot("ItemContainer")]
public class ItemContainer
{
    public List<Item> Items = new List<Item>();

    public static ItemContainer Load(string path)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemContainer));
        ItemContainer result;
        using (FileStream fileStream = new FileStream(path, FileMode.Open))
        {
            result = (xmlSerializer.Deserialize(fileStream) as ItemContainer);
        }
        return result;
    }
}

public class Item
{
    public string Name;
    public int Price;
    public string DescriptionTitle;
    public string Description;
    public string ShopkeeperMood;
    public string SmallSprite;
    public string LargeSprite;
    public string UnlockDescription;
    public bool EventItem = false;
}

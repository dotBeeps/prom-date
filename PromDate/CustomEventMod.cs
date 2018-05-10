using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

[XmlRoot("CustomEventMod")]
public class CustomEventMod
{
    public string Name;
    public string Author;
    public string Version;

    public static CustomEventMod Load(string path)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomEventMod));
        CustomEventMod result;
        using (FileStream fileStream = new FileStream(path, FileMode.Open))
        {
            result = (xmlSerializer.Deserialize(fileStream) as CustomEventMod);
        }
        return result;
    }
}

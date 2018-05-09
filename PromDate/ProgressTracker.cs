using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public static class ProgressTracker
{
    public static List<string> ModEventsSeenAllTime = new List<string>();
    public static List<bool[]> ModEventChoicesSeenAllTime = new List<bool[]>();
    public static List<string> ModEndingsSeenAllTime = new List<string>();
    public static List<int> ModEndingsSeenCount = new List<int>();

    public static void LoadModProgress()
    {
        if (!File.Exists(ModConstants.MOD_PROGRESS_PATH)) return;

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModSaveData));
        ModSaveData saveData;

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(CryptoManager.DecryptXML(ModConstants.MOD_PROGRESS_PATH));
        string xml = String.Empty;

        using (StringWriter strWriter = new StringWriter())
        using (XmlWriter xmlStrWriter = XmlWriter.Create(strWriter))
        {
            doc.WriteTo(xmlStrWriter);
            xmlStrWriter.Flush();
            xml = strWriter.GetStringBuilder().ToString();
        }

        using (TextReader reader = new StringReader(xml))
        {
            saveData = (xmlSerializer.Deserialize(reader) as ModSaveData);
        }
        ModEventsSeenAllTime = saveData.ModEventsSeenAllTime;
        ModEventChoicesSeenAllTime = saveData.ModEventChoicesSeenAllTime;
        ModEndingsSeenAllTime = saveData.ModEndingsSeenAllTime;
        ModEndingsSeenCount = saveData.ModEndingsSeenCount;
    }

    public static void SaveModProgress()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModSaveData));
        ModSaveData saveData = new ModSaveData() { ModEventsSeenAllTime = ModEventsSeenAllTime, ModEndingsSeenAllTime = ModEndingsSeenAllTime, ModEndingsSeenCount = ModEndingsSeenCount, ModEventChoicesSeenAllTime = ModEventChoicesSeenAllTime };
        string xml = String.Empty;
        using (StringWriter strWriter = new StringWriter())
        {
            var settings = new XmlWriterSettings();
            settings.Encoding = Encoding.ASCII;
            settings.Indent = true;
            using (XmlWriter xmlWriter = XmlWriter.Create(ModConstants.MOD_PROGRESS_PATH, settings))
            {
                xmlSerializer.Serialize(xmlWriter, saveData);
            }
        }
        CryptoManager.EncryptXML(ModConstants.MOD_PROGRESS_PATH);
    }

    public class ModSaveData
    {
        public List<string> ModEventsSeenAllTime = new List<string>();
        public List<bool[]> ModEventChoicesSeenAllTime = new List<bool[]>();
        public List<string> ModEndingsSeenAllTime = new List<string>();
        public List<int> ModEndingsSeenCount = new List<int>();
    }
}


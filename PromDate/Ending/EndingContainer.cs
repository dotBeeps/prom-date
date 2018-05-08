using NGameConstants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

[XmlRoot("EndingContainer")]
public class EndingContainer
{
    [XmlArray("Endings")]
    [XmlArrayItem("Ending")]
    public List<Ending> Endings = new List<Ending>();

    public static EndingContainer Load(string path)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(EndingContainer));
        EndingContainer result;
        using (FileStream fileStream = new FileStream(path, FileMode.Open))
        {
            result = (xmlSerializer.Deserialize(fileStream) as EndingContainer);
        }
        return result;
    }
    
    public static EventManager.CSecretEndingConditions convertEndingConditions(Ending ending)
    {
        List<int> eventsRequired = new List<int>();
        foreach (string req in ending.EventsRequired)
        {
            eventsRequired.Add(EventManager.Instance.Events.TakeWhile(ev => !ev.EventName.Contains(req)).Count());
        }
        int secretEndingIndex = EventManager.Instance.Events.TakeWhile(ev => !ev.EventName.Contains(ending.SecretEndingName)).Count();

        EventManager.CSecretEndingConditions endingConditions = new EventManager.CSecretEndingConditions(ending.SecretEndingName, secretEndingIndex, eventsRequired.ToArray(), ending.ChoicesNeeded.Select(a => a.Choices.Select(str => (EEventChoiceSelected)Enum.Parse(typeof(EEventChoiceSelected), str)).ToArray()).ToArray(), ending.NpcsChosenForProm.ToArray(), ending.NpcRestriction, ending.LovePointsNeededWithNpc, ending.ContinuitySuccessNeeded);
        return endingConditions;
    }
}

public class Ending
{
    [XmlArray("ChoicesNeeded")]
    [XmlArrayItem("EventChoices")]
    public List<EventChoices> ChoicesNeeded = new List<EventChoices>();
    public int ContinuitySuccessNeeded;
    [XmlArray("EventsRequired")]
    [XmlArrayItem("EventName")]
    public List<string> EventsRequired = new List<string>();
    public int LovePointsNeededWithNpc;
    public NGameConstants.ESecretEndingNpcRestriction NpcRestriction;
    [XmlArray("NpcsChosenForProm")]
    [XmlArrayItem("Npc")]
    public List<string> NpcsChosenForProm = new List<string>();
    public string SecretEndingName;
}

public class EventChoices
{
    [XmlArray("Choices")]
    [XmlArrayItem("Choice")]
    public List<string> Choices = new List<string>();
}
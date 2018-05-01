using System;
using System.Xml.Serialization;

public class EventRestriction
{
	[XmlAttribute("type")]
	public string Type;
	public bool TrueOrFalse;
	public string RestrictionData;
}

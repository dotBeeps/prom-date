using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;


public class Event
{
	
	public string Name;

	
	[DefaultValue("Normal")]
	public string Type;

	
	[DefaultValue("None")]
	public string Location;

	
	[XmlArray("EventScenes", IsNullable = true)]
	public List<Scene> EventScenes = new List<Scene>();

	
	[DefaultValue(9999)]
	public int IndexSceneOfOption1Success;

	
	[DefaultValue(9999)]
	public int IndexSceneOfOption1Failure;

	
	[DefaultValue(9999)]
	public int IndexSceneOfOption2Success;

	
	[DefaultValue(9999)]
	public int IndexSceneOfOption2Failure;

	
	public string StatRequired_Option1;

	
	public string StatRequired_Option2;

	
	[XmlArray("TopicTags", IsNullable = true)]
	[XmlArrayItem("TopicTag")]
	public List<string> TopicTags = new List<string>();

	
	[XmlArray("ArumentTags", IsNullable = true)]
	[XmlArrayItem("ArgTag")]
	public List<string> ArgumentTags = new List<string>();

	
	[XmlArray("CharacterTags", IsNullable = true)]
	[XmlArrayItem("CharTag")]
	public List<string> CharacterTags = new List<string>();

	
	[XmlArray("EventRestrictions", IsNullable = true)]
	[XmlArrayItem("EventRestriction")]
	public List<EventRestriction> EventRestrictions = new List<EventRestriction>();

	
	[XmlArray("Option1SuccessLoves", IsNullable = true)]
	[XmlArrayItem("Love")]
	public List<string> Option1SuccessLoves = new List<string>();

	
	[XmlArray("Option1FailureLoves", IsNullable = true)]
	[XmlArrayItem("Love")]
	public List<string> Option1FailureLoves = new List<string>();

	
	[XmlArray("Option2SuccessLoves", IsNullable = true)]
	[XmlArrayItem("Love")]
	public List<string> Option2SuccessLoves = new List<string>();

	
	[XmlArray("Option2FailureLoves", IsNullable = true)]
	[XmlArrayItem("Love")]
	public List<string> Option2FailureLoves = new List<string>();

	
	public ContData ContinuityData;

	
	[XmlArray("Option1Success_RuleNoMoreTags", IsNullable = true)]
	[XmlArrayItem("Tag")]
	public List<string> Option1Success_RuleNoMoreTags = new List<string>();

	
	[XmlArray("Option1Failure_RuleNoMoreTags", IsNullable = true)]
	[XmlArrayItem("Tag")]
	public List<string> Option1Failure_RuleNoMoreTags = new List<string>();

	
	[XmlArray("Option2Success_RuleNoMoreTags", IsNullable = true)]
	[XmlArrayItem("Tag")]
	public List<string> Option2Success_RuleNoMoreTags = new List<string>();

	
	[XmlArray("Option2Failure_RuleNoMoreTags", IsNullable = true)]
	[XmlArrayItem("Tag")]
	public List<string> Option2Failure_RuleNoMoreTags = new List<string>();

	
	[XmlArray("Option1Success_RuleMoreTags", IsNullable = true)]
	[XmlArrayItem("Tag")]
	public List<string> Option1Success_RuleMoreTags = new List<string>();

	
	[XmlArray("Option1Failure_RuleMoreTags", IsNullable = true)]
	[XmlArrayItem("Tag")]
	public List<string> Option1Failure_RuleMoreTags = new List<string>();

	
	[XmlArray("Option2Success_RuleMoreTags", IsNullable = true)]
	[XmlArrayItem("Tag")]
	public List<string> Option2Success_RuleMoreTags = new List<string>();

	
	[XmlArray("Option2Failure_RuleMoreTags", IsNullable = true)]
	[XmlArrayItem("Tag")]
	public List<string> Option2Failure_RuleMoreTags = new List<string>();
}

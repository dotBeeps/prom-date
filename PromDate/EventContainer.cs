﻿using NGameConstants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("EventCollection")]
public class EventContainer
{
	[XmlArray("Events")]
	[XmlArrayItem("Event")]
	public List<Event> Events = new List<Event>();

    public static EventContainer Load(string path)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(EventContainer));
        EventContainer result;
        using (FileStream fileStream = new FileStream(path, FileMode.Open))
        {
            result = (xmlSerializer.Deserialize(fileStream) as EventContainer);
        }
        return result;
    }

    public static EventManager.CEventFlow eventToFlow(Event ev, int eventIndex)
    {
        EventManager.CEventFlow flow = new EventManager.CEventFlow();
        flow.EventName = eventIndex + ": " + ev.Name;
        flow.EventType = (EEventType)Enum.Parse(typeof(EEventType), ev.Type);
        flow.Location = (ESchoolLocation)Enum.Parse(typeof(ESchoolLocation), ev.Location);
        flow.EventScenes = new EventManager.CEventFlow.CEventScene[ev.EventScenes.Count];
        for (int i = 0; i < ev.EventScenes.Count; i++)
        {
            if (ev.EventScenes[i] != null)
            {
                flow.EventScenes[i] = new EventManager.CEventFlow.CEventScene();
            }
            if (ev.EventScenes[i].SceneLayout != null)
            {
                flow.EventScenes[i].SceneLayout = (EEventLayoutType)Enum.Parse(typeof(EEventLayoutType), ev.EventScenes[i].SceneLayout);
            }
            if (ev.EventScenes[i].Illustration_BG != null)
            {
                flow.EventScenes[i].Illustration_BG = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_BG, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_FG != null)
            {
                flow.EventScenes[i].Illustration_FG = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_FG, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_Player != null)
            {
                flow.EventScenes[i].Illustration_Player = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_Player, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Solo_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Solo_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Solo_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Duo_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Duo_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Duo_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Duo_2 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Duo_2 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Duo_2, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Trio_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Trio_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Trio_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Trio_2 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Trio_2 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Trio_2, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Trio_3 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Trio_3 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Trio_3, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Quartet_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Quartet_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Quartet_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Quartet_2 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Quartet_2 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Quartet_2, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Quartet_3 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Quartet_3 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Quartet_3, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Quartet_4 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Quartet_4 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Quartet_4, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team1to2_T1_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team1to2_T1_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team1to2_T1_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team1to2_T2_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team1to2_T2_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team1to2_T2_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team1to2_T2_2 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team1to2_T2_2 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team1to2_T2_2, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team2to1_T1_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team2to1_T1_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team2to1_T1_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team2to1_T1_2 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team2to1_T1_2 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team2to1_T1_2, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team2to1_T2_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team2to1_T2_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team2to1_T2_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team2to2_T1_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team2to2_T1_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team2to2_T1_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team2to2_T1_2 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team2to2_T1_2 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team2to2_T1_2, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team2to2_T2_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team2to2_T2_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team2to2_T2_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team2to2_T2_2 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team2to2_T2_2 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team2to2_T2_2, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team1to3_T1_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team1to3_T1_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team1to3_T1_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team1to3_T2_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team1to3_T2_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team1to3_T2_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team1to3_T2_2 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team1to3_T2_2 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team1to3_T2_2, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team1to3_T2_3 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team1to3_T2_3 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team1to3_T2_3, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team3to1_T1_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team3to1_T1_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team3to1_T1_1, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team3to1_T1_2 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team3to1_T1_2 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team3to1_T1_2, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team3to1_T1_3 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team3to1_T1_3 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team3to1_T1_3, typeof(Sprite));
            }
            if (ev.EventScenes[i].Illustration_NPC_Team3to1_T2_1 != null)
            {
                flow.EventScenes[i].Illustration_NPC_Team3to1_T2_1 = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_NPC_Team3to1_T2_1, typeof(Sprite));
            }
            flow.EventScenes[i].IsLarge_Illustration_NPC_Solo_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Solo_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Duo_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Duo_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Duo_2 = ev.EventScenes[i].IsLarge_Illustration_NPC_Duo_2;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Trio_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Trio_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Trio_2 = ev.EventScenes[i].IsLarge_Illustration_NPC_Trio_2;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Trio_3 = ev.EventScenes[i].IsLarge_Illustration_NPC_Trio_3;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Quartet_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Quartet_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Quartet_2 = ev.EventScenes[i].IsLarge_Illustration_NPC_Quartet_2;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Quartet_3 = ev.EventScenes[i].IsLarge_Illustration_NPC_Quartet_3;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Quartet_4 = ev.EventScenes[i].IsLarge_Illustration_NPC_Quartet_4;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team1to2_T1_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team1to2_T1_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team1to2_T2_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team1to2_T2_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team1to2_T2_2 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team1to2_T2_2;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team2to1_T1_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team2to1_T1_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team2to1_T1_2 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team2to1_T1_2;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team2to1_T2_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team2to1_T2_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team2to2_T1_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team2to2_T1_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team2to2_T1_2 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team2to2_T1_2;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team2to2_T2_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team2to2_T2_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team2to2_T2_2 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team2to2_T2_2;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team1to3_T1_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team1to3_T1_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team1to3_T2_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team1to3_T2_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team1to3_T2_2 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team1to3_T2_2;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team1to3_T2_3 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team1to3_T2_3;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team3to1_T1_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team3to1_T1_1;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team3to1_T1_2 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team3to1_T1_2;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team3to1_T1_3 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team3to1_T1_3;
            flow.EventScenes[i].IsLarge_Illustration_NPC_Team3to1_T2_1 = ev.EventScenes[i].IsLarge_Illustration_NPC_Team3to1_T2_1;
            if (ev.EventScenes[i].WhoSpeaksNPC != null)
            {
                flow.EventScenes[i].WhoSpeaksNPC = ev.EventScenes[i].WhoSpeaksNPC;
            }
            if (ev.EventScenes[i].WhoSpeaksSprite != null)
            {
                flow.EventScenes[i].WhoSpeaksSprite = ev.EventScenes[i].WhoSpeaksSprite;
            }
            flow.EventScenes[i].Text[ELanguage.English] = ev.EventScenes[i].Text;
            flow.EventScenes[i].IsOptionScene = ev.EventScenes[i].IsOptionScene;
            flow.EventScenes[i].TextOption2[ELanguage.English] = ev.EventScenes[i].TextOption2;
            flow.EventScenes[i].IsFinalScene = ev.EventScenes[i].IsFinalScene;
            flow.EventScenes[i].HasForcedLocation = ev.EventScenes[i].HasForcedLocation;
            if (ev.EventScenes[i].ForcedOutfitForCharacters != null)
            {
                flow.EventScenes[i].ForcedOutfitForCharacters = ev.EventScenes[i].ForcedOutfitForCharacters.ToArray();
            }
            if (ev.EventScenes[i].SfxId != null)
            {
                flow.EventScenes[i].SfxId = ev.EventScenes[i].SfxId;
            }
        }
        flow.IndexSceneOfOption1Success = ev.IndexSceneOfOption1Success;
        flow.IndexSceneOfOption1Failure = ev.IndexSceneOfOption1Failure;
        flow.IndexSceneOfOption2Success = ev.IndexSceneOfOption2Success;
        flow.IndexSceneOfOption2Failure = ev.IndexSceneOfOption2Failure;
        if (ev.StatRequired_Option1 != null)
        {
            flow.StatRequired_Option1 = (EStat)Enum.Parse(typeof(EStat), ev.StatRequired_Option1);
        }
        if (ev.StatRequired_Option2 != null)
        {
            flow.StatRequired_Option2 = (EStat)Enum.Parse(typeof(EStat), ev.StatRequired_Option2);
        }
        if (ev.TopicTags != null)
        {
            flow.TopicTags = ev.TopicTags.ToArray();
        }
        if (ev.ArgumentTags != null)
        {
            flow.ArgumentTags = ev.ArgumentTags.ToArray();
        }
        if (ev.CharacterTags != null)
        {
            flow.CharacterTags = ev.CharacterTags.ToArray();
        }
        if (ev.EventRestrictions != null)
        {
            flow.EventRestrictions = new EventManager.CEventFlow.CEventRestriction[ev.EventRestrictions.Count];
        }
        for (int j = 0; j < ev.EventRestrictions.Count; j++)
        {
            if (ev.EventRestrictions[j].Type != null)
            {
                flow.EventRestrictions[j].RestrictionType = (EEventRestrictionType)Enum.Parse(typeof(EEventRestrictionType), ev.EventRestrictions[j].Type);
            }
            flow.EventRestrictions[j].TrueOrFalse = ev.EventRestrictions[j].TrueOrFalse;
            if (ev.EventRestrictions[j].RestrictionData != null)
            {
                flow.EventRestrictions[j].RestrictionData = ev.EventRestrictions[j].RestrictionData;
            }
        }
        if (ev.Option1SuccessLoves != null)
        {
            flow.Option1Success_Loves = ev.Option1SuccessLoves.ToArray();
        }
        if (ev.Option1FailureLoves != null)
        {
            flow.Option1Failure_Loves = ev.Option1FailureLoves.ToArray();
        }
        if (ev.Option2SuccessLoves != null)
        {
            flow.Option2Success_Loves = ev.Option2SuccessLoves.ToArray();
        }
        if (ev.Option2FailureLoves != null)
        {
            flow.Option2Failure_Loves = ev.Option2FailureLoves.ToArray();
        }
        flow.ContinuityData = new EventManager.CEventFlow.CEventContinuity();
        if (ev.ContinuityData != null)
        {
            flow.ContinuityData.Initialized = ev.ContinuityData.Initialized;
            flow.ContinuityData.IsContinuityRoot = ev.ContinuityData.IsContinuityRoot;
            flow.ContinuityData.ContinuityEventLengthFromThisNode = ev.ContinuityData.ContinuityEventLengthFromThisNode;
            flow.ContinuityData.Option1Success_ContinuityIndex = ev.ContinuityData.Option1SuccessContinuityIndex;
            flow.ContinuityData.Option1Failure_ContinuityIndex = ev.ContinuityData.Option1FailureContinuityIndex;
            flow.ContinuityData.Option2Success_ContinuityIndex = ev.ContinuityData.Option2SuccessContinuityIndex;
            flow.ContinuityData.Option2Failure_ContinuityIndex = ev.ContinuityData.Option2FailureContinuityIndex;
            flow.ContinuityData.Option1Success_IsSelfContinuity = ev.ContinuityData.Option1SuccessIsSelfContinuity;
            flow.ContinuityData.Option1Failure_IsSelfContinuity = ev.ContinuityData.Option1FailureIsSelfContinuity;
            flow.ContinuityData.Option2Success_IsSelfContinuity = ev.ContinuityData.Option2SuccessIsSelfContinuity;
            flow.ContinuityData.Option2Failure_IsSelfContinuity = ev.ContinuityData.Option2FailureIsSelfContinuity;
        }
        if (ev.Option1Success_RuleNoMoreTags != null)
        {
            flow.Option1Success_RuleNoMoreTags = ev.Option1Success_RuleNoMoreTags.ToArray();
        }
        if (ev.Option1Failure_RuleNoMoreTags != null)
        {
            flow.Option1Failure_RuleNoMoreTags = ev.Option1Failure_RuleNoMoreTags.ToArray();
        }
        if (ev.Option2Success_RuleNoMoreTags != null)
        {
            flow.Option2Success_RuleNoMoreTags = ev.Option2Success_RuleNoMoreTags.ToArray();
        }
        if (ev.Option2Failure_RuleNoMoreTags != null)
        {
            flow.Option2Failure_RuleNoMoreTags = ev.Option2Failure_RuleNoMoreTags.ToArray();
        }
        if (ev.Option1Success_RuleMoreTags != null)
        {
            flow.Option1Success_RuleMoreTags = ev.Option1Success_RuleMoreTags.ToArray();
        }
        if (ev.Option1Failure_RuleMoreTags != null)
        {
            flow.Option1Failure_RuleMoreTags = ev.Option1Failure_RuleMoreTags.ToArray();
        }
        if (ev.Option2Success_RuleMoreTags != null)
        {
            flow.Option2Success_RuleMoreTags = ev.Option2Success_RuleMoreTags.ToArray();
        }
        if (ev.Option2Failure_RuleMoreTags != null)
        {
            flow.Option2Failure_RuleMoreTags = ev.Option2Failure_RuleMoreTags.ToArray();
        }
        return flow;
    }
}
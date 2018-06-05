using NGameConstants;
using PromDate;
using PromDate.EventLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PromDate
{
    [XmlRoot("EventCollection")]
    public class EventContainer
    {
        [XmlArray("Events")]
        [XmlArrayItem("Event")]
        public List<ModEvent> Events = new List<ModEvent>();

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

        public static EventManager.CEventFlow eventToFlow(ModEvent ev)
        {
            EventManager.CEventFlow flow = new EventManager.CEventFlow();

            flow.EventName = ev.Name;
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
                if (ev.EventScenes[i].Illustration_BG != "")
                {
                    flow.EventScenes[i].Illustration_BG = SpriteHelper.LookupBG(ev.EventScenes[i].Illustration_BG, ev);
                }
                if (ev.EventScenes[i].Illustration_FG != "")
                {
                    flow.EventScenes[i].Illustration_FG = SpriteHelper.LookupCustomSprite(ev.EventScenes[i].Illustration_FG);
                }
                if (ev.EventScenes[i].Illustration_Player != null)
                {
                    flow.EventScenes[i].Illustration_Player = (Sprite)Resources.Load(ev.EventScenes[i].Illustration_Player, typeof(Sprite));
                }

                SetupSprites(ref flow.EventScenes[i], ev.EventScenes[i]);

                if (ev.EventScenes[i].WhoSpeaksNPC != null)
                {
                    flow.EventScenes[i].WhoSpeaksNPC = ev.EventScenes[i].WhoSpeaksNPC;
                }
                if (ev.EventScenes[i].WhoSpeaksSprite != null)
                {
                    flow.EventScenes[i].WhoSpeaksSprite = ev.EventScenes[i].WhoSpeaksSprite;
                }
                if (ev.EventScenes[i].Text == null) throw new ArgumentNullException("Text", "Text cannot be null, please check your xml file.");
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

            ev.ArgumentTags.Add("MOD");
            flow.ArgumentTags = ev.ArgumentTags.ToArray();

            if (ev.CharacterTags != null)
            {
                flow.CharacterTags = ev.CharacterTags.ToArray();
                ev.CharacterTags.ForEach(cha => { if (!ModConstants.VANILLA_CHARACTERS.Contains(cha)) EventHelper.AddCharacter(cha); });
            }
            if (ev.EventRestrictions != null)
            {
                flow.EventRestrictions = new EventManager.CEventFlow.CEventRestriction[ev.EventRestrictions.Count];
            }
            for (int j = 0; j < ev.EventRestrictions.Count; j++)
            {
                flow.EventRestrictions[j] = new EventManager.CEventFlow.CEventRestriction();
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

        public static void SetupSprites(ref EventManager.CEventFlow.CEventScene flowScene, ModScene scene)
        {
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Setting up sprites");
            if (scene == null)
            {
                GeneralManager.Instance.LogToFileOrConsole("[PromDate] The scene is null?");
            }
            switch (flowScene.SceneLayout)
            {
                case EEventLayoutType.Solo:
                    Character cha1 = scene.Illustration_NPC_Solo_1;
                    flowScene.IsLarge_Illustration_NPC_Solo_1 = cha1.Large;
                    GeneralManager.Instance.LogToFileOrConsole("[PromDate] Looking up sprite for " + cha1.Name);
                    flowScene.Illustration_NPC_Solo_1 = SpriteHelper.LookupNpcSprite(cha1.Name, cha1.Outfit, cha1.Mood);
                    break;
                case EEventLayoutType.Duo:
                    cha1 = scene.Illustration_NPC_Duo_1;
                    Character cha2 = scene.Illustration_NPC_Duo_2;
                    flowScene.IsLarge_Illustration_NPC_Duo_1 = cha1.Large;
                    flowScene.IsLarge_Illustration_NPC_Duo_2 = cha2.Large;
                    flowScene.Illustration_NPC_Duo_1 = SpriteHelper.LookupNpcSprite(cha1.Name, cha1.Outfit, cha1.Mood);
                    flowScene.Illustration_NPC_Duo_2 = SpriteHelper.LookupNpcSprite(cha2.Name, cha2.Outfit, cha2.Mood);
                    break;
                case EEventLayoutType.Trio:
                    cha1 = scene.Illustration_NPC_Trio_1;
                    cha2 = scene.Illustration_NPC_Trio_2;
                    Character cha3 = scene.Illustration_NPC_Trio_3;
                    flowScene.IsLarge_Illustration_NPC_Trio_1 = cha1.Large;
                    flowScene.IsLarge_Illustration_NPC_Trio_2 = cha2.Large;
                    flowScene.IsLarge_Illustration_NPC_Trio_3 = cha3.Large;
                    flowScene.Illustration_NPC_Trio_1 = SpriteHelper.LookupNpcSprite(cha1.Name, cha1.Outfit, cha1.Mood);
                    flowScene.Illustration_NPC_Trio_2 = SpriteHelper.LookupNpcSprite(cha2.Name, cha2.Outfit, cha2.Mood);
                    flowScene.Illustration_NPC_Trio_3 = SpriteHelper.LookupNpcSprite(cha3.Name, cha3.Outfit, cha3.Mood);
                    break;
                case EEventLayoutType.Quartet:
                    cha1 = scene.Illustration_NPC_Quartet_1;
                    cha2 = scene.Illustration_NPC_Quartet_2;
                    cha3 = scene.Illustration_NPC_Quartet_3;
                    Character cha4 = scene.Illustration_NPC_Quartet_4;
                    flowScene.IsLarge_Illustration_NPC_Quartet_1 = cha1.Large;
                    flowScene.IsLarge_Illustration_NPC_Quartet_2 = cha2.Large;
                    flowScene.IsLarge_Illustration_NPC_Quartet_3 = cha3.Large;
                    flowScene.IsLarge_Illustration_NPC_Quartet_4 = cha4.Large;
                    flowScene.Illustration_NPC_Quartet_1 = SpriteHelper.LookupNpcSprite(cha1.Name, cha1.Outfit, cha1.Mood);
                    flowScene.Illustration_NPC_Quartet_2 = SpriteHelper.LookupNpcSprite(cha2.Name, cha2.Outfit, cha2.Mood);
                    flowScene.Illustration_NPC_Quartet_3 = SpriteHelper.LookupNpcSprite(cha3.Name, cha3.Outfit, cha3.Mood);
                    flowScene.Illustration_NPC_Quartet_4 = SpriteHelper.LookupNpcSprite(cha4.Name, cha4.Outfit, cha4.Mood);
                    break;
                case EEventLayoutType.Team1to2:
                    cha1 = scene.Illustration_NPC_Team1to2_T1_1;
                    cha2 = scene.Illustration_NPC_Team1to2_T2_1;
                    cha3 = scene.Illustration_NPC_Team1to2_T2_2;
                    flowScene.IsLarge_Illustration_NPC_Team1to2_T1_1 = cha1.Large;
                    flowScene.IsLarge_Illustration_NPC_Team1to2_T2_1 = cha2.Large;
                    flowScene.IsLarge_Illustration_NPC_Team1to2_T2_2 = cha3.Large;
                    flowScene.Illustration_NPC_Team1to2_T1_1 = SpriteHelper.LookupNpcSprite(cha1.Name, cha1.Outfit, cha1.Mood);
                    flowScene.Illustration_NPC_Team1to2_T2_1 = SpriteHelper.LookupNpcSprite(cha2.Name, cha2.Outfit, cha2.Mood);
                    flowScene.Illustration_NPC_Team1to2_T2_2 = SpriteHelper.LookupNpcSprite(cha3.Name, cha3.Outfit, cha3.Mood);
                    break;
                case EEventLayoutType.Team2to1:
                    cha1 = scene.Illustration_NPC_Team2to1_T1_1;
                    cha2 = scene.Illustration_NPC_Team2to1_T1_2;
                    cha3 = scene.Illustration_NPC_Team2to1_T2_1;
                    flowScene.IsLarge_Illustration_NPC_Team2to1_T1_1 = cha1.Large;
                    flowScene.IsLarge_Illustration_NPC_Team2to1_T1_2 = cha2.Large;
                    flowScene.IsLarge_Illustration_NPC_Team2to1_T2_1 = cha3.Large;
                    flowScene.Illustration_NPC_Team2to1_T1_1 = SpriteHelper.LookupNpcSprite(cha1.Name, cha1.Outfit, cha1.Mood);
                    flowScene.Illustration_NPC_Team2to1_T1_2 = SpriteHelper.LookupNpcSprite(cha2.Name, cha2.Outfit, cha2.Mood);
                    flowScene.Illustration_NPC_Team2to1_T2_1 = SpriteHelper.LookupNpcSprite(cha3.Name, cha3.Outfit, cha3.Mood);
                    break;
                case EEventLayoutType.Team2to2:
                    cha1 = scene.Illustration_NPC_Team2to2_T1_1;
                    cha2 = scene.Illustration_NPC_Team2to2_T1_2;
                    cha3 = scene.Illustration_NPC_Team2to2_T2_1;
                    cha4 = scene.Illustration_NPC_Team2to2_T2_2;
                    flowScene.IsLarge_Illustration_NPC_Team2to2_T1_1 = cha1.Large;
                    flowScene.IsLarge_Illustration_NPC_Team2to2_T1_2 = cha2.Large;
                    flowScene.IsLarge_Illustration_NPC_Team2to2_T2_1 = cha3.Large;
                    flowScene.IsLarge_Illustration_NPC_Team2to2_T2_2 = cha4.Large;
                    flowScene.Illustration_NPC_Team2to2_T1_1 = SpriteHelper.LookupNpcSprite(cha1.Name, cha1.Outfit, cha1.Mood);
                    flowScene.Illustration_NPC_Team2to2_T1_2 = SpriteHelper.LookupNpcSprite(cha2.Name, cha2.Outfit, cha2.Mood);
                    flowScene.Illustration_NPC_Team2to2_T2_1 = SpriteHelper.LookupNpcSprite(cha3.Name, cha3.Outfit, cha3.Mood);
                    flowScene.Illustration_NPC_Team2to2_T2_2 = SpriteHelper.LookupNpcSprite(cha4.Name, cha4.Outfit, cha4.Mood);
                    break;
                case EEventLayoutType.Team3to1:
                    cha1 = scene.Illustration_NPC_Team3to1_T1_1;
                    cha2 = scene.Illustration_NPC_Team3to1_T1_2;
                    cha3 = scene.Illustration_NPC_Team3to1_T1_3;
                    cha4 = scene.Illustration_NPC_Team3to1_T2_1;
                    flowScene.IsLarge_Illustration_NPC_Team3to1_T1_1 = cha1.Large;
                    flowScene.IsLarge_Illustration_NPC_Team3to1_T1_2 = cha2.Large;
                    flowScene.IsLarge_Illustration_NPC_Team3to1_T1_3 = cha3.Large;
                    flowScene.IsLarge_Illustration_NPC_Team3to1_T2_1 = cha4.Large;
                    flowScene.Illustration_NPC_Team3to1_T1_1 = SpriteHelper.LookupNpcSprite(cha1.Name, cha1.Outfit, cha1.Mood);
                    flowScene.Illustration_NPC_Team3to1_T1_2 = SpriteHelper.LookupNpcSprite(cha2.Name, cha2.Outfit, cha2.Mood);
                    flowScene.Illustration_NPC_Team3to1_T1_3 = SpriteHelper.LookupNpcSprite(cha3.Name, cha3.Outfit, cha3.Mood);
                    flowScene.Illustration_NPC_Team3to1_T2_1 = SpriteHelper.LookupNpcSprite(cha4.Name, cha4.Outfit, cha4.Mood);
                    break;
                case EEventLayoutType.Team1to3:
                    cha1 = scene.Illustration_NPC_Team1to3_T1_1;
                    cha2 = scene.Illustration_NPC_Team1to3_T2_1;
                    cha3 = scene.Illustration_NPC_Team1to3_T2_2;
                    cha4 = scene.Illustration_NPC_Team1to3_T2_3;
                    flowScene.IsLarge_Illustration_NPC_Team1to3_T1_1 = cha1.Large;
                    flowScene.IsLarge_Illustration_NPC_Team1to3_T2_1 = cha2.Large;
                    flowScene.IsLarge_Illustration_NPC_Team1to3_T2_2 = cha3.Large;
                    flowScene.IsLarge_Illustration_NPC_Team1to3_T2_3 = cha4.Large;
                    flowScene.Illustration_NPC_Team1to3_T1_1 = SpriteHelper.LookupNpcSprite(cha1.Name, cha1.Outfit, cha1.Mood);
                    flowScene.Illustration_NPC_Team1to3_T2_1 = SpriteHelper.LookupNpcSprite(cha2.Name, cha2.Outfit, cha2.Mood);
                    flowScene.Illustration_NPC_Team1to3_T2_2 = SpriteHelper.LookupNpcSprite(cha3.Name, cha3.Outfit, cha3.Mood);
                    flowScene.Illustration_NPC_Team1to3_T2_3 = SpriteHelper.LookupNpcSprite(cha4.Name, cha4.Outfit, cha4.Mood);
                    break;
            }
            GeneralManager.Instance.LogToFileOrConsole("[PromDate] Finished loading sprites.");
        }
    }
}
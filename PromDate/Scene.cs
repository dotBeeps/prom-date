using System;
using System.Collections.Generic;
using System.Xml.Serialization;

// Token: 0x02000004 RID: 4
public class Scene
{
	// Token: 0x0400001D RID: 29
	public string SceneLayout;

	// Token: 0x0400001E RID: 30
	public string Illustration_BG;

	// Token: 0x0400001F RID: 31
	public string Illustration_FG;

	// Token: 0x04000020 RID: 32
	public string Illustration_Player;

	// Token: 0x04000021 RID: 33
	public Character Illustration_NPC_Solo_1;

	// Token: 0x04000023 RID: 35
	public Character Illustration_NPC_Duo_1;

	// Token: 0x04000025 RID: 37
	public Character Illustration_NPC_Duo_2;

	// Token: 0x04000027 RID: 39
	public Character Illustration_NPC_Trio_1;

	// Token: 0x04000029 RID: 41
	public Character Illustration_NPC_Trio_2;

	// Token: 0x0400002B RID: 43
	public Character Illustration_NPC_Trio_3;

	// Token: 0x0400002D RID: 45
	public Character Illustration_NPC_Quartet_1;

	// Token: 0x0400002F RID: 47
	public Character Illustration_NPC_Quartet_2;

	// Token: 0x04000031 RID: 49
	public Character Illustration_NPC_Quartet_3;

	// Token: 0x04000033 RID: 51
	public Character Illustration_NPC_Quartet_4;

	// Token: 0x04000035 RID: 53
	public Character Illustration_NPC_Team1to2_T1_1;

	// Token: 0x04000036 RID: 54
	public bool IsLarge_Illustration_NPC_Team1to2_T1_1;

	// Token: 0x04000037 RID: 55
	public Character Illustration_NPC_Team1to2_T2_1;

	// Token: 0x04000039 RID: 57
	public Character Illustration_NPC_Team1to2_T2_2;

	// Token: 0x0400003B RID: 59
	public Character Illustration_NPC_Team2to1_T1_1;

	// Token: 0x0400003D RID: 61
	public Character Illustration_NPC_Team2to1_T1_2;

	// Token: 0x0400003F RID: 63
	public Character Illustration_NPC_Team2to1_T2_1;

	// Token: 0x04000041 RID: 65
	public Character Illustration_NPC_Team2to2_T1_1;

	// Token: 0x04000043 RID: 67
	public Character Illustration_NPC_Team2to2_T1_2;

	// Token: 0x04000045 RID: 69
	public Character Illustration_NPC_Team2to2_T2_1;

	// Token: 0x04000047 RID: 71
	public Character Illustration_NPC_Team2to2_T2_2;

	// Token: 0x04000049 RID: 73
	public Character Illustration_NPC_Team1to3_T1_1;

	// Token: 0x0400004B RID: 75
	public Character Illustration_NPC_Team1to3_T2_1;

	// Token: 0x0400004D RID: 77
	public Character Illustration_NPC_Team1to3_T2_2;

	// Token: 0x0400004F RID: 79
	public Character Illustration_NPC_Team1to3_T2_3;

	// Token: 0x04000051 RID: 81
	public Character Illustration_NPC_Team3to1_T1_1;

	// Token: 0x04000053 RID: 83
	public Character Illustration_NPC_Team3to1_T1_2;

	// Token: 0x04000055 RID: 85
	public Character Illustration_NPC_Team3to1_T1_3;

	// Token: 0x04000057 RID: 87
	public Character Illustration_NPC_Team3to1_T2_1;

	// Token: 0x04000059 RID: 89
	public string WhoSpeaksNPC;

	// Token: 0x0400005A RID: 90
	public string WhoSpeaksSprite;

	// Token: 0x0400005B RID: 91
	public string Text;

	// Token: 0x0400005C RID: 92
	public bool IsOptionScene;

	// Token: 0x0400005D RID: 93
	public string TextOption2;

	// Token: 0x0400005E RID: 94
	public bool IsFinalScene;

	// Token: 0x0400005F RID: 95
	public bool HasForcedLocation;

	// Token: 0x04000060 RID: 96
	[XmlArray("ForcedOutfitForCharacters")]
	[XmlArrayItem("Outfit")]
	public List<string> ForcedOutfitForCharacters = new List<string>();

	// Token: 0x04000061 RID: 97
	public string SfxId;
}

public class Character
{
    public string Name;
    public int Outfit;
    public string Mood;
    public bool Large;
}

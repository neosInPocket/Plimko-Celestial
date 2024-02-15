using System;

[Serializable]
public class SerializeData
{
	public int progressValue;
	public int coins;
	public bool enabledMain;
	public bool enabledSFXSounds;
	public bool conductorPassed;
	public int skillPoints;
	public bool[] skills;

	public SerializeData()
	{

	}

	public SerializeData(SerializeData serializeData)
	{
		progressValue = serializeData.progressValue;
		coins = serializeData.coins;
		enabledMain = serializeData.enabledMain;
		enabledSFXSounds = serializeData.enabledSFXSounds;
		skillPoints = serializeData.skillPoints;
		skills = serializeData.skills;
		conductorPassed = serializeData.conductorPassed;

	}
}

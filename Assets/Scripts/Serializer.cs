using System.IO;
using UnityEngine;

public class Serializer
{
	private SerializeData gameStartValues;
	private static string serializedPath => Application.persistentDataPath + "/SerializedInfo.json";
	private SerializeData data;
	public SerializeData SerializedData => data;

	public Serializer(bool keepData, SerializeData defaultData)
	{
		gameStartValues = defaultData;

		if (keepData)
		{
			LoadSerializedData();
		}
		else
		{
			SetDefaultSerializedData();
		}
	}

	public void SaveChanges()
	{
		if (!File.Exists(serializedPath))
		{
			CreateNewSaveFile();
		}
		else
		{
			WriteNewData();
		}
	}

	private void LoadSerializedData()
	{
		if (!File.Exists(serializedPath))
		{
			CreateNewSaveFile();
		}
		else
		{
			LoadFile();
		}
	}

	private void SetDefaultSerializedData()
	{
		CreateNewSaveFile();
	}

	private void CreateNewSaveFile()
	{
		data = new SerializeData(gameStartValues);
		File.WriteAllText(serializedPath, JsonUtility.ToJson(data, prettyPrint: true));
	}

	private void WriteNewData()
	{
		File.WriteAllText(serializedPath, JsonUtility.ToJson(data, prettyPrint: true));
	}

	private void LoadFile()
	{
		string jsonData = File.ReadAllText(serializedPath);
		data = JsonUtility.FromJson<SerializeData>(jsonData);
	}
}

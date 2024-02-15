using TMPro;
using UnityEngine;

public class CurrentLevelFiller : MonoBehaviour
{
	[SerializeField] private TMP_Text currentLevel;

	private void Start()
	{
		currentLevel.text = SerializeDataView.Data.SerializedData.progressValue.ToString();
	}
}

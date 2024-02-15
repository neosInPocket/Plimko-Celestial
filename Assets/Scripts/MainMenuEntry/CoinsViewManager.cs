using TMPro;
using UnityEngine;

public class CoinsViewManager : MonoBehaviour
{
	[SerializeField] private TMP_Text coinsText;

	private void Start()
	{
		coinsText.text = SerializeDataView.Data.SerializedData.coins.ToString();
	}

	public void SetCoinsValue()
	{
		coinsText.text = SerializeDataView.Data.SerializedData.coins.ToString();
	}
}

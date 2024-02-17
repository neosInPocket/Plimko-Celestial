using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastEndFrame : MonoBehaviour
{
	[SerializeField] private TMP_Text coinsAmountText;
	[SerializeField] private TMP_Text gameScreenEndText;
	[SerializeField] private TMP_Text nextButtonText;
	public void EndGameResult(int coinsRecieved)
	{
		gameObject.SetActive(true);
		bool isLose = coinsRecieved == 0;

		if (isLose)
		{
			gameScreenEndText.text = "LOSE";
			nextButtonText.text = "try again";
		}
		else
		{
			gameScreenEndText.text = "WIN";
			nextButtonText.text = "next";
			SerializeDataView.Data.SerializedData.coins += coinsRecieved;
			SerializeDataView.Data.SerializedData.progressValue++;
			SerializeDataView.Data.SaveChanges();
		}

		coinsAmountText.text = coinsRecieved.ToString();
	}

	public void MenuToReturn()
	{
		SceneManager.LoadScene("PlayEntry");
	}

	public void NewLoad()
	{
		SceneManager.LoadScene("RunnerScene");
	}
}

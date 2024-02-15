using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointsContainer : MonoBehaviour
{
	[SerializeField] private TMP_Text skillText;
	[SerializeField] private int cost;
	[SerializeField] private TMP_Text costText;
	[SerializeField] private GameObject status;
	[SerializeField] private CoinsViewManager coinsViewManager;
	[SerializeField] private Button button;
	[SerializeField] private SkillStoreController skillStoreController;

	private void Start()
	{
		RefreshSkillPoints();
	}

	public void RefreshSkillPoints()
	{
		skillText.text = SerializeDataView.Data.SerializedData.skillPoints.ToString();

		costText.text = cost.ToString();

		status.SetActive(SerializeDataView.Data.SerializedData.coins < cost);
		button.interactable = SerializeDataView.Data.SerializedData.coins >= cost;

		coinsViewManager.SetCoinsValue();
	}

	public void PurchaseSkillPoint()
	{
		SerializeDataView.Data.SerializedData.skillPoints++;
		SerializeDataView.Data.SerializedData.coins -= cost;
		SerializeDataView.Data.SaveChanges();

		RefreshSkillPoints();
		skillStoreController.RefreshAllSkills();
		skillStoreController.RefreshCurrentSkill();
	}
}

using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillStoreController : MonoBehaviour
{
	[SerializeField] private List<SkillPointHolder> holders;
	[SerializeField] private GameObject statusText;
	[SerializeField] private TMP_Text costText;
	[SerializeField] private TMP_Text descriptionText;
	[SerializeField] private Button button;
	[SerializeField] private SkillPointsContainer skillPointsContainer;
	private int currentIndex;

	private void Start()
	{
		SelectSkill(holders[0]);
	}

	public void SelectSkill(SkillPointHolder skillPointHolder)
	{
		holders.ForEach(x => x.SelectSkill(false));
		skillPointHolder.SelectSkill(true);

		currentIndex = holders.IndexOf(skillPointHolder);

		bool purchased = SerializeDataView.Data.SerializedData.skills[currentIndex];
		bool avaliable = SerializeDataView.Data.SerializedData.skillPoints >= skillPointHolder.Cost;

		if (avaliable)
		{
			button.interactable = true;
		}
		else
		{
			button.interactable = false;
		}

		if (purchased)
		{
			button.interactable = false;
		}


		statusText.SetActive(!purchased && !avaliable);
		descriptionText.text = skillPointHolder.Description;
		costText.text = skillPointHolder.Cost.ToString();

	}

	public void BuySkill()
	{
		SerializeDataView.Data.SerializedData.skills[currentIndex] = true;
		SerializeDataView.Data.SerializedData.skillPoints -= holders[currentIndex].Cost;

		SerializeDataView.Data.SaveChanges();

		RefreshAllSkills();
		skillPointsContainer.RefreshSkillPoints();
		SelectSkill(holders[currentIndex]);
	}

	public void RefreshCurrentSkill()
	{
		SelectSkill(holders[currentIndex]);
	}

	public void RefreshAllSkills()
	{
		holders.ForEach(x => x.RefreshSkill());
	}
}

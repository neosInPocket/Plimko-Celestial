using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointHolder : MonoBehaviour
{
	[SerializeField] private Image frame;
	[SerializeField] private Sprite frameActive;
	[SerializeField] private Sprite frameInactive;
	[SerializeField] private Color frameInactiveColor;
	[SerializeField] private Color skillInactive;
	[SerializeField] private SkillStoreController store;
	[SerializeField] private Image skillImage;
	[SerializeField] private int skillIndex;
	[SerializeField] private int cost;
	[SerializeField] private string description;
	public int Cost => cost;
	public string Description => description;

	private void Start()
	{
		RefreshSkill();
	}

	public void SelectSkill(bool value)
	{
		if (value)
		{
			frame.sprite = frameActive;
			frame.color = Color.white;
		}
		else
		{
			frame.sprite = frameInactive;
			frame.color = frameInactiveColor;
		}
	}

	public void RefreshSkill()
	{
		skillImage.color = SerializeDataView.Data.SerializedData.skills[skillIndex] ? Color.white : skillInactive;
	}
}

using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeMain : MonoBehaviour
{
	[SerializeField] private Color backgroundDisabled;
	[SerializeField] private Image button;
	private BackgroundVolumeManager soundsSystem;

	private void Start()
	{
		soundsSystem = GameObject.FindFirstObjectByType<BackgroundVolumeManager>();

		button.color = SerializeDataView.Data.SerializedData.enabledMain ? Color.white : backgroundDisabled;
	}

	public void Toggle()
	{
		bool enabled = button.color == Color.white;

		soundsSystem.Enabled(!enabled);

		button.color = SerializeDataView.Data.SerializedData.enabledMain ? Color.white : backgroundDisabled;
	}
}

using UnityEngine;

public class ToggleVolume : MonoBehaviour
{
	private void Start()
	{
		var audio = GetComponent<AudioSource>();

		audio.volume = SerializeDataView.Data.SerializedData.enabledSFXSounds ? 1f : 0;
	}
}

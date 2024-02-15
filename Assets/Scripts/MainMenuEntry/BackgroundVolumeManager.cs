using System.Linq;
using UnityEngine;

public class BackgroundVolumeManager : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;

	private void Awake()
	{
		var manager =
		FindObjectsByType<BackgroundVolumeManager>(sortMode: FindObjectsSortMode.None)
		.FirstOrDefault(x => x.gameObject.scene.name == "DontDestroyOnLoad");

		if (manager != null && manager != this)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		Enabled(SerializeDataView.Data.SerializedData.enabledMain);
	}

	public void Enabled(bool value)
	{
		audioSource.enabled = value;
		SerializeDataView.Data.SerializedData.enabledMain = value;
		SerializeDataView.Data.SaveChanges();
	}
}

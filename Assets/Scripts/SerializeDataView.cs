using UnityEngine;

public class SerializeDataView : MonoBehaviour
{
	[SerializeField] private bool keepData;
	[SerializeField] private SerializeData defaultData;
	public static Serializer Data { get; private set; }

	private void Awake()
	{
		Data = new Serializer(keepData, defaultData);
	}
}

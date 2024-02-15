using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewController : MonoBehaviour
{
	[SerializeField] private List<CanvasGroup> canvasGroups;
	[SerializeField] private float speed;
	private CanvasGroup start;

	private void Start()
	{
		canvasGroups.ForEach(x => x.gameObject.SetActive(false));

		start = canvasGroups[0];
		start.gameObject.SetActive(true);
		start.alpha = 1;
		//start.blocksRaycasts = true;
	}

	public void ChangeView(int index)
	{
		StopAllCoroutines();
		StartCoroutine(ViewChanger(index));
	}

	private IEnumerator ViewChanger(int index)
	{
		//start.blocksRaycasts = false;
		//canvasGroups[index].blocksRaycasts = false;
		float distance = 1;

		while (start.alpha > 0)
		{
			start.alpha -= (distance + 0.01f) * speed * Time.deltaTime;
			distance = start.alpha;
			yield return null;
		}

		start.alpha = 0;
		start.gameObject.SetActive(false);

		start = canvasGroups[index];
		start.alpha = 0;
		start.gameObject.SetActive(true);

		distance = 1;
		while (start.alpha < 1)
		{
			start.alpha += (distance + 0.01f) * speed * Time.deltaTime;
			distance = 1 - start.alpha;
			yield return null;
		}

		start.alpha = 1;
		//start.blocksRaycasts = true;
	}

	public void Game()
	{
		SceneManager.LoadScene("RunnerScene");
	}
}

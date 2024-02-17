using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWinTimer : MonoBehaviour
{
	[SerializeField] private TMP_Text seconds;
	[SerializeField] private Image timer;
	private float secondsLeft;
	private float allSeconds;
	public bool Enabled
	{
		get => _enabled;
		set => _enabled = value;
	}

	private bool _enabled;
	public Action TimeEnd { get; set; }

	public void SetTime(int secondsToRun)
	{
		allSeconds = secondsToRun;
		secondsLeft = secondsToRun;
		seconds.text = ((int)(secondsLeft)).ToString() + "s";
		timer.fillAmount = 1f;
	}

	private void Update()
	{
		if (!_enabled) return;

		Tick();
	}

	private void Tick()
	{
		secondsLeft -= Time.deltaTime;
		if (secondsLeft <= 0)
		{
			_enabled = false;
			TimeEnd?.Invoke();
		}

		RefreshVisuals();
	}

	private void RefreshVisuals()
	{
		seconds.text = ((int)(secondsLeft)).ToString();
		timer.fillAmount = (float)secondsLeft / (float)allSeconds;
	}
}

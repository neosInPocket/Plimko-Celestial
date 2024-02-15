using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class SegmentLevelManager : MonoBehaviour
{
	[SerializeField] private Conductor conductor;
	[SerializeField] private Runner runner;
	// [SerializeField] private LoseGameScreen loseGameScreen;
	[SerializeField] private TMP_Text valueLevel;
	[SerializeField] private SegmentSpawner spawner;
	[SerializeField] private PlayerWinTimer playerTimer;

	private int _coinsRecieved;
	private int _timeCanBe;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		CalculateLevelData();

		//valueLevel.text = SerializeDataView.Data.SerializedData.progressValue.ToString();
		//playerTimer.SetTime(_timeCanBe);

		// bool conductorPassed = SerializeDataView.Data.SerializedData.conductorPassed;

		// if (!conductorPassed)
		// {
		// 	SerializeDataView.Data.SerializedData.conductorPassed = false;
		// 	SerializeDataView.Data.SaveChanges();
		// 	Conduction();
		// }
		// else
		// {
		// 	ConductPassed();
		// }
	}

	private void Conduction()
	{
		// guide.GuideCompleted += GuideEndedHandler;
		// guide.Guide();
	}

	private void ConductPassed()
	{
		// guide.GuideCompleted -= GuideEndedHandler;
		Touch.onFingerDown += OnStart;

	}

	private void OnStart(Finger finger)
	{
		Touch.onFingerDown -= OnStart;
		StartLevelPlay();
	}

	private void StartLevelPlay()
	{
		// playerSpring.OnJumpSuccess += OnPlayerJumpSuccess;
		// playerSpring.OnDestroyed += OnPlayerDestroyed;
		// playerSpring.ProvideControls();
	}

	private void OnTimerValueEnd()
	{
		// playerSpring.OnJumpSuccess -= OnPlayerJumpSuccess;
		// playerSpring.OnDestroyed -= OnPlayerDestroyed;

		// loseGameScreen.ShowWindowEnd(rubies, "you win!", "you lose...");
		SerializeDataView.Data.SerializedData.progressValue += 1;
		SerializeDataView.Data.SerializedData.coins += _coinsRecieved;
		SerializeDataView.Data.SaveChanges();
	}

	private void OnPlayerCollided()
	{
		// playerSpring.OnJumpSuccess -= OnPlayerJumpSuccess;
		// playerSpring.OnDestroyed -= OnPlayerDestroyed;

		// loseGameScreen.ShowWindowEnd(0, "you win!", "you lose...");
	}

	public void ReturnToEntryPoint()
	{
		SceneManager.LoadScene("PlayEntry");
	}

	public void ReturnToRunner()
	{
		SceneManager.LoadScene("RunnerScene");
	}

	public void CalculateLevelData()
	{
		var x = SerializeDataView.Data.SerializedData.progressValue;

		_timeCanBe = (int)(10 * Mathf.Sqrt(x));
		_coinsRecieved = (int)(10 * Mathf.Log(x + 2));
	}
}

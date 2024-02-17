using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class SegmentLevelManager : MonoBehaviour
{
	[SerializeField] private Conductor conductor;
	[SerializeField] private Runner runner;
	[SerializeField] private LastEndFrame lastEndFrame;
	[SerializeField] private TMP_Text valueLevel;
	[SerializeField] private SegmentSpawner spawner;
	[SerializeField] private PlayerWinTimer playerTimer;
	[SerializeField] private GameObject startGameText;

	private int _coinsRecieved;
	private int _timeCanBe;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		CalculateLevelData();

		valueLevel.text = "level " + SerializeDataView.Data.SerializedData.progressValue.ToString();
		playerTimer.SetTime(_timeCanBe);

		bool conductorPassed = SerializeDataView.Data.SerializedData.conductorPassed;

		if (!conductorPassed)
		{
			SerializeDataView.Data.SerializedData.conductorPassed = true;
			SerializeDataView.Data.SaveChanges();
			Conduction();
		}
		else
		{
			ConductPassed();
		}
	}

	private void Conduction()
	{
		conductor.ConductorPassed += ConductPassed;
		conductor.StartConductor();
	}

	private void ConductPassed()
	{
		conductor.ConductorPassed -= ConductPassed;
		Touch.onFingerDown += OnStart;
		startGameText.SetActive(true);
	}

	private void OnStart(Finger finger)
	{
		startGameText.SetActive(false);
		Touch.onFingerDown -= OnStart;
		StartLevelPlay();
	}

	private void StartLevelPlay()
	{
		runner.OnDeath += OnRunnerDeath;
		playerTimer.TimeEnd += OnTimerValueEnd;
		playerTimer.Enabled = true;

		runner.EnableControls(true);
		runner.EnableMoving(true);
		spawner.Enable(true);
	}

	private void OnTimerValueEnd()
	{
		playerTimer.TimeEnd -= OnTimerValueEnd;
		runner.OnDeath -= OnRunnerDeath;

		runner.EnableControls(false);
		runner.EnableMoving(false);
		spawner.Enable(false);

		lastEndFrame.EndGameResult(_coinsRecieved);
	}

	private void OnRunnerDeath()
	{
		runner.OnDeath -= OnRunnerDeath;
		playerTimer.TimeEnd -= OnTimerValueEnd;
		runner.EnableControls(false);
		runner.EnableMoving(false);
		spawner.Enable(false);

		lastEndFrame.EndGameResult(0);
	}

	public void CalculateLevelData()
	{
		var x = SerializeDataView.Data.SerializedData.progressValue;

		if (SerializeDataView.Data.SerializedData.skills[2])
		{
			_timeCanBe = (int)(10 * Mathf.Sqrt(x)) - 2;
		}
		else
		{
			_timeCanBe = (int)(10 * Mathf.Sqrt(x));
		}

		_coinsRecieved = (int)(10 * Mathf.Log(x + 2));
	}
}

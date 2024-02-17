using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Conductor : MonoBehaviour
{
	[SerializeField] private GameObject runnerArrow;
	[SerializeField] private GameObject timerArrow;
	[SerializeField] private GameObject auraArrow;
	public Action ConductorPassed { get; set; }
	[SerializeField] private TMP_Text text;
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void StartConductor()
	{
		gameObject.SetActive(true);
		Touch.onFingerDown += One;
		text.text = "WELCOME TO new era plinko!";
	}

	private void One(Finger finger)
	{
		Touch.onFingerDown -= One;
		Touch.onFingerDown += Two;
		text.text = "control the ball as it moves in a circle!";
		runnerArrow.SetActive(true);
	}

	private void Two(Finger finger)
	{
		Touch.onFingerDown -= Two;
		Touch.onFingerDown += Three;
		text.text = "tap the screen to make it jump. Dodge oncoming obstacles to complete the level!";
		runnerArrow.SetActive(false);
	}

	private void Three(Finger finger)
	{
		Touch.onFingerDown -= Three;
		Touch.onFingerDown += Four;
		text.text = "if your ball reaches the blue aura or screen edge, then you will lose. hold out until time runs out!";
		timerArrow.SetActive(true);
		auraArrow.SetActive(true);
	}

	private void Four(Finger finger)
	{
		Touch.onFingerDown -= Four;
		Touch.onFingerDown += GameEnd;
		text.text = "Do you have enough reaction to complete the level? Let's check! Tap the screen to start";
		timerArrow.SetActive(false);
		auraArrow.SetActive(false);
	}

	private void GameEnd(Finger finger)
	{
		Touch.onFingerDown -= GameEnd;
		ConductorPassed?.Invoke();

		if (this != null)
		{
			gameObject.SetActive(false);
		}
	}
}

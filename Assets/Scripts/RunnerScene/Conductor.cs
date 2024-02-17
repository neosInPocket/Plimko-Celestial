using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Conductor : MonoBehaviour
{
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
		Touch.onFingerDown += Play;
		text.text = "WELCOME TO new era plinko!";
	}

	private void Play(Finger finger)
	{
		Touch.onFingerDown -= Play;
		Touch.onFingerDown += Play1;
		text.text = "control the ball as it moves in a circle!";
	}

	private void Play1(Finger finger)
	{
		Touch.onFingerDown -= Play1;
		Touch.onFingerDown += Play2;
		text.text = "tap the screen to make it jump. Dodge oncoming obstacles to complete the level!";
	}

	private void Play2(Finger finger)
	{
		Touch.onFingerDown -= Play2;
		Touch.onFingerDown += Play3;
		text.text = "if your ball reaches the blue aura or screen edge, then you will lose. hold out until time runs out!";
	}

	private void Play3(Finger finger)
	{
		Touch.onFingerDown -= Play3;
		Touch.onFingerDown += PlatEnd;
		text.text = "Do you have enough reaction to complete the level? Let's check! Tap the screen to start";
	}

	private void PlatEnd(Finger finger)
	{
		Touch.onFingerDown -= PlatEnd;
		ConductorPassed?.Invoke();

		gameObject.SetActive(false);
	}
}

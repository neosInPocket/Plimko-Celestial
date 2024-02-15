using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements.Experimental;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Runner : MonoBehaviour
{
	[SerializeField] private CircleCollider2D circleCollider;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float gravityMultiplier;
	[SerializeField] private float velocity;
	[SerializeField] private float jumpMultiplier;
	public float Radius => circleCollider.radius;
	private Vector2 currentGravity;
	private bool movingEnabled;
	private Vector2 currentVelocity;
	private Vector2 velocityAmplifier;
	private Vector2 currentGravityDirection;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		movingEnabled = true;
		EnableControls(true);
	}

	private void Update()
	{
		if (!movingEnabled) return;
		currentGravityDirection = new Vector2(transform.position.x / Mathf.Abs(transform.position.x), transform.position.y / Mathf.Abs(transform.position.y));

		if (velocityAmplifier.magnitude > 0)
		{
			velocityAmplifier -= currentGravityDirection * gravityMultiplier * Time.deltaTime;
		}
		else
		{
			velocityAmplifier = Vector3.zero;
		}

		Debug.Log(velocityAmplifier);
		currentVelocity = CalculateNormal() * velocity + velocityAmplifier;
		rb.velocity = currentVelocity;
	}

	public void EnableControls(bool value)
	{
		if (value)
		{
			Touch.onFingerDown += OnRunnerJump;
		}
		else
		{
			Touch.onFingerDown -= OnRunnerJump;
		}
	}

	private void OnRunnerJump(Finger finger)
	{
		if (this != null)
		{
			velocityAmplifier = (Vector2)transform.position.normalized * jumpMultiplier;
		}
	}

	public void EnableMoving(bool value)
	{
		movingEnabled = value;
	}

	private Vector2 CalculateNormal()
	{
		return new Vector2(transform.position.y, -transform.position.x).normalized;
	}

	private void OnTriggerEnter2D()
	{

	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnRunnerJump;
	}
}

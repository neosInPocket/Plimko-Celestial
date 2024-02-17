using System;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements.Experimental;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Runner : MonoBehaviour
{
	[SerializeField] private CircleCollider2D circleCollider;
	[SerializeField] private TrailRenderer trailRenderer;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float gravityMultiplier;
	[SerializeField] private float gravityAmplifierMultiplier;
	[SerializeField] private Vector2 velocities;
	[SerializeField] private float jumpMultiplier;
	[SerializeField] private GameObject deathObject;
	[SerializeField] private Vector2 sizes;
	public TrailRenderer Trail => trailRenderer;
	public float Radius => circleCollider.radius;
	private bool movingEnabled;
	private Vector2 currentGravity;
	private Vector2 normalSpeed;
	private Vector2 gravitySpeed;
	private Vector2 gravitySpeedAmplifier;
	private float startGravitySpeedAmplifierXDirection;
	public GameObject startCircle { get; set; }
	public Action OnDeath { get; set; }
	private Vector2 screenSize;
	private float velocity;

	private void Awake()
	{
		if (SerializeDataView.Data.SerializedData.skills[0])
		{
			spriteRenderer.size = new Vector2(sizes.y, sizes.y);
			circleCollider.radius = sizes.y / 2;
		}
		else
		{
			spriteRenderer.size = new Vector2(sizes.x, sizes.x);
			circleCollider.radius = sizes.x / 2;
		}

		if (SerializeDataView.Data.SerializedData.skills[1])
		{
			velocity = velocities.y;
		}
		else
		{
			velocity = velocities.x;
		}

		screenSize = ExtensionScript.ScreenSize;
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Update()
	{
		if (!movingEnabled) return;

		currentGravity = -transform.position.normalized * gravityMultiplier;
		gravitySpeed = currentGravity + gravitySpeedAmplifier;
		normalSpeed = CalculateNormal() * velocity;
		rb.velocity = normalSpeed + gravitySpeed;

		if (gravitySpeedAmplifier.magnitude > 0)
		{
			if ((startGravitySpeedAmplifierXDirection > 0 && gravitySpeedAmplifier.x < 0) || (startGravitySpeedAmplifierXDirection < 0 && gravitySpeedAmplifier.x > 0))
			{
				gravitySpeedAmplifier = Vector2.zero;
			}
			else
			{
				gravitySpeedAmplifier -= gravitySpeedAmplifier.normalized / gravityAmplifierMultiplier * Time.deltaTime;
			}
		}

		if (transform.position.x < -screenSize.x || transform.position.x > screenSize.x || transform.position.y < -screenSize.y || transform.position.y > screenSize.y)
		{
			DestroyRunner();
			movingEnabled = false;
		}
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
		gravitySpeedAmplifier = -currentGravity * jumpMultiplier;
		startGravitySpeedAmplifierXDirection = gravitySpeedAmplifier.x / Mathf.Abs(gravitySpeedAmplifier.x);
	}

	public void EnableMoving(bool value)
	{
		movingEnabled = value;
	}

	private Vector2 CalculateNormal()
	{
		var x = transform.position.y;
		var y = -transform.position.x;
		return new Vector2(x, y).normalized;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<DeathPoint>(out DeathPoint component))
		{
			DestroyRunner();
		}
	}

	private void DestroyRunner()
	{
		deathObject.SetActive(true);
		spriteRenderer.enabled = false;
		movingEnabled = false;
		OnDeath?.Invoke();
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnRunnerJump;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + (Vector3)CalculateNormal() * velocity);
	}
}

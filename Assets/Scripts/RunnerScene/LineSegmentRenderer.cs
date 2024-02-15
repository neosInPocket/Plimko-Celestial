using System.Collections.Generic;
using UnityEngine;

public class LineSegmentRenderer : MonoBehaviour
{
	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private EdgeCollider2D edgeCollider;
	[SerializeField] private Transform pivot;
	[SerializeField] private Vector2 randomRotationSpeedsPositive;
	[SerializeField] private float deltaThetaMultiplier;
	[SerializeField] private float scaleSpeed;
	private float rotationSpeed;
	private Vector3 currentRotation;
	private Vector3 currentScale = Vector3.one;
	private float deltaTheta;
	private bool isEnabled;
	public float CurrentWidth => lineRenderer.endWidth * pivot.localScale.x;

	public void Enable(bool value)
	{
		isEnabled = value;
	}

	public void Initialize()
	{
		deltaTheta = Mathf.PI / 360f * deltaThetaMultiplier;

		currentRotation = pivot.eulerAngles;
	}

	private void Update()
	{
		currentRotation.z += rotationSpeed * Time.deltaTime;
		//pivot.rotation = Quaternion.Euler(currentRotation);

		if (!isEnabled) return;

		currentScale.x -= scaleSpeed * Time.deltaTime;
		currentScale.y = currentScale.x;

		pivot.localScale = currentScale;

		if (pivot.localScale.x <= 0)
		{
			gameObject.SetActive(false);
		}

	}

	public void SetLinePosition(float radius, float startAngle, float endAngle)
	{
		float angle = endAngle - startAngle;
		int steps = (int)(angle / deltaTheta);
		float currentAngle = startAngle;
		lineRenderer.positionCount = steps;

		for (int i = 0; i < steps; i++)
		{
			Vector2 position = new Vector2(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle)) * radius;
			lineRenderer.SetPosition(i, position);

			currentAngle += deltaTheta;
		}

		SetRandomRotationSpeed();
		GenerateCollider();
	}

	private void SetRandomRotationSpeed()
	{
		if (Random.Range(0, 2) == 0)
		{
			rotationSpeed = Random.Range(randomRotationSpeedsPositive.x, randomRotationSpeedsPositive.y);
		}
		else
		{
			rotationSpeed = Random.Range(-randomRotationSpeedsPositive.x, -randomRotationSpeedsPositive.y);
		}
	}

	private void GenerateCollider()
	{
		List<Vector2> points = new List<Vector2>();

		for (int i = 0; i < lineRenderer.positionCount; i++)
		{
			points.Add(lineRenderer.GetPosition(i));
		}

		points.Add(lineRenderer.GetPosition(0));

		edgeCollider.SetPoints(points);
	}

	public Vector2 GetLinePointNormal()
	{
		var point = lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0);
		return new Vector2(point.y, -point.x).normalized;
	}

	public Vector2 GetLinePointPosition()
	{
		return lineRenderer.GetPosition(0);
	}
}

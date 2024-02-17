using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SegmentSpawner : MonoBehaviour
{
	[SerializeField] private LineSegmentRenderer linePrefab;
	[SerializeField] private LineSegmentRenderer startCircle;
	[SerializeField] private LineSegmentRenderer secondStartCircle;
	[SerializeField] private List<LineSegmentRenderer> pool;
	[SerializeField] private float startRadius;
	[SerializeField] private Runner runner;
	[SerializeField] private Vector2 drawRanges;
	[SerializeField] private Vector2 randomRotationSpeeds;
	[SerializeField] private float segmentDistances;
	[SerializeField] private Vector2Int countSizes;
	[SerializeField] private float startSpawnRadius;
	[SerializeField] private float spawnRadius;
	[SerializeField] private float spawnRadiusThreshold;
	private List<LineSegmentRenderer> currentPool;
	private LineSegmentRenderer lastSegment;
	private bool isenabled;

	private void Awake()
	{
		currentPool = pool;
		InitializeAll();
		startCircle.Initialize();
		runner.startCircle = startCircle.gameObject;
		startCircle.SetLinePosition(startRadius, 0, 2 * (float)Math.PI);
		startCircle.GenerateColliderLooped();
		lastSegment = startCircle;

		runner.transform.position = startCircle.GetLinePointPosition() + startCircle.GetLinePointNormal() * (startCircle.CurrentWidth / 2 + runner.Radius);
		runner.Trail.Clear();
	}

	public void Update()
	{
		if (!isenabled) return;

		Debug.Log(lastSegment.CurrentRadius);
		if (lastSegment.CurrentRadius > spawnRadiusThreshold) return;
		DrawWholeSegment(lastSegment.CurrentRadius + spawnRadius);
	}

	public void Enable(bool value)
	{
		if (value)
		{
			EnableAllSegments(true);
			isenabled = true;
		}
		else
		{
			isenabled = false;
			EnableAllSegments(false);
		}
	}

	private LineSegmentRenderer PullSegmentRenderer()
	{
		var freeSegment = currentPool.FirstOrDefault(x => !x.gameObject.activeSelf);

		if (freeSegment != null)
		{
			freeSegment.gameObject.SetActive(true);
			return freeSegment;
		}
		else
		{
			var segment = Instantiate(linePrefab, Vector2.zero, Quaternion.identity, transform);
			segment.Initialize();
			segment.Enable(true);
			currentPool.Add(segment);

			return segment;
		}
	}

	public void EnableAllSegments(bool value)
	{
		currentPool.ForEach(x => x.Enable(value));
		startCircle.Enable(value);
	}

	public void InitializeAll()
	{
		currentPool.ForEach(x => x.Initialize());
	}

	public List<Vector2> SplitRange(Vector2 degSizeRanges, float degDistance, Vector2Int minMaxCount)
	{
		float distance = Mathf.Deg2Rad * degDistance;
		Vector2 sizeRanges = degSizeRanges * Mathf.Deg2Rad;
		List<Vector2> ranges = new List<Vector2>();
		int count = Random.Range(minMaxCount.x, minMaxCount.y);
		float lastAngle = 0;

		for (int i = 0; i < count; i++)
		{
			Vector2 newRange = new Vector2(lastAngle + distance, lastAngle + distance + Random.Range(sizeRanges.x, sizeRanges.y));
			lastAngle = newRange.y;
			ranges.Add(newRange);
		}

		return ranges;
	}

	private void DrawWholeSegment(float scale)
	{
		var ranges = SplitRange(drawRanges, segmentDistances, countSizes);
		float randomRotationSpeed;

		if (Random.Range(0, 2) == 1)
		{
			randomRotationSpeed = Random.Range(randomRotationSpeeds.x, randomRotationSpeeds.y);
		}
		else
		{
			randomRotationSpeed = Random.Range(-randomRotationSpeeds.x, -randomRotationSpeeds.y);
		}

		for (int i = 0; i < ranges.Count; i++)
		{
			var renderer = PullSegmentRenderer();
			renderer.SetLinePosition(startRadius, ranges[i].x, ranges[i].y);
			renderer.SetRotationSpeed(randomRotationSpeed);
			renderer.GenerateCollider();

			lastSegment = renderer;
			renderer.SetScale(scale);
		}
	}
}

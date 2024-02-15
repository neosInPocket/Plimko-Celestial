using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SegmentSpawner : MonoBehaviour
{
	[SerializeField] private LineSegmentRenderer linePrefab;
	[SerializeField] private List<LineSegmentRenderer> pool;
	[SerializeField] private float startRadius;
	[SerializeField] private Runner runner;
	private List<LineSegmentRenderer> currentPool;

	private void Start()
	{
		currentPool = pool;

		InitializeAll();

		var segment = PullSegmentRenderer();
		segment.SetLinePosition(startRadius, 0, 2 * (float)Math.PI);

		runner.transform.position = segment.GetLinePointPosition() + segment.GetLinePointNormal() * (segment.CurrentWidth / 2 + runner.Radius);
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
			currentPool.Add(segment);

			return segment;
		}
	}

	public void EnableAllSegments(bool value)
	{
		currentPool.ForEach(x => x.Enable(value));
	}

	public void InitializeAll()
	{
		currentPool.ForEach(x => x.Initialize());
	}
}

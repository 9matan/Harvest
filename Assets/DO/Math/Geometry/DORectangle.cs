using UnityEngine;
using System.Collections;

public class DORectangle {

	public Vector3[] points = new Vector3[4];

#if UNITY_EDITOR

	public void DrawGizmos()
	{
		DrawGizmos (points);
	}

	public static void DrawGizmos(Vector3[] points)
	{
		for (int i = 0; i < points.Length - 1; ++i)
			Gizmos.DrawLine (points [i], points [i + 1]);

		Gizmos.DrawLine (points [points.Length - 1], points [0]);
	}

#endif

}

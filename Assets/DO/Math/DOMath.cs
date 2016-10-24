using UnityEngine;
using System.Collections;

public class DOMath {

	public const float EPS = 1e-11f;

	[System.Serializable]
	public class Quadratic : System.Object {

		public float a;
		public float b;
		public float c;

		public Quadratic(float __a, float __b, float __c)
		{
			a = __a; b = __b; c = __c;
		}

		public float F(float x)
		{
			return x * x * a + x * b + c;
		}
	}

	static public int Max (int[,] arr)
	{
		int f = arr.GetLength(0),
			s = arr.GetLength(1);

		if(f == 0 || s == 0) throw new System.Exception("Empty array[,]!");

		int maxx = arr [0, 0];

		for(int i = 0; i < f; ++i)
			for(int j = 0; j < s; ++j)
				if(maxx < arr[i, j])
					maxx = arr[i, j];

		return maxx;
	}

	static public Vector2 AdaptVectorToPower( Vector2 v, float power )
	{
		float k = 0.0f;
		if(v.magnitude != 0.0f)
			k = power / v.magnitude;
		return new Vector2 (v.x * k, v.y * k);
	}

	static public Vector3 AdaptVectorToPower( Vector3 v, float power )
	{
		float k = 0.0f;
		if(v.magnitude != 0.0f)
			k = power / v.magnitude;
		return new Vector3 (v.x * k, v.y * k);
	}

	static public Vector2 RotateVector2(Vector2 v, float angle)
	{
		float rad = angle * Mathf.Deg2Rad;
		float sin = Mathf.Sin (rad), cos = Mathf.Cos (rad);
		float x = v.x, y = v.y;
		
		v.x = x * cos - y * sin;
		v.y = x * sin + y * cos;
		return v;
	}

}


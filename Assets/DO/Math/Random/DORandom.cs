using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DORandom {

	static public T AbsProb<T> (Dictionary<T, float> prob)
	{
		float sum = 0.0f;

		foreach(var item in prob)
			sum += item.Value;

		Dictionary<T, float> nprob = new Dictionary<T, float>();

		foreach(var item in prob)
			nprob.Add(item.Key, item.Value / sum);

		return RelatProb (nprob);
	}

	static public T RelatProb<T> (Dictionary<T, float> prob)
	{
		float [] nprob = new float[prob.Count];
		T [] number = new T[prob.Count];
		int iter = 0;

		foreach(var item in prob)
		{
			nprob[iter] = item.Value;
			if(iter != 0)
				nprob[iter] += nprob[iter - 1];
			number[iter++] = item.Key;
		}

		float R = Random.Range (0.0f, 1.0f);

		for(int i = 0; i < nprob.Length; ++i)
		{
			if(nprob[i] >= R) 
				return number[i];
		}

		return number [0];
	}

	static public T Rand<T>(T[] arr)
	{
		int len = arr.Length;
		int r = Random.Range (0, len);
		return arr [r];
	}

	static public Vector3 GetRandomVector3(Vector3 min, Vector3 max)
	{
		return new Vector3(
			Random.Range(min.x, max.x),
			Random.Range(min.y, max.y),
			Random.Range(min.z, max.z));
	}

	static public Vector3 GetRandomVector3(float min = 0.0f, float max = 1.0f)
	{
		return new Vector3 (
			Random.Range (min, max),
			Random.Range (min, max),
			Random.Range (min, max)
		);
	}

	static public Vector3 GetRandomVector2(float min = 0.0f, float max = 1.0f)
	{
		return new Vector3 (
			Random.Range (min, max),
			Random.Range (min, max)
			);
	}

	static public List<T> RandomShuffle<T>(List<T> rs)
	{
		int n = rs.Count;
		if(n == 0) return rs;

		int l = 0, r = 0;

		for(int i = 0; i < n ; ++i)
		{
			l = Random.Range(0, n);
			r = Random.Range(0, n);

			var t = rs[l];
				rs[l] = rs[r];
				rs[r] = t;
		}

		return rs;
	}

	static public bool Chance(float c)
	{
		return (c >= Random.Range (0.0f, 1.0f));
	}

}















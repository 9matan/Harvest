using UnityEngine;
using System.Collections;

public class DODestroyAtAwakeNotEditor : MonoBehaviour {

#if !UNITY_EDITOR

	protected virtual void Awake()
	{
		Destroy (this.gameObject);
	}

#endif

}

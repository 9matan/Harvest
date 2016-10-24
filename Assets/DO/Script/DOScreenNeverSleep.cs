using UnityEngine;
using System.Collections;

public class DOScreenNeverSleep : MonoBehaviour {

	void Awake()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

}

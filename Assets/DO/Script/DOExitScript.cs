using UnityEngine;
using System.Collections;

public class DOExitScript : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
}

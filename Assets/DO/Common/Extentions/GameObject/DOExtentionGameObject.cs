using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class DOExtentionGameObject {

	public static GameObject CreateChild (this GameObject go, string name)
	{
		var imggo = new GameObject (name);
		imggo.transform.parent = go.transform;
		imggo.transform.localPosition = Vector3.zero;
		imggo.transform.localRotation = Quaternion.identity;
		return imggo;
	}

	public static T GetAdd<T>(this GameObject go) where T: Component
	{
		var comp = go.GetComponent<T> ();
		if (comp == null)
			comp = go.AddComponent<T> ();

		return comp;
	}

	public static void Show (this GameObject go)
	{
		go.SetActive (true);
	}

	public static void Hide (this GameObject go)
	{
		go.SetActive (false);
	}

}

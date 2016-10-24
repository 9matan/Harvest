using UnityEngine;
using System.Collections;

public static class DOExtentionComponent 
{

	public static void Show (this Component component)
	{
		component.gameObject.SetActive (true);
	}

	public static void Hide (this Component component)
	{
		component.gameObject.SetActive (false);
	}

}

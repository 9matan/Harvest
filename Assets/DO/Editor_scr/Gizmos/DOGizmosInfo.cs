using UnityEngine;
using System.Collections;

[System.Serializable]
public class DOGizmosInfo {

#if UNITY_EDITOR

	public Color color = Color.black;

	protected Color _temp;

	public void BeginDrowing()
	{
		this.SetColor ();
	}

	public void EndDrawing()
	{
		this.ResetColor ();
	}

	public void SetColor()
	{
		_temp = Gizmos.color;
		Gizmos.color = color;
	}

	public void ResetColor()
	{
		Gizmos.color = _temp;
	}

#endif

}


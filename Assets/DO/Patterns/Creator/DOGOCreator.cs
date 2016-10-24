using UnityEngine;
using System.Collections;

public interface IDOCreator<T>
{
	T Create();
}

public interface IDONETCreator<T> : IDOCreator<T>
{
	T CreateNet();
}

public class DOGOCreator : MonoBehaviour, IDOCreator<GameObject>, IDOBuilder {
	
	public Vector3 		instPos = Vector3.zero;
	public Vector3 		instRot = Vector3.zero;
	public GameObject 	prefab = null;

	public GameObject Create()
	{
		return (Instantiate (prefab, instPos, Quaternion.Euler(instRot)) as GameObject);
	}

#if UNITY_EDITOR

	//
	// < Builder >
	//	
	
	[ContextMenu("Build")]
	virtual public void Build()
	{
		Debug.Log ("Set prefab to creator!");
	}
	
	//
	// < /Builder >
	//
#else
	
	virtual public void Build() {}

#endif

}

using UnityEngine;
using System.Collections;

public interface IDOBuilder
{
	void Build();
}

public static class DOBuilder
{

	public static T Build<T>(this GameObject parent, string name)
		where T: Component, IDOBuilder 
	{
		GameObject go = null;

		if(parent.transform.FindChild(name) != null)
			go = parent.transform.FindChild(name).gameObject;
		else
			go = new GameObject(name);

		go.transform.parent = parent.transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		
		var c = go.AddComponent<T> ();
		c.Build ();
		return c;
	}

	public static T Build<T>(this GameObject parent, T inst, string name)
		where T: Component, IDOBuilder 
	{
		if (inst != null)
		{
			inst.transform.parent = parent.transform;
			inst.transform.localPosition = Vector3.zero;
			inst.transform.localRotation = Quaternion.identity;

			inst.Build ();			
		}
		else
			inst = Build<T> (parent, name);

		return inst;
	}


	public static T BuildAtObject<T>(this GameObject go)
		where T: Component, IDOBuilder 
	{		
		var c = go.GetAdd<T> ();
		c.Build ();
		return c;
	}

	public static T BuildAtObject<T>(this GameObject go, T inst)
		where T: Component, IDOBuilder 
	{
		if (inst != null)
			inst.Build ();
		else
			inst = BuildAtObject<T> (go);
		
		return inst;
	}

}

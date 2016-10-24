using UnityEngine;
using System.Collections;

public enum EDOLocation
{
	WORLD,
	LOCAL
}

public interface IDOLocationable
{
	void Rotate(Transform tr, Vector3 rot);
	void Rotate(Vector3 rot);

	void SetRotation(Transform tr, Vector3 rot);
	void SetRotation(Transform tr, Quaternion rot);
	Quaternion GetRotation(Transform tr);

	void SetPosition(Transform tr, Vector3 pos);
	Vector3 GetPosition(Transform tr);

	void SetRotation(Vector3 rot);
	void SetRotation(Quaternion rot);
	Quaternion GetRotation();

	void SetPosition(Vector3 pos);
	Vector3 GetPosition();

	Vector3 ToWorldPoint (Transform tr, Vector3 pos);
	Vector3 ToWorldPoint (Vector3 pos);
	Vector3 ToLocalPoint (Transform tr, Vector3 pos);
	Vector3 ToLocalPoint (Vector3 pos);
}

public class DOLocationable : MonoBehaviour, IDOLocationable {

	public EDOLocation location {
		get { return _location; }
		set { _location = value; }
	}

	[SerializeField] protected EDOLocation _location;

	//
	// < this >
	//

	virtual public void Rotate(Vector3 rot)
	{
		this.Rotate (this.transform, rot);
	}

	virtual public void SetRotation(Vector3 rot)
	{
		this.SetRotation (this.transform, rot);
	}

	virtual public void SetRotation(Quaternion rot)
	{
		this.SetRotation (this.transform, rot);
	}

	virtual public Quaternion GetRotation()
	{
		return this.GetRotation (this.transform);
	}


	virtual public void SetPosition(Vector3 pos)
	{
		this.SetPosition (this.transform, pos);
	}

	virtual public Vector3 GetPosition()
	{
		return this.GetPosition (this.transform);
	}

	//
	// </ this >
	//

	virtual public void Rotate(Transform tr, Vector3 rot)
	{
		switch(_location)
		{
		case EDOLocation.LOCAL:
			tr.Rotate(rot, Space.Self);
			break;
		case EDOLocation.WORLD:
			tr.Rotate(rot, Space.World);
			break;
		}
	}

	virtual public void SetRotation(Transform tr, Vector3 rot)
	{
		this.SetRotation (tr, Quaternion.Euler (rot));
	}

	virtual public void SetRotation(Transform tr, Quaternion rot)
	{
		switch(_location)
		{
		case EDOLocation.LOCAL:
			tr.localRotation = rot;
			break;
		case EDOLocation.WORLD:
			tr.rotation = rot;
			break;
		}
	}

	virtual public Quaternion GetRotation(Transform tr)
	{
		Quaternion rot = Quaternion.identity;

		switch(_location)
		{
		case EDOLocation.LOCAL:
			rot = tr.localRotation;
			break;
		case EDOLocation.WORLD:
			rot = tr.rotation;
			break;
		}

		return rot;
	}



	virtual public void SetPosition(Transform tr, Vector3 pos)
	{
		switch(_location)
		{
		case EDOLocation.LOCAL:
			tr.localPosition = pos;
			break;
		case EDOLocation.WORLD:
			tr.position = pos;
			break;
		}
	}

	virtual public Vector3 GetPosition(Transform tr)
	{
		Vector3 pos = Vector3.zero;
		
		switch(_location)
		{
		case EDOLocation.LOCAL:
			pos = tr.localPosition;
			break;
		case EDOLocation.WORLD:
			pos = tr.position;
			break;
		}
		
		return pos;
	}

	//
	// < Get point >
	//

	virtual public Vector3 ToWorldPoint (Transform tr, Vector3 pos)
	{
		if(_location == EDOLocation.WORLD) return pos;
		
		return tr.TransformPoint (pos);
	}
	
	virtual public Vector3 ToWorldPoint (Vector3 pos)
	{
		return this.ToWorldPoint (this.transform, pos);
	}

	virtual public Vector3 ToLocalPoint (Transform tr, Vector3 pos)
	{
		if(_location == EDOLocation.LOCAL) return pos;
		
		return tr.InverseTransformPoint (pos);
	}
	
	virtual public Vector3 ToLocalPoint (Vector3 pos)
	{
		return this.ToLocalPoint (this.transform, pos);
	}

/*	virtual public Vector3 GetFromLocalPoint (Transform tr, Vector3 pos)
	{
		if(_location == EDOLocation.LOCAL) return pos;

		return tr.TransformPoint (pos);
	}

	virtual public Vector3 GetFromLocalPoint (Vector3 pos)
	{
		return this.GetFromLocalPoint (this.transform, pos);
	}

	virtual public Vector3 GetFromWorldPoint (Transform tr, Vector3 pos)
	{
		if(_location == EDOLocation.WORLD) return pos;
		
		return tr.InverseTransformPoint (pos);
	}
	
	virtual public Vector3 GetFromWorldPoint (Vector3 pos)
	{
		return this.GetFromWorldPoint (this.transform, pos);
	}
*/
}





























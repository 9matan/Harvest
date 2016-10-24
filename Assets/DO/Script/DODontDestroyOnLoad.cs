using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DODontDestroyOnLoad : MonoBehaviour {

	static HashSet<string> _instances = new HashSet<string>();

	public bool isChecked { get; protected set; }

	[SerializeField] protected bool		_useHash = true;
	[SerializeField] protected string 	_id = "";

	protected virtual void Awake()
	{
		if (_useHash)
		{
			if((IsExist (_id) || _id == ""))
				Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);
		this._AddHash ();
		isChecked = true;
	}

	protected virtual void OnDestroy()
	{
		if(_useHash && _id != "")
			this._RemoveHash ();
	}

	static public bool IsExist(string __id)
	{
		return _instances.Contains (__id);
	}

	virtual protected void _AddHash()
	{
		_instances.Add (_id);
	}

	virtual protected void _RemoveHash()
	{
		_instances.Remove (_id);
	}

}

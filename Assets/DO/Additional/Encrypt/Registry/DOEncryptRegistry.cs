using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DOEncryptRegistry : MonoBehaviour, IDOBuilder {

	public bool isInit { get; protected set; }

	public bool validateAtLoad = false;
	public bool loadAtInitialize = false;

	public string regName {
		get { return _regName; }
		set { _regName = value; }
	}

	[SerializeField] protected string 	_regName;

	List<DOEncryptor> 	_encryptors = new List<DOEncryptor>();
	List<string>		_eData = new List<string>();

	protected string _data;

	//
	// < Initialize >
	//

	virtual public void Initialize()
	{
		this._InitializeEncryptors ();

		if (loadAtInitialize)
			this.Load ();

		isInit = true;
	}

	virtual protected void _InitializeEncryptors()
	{
		var list = this.GetEncryptors (); _eData.Clear();

		for (int i = 0; i < list.Count; ++i)
		{
			_encryptors.Add (list [i].Create ());
			_eData.Add("");
		}
	}

	//
	// </ Initialize >
	//

	virtual public void Set(int __data)
	{
		_data = __data.ToString ();
	}

	virtual public void Set(string __data)
	{
		_data = __data;
	}

	virtual public int GetInt()
	{
		if (_data == "")
			return 0;

		return System.Convert.ToInt32 (_data);
	}

	virtual public string GetString()
	{
		return _data;
	}

	//
	// < save/ load >
	//

	virtual public void Load()
	{
		if(!this.Contains()) 
		{
			this._Reset();
			return;
		}

		if (_encryptors.Count == 0)
			this._SimpleLoad ();
		else
		{
			this._EncryptorsLoad ();
			if (validateAtLoad)
				this.Validate ();
		}
	}

	virtual protected void _SimpleLoad()
	{
		_data = PlayerPrefs.GetString (_regName);
	}

	virtual protected void _EncryptorsLoad()
	{
		try
		{
			for(int i = 0; i < _encryptors.Count; ++i)
			{
				_eData[i] = PlayerPrefs.GetString(this._EncryptName(i));
				_eData[i] = this._Dencrypt(i, _eData[i]);
			}

			_data = _eData [0];
		}
		catch(System.Exception e)
		{
			this._Reset();
			Debug.LogError(e.Message);
		}
	}

	// < save >

	virtual public void Save()
	{
		PlayerPrefs.SetString (_regName, "contains");

		if (_encryptors.Count == 0)
			this._SimpleSave ();
		else
			this._EncryptorsSave ();
	}

	virtual protected void _SimpleSave()
	{
		PlayerPrefs.SetString (_regName, _data);
	}

	virtual protected void _EncryptorsSave()
	{
		for(int i = 0; i < _encryptors.Count; ++i)
		{
			var str = this._Encrypt(i, _data);
			PlayerPrefs.SetString (
				this._EncryptName(i), str);
		}
	}

	//
	// </ save/ load >
	//

	virtual public bool Contains()
	{
		return (PlayerPrefs.HasKey (_regName));
	}

	virtual public bool Validate()
	{
		bool isValid = this.IsValid ();

		if (!isValid)
			this._Reset ();

		return !isValid;
	}

	virtual public bool IsValid()
	{
		bool isValid = true;

		for (int i = 0; i < _eData.Count && isValid; ++i)
			if (_eData [i] != _data)
				isValid = false;

		return isValid;
	}

	virtual protected void _Reset()
	{
		_data = "0";
	}

	//
	// < encrypt >
	//

	virtual public List<DOMonoEncryptor> GetEncryptors()
	{
		var list = new List<DOMonoEncryptor> ();
		int cnum = this.transform.childCount;

		for(int i = 0; i < cnum; ++i)
		{
			var e = this.transform.GetChild(i).GetComponent<DOMonoEncryptor>();

			if(e != null)
				list.Add(e);
		}
	
		return list;
	}

	virtual protected string _Encrypt(int number, string val)
	{
		return _encryptors [number].EncryptString (val);
	}

	virtual protected string _Dencrypt(int number, string val)
	{
		return _encryptors [number].DecryptString (val);
	}

	virtual protected string _EncryptName(int number)
	{
		return (_regName + "_" + number);
	}

	//
	// </ encrypt >
	//

	//
	// < Clear >
	//

	virtual public void Clear()
	{
		_encryptors.Clear ();
		_eData.Clear ();
	}

#if UNITY_EDITOR

	[ContextMenu("Add encryptor")]
	virtual public void AddEncryptor()
	{
		var go = new GameObject (_regName + "_" + this.transform.childCount.ToString ());
		go.transform.parent = this.transform;

		var e = go.AddComponent<DOMonoEncryptor> ();
		e.GenerateKey ();
	}

	[ContextMenu("Save encryptors")]
	virtual public void SaveEncryptors()
	{
		var list = this.GetEncryptors ();

		for(int i = 0; i < list.Count; ++i)
			list[i].SaveKey(_regName + "_" + i.ToString() + ".txt");
	}

	//
	// < Build >
	//

	[ContextMenu("Build")]
	virtual public void Build()
	{
	}

	//
	// </ Build >
	//

#else

	public virtual void Build() {}

#endif

}






















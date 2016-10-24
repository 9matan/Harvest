using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using System.IO;
#endif

public class DOMonoEncryptor : MonoBehaviour {

	public byte[] key {
		get { return _key; }
		set { _key = value; }
	}

	public byte[] iv {
		get { return _iv; }
		set { _iv = value; }
	}

	[SerializeField] byte[] _key;
	[SerializeField] byte[] _iv;

	virtual public DOEncryptor Create()
	{
		return new DOEncryptor (_key, _iv);
	}

#if UNITY_EDITOR

	public string fileName {
		get { return _fileName; }
		set { _fileName = value; }
	}

	[SerializeField] protected string _fileName;

	virtual public void SaveKey(string file)
	{
		string t = _fileName;
		_fileName = file;

		this.SaveKey ();

		_fileName = t;
	}

	[ContextMenu("Save key")]
	virtual public void SaveKey()
	{
		StreamWriter w = new StreamWriter (_fileName);
		this._WriteByteArr (w, _key);
		w.WriteLine ();
		this._WriteByteArr (w, _iv);
		w.Close ();
	}

	[ContextMenu("Fast save")]
	public void FastSave()
	{
		_fileName = Directory.GetCurrentDirectory() + "\\registries";//\\" + this.gameObject.name;
		Directory.CreateDirectory(_fileName);
		_fileName += "\\" + this.gameObject.name + ".txt";

		this.SaveKey();
	}

	virtual protected void _WriteByteArr(StreamWriter w, byte[] bytes)
	{
		for(int i = 0; i < bytes.Length; ++i)
		{
			w.Write((int)bytes[i]);
			w.Write(" ");
		}
	}

	[ContextMenu("Load key")]
	virtual public void LoadKey()
	{
		StreamReader r = new StreamReader (_fileName);
		_key = this._LineToBytes (r.ReadLine ());
		_iv = this._LineToBytes (r.ReadLine ());
		r.Close ();
	}
	
	virtual protected byte[] _LineToBytes(string line)
	{
		var str = line.Split(' ');

		List<byte> bytes = new List<byte> ();

		for(int i = 0; i < str.Length; ++i)
		{
			if(str[i] == "") continue;
		
			var ib = System.Convert.ToInt32(str[i]);
			bytes.Add( System.Convert.ToByte(ib) );
		}

		return bytes.ToArray();
	}

	[ContextMenu("Generate key")]
	virtual public void GenerateKey()
	{
		var e = new DOEncryptor();

		_key = e.Key;
		_iv = e.IV;
	}

#endif

}

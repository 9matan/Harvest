using UnityEngine;
using System.Collections;

public class DOEncryptRegistryArray : DOEncryptRegistry
{

	protected System.UInt16[] _array;

	public System.UInt16 GetArrayElement(int indx)
	{
		if (_array[indx] == System.Int16.MaxValue)
			return 0;
		return _array[indx];
	}

	public void SetArrayElement(int indx, System.UInt16 value)
	{
		_array[indx] = value;
	}

	public override void Load()
	{
		base.Load();

		_array = new System.UInt16[_data.Length];
		for (int i = 0; i < _data.Length; ++i)
		{
			_array[i] = (System.UInt16)_data[i];
			if (_array[i] == System.UInt16.MaxValue)
				_array[i] = 0;

//			Debug.Log(i + " " + _array[i]);
        }
	}

	public override void Save()
	{
		System.UInt16 num = 0;
		_data = "";

		for (int i = 0; i < _array.Length; ++i)
		{
			num = _array[i];
            if (_array[i] == 0)
				num = System.UInt16.MaxValue;

			_data += (System.Char)num;
		}

		base.Save();
	}

	public void ResizeArrayTo(int size)
	{
		if (size == _array.Length) return;

		System.UInt16[] newArray = new System.UInt16[size];

		if (size < _array.Length)
		{
			for (int i = 0; i < size; ++i)
				newArray[i] = _array[i];
        }
		else
		{
			for (int i = 0; i < _array.Length; ++i)
				newArray[i] = _array[i];

			for (int i = _array.Length; i < size; ++i)
				newArray[i] = 0;
        }

		_array = newArray;
	}

	public void ResetArray(System.UInt16 val = 0)
	{
		for (int i = 0; i < _array.Length; ++i)
			_array[i] = val;
    }

}

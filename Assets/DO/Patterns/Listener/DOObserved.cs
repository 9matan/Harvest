using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IDOListener
{
//	void OnInvokeListener();
}

public class DOObserved<Listener>
	where Listener : class, IDOListener
{
	public HashSet<Listener> list {
		get { return _list; }
	}

	protected HashSet<Listener> _list = null;

	public DOObserved()
	{
		_list = new HashSet<Listener> ();
	}


	virtual public bool Contains(Listener l)
	{
		return _list.Contains (l); 
	}

	virtual public void Add(Listener l, bool once = true)
	{
		if(once && this.Contains(l)) return;

		_list.Add (l);
	}

	virtual public void Remove(Listener l)
	{
		if(!this.Contains(l)) return;
		
		_list.Remove (l);
	}


	virtual public void Clear()
	{
		_list.Clear ();
	}


	virtual public void RemoveNullListener()
	{
		_list.RemoveWhere (_IsNull);
	}

	static protected bool _IsNull(Listener l)
	{
		return (l == null);
	}



}

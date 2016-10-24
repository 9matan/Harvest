using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IDOMemFactory<T> 
	where T: class
{

	void 	CreateNewObjects(int number);
	T 		Allocate();
	void 	Free (T inst);
	bool 	IsFree ();

}

public interface IDOMemFactoryListener<T> : IDOListener
	where T: UnityEngine.Object
{
	void OnInstanceAllocated (DOMemFactory<T> factory, T inst);
}

public class DOMemFactoryObserved<T> : DOObserved<IDOMemFactoryListener<T>>, IDOMemFactoryListener<T>
	where T: UnityEngine.Object
{
	public void OnInstanceAllocated (DOMemFactory<T> factory, T inst)
	{
		this.RemoveNullListener ();

		foreach (var l in _list)
			l.OnInstanceAllocated (factory, inst);
	}
}

public abstract class DOMemFactory<T> : MonoBehaviour, IDOMemFactory<T>
	where T: UnityEngine.Object
{

	public bool initAtAwake = false;
	public bool autoAllocateNewObject = true;

	public DOMemFactoryObserved<T>	listeners { get { return _listeners; } }
	public IDOCreator<T> 			icreator { get; private set;}
	public bool						isInit { get; protected set;}

	[SerializeField] protected int _allocateStartNumber;

	protected DOMemFactoryObserved<T> _listeners = new DOMemFactoryObserved<T> ();

	protected int 		_createdObject = 0;
	protected List<T> 	_free;

	// -------------------------

	virtual protected void Awake()
	{
		if (initAtAwake)
			this.Initialize ();
	}

	virtual protected void OnDestroy()
	{
		this._DestroyAllFree ();
	}

	// -------------------------

	public abstract IDOCreator<T> GetCreator (GameObject go);

	//
	// < Events > 
	//

	virtual protected void OnInstanceFree (T inst) {}
	virtual protected void OnInstanceAllocated (T inst) 
	{
		_listeners.OnInstanceAllocated (this, inst);
	}
	virtual protected void OnInstanceCreated (T inst) {}

	//
	// </ Events > 
	//

	//
	// < Initialize >
	//


	virtual public void Initialize(bool init = false)
	{
		if(init || !isInit)
			this.Initialize (_allocateStartNumber);
	}

	virtual public void Initialize(int allocate)
	{
		_InitializeFree ();
		_InitializeCreator ();
		_AllocateStartNumber (allocate);

		isInit = true;
	}

	protected void _InitializeFree()
	{
		_free = new List<T>(_allocateStartNumber);
	}

	protected void _InitializeCreator()
	{
		icreator = this.GetCreator (this.gameObject); 
	}

	protected void _AllocateStartNumber(int allocate)
	{
		this.CreateNewObjects (allocate);
	}

	//
	// </ Initialize >
	//

	virtual public void SetAllocatedStartNumber(int __number)
	{
		_allocateStartNumber = __number;
	}

	// create new go
	virtual protected T _CreateNewInstance()
	{
		++_createdObject;

		var inst = icreator.Create (); 
		OnInstanceCreated (inst);
		return inst;
	}
	
	// free instance
	virtual protected void _FreeInstance(T inst)
	{
		_free.Add (inst);
		this.OnInstanceFree (inst);
	}

	// check for free and create if needed
	virtual protected bool _CheckForFree()
	{
		if(!this.IsFree()) 
		{
			if(autoAllocateNewObject)
				this.CreateNewObjects(1);
			else
				return false;
		}

		return true;
	}

	// return free object
	virtual protected T _GetFree()
	{
		int curIndex = _free.Count - 1;
		var inst = _free [curIndex];
		_free.RemoveAt (curIndex);

		return inst;
	}

	// destroy

	virtual protected void _DestroyAllFree()
	{
		if(_free == null) return;

		for(int i = 0; i < _free.Count; ++i)
		{
			_DestroyObject(_free[i]);
		}

		_free.Clear ();
	}

	virtual protected void _DestroyObject(T obj)
	{
		Destroy (obj);
	}

	//
	// public
	//
	
	// create new n go
	virtual public void CreateNewObjects(int number)
	{
		for (int i = 0; i < number; ++i)
			this._FreeInstance (this._CreateNewInstance ());
	}
	
	// has factory free object
	virtual public bool IsFree()
	{
		return (_free.Count != 0);
	}
	
	// allocate free or create new object
	virtual public T Allocate()
	{
		if(!this._CheckForFree()) return null;

		var inst = this._GetFree ();
	
		this.OnInstanceAllocated (inst);
		return inst;
	}
	
	// free object
	virtual public void Free(T inst)
	{
		_FreeInstance (inst);
	}

	//
	// < Clear >
	//

	public virtual void Clear()
	{
		_listeners.Clear ();
	}

	//
	// </ Clear >
	//








}

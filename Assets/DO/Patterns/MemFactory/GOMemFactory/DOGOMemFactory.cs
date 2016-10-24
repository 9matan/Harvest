using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IDOGOMemFactoryListener : IDOMemFactoryListener<GameObject>
{}

public class DOGOMemFactory : DOMemFactory<GameObject>, IDOBuilder {

	public enum ERigidBody
	{
		NONE,
		RB2D,
		RB3D
	}

	public DOGOCreator creator {
		get { return _creator; }
	}

	[Header("On free")]
	[SerializeField] protected bool 			_hideOnFree = true;
	[SerializeField] protected bool				_moveOnFree = false;
	[Header("Allocate")]
	[SerializeField] protected bool 			_instPosAtAllocate = true;
	[SerializeField] protected ERigidBody		_rigidbody = ERigidBody.NONE;
	[SerializeField] protected DOGOCreator 		_creator = null;
	[Header("Message")]
	[SerializeField] protected bool				_onActivatedMessage = false;
	[SerializeField] protected bool				_onDeactivatedMessage = false;

	protected List<GameObject> _toRelease = new List<GameObject> ();

	public override IDOCreator<GameObject> GetCreator (GameObject go)
	{
		return _creator;
	}

	protected override void OnInstanceFree (GameObject inst)
	{
		base.OnInstanceFree (inst);

		inst.transform.parent = this.transform;
		if(_moveOnFree)
		{
			if(_rigidbody == ERigidBody.RB2D)
				inst.GetComponent<Rigidbody2D>().position = this.transform.position;
			else if (_rigidbody == ERigidBody.RB3D)
				inst.GetComponent<Rigidbody>().position = this.transform.position;
			else
				inst.transform.localPosition = Vector3.zero;
		}

	//	Debug.Log ("Pos: " + inst.transform.position + " " + inst.name);

		if (_hideOnFree)
			inst.SetActive (false);

		if (_onDeactivatedMessage)
			inst.SendMessage ("OnDeactivated", this);
	}

	protected override void OnInstanceCreated (GameObject inst)
	{
		base.OnInstanceCreated(inst);

		inst.name = inst.name.Replace ("(Clone)", "");
	}



	protected override void OnInstanceAllocated (GameObject inst)
	{
		base.OnInstanceAllocated (inst);

		if(_instPosAtAllocate)
		{
			if(_rigidbody == ERigidBody.RB2D)
				inst.GetComponent<Rigidbody2D>().position = _creator.instPos;
			else if (_rigidbody == ERigidBody.RB3D)
				inst.GetComponent<Rigidbody>().position = _creator.instPos;
			else
				inst.transform.position = _creator.instPos;
		}

		if (_onActivatedMessage)
			inst.SendMessage ("OnActivated", this);
	}

	//
	// < free queue >
	//

	public virtual void FreeQueue(GameObject inst)
	{
		_toRelease.Add (inst);
	}

	public virtual void ClearToRelease()
	{
		if(_toRelease.Count == 0) return;

		for (int i = 0; i < _toRelease.Count; ++i) 
		{
			this.Free (_toRelease [i]);
		}

		_toRelease.Clear ();
	}



	//
	// </ free queue >
	//

	// view

#if UNITY_EDITOR

	[Header("View")]
	[SerializeField] protected bool _view = true;
	[SerializeField] protected int _freeNumber = 0;
	[SerializeField] protected int _createdNumber = 0; 

	protected virtual void Update()
	{
		UpdateView ();
		this.ClearToRelease ();
	}

	void UpdateView()
	{
		if (!_view || !isInit)
			return;

		_freeNumber = _free.Count;
		_createdNumber = _createdObject;
	}

	// test

	void Test(string param)
	{
		int n = System.Convert.ToInt32 (param);

		base.CreateNewObjects (n);
	}


	//
	// < Builder >
	//	

	[ContextMenu("Build")]
	virtual public void Build()
	{
		this._BuildCreator ();
	}

	virtual protected void _BuildCreator()
	{
		_creator = this.gameObject.Build(
			_creator, "Creator");
	}

	//
	// < /Builder >
	//
	
#else
	
	virtual public void Build() {}

	protected virtual void Update()
	{
		this.ClearToRelease ();
	}

#endif
}

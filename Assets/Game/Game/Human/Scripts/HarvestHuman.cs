using UnityEngine;
using System.Collections;

public enum EHarvestHumanType
{
	SIMPLE,
	MAGE
}

public class HarvestHuman : MonoBehaviour {

	public HarvestHumanGenerator.HumanInfo	info { get; set; }
	public HarvestHumanGenerator			generator { get; set; }

	public bool isScared { get; protected set; }
	public bool isAlive { get; protected set; }

	public Rigidbody2D rb2d {
		get
		{
			if (_rb2d == null)
				_rb2d = this.GetComponent<Rigidbody2D>();
			return _rb2d;
		}
	}

	public EHarvestHumanMovementState movementState {
		get { return _controller.movementState;  }
	}
	
	public EHarvestHumanType type
	{
		get { return _type; }
	}

	[SerializeField]
	protected EHarvestHumanType			_type;
	[SerializeField]
	protected HarvestHumanController	_controller;
	[SerializeField]
	protected HarvestHumanFace			_face;

	protected Rigidbody2D				_rb2d;

	[SerializeField]
	protected bool _initAtAwake = false;

	protected void Awake()
	{
		if (_initAtAwake)
			this.Initialize();
	}

	[ContextMenu("Initialize")]
	public virtual void Initialize()
	{
		isAlive = true;
		isScared = false;

		_controller.Initialize(this);
		_face.Initialize();
	}

	public void Update()
	{
		if(isAlive)
			this._UpdateHumanState();
	}

	protected void _UpdateHumanState()
	{
		_controller.UpdateState();
		_face.UpdateState(_controller.movementState);
	}

	[ContextMenu("Scare")]
	public void Scare()
	{
		isScared = true;
		_face.Scare();
	}

	//
	// < death >
	//

	public void OnLight()
	{
		var dust = HarvestDustFactory.i.Allocate();
		dust.transform.position = this.transform.position;
		dust.Show();

		this.OnDead();
    }


	[SerializeField]
	protected Animator _fireAnimator;
	[SerializeField]
	protected float _fireTime = 3.0f;
	public void OnFire()
	{
		this.Scare();
		_fireAnimator.Show();
		Invoke("OnDead", _fireTime);
    }

	[SerializeField]
	protected MinMax _bloodSpeed;
	public void OnHole()
	{
		this.OnDead();

		int num = Random.Range(1, 3);
		for(int i = 0; i < num; ++i)
		{
			var blood = HarvestBloodFactory.i.GetBlood();
			blood.Fly(
				this.transform.position, DORandom.GetRandomVector3(
					_bloodSpeed.min, _bloodSpeed.max));
        }
	}

	[ContextMenu("Dead")]
	public virtual void OnDead()
	{
		_fireAnimator.Hide();

		var ghost = HarvestGhostFactory.i.GetGhost();
		var pos = this.transform.position;
//		Debug.Log("This: " + this.transform.position);
		pos.z = ghost.transform.position.z;
		ghost.transform.position = pos;
		ghost.Show();

		CancelInvoke();
		generator.Reset(this);		
    }

	//
	// < death >
	//

	

	public void StopMovement()
	{
		_face.StopAnimation();
		_face.Hide();
        _controller.StopMovement();
	}

	public void ContinueMovement()
	{
		_controller.ContinueMovement();
		_face.Show();
        _face.StartAnimation();
	}

	public void Clear()
	{
		CancelInvoke();
	}

}

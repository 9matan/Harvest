using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class HarvestHumanFace : MonoBehaviour {

	[Header("states")]
	[SerializeField]
	protected string _leftState = "left";
	[SerializeField]
	protected string _rightState = "right";
	[SerializeField]
	protected string _topState = "top";
	[SerializeField]
	protected string _bottomState = "bottom";

	protected GameObject					_currentStateGo;
	protected EHarvestHumanMovementState	_currentState;

	public Animator animator
	{
		get
		{
			if (_animator == null)
				_animator = this.GetComponent<Animator>();

			return _animator;
        }
	}

	protected Animator _animator = null;
	
	protected void Awake()
	{
		_currentState = EHarvestHumanMovementState.STAY;
    }

	public void Initialize()
	{
		animator.speed = _animSpeed;
    }

	// 
	// < Update state >
	//

	public void UpdateState(EHarvestHumanMovementState state)
	{
		if (_currentState == state || state == EHarvestHumanMovementState.STAY) return;

		switch (state)
		{
			case EHarvestHumanMovementState.LEFT:
				this._SetLeftState();
				break;
			case EHarvestHumanMovementState.RIGHT:
				this._SetRightState();
				break;
			case EHarvestHumanMovementState.BOTTOM:
				this._SetBottomState();
				break;
			case EHarvestHumanMovementState.TOP:
				this._SetTopState();
				break;
			default:
				break;
		}

		_currentState = state;
	}

	protected void _SetRightState()
	{
		animator.Play(_rightState);
	}

	protected void _SetLeftState()
	{
		animator.Play(_leftState);
	}

	protected void _SetTopState()
	{
		animator.Play(_topState);
	}

	protected void _SetBottomState()
	{
		animator.Play(_bottomState);
	}

	// 
	// </ Update state >
	//



	[SerializeField]
	protected float _scareAnimSpeed = 0.6f;
	[SerializeField]
	protected float _animSpeed = 0.3f;

	public void StopAnimation()
	{
		animator.enabled = false;
	}

	public void StartAnimation()
	{
		animator.enabled = true;
	}

	public void Scare()
	{
		_animator.speed = _scareAnimSpeed;
    }

}

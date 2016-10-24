using UnityEngine;
using System.Collections;

[System.Serializable]
public struct MinMax
{
	public float min;
	public float max;

	public float GetRandom()
	{
		return Random.Range(min, max);
	}
}

public class HarvestHumanRandomMovement : MonoBehaviour {
	
	public HarvestHumanController controller { get; protected set; }

	[SerializeField]
	protected Rigidbody2D _rb2d;
	[SerializeField]
	protected MinMax _scaredSpeed;
	[SerializeField]
	protected MinMax _speed;
	[SerializeField]
	protected MinMax _rate;
	[SerializeField]
	protected MinMax _scaredRate;

	protected bool _startChangingCorountine = false;

	public void Initialize(HarvestHumanController __controller)
	{
		controller = __controller;

		this._ChangeMovement();
    }

	protected void OnEnable()
	{
		if(controller != null)
			this._ChangeMovement();
	}

	protected void OnDisable()
	{
		CancelInvoke();
	}

	protected void Update()
	{
		if (_startChangingCorountine)
		{
			this._InvokeChangingCorountine();
			_startChangingCorountine = false;
		}
	}

	protected void _InvokeChangingCorountine()
	{
		Invoke("_ChangeMovement", this._GetNextRate());
	}

	protected void _ChangeMovement()
	{
		var dir = this._GetDirection();
		_rb2d.velocity = dir * this._GetSpeed();
		this._InvokeChangingCorountine();
    }

	protected Vector3 _GetDirection()
	{
		var dir = DORandom.GetRandomVector3(0.1f, 1.0f);
		if (DORandom.Chance(0.5f))
			dir.x *= -1.0f;
		if (DORandom.Chance(0.5f))
			dir.y *= -1.0f;

		return DOMath.AdaptVectorToPower(dir, 1.0f);
	}

	protected float _GetSpeed()
	{
		if(controller.human.isScared)
			return Random.Range(_scaredSpeed.min, _scaredSpeed.max);
		else
			return Random.Range(_speed.min, _speed.max);
	}

	protected float _GetNextRate()
	{
		if (controller.human.isScared)
			return Random.Range(_scaredRate.min, _scaredRate.max);
		else
			return Random.Range(_rate.min, _rate.max);
	}

	public void Clear()
	{
		CancelInvoke();
	}

}

using UnityEngine;
using System.Collections;

public class HarvestBlood : MonoBehaviour {

	protected Vector3 _velocity;

	public void Fly(Vector3 from, Vector3 velocity)
	{
		this.transform.position = from;
		_velocity = velocity * _flyspeed; _velocity.z = 0.0f;
		_startPos = this.transform.position;
		_endPos = _startPos + _velocity;
		_rat = 0.0f;
	}

	protected float		_rat = 2.0f;
	protected Vector3	_startPos;
	protected Vector3	_endPos;

	[SerializeField]
	protected float _flytime = 0.5f;
	[SerializeField]
	protected float _flyspeed = 2.0f;

	protected void Update()
	{
		if(_rat < _flytime)
		{
			this.transform.position = Vector3.Lerp(_startPos, _endPos, _rat);
			_rat += Time.deltaTime;
		}
	}

}

using UnityEngine;
using System.Collections;

public class HarvestMage : HarvestHuman {

	[Header("Mage")]
	[SerializeField]
	protected MinMax _castPeriod;
	[SerializeField]
	protected float _castTime = 3.0f;

	public override void Initialize()
	{
		base.Initialize();
		Invoke("CastMagic", this._GetCastPeriod());
	}

	[SerializeField]
	protected Animator _magicAnimator;
	[SerializeField]
	protected int _damage = 4;
	public void CastMagic()
	{
		this.StopMovement();
		_magicAnimator.Show();
//        _magicAnimator.Play("Magic");

		Invoke("OnMagicEnded", _castTime);
	}

	public void OnMagicEnded()
	{
		_magicAnimator.Hide();
		this.ContinueMovement();
		GameController.i.Damage(_damage);
		Invoke("CastMagic", this._GetCastPeriod());
	}

	protected float _GetCastPeriod()
	{
		return _castPeriod.GetRandom();
	}

	public override void OnDead()
	{
		base.OnDead();

		_magicAnimator.Hide();
	}

}

using UnityEngine;
using System.Collections;

public class HarvestBlackholeSkill : HarvestPlayerSkill {

	public HarvestBlackholeSkill()
	{
		_type = EHarvestPlayerSkillType.BLACKHOLE;
	}

	[SerializeField]
	protected float _gravityRadius = 6.0f;
	[SerializeField]
	protected float _killRadius = 3.5f;
	[SerializeField]
	protected float _castTime = 3.5f;
	[SerializeField]
	protected PointEffector2D _effector;

	public override void Activate(Vector3 target)
	{
		base.Activate(target);

		_effector.distanceScale = _gravityRadius;
		_effector.enabled = true;

		Invoke("DestroyAll", _castTime);
	}

	public void DestroyAll()
	{
		this._KillPeople();
		this.Deactivate();
    }

	protected void _KillPeople()
	{
		var men = Physics2D.OverlapCircleAll(
			this.transform.position,
			_killRadius,
			1 << (int)EHarvestPhysX.HUMAN);

		for (int i = 0; i < men.Length; ++i)
			men[i].attachedRigidbody.GetComponent<HarvestHuman>().OnHole();
	}

	public override void Deactivate()
	{
		_effector.enabled = false;

		base.Deactivate();
	}



}

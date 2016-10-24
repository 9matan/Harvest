using UnityEngine;
using System.Collections;

public class HarvestLightSkill : HarvestPlayerSkill {

	[SerializeField]
	protected float _killRadius = 2.0f;
	[SerializeField]
	protected float _scareRadius = 3.0f;

	public override void Activate(Vector3 target)
	{
		base.Activate(target);

		this._KillPeople();
		this._ScarePeople();
    }

	protected void _KillPeople()
	{
		var men = Physics2D.OverlapCircleAll(
			this.transform.position,
			_killRadius,
			1 << (int)EHarvestPhysX.HUMAN);

		//	Debug.Log("Number: " + men.Length);
		for (int i = 0; i < men.Length; ++i)
			men[i].attachedRigidbody.GetComponent<HarvestHuman>().OnLight();
	}

	protected void _ScarePeople()
	{
		var men = Physics2D.OverlapCircleAll(
			this.transform.position,
			_scareRadius,
			1 << (int)EHarvestPhysX.HUMAN);

		//	Debug.Log("Number: " + men.Length);
		for (int i = 0; i < men.Length; ++i)
			men[i].attachedRigidbody.GetComponent<HarvestHuman>().Scare();
	}

	public void _OnFlashingEnded()
	{
		this.Deactivate();
	}



}

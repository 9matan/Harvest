using UnityEngine;
using System.Collections;

public class HarvestFireballSkill : HarvestPlayerSkill {

	[SerializeField]
	protected float _fireRadius = 2.5f; 
	
	public override void Activate(Vector3 target)
	{
		base.Activate(target);		
    }

	protected void _FirePeople()
	{
		var men = Physics2D.OverlapCircleAll(
			this.transform.position,
			_fireRadius,
			1 << (int)EHarvestPhysX.HUMAN);

		for (int i = 0; i < men.Length; ++i)
			men[i].attachedRigidbody.GetComponent<HarvestHuman>().OnFire();
	}

	public void _OnFallingDown()
	{
		this._FirePeople();
		this.Deactivate();
	}
}

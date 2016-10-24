using UnityEngine;
using System.Collections;

public class HarvestPlayerSkill : MonoBehaviour {

	public DOGOMemFactory factory { get; set; }

	[SerializeField]
	protected EHarvestPlayerSkillType _type;

	public virtual void Activate(Vector3 target)
	{
		target.z = this.transform.position.z;
        this.transform.position = target;
		this.Show();
	}

	public virtual void Deactivate()
	{
		factory.Free(this.gameObject);
	}

}

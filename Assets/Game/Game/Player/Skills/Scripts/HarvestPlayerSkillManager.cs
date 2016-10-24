using UnityEngine;
using System.Collections;

public enum EHarvestPlayerSkillType
{
	NONE,
	LIGHT,
	FIREBALL,
	BLACKHOLE
}

public class HarvestPlayerSkillManager : MonoBehaviour {

	protected EHarvestPlayerSkillType _currentSkill = EHarvestPlayerSkillType.NONE;
	protected bool _setSkill = false;

	protected void _SetSkill(EHarvestPlayerSkillType _skill)
	{
		_currentSkill = _skill;
		_setSkill = true;
	}

	public void SetLight()
	{
		this._SetSkill(EHarvestPlayerSkillType.LIGHT);
    }

	public void SetFireball()
	{
		this._SetSkill(EHarvestPlayerSkillType.FIREBALL);
	}

	public void SetBlackhole()
	{
		this._SetSkill(EHarvestPlayerSkillType.BLACKHOLE);
	}


	public void UseSkill(Vector3 target)
	{
		if (_currentSkill == EHarvestPlayerSkillType.NONE) return;
	//	Debug.Log("Use skill: " + _currentSkill + " " + target);

		switch(_currentSkill)
		{
			case EHarvestPlayerSkillType.LIGHT:
				this._UseLight(target);
				break;

			case EHarvestPlayerSkillType.FIREBALL:
				this._UseFireBall(target);
				break;

			case EHarvestPlayerSkillType.BLACKHOLE:
				this._UseBlackhole(target);
				break;
		}

		_currentSkill = EHarvestPlayerSkillType.NONE;
    }

	[Header("Light")]
	[SerializeField]
	protected DOGOMemFactory	_lightFactory;
	[SerializeField]
	protected Animator			_canvasAnimator;

	[ContextMenu("Use light")]
	protected void _UseLight()
	{
		this._UseLight(Vector3.zero);	
	}

	protected void _UseLight(Vector3 target)
	{
		this._UseSkillFactory(target, _lightFactory);
		_canvasAnimator.Play("flash");
    }

	[Header("Fireball")]
	[SerializeField]
	protected DOGOMemFactory _fireballFactory;
	protected void _UseFireBall(Vector3 target)
	{
		this._UseSkillFactory(target, _fireballFactory);
	}

	[Header("Blackhole")]
	[SerializeField]
	protected DOGOMemFactory _blackholeFactory;
	protected void _UseBlackhole(Vector3 target)
	{
		this._UseSkillFactory(target, _blackholeFactory);
	}

	protected void _UseSkillFactory(Vector3 target, DOGOMemFactory factory)
	{
		if (!factory.isInit)
			factory.Initialize();

		var skill = factory.Allocate().GetComponent<HarvestPlayerSkill>();
		skill.factory = factory;
        skill.Activate(target);
    }

}

using UnityEngine;
using System.Collections;

public class HarvestMageMagicCast : MonoBehaviour {

	[SerializeField]
	protected HarvestMage _mage;

	protected void _OnMagicEnded()
	{
		_mage.OnMagicEnded();
	}

}

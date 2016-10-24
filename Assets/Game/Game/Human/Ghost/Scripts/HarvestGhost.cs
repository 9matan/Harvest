using UnityEngine;
using System.Collections;

public class HarvestGhost : MonoBehaviour {

	public DOGOMemFactory factory { get; set; }

	protected void _OnFlyingEnded()
	{
		
		factory.Free(this.gameObject);
	}

}

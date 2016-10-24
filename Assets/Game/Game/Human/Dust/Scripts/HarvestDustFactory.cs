using UnityEngine;
using System.Collections;

public class HarvestDustFactory : DOGOMemFactory {

	static public HarvestDustFactory i
	{
		get { return _instance; }
	}

	static protected HarvestDustFactory _instance;

	protected override void Awake()
	{
		_instance = this;
	}

	public override GameObject Allocate()
	{
		if (!isInit)
			this.Initialize();

		return base.Allocate();
	}

}

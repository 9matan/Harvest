using UnityEngine;
using System.Collections;

public class HarvestBloodFactory : MonoBehaviour {

	static public HarvestBloodFactory i {
		get { return _instance; }
	}

	static protected HarvestBloodFactory _instance;

	[SerializeField]
	protected DOGOMemFactory[] _factories;
	
	public bool isInit { get; protected set; }

	protected void Awake()
	{
		_instance = this;
		this.Initialize();
    }

	public void Initialize()
	{
		this._InitializeFactories();

		isInit = true;
    }

	protected void _InitializeFactories()
	{
		for (int i = 0; i < _factories.Length; ++i)
			if (!_factories[i].isInit)
				_factories[i].Initialize();
    }

	public HarvestBlood GetBlood()
	{
		if (!isInit)
			this.Initialize();

		return _factories[Random.Range(0, _factories.Length)].Allocate().GetComponent<HarvestBlood>();
	}

}

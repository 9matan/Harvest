using UnityEngine;
using System.Collections;

public class HarvestGhostFactory : MonoBehaviour {

	static public HarvestGhostFactory i
	{
		get { return _instance; }
	}

	static protected HarvestGhostFactory _instance;

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

	public HarvestGhost GetGhost()
	{
		if (!isInit)
			this.Initialize();

		var num = Random.Range(0, _factories.Length);
        var ghost = _factories[num].Allocate().GetComponent<HarvestGhost>();
		ghost.factory = _factories[num];
		return ghost;
	}

}

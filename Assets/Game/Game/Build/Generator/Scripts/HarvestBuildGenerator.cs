using UnityEngine;
using System.Collections;

public class HarvestBuildGenerator : MonoBehaviour {

	[SerializeField]
	protected DOGOMemFactory[]	_factories;
	[SerializeField]
	protected DONCArea2D[]		_areas;

//	protected float _generateProbability = 0.95f;

	protected void Awake()
	{
		this.Initialize();
	}

	public void Initialize()
	{
		this._InitializeFactories();
		this._InitializeAreas();
	}

	protected void _InitializeFactories()
	{
		for (int i = 0; i < _factories.Length; ++i)
			if(!_factories[i].isInit)
				_factories[i].Initialize();
	} 

	protected void _InitializeAreas()
	{
		for (int i = 0; i < _areas.Length; ++i)
			this._GenerateOnArea(_areas[i]);
    }

	protected void _GenerateOnArea(DONCArea2D area)
	{
		var build = this._GetBuild();
		build.Show();

		var pos = area.GetRandomWorldPosition();
		pos.z = build.transform.position.z;
		build.transform.position = pos;
    }

	protected HarvestBuild _GetBuild()
	{
		return _factories[Random.Range(0, _factories.Length)].Allocate().GetComponent<HarvestBuild>();
	}

}

using UnityEngine;
using System.Collections;

public class HarvestGrassGenerator : MonoBehaviour
{

	[SerializeField]
	protected DOGOMemFactory[] _factories;
	[SerializeField]
	protected DONCArea2D[] _areas;
	[SerializeField]
	protected int _number = 200; 

	protected void Awake()
	{
		this.Initialize();
	}

	public void Initialize()
	{
		this._InitializeFactories();
		this._InitializeGrass();
	}

	protected void _InitializeFactories()
	{
		for (int i = 0; i < _factories.Length; ++i)
			if (!_factories[i].isInit)
				_factories[i].Initialize();
	}

	protected void _InitializeGrass()
	{
		for (int i = 0; i < _number; ++i)
			this._Generate();
	}

	public void _Generate()
	{
		this._GenerateOnArea(
			this._GetArea());
    }

	protected void _GenerateOnArea(DONCArea2D area)
	{
		var grass = this._GetGrass();
		grass.Show();

		var pos = area.GetRandomWorldPosition();
		pos.z = grass.transform.position.z;
		grass.transform.position = pos;
	}

	protected DONCArea2D _GetArea()
	{
		return _areas[Random.Range(0, _areas.Length)];
    }

	protected GameObject _GetGrass()
	{
		return _factories[Random.Range(0, _factories.Length)].Allocate();
	}


}

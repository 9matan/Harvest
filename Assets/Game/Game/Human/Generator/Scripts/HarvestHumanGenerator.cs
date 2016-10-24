using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HarvestHumanGenerator : MonoBehaviour {

	[System.Serializable]
	public class HumanInfo
	{
		public EHarvestHumanType	type;
		public float				prob;
		public DOGOMemFactory		factory;
	}

	[SerializeField]
	protected float _period = 5.0f;
	[SerializeField]
	protected float maxNumber = 20;
	[SerializeField]
	protected int _startNumber = 10;
	[SerializeField]
	protected GameController _score;
	[SerializeField]
	protected HumanInfo[] _infoesArr;
	
	
	protected Dictionary<EHarvestHumanType, HumanInfo> _infoes = new Dictionary<EHarvestHumanType, HumanInfo>();
	protected int _currentNumber = 0;
	

	protected void Awake()
	{
		this.Initialize();
	}

	[ContextMenu("Initialize")]
	public void Initialize()
	{
		this._InitializeInfoes();
		this._InitilializeStartNumber();
        this._StartGenerating();
    }

	protected void _InitializeInfoes()
	{
		for(int i = 0; i < _infoesArr.Length; ++i)
		{
			_infoes.Add(_infoesArr[i].type, _infoesArr[i]);
		}
    }

	protected void _StartGenerating()
	{
		InvokeRepeating("_TryGenerate", 0.5f, _period);
	}

	protected void _InitilializeStartNumber()
	{
		for (int i = 0; i < _startNumber; ++i)
			this._TryGenerate();
    }

	//
	// < Generate >
	//

	[SerializeField]
	protected HarvestHumanGenArea[] _areas;

	protected void _TryGenerate()
	{
		if (_currentNumber <= maxNumber)
			this.Generate();
	}

	[ContextMenu("Generate")]
	public void Generate()
	{
		++_currentNumber;
        var human = this._GetHuman();
		var area = this._GetGenArea();
		area.Generate(human);

		human.Initialize();
	}

	protected HarvestHuman _GetHuman()
	{
		var probs = new Dictionary<HumanInfo, float>();
		for (int i = 0; i < _infoesArr.Length; ++i)
			probs.Add(_infoesArr[i], _infoesArr[i].prob);
		var info = DORandom.AbsProb(probs);

		if (!info.factory.isInit)
			info.factory.Initialize();

		var human = info.factory.Allocate().GetComponent<HarvestHuman>();
		human.info		= info;
		human.generator = this;
        return human;
    }

	protected HarvestHumanGenArea _GetGenArea()
	{
		return _areas[Random.Range(0, _areas.Length)];
	}

	//
	// </ Generate >
	//

	public void Reset(HarvestHuman human)
	{
		_score.Change_soul_count(1);

		--_currentNumber;
		human.Clear();
        human.rb2d.velocity = Vector2.zero;
		human.info.factory.Free(human.gameObject);
    }

}

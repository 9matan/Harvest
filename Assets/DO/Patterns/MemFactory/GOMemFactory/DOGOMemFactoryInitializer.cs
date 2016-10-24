using UnityEngine;
using System.Collections;

public class DOGOMemFactoryInitializer : MonoBehaviour {

	public EDOInitState initState; 

	protected virtual void Awake()
	{
		if (initState == EDOInitState.AWAKE)
			this.Initialize ();
	}

	protected virtual void Start()
	{
		if (initState == EDOInitState.START)
			this.Initialize ();
	}

	[Header("Factories")]
	[SerializeField] DOGOMemFactory[] _factories;
	
	public void  Initialize()
	{
		for (int i = 0; i < _factories.Length; ++i)
			if(!_factories[i].isInit)
				_factories[i].Initialize ();
	}
	
}

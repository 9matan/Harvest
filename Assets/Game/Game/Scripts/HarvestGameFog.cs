using UnityEngine;
using System.Collections;

public class HarvestGameFog : MonoBehaviour
{

	static public bool useFog = false;

	[SerializeField]
	protected ParticleSystem[] _systems;

	protected virtual void Awake()
	{
		this.SetFogActive(useFog);
    }
	
	[ContextMenu("Activate fog")]
	public void ActivateFog()
	{
		this.SetFogActive(true);
	}

	[ContextMenu("Deactivate fog")]
	public void DeactivateFog()
	{
		this.SetFogActive(false);
	}

	public void SetFogActive(bool active)
	{
		for (int i = 0; i < _systems.Length; ++i)
		{
			_systems[i].enableEmission = active;
        }
	}

}

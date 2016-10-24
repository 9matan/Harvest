using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuController : MonoBehaviour {

	[SerializeField]
	protected UnityEngine.UI.Toggle fogToggle;

	protected virtual void Awake ()
	{
		fogToggle.isOn = HarvestGameFog.useFog;
    }

	public void StartGame()
    {		
        Application.LoadLevel("game");
		HarvestGameFog.useFog = fogToggle.isOn;
	}
}

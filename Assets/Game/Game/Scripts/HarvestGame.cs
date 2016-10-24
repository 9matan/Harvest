using UnityEngine;
using System.Collections;

public enum EHarvestPhysX 
{
	WALL = 8,
	HUMAN = 9,
	BLACKHOLE = 10
}

public class HarvestGame : MonoBehaviour {

	public void Initialize()
	{

	}

	public void LoadMenu()
	{
		Application.LoadLevel("menu");
	}

	public void LoadGame()
    {
		Application.LoadLevel("game");
	}
	
}

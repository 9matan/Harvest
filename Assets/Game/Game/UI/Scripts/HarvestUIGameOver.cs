using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HarvestUIGameOver : MonoBehaviour {

	[SerializeField]
	protected Text _bestScoreText;

	public void OnEnable()
	{
		_bestScoreText.text = GameController.i.bestScore.ToString();
    }

}

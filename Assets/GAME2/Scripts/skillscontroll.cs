using UnityEngine;
using System.Collections;

using UnityEngine.UI;
public class skillscontroll : MonoBehaviour {

	public GameObject progress_obj;
	public float time;
    public int price;
    public GameObject no_money;

	protected Image progress_img;
	protected float progress = 0;
	protected float currentTime = 0.0f;

	// Use this for initialization
	void Start ()
	{
		progress_img = progress_obj.GetComponent<Image>();   
    }
	
	// Update is called once per frame
	void Update () {

		if(currentTime > 0.0f)
		{
			currentTime -= Time.deltaTime;
			progress_img.fillAmount = currentTime / time;
        }
		
		if(currentTime < 0.0f)
		{
			progress_obj.SetActive(false);
			currentTime = 0.0f;
        }

/*        if (progress > 0)
        {
            progress -= speed;
            progress_img.fillAmount = progress;
        }
        if (progress < 0)
        {
            progress_obj.SetActive(false);
            progress = 0;
        }
*/	}
	public void enable()
    {
		GameController.i.Change_soul_count(-price);  
		progress_obj.SetActive(true);
		currentTime = time;
	}
}

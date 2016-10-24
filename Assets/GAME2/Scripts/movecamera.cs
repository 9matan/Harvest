using UnityEngine;
using System.Collections;
using System;
public class movecamera : MonoBehaviour {

    bool pressed = false;
    Vector2 lastMousePos = Vector2.zero;
    Vector2 touchDelta = new Vector2(0f, 0f);
    Vector2 newMousePos;
     
    Vector2 bounds;
    public GameObject map;
    // Use this for initialization
    void Start () {
        Vector2 mapsize = map.GetComponent<Renderer>().bounds.size;
        var verticalSize = Camera.main.orthographicSize * 2.0;
        var horizontalSize = verticalSize * Screen.width / Screen.height;
        Vector2 camerasize = new Vector2((float)horizontalSize, (float)verticalSize);
        bounds.x = mapsize.x / 2 - camerasize.x / 2;
        bounds.y = mapsize.y / 2 - camerasize.y / 2;
    }
	
	// Update is called once per frame
	void Update () {
         newMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButtonDown("Fire1")) {
            lastMousePos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pressed = true;
        }

        if (Input.GetButton("Fire1")&&pressed) {
            touchDelta = newMousePos - lastMousePos;
            lastMousePos = newMousePos-touchDelta;
            transform.position +=new Vector3(-touchDelta.x,-touchDelta.y,0);

            if (transform.position.x > bounds.x)
                transform.position=new Vector3(bounds.x, transform.position.y,-10);
            if (transform.position.x < -bounds.x)
                transform.position = new Vector3(-bounds.x, transform.position.y, -10);
            if (transform.position.y > bounds.y)
                transform.position = new Vector3( transform.position.x, bounds.y, -10);
            if (transform.position.y < -bounds.y)
                transform.position = new Vector3( transform.position.x, -bounds.y, -10);
        }

        if (Input.GetButtonUp("Fire1")) {
            pressed = false;
        }
      
    }
}

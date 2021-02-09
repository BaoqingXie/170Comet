using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
	private SpriteRenderer circle;
	private float one = 1f;
	public KeyCode KeyToPress;
	public Sprite Pic;
	public Sprite pressedPic;

    void Start() {
        circle = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyToPress)) {
        	//circle.color = new Color(0,0,0,255);
        	circle.sprite = pressedPic;
        }
        if (Input.GetKeyUp(KeyToPress)) {
        	//circle.color = new Color(255,255,255,255);
        	circle.sprite = Pic;
        }
        if (one < 5) {
        	if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)){
        		transform.position -= new Vector3(0f, 1.05f, 0f);
        		one++;
        	}
        }
        if (one > 1) {
        	if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
            {
        		transform.position += new Vector3(0f, 1.05f, 0f);
        		one--;
        	}
        }
    }
}

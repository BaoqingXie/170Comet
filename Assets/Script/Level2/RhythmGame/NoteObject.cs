using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool CanBePressed;
    public KeyCode KeyToPress;
    private GameObject EndingLine;
    public SpriteRenderer NotesSR;
    public AudioSource noteSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyToPress)) {
        	if (CanBePressed) {
        		NotesSR.enabled = false;
                RhythmScore.NoteHit();
                noteSound.Play();
        	}
        }
    }

    //SPACE can be pressed
    //when the startCircle collide with the Notes
    private void OnTriggerEnter2D(Collider2D other) {
    	if (other.tag == "Activator") {
    		CanBePressed = true;
    	}
        
    }

    //SPACE cannot be pressed
    //when the Notes pass the startCircle
    private void OnTriggerExit2D(Collider2D other) {
    	if (other.tag == "Activator") {
    		CanBePressed = false;
    	}
        if (other.tag == "EndingLine"){
            RhythmScore.NotePass();
            NotesSR.enabled = true;
        }
    }

    //Change the endingLine to RED and SHAKING
    //when the Notes were missed
    
}

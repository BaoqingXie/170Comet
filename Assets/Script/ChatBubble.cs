using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    public SpriteRenderer backgroundSpriteRenderer;
    public SpriteRenderer iconSpriteRenderer;

    private void Awake(){
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
    }

    private void Start(){
        iconSpriteRenderer.enabled = false;
            backgroundSpriteRenderer.enabled = false;
    }

    void Update(){

        if (Input.GetKeyUp(KeyCode.K)){
            iconSpriteRenderer.enabled = false;
            backgroundSpriteRenderer.enabled = false;
        }
        
        if (Input.GetKeyDown(KeyCode.K)){
            iconSpriteRenderer.enabled = true;
            backgroundSpriteRenderer.enabled = true;
        }
    }
}

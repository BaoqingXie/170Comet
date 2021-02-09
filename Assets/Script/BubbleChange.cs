using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleChange : MonoBehaviour
{

    public SpriteRenderer Bubble;
    private IEnumerator coroutine;

    private void Awake(){
        
        Bubble = transform.Find("bubble_pen_paper").GetComponent<SpriteRenderer>();
        Bubble.enabled = false;
    }

    



    private void OnTriggerStay2D(Collider2D collider){
        //Debug.Log("Trigger");
        Bubble.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collider){
        //Debug.Log("Trigger");
        StartCoroutine(WaitAndShow(1));
    }

    private IEnumerator WaitAndShow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Bubble.enabled = false;
    }

}

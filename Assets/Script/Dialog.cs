using UnityEngine;

public class Dialog : MonoBehaviour
{
    //能在unity里面看到
    public GameObject enterDialog;

    void start(){
        enterDialog.SetActive(false);
        Debug.Log("false");
    }
    

    private void OnTriggerEnter2D(Collider2D collision){
        
        if(collision.tag == "Player"){
            Debug.Log("true");
            enterDialog.SetActive(true);
        }
    }
}

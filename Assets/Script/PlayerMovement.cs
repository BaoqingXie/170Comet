using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    private Vector2 direction;
    [SerializeField] private float moveSpeed = 1.0f;
    
    private void Awake(){
            rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        //碰撞到npcone的时候
        if (SceneManager.GetActiveScene().name == "Loading" || (SceneManager.GetActiveScene().name == "Level2" && (!AutoMovement.isPlaCanFly || NpcController.isPlayerMove))) {
            direction = new Vector2(0, 0);
            rb.velocity = direction;
            Debug.Log("Stop PlayerMovement");
        } else { //normal时
            moveH = Input.GetAxisRaw("Horizontal") * moveSpeed;
            //Debug.Log(Input.GetAxisRaw("Horizontal"));
            moveV = Input.GetAxisRaw("Vertical") * moveSpeed;
            //Debug.Log(Input.GetAxisRaw("Vertical"));
            rb.velocity = new Vector2(moveH, moveV);
            direction = new Vector2(moveH, moveV);

            //Debug.Log("Start PlayerMovement");
        }
        FindObjectOfType<PlayerAnimation1>().SetDirection(direction);

    }

    

}

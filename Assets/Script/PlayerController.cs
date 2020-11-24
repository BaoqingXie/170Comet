using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 可以在controller看到的变量
    [SerializeField]private Rigidbody2D rb;
    public float speed;
    public float jumpforce;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        // char jump
        if(Input.GetButtonDown("Jump")/*&& coll.IsTouchingLayers(ground)*/){
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            // anim.SetBool("jumping", true); 
        }
    }

    void Movement(){
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        // char move
        if(horizontalmove != 0){
            rb.velocity = new Vector2(horizontalmove*speed*Time.deltaTime, rb.velocity.y);
        }

        if(facedirection != 0){
            transform.localScale = new Vector3(2*facedirection, 2, 1);
        }

    }
}

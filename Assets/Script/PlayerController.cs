using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 可以在controller看到的变量
    [SerializeField]
    private Rigidbody2D rb;
    //public float speed;
    public float move_speed;
    private bool facing_right = false;
    private float move_x;

    public float jumpforce;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){

        player_move();
        if(Input.GetButtonDown("Jump")/*&& coll.IsTouchingLayers(ground)*/){
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
             // anim.SetBool("jumping", true); 
        }

    }

    //functions used for all the player actions
    void player_move(){

        //control
        move_x = Input.GetAxisRaw("Horizontal");

        //animation

        //player direction
        if(move_x > 0.0f && facing_right == false){

            flip_player();

        }
        else if(move_x < 0.0f && facing_right == true){

            flip_player();

        }
        //physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (move_x * move_speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);


    }

    //function used to flip the character sprite alone the x axis
    void flip_player(){

        facing_right = !facing_right;
        Vector2 local_scale = gameObject.transform.localScale;
        local_scale.x *= -1;
        transform.localScale = local_scale;

    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     Movement();
    //     // char jump
    //     if(Input.GetButtonDown("Jump")/*&& coll.IsTouchingLayers(ground)*/){
    //         rb.velocity = new Vector2(rb.velocity.x, jumpforce);
    //         // anim.SetBool("jumping", true); 
    //     }
    // }

    // void Movement(){
    //     float horizontalmove = Input.GetAxis("Horizontal");
    //     float facedirection = Input.GetAxisRaw("Horizontal");

    //     // char move
    //     if(horizontalmove != 0){
    //         rb.velocity = new Vector2(horizontalmove*speed*Time.deltaTime, rb.velocity.y);
    //     }

    //     if(facedirection != 0){
    //         transform.localScale = new Vector3(2*facedirection, 2, 1);
    //     }

    // }
}

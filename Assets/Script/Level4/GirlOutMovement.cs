using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GirlOutMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    private Vector2 direction;
    [SerializeField] private float moveSpeed = 1.0f;
    private Animator GirlAnimator;
    private float tempX;
    private float tempY;
    private bool IsinHideObj;
    private bool Isinhat;
    private bool Isindoor;
    private GameObject HideHint;
    private GameObject LeaveHint;
    private GameObject DoorHint;
    private SpriteRenderer sprite;
    public bool isHiding;
    public GameObject Hat;
    public bool isPickHat;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        GirlAnimator = GetComponent<Animator>();
        tempX = 0;
        tempY = 0;
        IsinHideObj = false;
        HideHint = GameObject.Find("HideHint");
        LeaveHint = GameObject.Find("LeaveHint");
        DoorHint = GameObject.Find("DoorHint");
        sprite = GetComponent<SpriteRenderer>();
        isHiding = false;
        isPickHat = false;
    }

    void Start()
    {
        HideHint.SetActive(false);
        LeaveHint.SetActive(false);
        DoorHint.SetActive(false);
    }


    void Update() 
    {
        if (GameManager.instance.stopMoving)
        {
            rb.velocity = Vector2.zero;
        }
        moveH = Input.GetAxisRaw("Horizontal");
        GirlAnimator.SetFloat("Direaction", moveH);
        GirlAnimator.SetFloat("Speed", Mathf.Abs(moveH));

        //Debug.Log("speed is " + Mathf.Abs(moveH));
        //Debug.Log("direaction is " + moveH);

        if(moveH < 0){
            GirlAnimator.SetBool("FaceR", false);
            //Debug.Log("FaceR is " + false);
        }else if(moveH > 0){
            GirlAnimator.SetBool("FaceR", true);
            //Debug.Log("FaceR is " + true);
        }

        direction = new Vector2(moveH, 0);
        rb.velocity = direction * moveSpeed;  

        if(isHiding && IsinHideObj){
            HideHint.SetActive(false);
            LeaveHint.SetActive(true);
        }else if(!isHiding && IsinHideObj){
            HideHint.SetActive(true);
            LeaveHint.SetActive(false);
        }else{
            HideHint.SetActive(false);
            LeaveHint.SetActive(false);
        }

        if(IsinHideObj && Input.GetKeyDown("space") && !isHiding){
            sprite.sortingOrder = -1;
            isHiding = true;
        }else if(IsinHideObj && Input.GetKeyDown("space") && isHiding){
            sprite.sortingOrder = 0;
            isHiding = false;
        }

        if(!IsinHideObj){
            sprite.sortingOrder = 0;
            isHiding = false;
        }

        if(Isindoor){
            DoorHint.SetActive(true);
        }else{
            DoorHint.SetActive(false);
        }

        if(Isinhat && Input.GetKeyDown("space")){
            Hat.SetActive(false);
            isPickHat = true;
            GirlAnimator.SetBool("IsPickHat", true);
            //GirlAnimator.SetTrigger("PickTrigger");
        }

        if(Isindoor && Input.GetKeyDown("space")){
            LevelLoader.instance.LoadLevel("Level4Part2");
        }
        

    }

        void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hide")
        {
            //Debug.Log("hit box");
            IsinHideObj = true;
        }

        if (collision.gameObject.tag == "Hat")
        {
            //Debug.Log("hit box");
            Isinhat = true;
        }

        if (collision.gameObject.tag == "Door")
        {
            //Debug.Log("miss hit box");
            Isindoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hide")
        {
            //Debug.Log("miss hit box");
            IsinHideObj = false;
        }

        if (collision.gameObject.tag == "Hat")
        {
            //Debug.Log("miss hit box");
            Isinhat = false;
        }

        if (collision.gameObject.tag == "Door")
        {
            //Debug.Log("miss hit box");
            Isindoor = false;
        }
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenNpcMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float distance = 5.0f;
    private Animator GAnimator;
    public GameObject talkHint;
    public GameObject ClimbHint;
    public GameObject Girl1;
    public Transform groundDe;
    public Transform girlDe;
    private bool moveingRight = true;
    private float timer = 0.0f;
    public float waitingTime = 0.2f;
    public bool ismoving = true;
    bool isDiaActive = false;
    bool isDiaNeed = false;
    public bool Istalk = false;
    public Animator PAnimator;
    private bool takeTrigger = false;
    

    private void Start(){
        GAnimator = GetComponent<Animator>();
        talkHint.SetActive(false);
        ClimbHint.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

        Debug.Log("now moving" + ismoving);


        if(takeTrigger && !GameManager.instance.IsDialogShow()){
            PAnimator.SetBool("TakeTrigger", true);
            StartCoroutine(WaitAnimDoneB());
        }
    

        if(ismoving && !isDiaActive){
            move();
        }else if(Istalk){
            MovetoTalk();
        }else{
            stop();
        }

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDe.position, Vector2.down, distance);
        RaycastHit2D girlInfo = Physics2D.Raycast(girlDe.position, Vector2.right, distance);

        if(moveingRight == true){
            girlInfo = Physics2D.Raycast(girlDe.position, Vector2.right, distance);
        }else{
            girlInfo = Physics2D.Raycast(girlDe.position, Vector2.left, distance);
        }

        if(girlInfo.rigidbody == true){
            if(girlInfo.rigidbody.name == "GirlDe"){
            }
        }
        


        if(groundInfo.collider.name == "G_Background"){
            Debug.Log(groundInfo.collider.name);
            //Debug.Log("not in obj");
            ismoving = false;
                if(moveingRight == true){
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    moveingRight = false;
                    //Debug.Log("moveleft");
                }else{
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    moveingRight = true;
                    //Debug.Log("moveright");
                }
            ismoving = true;
        }else{
                //Debug.Log("in obj");
        }
        
            

        if(Istalk && !isDiaActive && !isDiaNeed){
            Debug.Log("in obj");
            isDiaActive = true;
            isDiaNeed = true;
            talkHint.SetActive(false);
            Dialog.PrintDialog("Lv3Part2");
            GameManager.instance.stopMoving = true;
            PAnimator.SetBool("GiveTrigger", true);
            StartCoroutine(WaitAnimDone());
        }
    }


    void move(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        GAnimator.SetBool("DialogFlag", false);
    }

    void stop(){
        transform.Translate(Vector2.right * speed * Time.deltaTime * 0);
        GAnimator.SetBool("DialogFlag", true);
    }

    void MovetoTalk(){
        Debug.Log("movetotalk");
        if(transform.position.x <= 20.5f){
            transform.Translate(Vector2.right * speed * Time.deltaTime * 0);
            GAnimator.SetBool("DialogFlag", true);
            Debug.Log("stop");
        }else{
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    IEnumerator WaitAnimDone(){
        yield return new WaitWhile(() => PAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        //transform.Translate(Vector2.left * speed * Time.deltaTime);
        PAnimator.SetBool("GiveTrigger", false);
        takeTrigger = true;
    }

    IEnumerator WaitAnimDoneB(){
        yield return new WaitWhile(() => PAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        PAnimator.SetBool("TakeTrigger", false);
        GAnimator.SetBool("DialogFlag", false);
        isDiaActive = false;
        takeTrigger = false;
    }

}

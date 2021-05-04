﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GirlAction : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator Anim;
    private GameObject Hint;
    private SpriteRenderer sprite;
    public float Speed;
    private bool IsMoving;
    private GameObject Drawing;
    public bool IsCollidingSoldier;
    public Transform EndPoint;
    private float pointX;
    public bool IsArrived;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        Hint = GameObject.Find("Hint");
        Drawing = GameObject.Find("Drawing");
        pointX = EndPoint.position.x;
    }

    void Start()
    {
        IsMoving = true;
        IsCollidingSoldier = false;
        Destroy(EndPoint.gameObject);
        IsArrived = false;
    }


    void Update() 
    {
        if (!IsCollidingSoldier)
        {
            GirlMovement();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        // if (Hint.activeSelf){
        //     if (Input.GetKeyDown("space"))
        //     {
        //         LevelLoader.instance.LoadLevel("Level4Part2");
        //     }
        // }
    }

    private void GirlMovement()
    {
        // 到终点
        if (transform.position.x > pointX)
        {
            //transform.localScale = new Vector3();
            IsCollidingSoldier = true;
            Anim.enabled = false;
            IsArrived = true;
            //fade out黑屏
            LevelLoader.instance.LoadLevel("Level4Trace");
        }

        if (Input.GetKeyDown("space"))
        {
            Hint.SetActive(false);
            if (IsMoving)
                IsMoving = false;
            else
                IsMoving = true;
        }
        // 继续行走
        if(IsMoving)
        {
            Anim.enabled = true;
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }
        //停止行走
        else
        {
            Anim.enabled = false;
            sprite.sprite = Resources.Load<Sprite>("Level4/GirlHat/A_GirlHatMove00");
            rb.velocity = Vector2.zero;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Soldier2")
        {
            Debug.Log("Soldier2");
            IsCollidingSoldier = true; //撞了士兵
            Anim.enabled = true;
            Anim.SetInteger("Fall", 1);
            //fade out黑屏
            SceneManager.LoadScene("Level4Part2");
        }
        else if(collision.name == "Soldier3")
        {
            Debug.Log("Soldier3");
            IsCollidingSoldier = true;//撞了士兵
            Anim.enabled = true;
            Anim.SetInteger("Fall", 2);
            //fade out黑屏
            SceneManager.LoadScene("Level4Part2");
        }
    }


}

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
    private GameObject ReviveHint;
    private GameObject Crossfade;
    private GameObject GameOverText;

    private void Awake()
    {
        Crossfade = GameObject.Find("Crossfade");
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        Hint = GameObject.Find("Hint");
        Drawing = GameObject.Find("Drawing");
        pointX = EndPoint.position.x;
        ReviveHint = GameObject.Find("ReviveHint");
        GameOverText = GameObject.Find("GameOverText");
    }

    void Start()
    {
        IsMoving = true;
        IsCollidingSoldier = false;
        Destroy(EndPoint.gameObject);
        IsArrived = false;
        ReviveHint.SetActive(false);
        GameOverText.SetActive(false);
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
        if (ReviveHint.activeSelf){
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene("Level4Part2");
            }
        }
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
            StartCoroutine(LoadLevel());
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

    IEnumerator LoadLevel()
	{
		Crossfade.GetComponent<Animator>().SetTrigger("Start");
		yield return new WaitForSeconds(1f);
        //进入Level4Part3
        SceneManager.LoadScene("Level4Trace");
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
            StartCoroutine(ReloadLevel());
        }
        else if(collision.name == "Soldier3")
        {
            Debug.Log("Soldier3");
            IsCollidingSoldier = true;//撞了士兵
            Anim.enabled = true;
            Anim.SetInteger("Fall", 2);
            //fade out黑屏
            StartCoroutine(ReloadLevel());
        }
    }
    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(1.2f);
        Crossfade.GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);
        GameOverText.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        ReviveHint.SetActive(true);
    }


}

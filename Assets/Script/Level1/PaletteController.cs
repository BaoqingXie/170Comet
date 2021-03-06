﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteController : MonoBehaviour {
    public int i = 0;
    public float timerout = 0.0f;
    public float timerin = 0.0f;
    public float wait = 1.0f;
    public bool fadeout = false;
    public bool fadein = false; 
    public int ispickPen = 0;
    public int ispickPaper = 0;
    public static Image board;
    public static Image board2;
    public static Image board3;
    public static Image board4;
    public static Image board5;
    public static GameObject paper;
    public static GameObject pen;
    public static GameObject pen_paper;
    public static GameObject Rpaper;
    public static GameObject Rpen;
    public static GameObject QuestionMark;
    public static GameObject PickUpHint;
    public GameObject LeaveTip;
    static AudioSource[] audioSources;
    public static AudioClip drawingSound;

    public static bool isLevel1End = false;
    bool IsBubbleShowed = false;
    bool IsDialogStart = true;
    bool IsInthePen = false;
    bool IsInthePaper = false;
    bool IsCollideBed = false;

    void OnEnable(){
        audioSources = this.gameObject.GetComponents<AudioSource>();
        drawingSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_DrawingSound");
        audioSources[0].clip = drawingSound;
        board = GameObject.Find("palette1").GetComponent<Image>();
        board2 = GameObject.Find("palette2").GetComponent<Image>();
        board3 = GameObject.Find("palette3").GetComponent<Image>();
        board4 = GameObject.Find("palette4").GetComponent<Image>();
        board5 = GameObject.Find("palette5").GetComponent<Image>();
        pen_paper = GameObject.Find("BubblePenPaper");
        pen =  GameObject.Find("BubblePen");
        paper =  GameObject.Find("BubblePaper");
        Rpen =  GameObject.Find("Pen");
        Rpaper =  GameObject.Find("Paper");
        QuestionMark = GameObject.Find("GirlQMark");
        PickUpHint = GameObject.Find("PickUpHint");
        LeaveTip = GameObject.Find("LeaveTip");
        LeaveTip.SetActive(false);
        pen_paper.SetActive(false);
        paper.SetActive(false);
        pen.SetActive(false);
        PickUpHint.SetActive(false);
        QuestionMark.SetActive(true);
    }

    void Update() {
        //第一次碰撞床：第一个dialog
        if (IsCollideBed && IsDialogStart) {
            PickUpHint.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)) {
                PickUpHint.SetActive(false);
                Dialog.PrintDialog("Level1 Start"); 
                QuestionMark.SetActive(false);
                IsDialogStart = false;
            }
        }

        //第一个dialog结束出现气泡
        //Debug.Log(i);
        if (!IsDialogStart && !IsBubbleShowed && !GameManager.instance.IsDialogShow()) {
            pen_paper.SetActive(true);
            IsBubbleShowed = true;
        }

        Debug.Log("in the paper" + IsInthePaper);
        Debug.Log("in the pen" + IsInthePen);
        //pick up pen
        if(IsInthePen && Input.GetKeyDown(KeyCode.Space) && !GameManager.instance.IsDialogShow()){
            Rpen.SetActive(false);
                // if (GameObject.Find("GamePlaySystemManager").GetComponent<GamePlaySystemManager>().ispickPaper == 1) {
                if (ispickPaper == 1) {    
                    Rpaper.SetActive(true);
                    Rpaper.transform.position = Rpen.transform.position;
                    // Debug.Log("changetopen");
                    // Debug.Log(Rpaper.transform.position);
                    i--;
                    // GameObject.Find("GamePlaySystemManager").GetComponent<GamePlaySystemManager>().ispickPaper = 0;
                    ispickPaper = 0;
                    //GameManager.instance.PickPaper(false);
                }
            // GameObject.Find("GamePlaySystemManager").GetComponent<GamePlaySystemManager>().ispickPen = 1;
            ispickPen = 1;
            //GameManager.instance.PickPen(true);
            i++;
            Debug.Log("pen");
        }
        //pick up paper
        if(IsInthePaper && Input.GetKeyDown(KeyCode.Space) && !GameManager.instance.IsDialogShow()){
            Debug.Log(Rpaper.transform.position);
                    Rpaper.SetActive(false);
                    if (ispickPen == 1) {
                        Rpen.SetActive(true);
                        Rpen.transform.position = Rpaper.transform.position;
                        //Debug.Log("changetopen");
                        i--;
                        ispickPen = 0;
                        //GameManager.instance.PickPen(false);
                    }
                    ispickPaper = 1;
                    //GameManager.instance.PickPaper(true);
                    i++;
                    Debug.Log("paper");
        }

        //把笔和纸交给女孩
        if (i < 2 && !GameManager.instance.IsDialogShow()) {
            if (IsInthePen || IsInthePaper) {
                PickUpHint.SetActive(true);
            }
            //交 笔/纸
            if (IsCollideBed) {
                if (ispickPaper == 1 || ispickPen == 1) {
                    PickUpHint.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.Space)) {
                    if (ispickPaper == 1) {
                        pen_paper.SetActive(false);
                        pen.SetActive(true);
                        PickUpHint.SetActive(false);
                        Rpaper.transform.position = new Vector2(-2.12f, 1f);
                        Rpaper.GetComponent<BoxCollider2D>().enabled = false;
                        Rpaper.SetActive(true);
                        Debug.Log("givepaper");
                        ispickPaper = 0;
                        //GameManager.instance.PickPaper(false);
                    }
                    else if (ispickPen == 1) {
                        pen_paper.SetActive(false);
                        paper.SetActive(true);
                        PickUpHint.SetActive(false);
                        Rpen.transform.position = new Vector2(-2.89f, 1.6f);
                        Rpen.GetComponent<BoxCollider2D>().enabled = false;
                        Rpen.SetActive(true);
                        Debug.Log("givepen");
                        ispickPen = 0;
                        //GameManager.instance.PickPen(false);
                    }              
                }
            }
        }

        //笔和纸出现在床上
        if (i == 2) {
            if (IsCollideBed) {
                PickUpHint.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Space)) {
                    PickUpHint.SetActive(false);
                    paper.SetActive(false);
                    pen.SetActive(false);
                    if (Rpen.activeSelf) {
                        Rpaper.transform.position = new Vector2(-2.12f, 1f);
                        Rpaper.GetComponent<BoxCollider2D>().enabled = false;
                        Rpaper.SetActive(true);
                        i++;
                    } else {
                        Rpen.transform.position = new Vector2(-2.89f, 1.6f);
                        Rpen.GetComponent<BoxCollider2D>().enabled = false;
                        Rpen.SetActive(true);
                        i++;
                    }
                }
                ispickPen = 0;
                //GameManager.instance.PickPen(false);
                ispickPaper = 0;
                //GameManager.instance.PickPaper(false);
            }
        }

        //开始画板
        if (i == 3) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                fadein = true;
                GameObject.Find("Player").GetComponent<BirdInDoorMovement>().enabled = false;
                GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            if (fadein == true && timerin >= 0 && timerin < wait) {
                timerin += Time.deltaTime;
                board.color = new Color(255,255,255,timerin/wait);
                if (timerin/wait >= 1) {
                    fadein = false;
                    timerin = 0;
                    i++;
                }
            }
        }
        else if(i == 4) {
            if (fadein == false && Input.GetKeyDown(KeyCode.Space)) {
                fadeout = true;
                audioSources[0].PlayOneShot(drawingSound, 1f);
            }
            if (fadeout == true && timerout >= 0 && timerout < wait) {
                fadein = true;
                timerout += Time.deltaTime;
                board.color = new Color(1,1,1,1-(timerout/wait));
                if (timerout/wait >= 1) {
                    Destroy(board);
                    fadeout = false;
                    timerout = 0;
                }
            }
            if (fadein == true && timerin >= 0 && timerin < wait) {
                timerin += Time.deltaTime;
                board2.color = new Color(255,255,255,timerin/wait);
                if (timerin/wait >= 1) {
                    fadein = false;
                    timerin = 0;
                    i++;
                }
            }
        }
        else if (i == 5) {
            if (fadein == false && Input.GetKeyDown(KeyCode.Space)) {
                fadeout = true;
                audioSources[0].PlayOneShot(drawingSound, 1f);
            }
            if (fadeout == true && timerout >= 0 && timerout < wait) {
                fadein = true;
                timerout += Time.deltaTime;
                board2.color = new Color(1,1,1,1-(timerout/wait));
                if (timerout/wait >= 1) {
                    Destroy(board2);
                    fadeout = false;
                    timerout = 0;
                }
            }
            if (fadein == true && timerin >= 0 && timerin < wait) {
                timerin += Time.deltaTime;
                board3.color = new Color(255,255,255,timerin/wait);
                if (timerin/wait >= 1) {
                    fadein = false;
                    timerin = 0;
                    i++;
                }
            }
        }
        else if (i == 6) {
            if (fadein == false && Input.GetKeyDown(KeyCode.Space)) {
                fadeout = true;
                audioSources[0].PlayOneShot(drawingSound, 1f);
            }
            if(fadeout == true && timerout >= 0 && timerout < wait) {
                fadein = true;
                timerout += Time.deltaTime;
                board3.color = new Color(1,1,1,1-(timerout/wait));
                if (timerout/wait >= 1) {
                    Destroy(board3);
                    fadeout = false;
                    timerout = 0;
                }
            }
            if (fadein == true && timerin >= 0 && timerin < wait) {
                timerin += Time.deltaTime;
                board4.color = new Color(255,255,255,timerin/wait);
                if (timerin/wait >= 1) {
                    fadein = false;
                    timerin = 0;
                    i++;
                }
            }
        }
        else if (i == 7) {
            if (fadein == false && Input.GetKeyDown(KeyCode.Space)) {
                fadeout = true;
                audioSources[0].PlayOneShot(drawingSound, 1f);
            }
            if (fadeout == true && timerout >= 0 && timerout < wait) {
                fadein = true;
                timerout += Time.deltaTime;
                board4.color = new Color(1,1,1,1-(timerout/wait));
                if (timerout/wait >= 1) {
                    Destroy(board4);
                    fadeout = false;
                    timerout = 0;
                }
            }
            if (fadein == true && timerin >= 0 && timerin < wait) {
                timerin += Time.deltaTime;
                board5.color = new Color(255,255,255,timerin/wait);
                if (timerin/wait >= 1) {
                    fadein = false;
                    timerin = 0;
                    i++;
                }
            }
        }
        else if (i >= 8) {
            if (fadein == false && Input.GetKeyDown(KeyCode.Space)) {
                fadeout = true;
                GameObject.Find("Player").GetComponent<BirdInDoorMovement>().enabled = true;
            }
            if (fadeout == true && timerout >= 0 && timerout < wait) {
                timerout += Time.deltaTime;
                board5.color = new Color(1,1,1,1-(timerout/wait));
                if (timerout/wait >= 1) {
                    Destroy(board5);
                    Dialog.PrintDialog("Level1 End"); 
                    isLevel1End = true;
                    i++; 
                    fadeout = false;
                }
            }
        }
        if (GameObject.Find("LeaveTip") != null && Input.GetKeyDown("space") && !MainUI.instance.transform.GetChild(2).gameObject.active) {
            GameObject.Find("Player").transform.localRotation = Quaternion.Euler(0, 0, 0);
            GameObject.Find("Player").GetComponent<Animator>().enabled = true;
            GameManager.instance.stopMoving = true;
            StartCoroutine(waitFlyAnimOver());
        }
    }


    void OnTriggerEnter2D(Collider2D collision) {
    PickUpHint.SetActive(true);
    if(collision.tag == "pen"){
        IsInthePen =true;
    }

    if(collision.tag == "paper"){
        IsInthePaper =true;
    }

    if(collision.tag == "trigger"){
        IsCollideBed = true;
    }
    if (collision.name == "Window" && isLevel1End) {
        LeaveTip.SetActive(true);
    }
    }

    void OnTriggerExit2D(Collider2D collision) {
    PickUpHint.SetActive(false);
    LeaveTip.SetActive(false);
    if(collision.tag == "pen"){
        IsInthePen =false;
    }

    if(collision.tag == "paper"){
        IsInthePaper =false;
    }

    if(collision.tag == "trigger"){
        IsCollideBed = false;
    }
    }

    IEnumerator waitFlyAnimOver()
    {
        while (GameObject.Find("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
        LevelLoader.instance.LoadLevel("Level2Winter");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoverQuilt : MonoBehaviour
{
    public static Animator Anim; //animation
	public static GameObject Player;
    public static GameObject SpaceHint;
    public static GameObject KeyHint; //盖被子的提示1
    public static GameObject LeaveTip; //离开窗户提示（还没用）
    public static GameObject GirlQMark;
    public static bool isStart; //开始summer
    public static bool isFinished; //check animation是否结束

    void Start() {
    	Player = GameObject.FindGameObjectWithTag("Player");
    	Player.SetActive(false); //为啥报错？

        Anim = GetComponent<Animator>();
        //LeaveTip = GameObject.Find("LeaveTip");
    	SpaceHint = GameObject.Find("SpaceHint");
        GirlQMark = GameObject.Find("GirlQMark");
        KeyHint = GameObject.Find("KeyHint");
        //LeaveTip.SetActive(false);
        SpaceHint.SetActive(true);
        GirlQMark.SetActive(true);
        KeyHint.SetActive(false);
        isStart = true;
        isFinished = false;
    }

    void Update() {
        //开始盖被子，问号消失
        if (isStart && Input.GetKeyDown("space")) {
            SpaceHint.SetActive(false);
            GirlQMark.SetActive(false);
            KeyHint.SetActive(true);
            isStart = false;
        }
        //长按空格出发动画，松开空格从头开始
        if (KeyHint.activeSelf) {  
            if (Input.GetKey("space")) {
                switchAnim1();            
            }
        }        
    }


    void switchAnim1(){
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            //按上触发第一段
            if (!Anim.GetBool("Cover1") && !Anim.GetBool("Cover2") && !Anim.GetBool("Cover3")) {
                Anim.SetBool("Cover1", true);
                //Debug.Log(Anim.GetBool("Cover3"));
            }
            //按左触发第二段
            else if (Anim.GetBool("Cover1") && isFinished) {
                Anim.SetBool("Cover1", false);
                Anim.SetBool("Cover2", true);
                //Debug.Log(Anim.GetBool("Cover3"));
            }
            //按下触发第三段
            else if (Anim.GetBool("Cover2") && isFinished) {
                Anim.SetBool("Cover2", false);
                Anim.SetBool("Cover3", true); 
                isFinished = false;
                //Debug.Log(Anim.GetBool("Cover3"));
            }
            //按右触发接下来的关卡
            else if(Anim.GetBool("Cover3") && isFinished){
                KeyHint.SetActive(false);
                isFinished = false;
                //Player.GetComponent<PlayerMovement>().enabled = true; //玩家恢复
                Player.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)){
            //按上触发第一段
            if (!Anim.GetBool("Cover1") && !Anim.GetBool("Cover2") && !Anim.GetBool("Cover3")) {
                //Anim.SetBool("Cover1", true);
                //Debug.Log(Anim.GetBool("Cover3"));
            }
            //按左触发第二段
            else if (Anim.GetBool("Cover1")) {
                Anim.SetBool("Cover1", false);
            }
            //按下触发第三段
            else if (Anim.GetBool("Cover2")) {
                Anim.SetBool("Cover2", false);
                Anim.SetBool("Cover1", true);
            }
            //按右触发接下来的关卡
            else if(Anim.GetBool("Cover3")){
                Anim.SetBool("Cover3", false);
                Anim.SetBool("Cover2", true);
                //KeyHint.SetActive(false);
            }
        }                          
    }

    void checkAnimation(){
            isFinished = true;
    }

}

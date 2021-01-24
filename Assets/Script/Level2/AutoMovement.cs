﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    public float speed = 2f; //[1] 物体移动速度
    public static Transform Player;  // [2] 目标
    public float delta = 0.2f; // 误差值
    public Vector2 TargetPos;
    public static bool isAIMove; //玩家是否在自动移动到指定坐标
    public static bool isDialoged;
    Camera MainCamera;

    void Start() {
		MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		TargetPos = new Vector2(1.2f, -4f);
		isAIMove = false;
		isDialoged = false;
    }

    void Update () {
    	if (isAIMove){
            //如果玩家坐标和target坐标距离>误差值则继续自动移动
    		if (Vector2.Distance(Player.position, TargetPos) > delta) {
    			//Debug.Log(Player.position);
    			//玩家自动移到固定点
        		Player.position =  Vector2.MoveTowards(Player.position, TargetPos, Time.deltaTime * speed); 
        		GameObject.Find("Main Camera").GetComponent<CameraSystem>().y_min = -3f;
            	MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize,2,0.03f);//zoom in 
        	} else {
                //Debug.Log("player unmove");
        		GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;//禁止玩家移动，！！后续需要修改为可继续播放player。ainmation 
                GameObject.Find("NpcOne").GetComponent<BoxCollider2D>().enabled = false;
                // if zoom in 停止上面的工作
                if (MainCamera.orthographicSize < (2+delta)) {
                    isAIMove = false;
                    //Dialog.PrintDialog("Villager");
                    isDialoged = true;
                }
        	}
        }
        //if dialog真正结束，恢复镜头
        if(GameObject.Find("DialogBox") == null && isDialoged) {
               //GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
                GameObject.Find("Main Camera").GetComponent<CameraSystem>().y_min = 0;
                MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize,5,0.03f);
                if (MainCamera.orthographicSize > (5-delta)) {
            		MainCamera.orthographicSize = 5;
                	NpcController.NoticeMark.SetActive(true);
                	isDialoged = false;
            	}
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
    	if (other.tag.CompareTo("Player") == 0) {
    		Player = GameObject.Find("Player").GetComponent<Transform>();
    		isAIMove = true;
            GameObject.Find("QuestionMark").GetComponent<SpriteRenderer>().enabled = false;
    	}
    }
}
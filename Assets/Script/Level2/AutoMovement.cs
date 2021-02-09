//By Huazhen Xu
//Level2part1之后不在复用

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    public float speed = 2f; //[1] 物体移动速度
    public static Transform Player;  // [2] 目标
    public float delta = 0.01f; // 误差值
    public static bool isAIMove; //玩家是否在自动移动到指定坐标
    public static bool isPlaCanFly; //在playermovement引用
    bool isDialoged;
    Camera MainCamera;
    Vector2 TargetPos;
    Vector2 Direction;

    void Start() {
        Player = GameObject.Find("Player").GetComponent<Transform>();
		MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		TargetPos = new Vector2(-1f, -2.35f); //小鸟需要到达的位置
        Direction = new Vector2(1f,0f); //向右飞
		isAIMove = false;
		isDialoged = false;
        isPlaCanFly = true;
    }

    void Update () {
	    if (isAIMove){
            //如果玩家坐标和target坐标距离>误差值则继续自动移动
		    if (Vector2.Distance(Player.position, TargetPos) > delta) {
			    //Debug.Log(Player.position);
			    //玩家自动移到固定点
    		    Player.position =  Vector2.MoveTowards(Player.position, TargetPos, Time.deltaTime * speed); 
    		    GameObject.Find("Main Camera").GetComponent<CameraSystem>().y_min = -2f;
    	    } else {
                Player.position = TargetPos;
                Direction.y = -4.3f;
                //zoom in z
                MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize,3,0.03f);
                Debug.Log("Player unmove");
                GameObject.Find("NpcOne").GetComponent<BoxCollider2D>().enabled = false;
                // if zoom in 停止上面的工作
                if (MainCamera.orthographicSize < (3 + delta)){
                    MainCamera.orthographicSize = 3;
                    //地上静止
                    GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;//禁止玩家移动
                    FindObjectOfType<PlayerAnimation1>().TimelineAnimation();
                    isAIMove = false;
                    Dialog.PrintDialog("Villager");
                    NpcController.Npc01Animator.enabled = true;
                    isDialoged = true;
                }
    	    }
        }
        //if dialog真正结束，恢复镜头
        if(GameObject.Find("DialogBox") == null && isDialoged) {
            NpcController.Npc01Animator.Play("Npc01Turn");
            NpcController.Npc02Animator.enabled = true;
            //GameObject.Find("Main Camera").GetComponent<CameraSystem>().y_min = 0;
            //MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize,5,0.03f);
            //if (MainCamera.orthographicSize > (5 - delta)) {
                //恢复动画
                // //GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
    		    //MainCamera.orthographicSize = 5;
                NpcController.isToStartTimeline = true;
        	    //NpcController.NoticeMark.SetActive(true);
        	    isDialoged = false;
                isPlaCanFly = true;
    	    //z}
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
	    if (other.tag.CompareTo("Player") == 0) {
            isPlaCanFly = false;
		    isAIMove = true;
            GameObject.Find("QuestionMark").GetComponent<SpriteRenderer>().enabled = false;
            if (Player.position.x > TargetPos.x){
                Direction.x = -1;
            }
            FindObjectOfType<PlayerAnimation1>().SetDirection(Direction);
	    }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AIMove : MonoBehaviour
{
    public float speed = 2f; //[1] 物体移动速度
    public Transform Player;  // [2] 目标
    public float delta = 0.2f; // 误差值
    public Vector2 TargetPos = new Vector2(1f, -3.5f);
    public bool isAIMove = false;
    public bool isTimelineEnd = false;
    float Size = 2;
    Camera MainCamera;

    void Start() {
		MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update () {
    	if (isAIMove){
            //如果玩家坐标和target坐标距离>误差值则继续自动移动
    		if (Vector2.Distance(Player.position, TargetPos) > delta) {
    			//Debug.Log(Player.position);
    			//玩家自动移到固定点
        		Player.position =  Vector2.MoveTowards(Player.position, TargetPos, Time.deltaTime * speed); 
        	} else {
                //Debug.Log("player unmove");
        		GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;//禁止玩家移动，！！后续需要修改为可继续播放player。ainmation 
        		GameObject.Find("Main Camera").GetComponent<CameraSystem>().y_min = -3f;//镜头最低位
        		MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize,Size,0.03f);//zoom in 
                GameObject.Find("NpcOne").GetComponent<BoxCollider2D>().enabled = false;
                // if zoom in 停止上面的工作
                if (MainCamera.orthographicSize < (Size+delta)) {
                    isAIMove = false;
                }
        	}
        }
        //if动画+dialog真正结束，恢复镜头
        if(GameObject.FindObjectOfType<PlayableDirector>() == null && GameObject.Find("DialogBox") == null) {
                //Debug.Log("Cant find director");
                isTimelineEnd = true;
                GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
                GameObject.Find("Main Camera").GetComponent<CameraSystem>().y_min = 0;
                MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize,5,0.03f);
                MainCamera.orthographicSize = 5;
                //if(GameObject.Find("DialogBox") == null) {
                    GameObject.Find("NoticeMark").GetComponent<SpriteRenderer>().enabled = true;
                //}
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

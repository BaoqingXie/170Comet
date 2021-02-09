//By Huazhen Xu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NpcController : MonoBehaviour
{
    public static GameObject Player; 
    public static GameObject NoticeMark; 
    public static GameObject SadFace;   
   	public static GameObject Flower; //小花
    public static GameObject RhythmGame; //小花
    public static GameObject Horse;
    public static GameObject Npc01;
    public static GameObject Npc03;
    public static GameObject NpcTwoTimeline; //胡子男拿花的timeline
    public static GameObject VillagerTimeline; //骑马过来的timeline
    public static bool isToStartTimeline = false; //开始cg ->automovent.cs调用启动
    public static bool isPlayerMove = false;
    public static Animator Npc02Animator;
    public static Animator Npc01Animator;
    //public static Animator Npc02FallAnimator;
    public static GameObject NpcTwoGroup; //npc对话（情景1）中的npc2素材
    public static bool isCameraChanged = false;
    bool isDialoged = false;
    Vector2 Npc02OriPos;
	Vector2 Npc02TransPos;
    Camera MainCamera;

	void Awake() {
        Player = GameObject.Find("Player");
        Npc02TransPos = GameObject.Find("NpcTwoPick").GetComponent<Transform>().position;
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Npc02OriPos = this.GetComponent<Transform>().position;
        Npc02Animator = this.GetComponent<Animator>();
        Npc01 = GameObject.Find("NpcOne");
        Npc01Animator = Npc01.GetComponent<Animator>();
        Npc03 = GameObject.Find("NpcThree");
        NpcTwoGroup = GameObject.Find("G_NpcTimeline");
        SadFace = GameObject.Find("SadFace");
        NoticeMark = GameObject.Find("NoticeMark");
        VillagerTimeline = GameObject.Find("VillagerTimeline");
        NpcTwoTimeline = GameObject.Find("NpcTwoTimeline");
        RhythmGame = GameObject.Find("RhythmGame");
        Flower = GameObject.Find("Flower");
        Horse = GameObject.Find("Horse");
        NoticeMark.SetActive(false);
        VillagerTimeline.SetActive(false);
        NpcTwoTimeline.SetActive(false);
        NpcTwoGroup.SetActive(false);
        Flower.SetActive(false);
        Horse.SetActive(false);
        SadFace.SetActive(false);
        Npc03.SetActive(false);
        //RhythmGame.SetActive(false);
        Npc01Animator.enabled = false;
        Npc02Animator.enabled = false;
        //Npc02FallAnimator.enabled = false;
    }

    void Update() {
    //冬天
        //是否完成npc和骑马情节(此为情节1)
        if (!GamePlaySystemManager.isLevel2NpcPlot) {
            if (isToStartTimeline) {
                isPlayerMove = true;
                //GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
                GameManager.ins.GetDirector(VillagerTimeline.GetComponent<PlayableDirector>());
                VillagerTimeline.SetActive(true); //播放cg动画
                if(VillagerTimeline.GetComponent<PlayableDirector>().enabled == true) {
                    if (!GameManager.isTimeline) {
                        if (!isCameraChanged) {
                            Horse.SetActive(true);
                            NpcTwoGroup.SetActive(true);
                            this.GetComponent<SpriteRenderer>().enabled = false;
                            MainCamera.depth = -3;
                            GameManager.isTimeline = true;
                        }
                        // else {
                        //     GameObject.Find("Main Camera").GetComponent<CameraSystem>().y_min = 0;
                        //     MainCamera.orthographicSize = 5;
                        // }
                    }
                }
                //完成马的timeline，玩家恢复移动，npc2跌倒+哭
                else {
                    MainCamera.depth = -1;
                	NpcTransPos(true);
                    Npc03.SetActive(true);
                    this.GetComponent<SpriteRenderer>().enabled = true;
                    if (GameObject.Find("DialogBox") == null) {
                        isPlayerMove = false;
                        Player.GetComponent<PlayerMovement>().enabled = true;
                        GamePlaySystemManager.isLevel2NpcPlot = true;
                        //NoticeMark.SetActive(false);
                        isToStartTimeline = false;
                        Npc02Animator.enabled = false;
                    }
                }
            }
        }
        //完成情节1，未完成情节2
        else if (!GamePlaySystemManager.isLevel2WinterEnd) {
            //没完成音游
            if (!RhythmGame.GetComponent<RhythmScore>().IsGameEnded) {
            	NpcTransPos(true);
                Debug.Log("对话后摔倒");
            }
            // 完成音游 拿出花 结束情节2
            else {
                if (!isDialoged) {
                    Player.GetComponent<PlayerMovement>().enabled = false;
                    Dialog.PrintDialog("Lv2P1StandUp");
                    Debug.Log("拿花对话");

                    //之後插入拿出花的動畫

                    NpcTransPos(false);
                    isDialoged = true;
                }
                if(GameObject.Find("DialogBox") == null) {
                    Player.GetComponent<PlayerMovement>().enabled = true;
                    RhythmGame.GetComponent<RhythmScore>().IsGameEnded = false;

                    //第二關冬天任務結束
                    GamePlaySystemManager.isLevel2WinterEnd = true;
                    Debug.Log("任务结束");
                }
            }

        }
        //完成音游是否拿花--若返回房间出来后的场景
        else if (GamePlaySystemManager.isLevel2WinterEnd) {
                Npc01.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/GreenManTurn/A_Npc01Turn04");
                Npc03.SetActive(true);
            //拿了花
            if (GamePlaySystemManager.isLevel2Flower) {
                this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManTurn/A_Npc02Turn04");
            }
            //没拿花
            else {
                NpcTransPos(false);
            }
        }
    //夏天
        //完成part1冬天进入part2
        else if (GamePlaySystemManager.isLevel1Mission2End) {
            
        }
    }


    public void NpcTransPos(bool isNeedTrans) {
    	if (isNeedTrans) {
            Flower.SetActive(false); 
            SadFace.SetActive(true);
    		this.GetComponent<Transform>().position = Npc02TransPos;
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManPick/A_BrownMan_PickHat_05");
    	} 
    	else {
    		this.GetComponent<Transform>().position = Npc02OriPos;
		    this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManTurn/A_Npc02Turn04");
            Flower.SetActive(true); 
    	}

    }

    public void CameraTrans(bool isNeedChanged){
        isCameraChanged = isNeedChanged;
        GameObject.Find("Main Camera").GetComponent<CameraSystem>().y_min = 0;
        MainCamera.orthographicSize = 5;
        //Debug.Log("isCamerChanged = "+isCameraChanged);
    }

}

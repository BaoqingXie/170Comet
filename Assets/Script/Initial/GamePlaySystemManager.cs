//By Huazhen Xu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlaySystemManager : MonoBehaviour
{
    public static bool isLevel1Mission1End = false; //捡东西情节
    public static bool isLevel2NpcPlot = false; //Npc对话至播放跑马动画的情节->Npccontroller.cs中变值
    public static bool isLevel2WinterEnd = false; //完成音游情节->Npccontroller.cs中变值
    public static bool isLevel2Flower = false; //有无捡花 ->FlowerDisappear.cs中变值
    public static bool isLevel1Mission2End = false; //返回房间对话情节->GirlQuestion.cs变值
    public int ispickPen = 0; //返回房间对话情节->GirlQuestion.cs变值
    public int ispickPaper = 0; //返回房间对话情节->GirlQuestion.cs变值

    private bool isLevelExit1;
    private bool isPass;
    
    // Start is called before the first frame update
    void Awake() {
        DontDestroyOnLoad(this);
        isLevelExit1 = false;
        isPass = false;
    }

    // Update is called once per frame
    void Update() {

    	//跳过关卡 按P
    	if (SceneManager.GetActiveScene().name == "Level1") {
    		if (Input.GetKey(KeyCode.P)) {
    			isLevel1Mission1End = true;
    			isPass = true;
    		}
    	}
        //完成以后需要删除↑

        if (SceneManager.GetActiveScene().name != "Level1") {
            if (!isLevel1Mission1End) {
        	   GameObject.Find("Player").GetComponent<PaletteController>().enabled = false;
            } 
            else {
                Destroy(GameObject.Find("Player").GetComponent<PaletteController>());
            }
        //in level1
        } else {
            // 首次
            if (!isLevelExit1 && !isPass) {
        	    GameObject.Find("Player").GetComponent<PaletteController>().enabled = true;
                isLevel1Mission1End = PaletteController.isLevel1End;
            //if mission one end, destroy missionone's gameobject after loading in level1 scene agian再次进入销毁任务1的东西
            }
            else {
                Destroy(GameObject.Find("MissionOne"));
            }
        }

        if (SceneManager.GetActiveScene().name == "Level2") {
            isLevelExit1 = true;
            if (isLevel2NpcPlot) {
                Destroy(GameObject.Find("G_NpcPlot"));
                Destroy(GameObject.Find("NpcOne").GetComponent<AutoMovement>());
                // Debug.Log("删除npc对话");
                //删除音游相关
                if (isLevel2WinterEnd) {
                    Destroy(GameObject.Find("SadFace"));
                    //删除花朵部分
                    if(isLevel2Flower) {
                        Destroy(GameObject.Find("Flower"));
                    }
                }
            } 
            
        }


    }
}

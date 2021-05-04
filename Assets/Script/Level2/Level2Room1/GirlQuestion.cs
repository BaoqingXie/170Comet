//By Huazhen Xu
//完成音游(isLevel2WinterEnd==true)进入房间时使用
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GirlQuestion : MonoBehaviour
{
    public static GameObject Flower;
    public static GameObject QMark;
    public static bool isRoomStart = false;
    public static bool isRoomFlower = false;
    bool isDiaActive = false;
	private GameObject Crossfade;
	private GameObject Hint;

	void Awake() {
		Crossfade = GameObject.Find("EndOnly");
		QMark = GameObject.Find("GirlQMark");
		Flower =  GameObject.Find("Flower");
		Flower.SetActive(false);
		Hint = GameObject.Find("PickUpHint");
		Hint.SetActive(false);
		if (GameManager.instance.isLv2WinterEnd) {
        	SoundManager.playBgm(4);
            isRoomStart = true;
			QMark.SetActive(true);
            if (GameManager.instance.isLv2Flower) {
                isRoomFlower = true;
            } else {
                isRoomFlower = false;
            }
        }
		else {
			QMark.SetActive(false);
		}
	}

	void Update(){
		if (isDiaActive && GameObject.Find("DialogBox") == null) {
			//SceneManager.LoadScene("Level2SummerRoom"); // 夏天：按空格call scene3 in SceneTransition
			//黑屏
			LevelLoader.instance.LoadLevel("Level2SummerRoom");
			
		}
	}

	void OnTriggerStay2D(Collider2D collision) {
		if(isRoomStart && collision.tag == "Player" && !isDiaActive) {
			if (Input.GetKeyDown("space")) {
	        	QMark.SetActive(false);
	        	if (isRoomFlower) {
	        		Flower.SetActive(true);
	        		Dialog.PrintDialog("Lv2P2Flower");
					GameManager.instance.GiveFlower();
					GameObject.Find("Player").GetComponent<BirdInDoorMovement>().currentState = BirdInDoorMovement.BirdsState.STATIC;
	        	} 
	        	else {
	        		Dialog.PrintDialog("Lv2P2Room");
	        	}
	        	isDiaActive = true;
		    }
	    }
	}
}

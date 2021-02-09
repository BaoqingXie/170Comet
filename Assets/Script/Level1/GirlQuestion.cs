//By Huazhen Xu
//完成音游(isLevel2WinterEnd==true)进入房间时使用
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlQuestion : MonoBehaviour
{
    public static GameObject Flower;
    public static GameObject QMark;
    bool isDiaActive = false;

	void Awake() {
		QMark = GameObject.Find("GirlQMark");
		Flower =  GameObject.Find("Flower");
		Flower.SetActive(false);
	}

	void Update(){
		//是否完成音游触发level1情节2
		if (GamePlaySystemManager.isLevel1Mission1End && !isDiaActive) {
			if (!GamePlaySystemManager.isLevel2WinterEnd) {
				QMark.SetActive(false);
			}
			else {
				QMark.SetActive(true);
			}
		}
	}

	void OnTriggerStay2D(Collider2D collision) {
		if(GamePlaySystemManager.isLevel2WinterEnd && collision.tag == "Player" && !isDiaActive) {
			if (Input.GetKeyDown("space")) {
	        	QMark.SetActive(false);
	        	if (GamePlaySystemManager.isLevel2Flower) {
	        		Flower.SetActive(true);
	        		Dialog.PrintDialog("Lv2P2Flower");
	        	} 
	        	else {
	        		Dialog.PrintDialog("Lv2P2Room");
	        	}
	        	isDiaActive = true;
	        	GamePlaySystemManager.isLevel1Mission2End = true;
		    }
	    }
	}
}

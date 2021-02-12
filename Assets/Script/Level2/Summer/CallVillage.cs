using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CallVillage : MonoBehaviour
{
	public GameObject LeaveTip;

	void Start() {
		if (SceneManager.GetActiveScene().name == "Level2SummerRoom") {
			LeaveTip = GameObject.Find("LeaveTip");
			LeaveTip.SetActive(false);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
    	if (SceneManager.GetActiveScene().name == "Level2SummerRoom" && other.tag.CompareTo("Player") == 0 && GameObject.Find("DialogBox") == null) {
			LeaveTip.SetActive(false);
		}
	}

    void OnTriggerEnter2D(Collider2D other) {
    	if (SceneManager.GetActiveScene().name == "Level2SummerRoom" && other.tag.CompareTo("Player") == 0 && GameObject.Find("DialogBox") == null) {
			LeaveTip.SetActive(true);
		}
		if (SceneManager.GetActiveScene().name == "Level2" && other.tag.CompareTo("Player") == 0) {
			Destroy(GameObject.Find("Window"));
		}
	}
}

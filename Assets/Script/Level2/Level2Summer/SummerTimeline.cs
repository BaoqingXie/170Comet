using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SummerTimeline : MonoBehaviour
{
	public static GameObject player; 
	public static GameObject npc01;
	public static GameObject npc02;
	public static GameObject redSoldier;
	public static GameObject greenSoldier;
	public static Animator npc01Anim;
	public static Animator npc02Anim;

	public static GameObject NpcTwoTimeline;

    void Awake() {
        player = GameObject.Find("Player");
        npc01 = GameObject.Find("NpcOne");
        npc02 = GameObject.Find("NpcTwo");
        redSoldier = GameObject.Find("RedSoldier");
        greenSoldier = GameObject.Find("GreenSoldier");
        npc01Anim = npc01.GetComponent<Animator>();
        npc02Anim = npc02.GetComponent<Animator>();
        NpcTwoTimeline = GameObject.Find("NpcTwoTimeline");
        redSoldier.SetActive(false);
        greenSoldier.SetActive(false);
        NpcTwoTimeline.SetActive(false);
    }

    void Start() {
        TimelineGameManager.GetDirector(NpcTwoTimeline.GetComponent<PlayableDirector>());
        NpcTwoTimeline.SetActive(true);
        TimelineGameManager.isTimeline = true;
        player.GetComponent<BirdOutDoorMovement>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(NpcTwoTimeline.GetComponent<PlayableDirector>().enabled == false) {
            if (!TimelineGameManager.isTimeline) {
                player.GetComponent<BirdOutDoorMovement>().enabled = true;
                npc01.SetActive(false);
                npc02.SetActive(false);
                greenSoldier.GetComponent<Transform>().position = new Vector2(22.2f, greenSoldier.GetComponent<Transform>().position.y);
                redSoldier.GetComponent<Transform>().position = new Vector2(27.2f, redSoldier.GetComponent<Transform>().position.y);
                redSoldier.SetActive(true);
                greenSoldier.SetActive(true);
                LevelLoader.instance.LoadLevel("Level2Summer");
	        }
	    }
    }

    public void NpcWalk() {
        redSoldier.SetActive(true);
        greenSoldier.SetActive(true);
    }
}

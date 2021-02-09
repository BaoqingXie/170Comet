using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallStaff : MonoBehaviour
{
    public static GameObject RhythmGame;
    public static GameObject NoteHolder;
    Vector3 originalPos;
    public bool CanHitSadFace = true;
    public bool IsInFace = false;
    public static GameObject SpaceHint;

    void Start() {
        RhythmGame = GameObject.FindGameObjectWithTag("RhythmGame");
        NoteHolder = GameObject.Find("NoteHolder");
        //五线谱在碰sadFace之前不可见
        RhythmGame.SetActive(false);
        originalPos = new Vector3(NoteHolder.transform.position.x, NoteHolder.transform.position.y, NoteHolder.transform.position.z);
        SpaceHint = GameObject.Find("SpaceHint");
        SpaceHint.SetActive(false);
    }


    void Update() {
        //分数未达到，play again
        if (RhythmGame.GetComponent<RhythmScore>().PlayAgain) {
            //五线谱消失，重新控制小鸟
            RhythmGame.SetActive(false);
            GameObject.Find("Player").GetComponent<Rigidbody2D>().isKinematic = false;
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
            CanHitSadFace = true;
            Debug.Log("PlayAgain!");
            RhythmGame.GetComponent<RhythmScore>().PlayAgain = false;
        }

        if (IsInFace  && CanHitSadFace) {
            SpaceHint.SetActive(true);
            //按空格开始 
            if(Input.GetKeyDown("space")){
                SpaceHint.SetActive(false);
                CanHitSadFace = false;
                //notes结束后回到原位
                NoteHolder.transform.position = originalPos;
                //不能control小鸟
                GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
                GameObject.Find("Player").GetComponent<Rigidbody2D>().isKinematic = true;
                GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                //出现五线谱
                RhythmGame.SetActive(true);
            }
        }else{
            SpaceHint.SetActive(false);
        }
    }

    //当bird撞上sadFace时触发五线谱
    void OnTriggerEnter2D(Collider2D other) {
    	if (other.tag.CompareTo("Player") == 0) {
            IsInFace = true;
    	}
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag.CompareTo("Player") == 0){
            IsInFace = false;
        }
    }
}

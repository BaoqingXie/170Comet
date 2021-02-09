using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmScore : MonoBehaviour
{
    //score setting
    public static int currentScore;
    private static int scorePerNote = 100;
    public static int totalScore;
    public int totalS = 0;
    public bool IsGameEnded;
    public bool PlayAgain;

    public static GameObject RhythmGame;
    public static GameObject SadFace;

    void OnEnable()
    {
        IsGameEnded = false;
        RhythmGame = GameObject.FindGameObjectWithTag("RhythmGame");
        SadFace = GameObject.FindGameObjectWithTag("SadFace");
        currentScore = 0;
        totalScore = 0;
        PlayAgain = false;
    }

    void Update()
    {
        totalS = totalScore;

        //过关 - 得到400分
        if (currentScore >= 400 && totalScore == 500)
        {
            //stop rhythm game & 五线谱消失
            SadFace.SetActive(false);
            RhythmGame.SetActive(false);
            //continue controlling the bird
            GameObject.Find("Player").GetComponent<Rigidbody2D>().isKinematic = false;
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
            IsGameEnded = true;
        }
        //没过关 - 未得到400分
        else if (currentScore < 400 && totalScore == 500)
        {
            PlayAgain = true;
        }


    }

    public static void NoteHit()
    {
        currentScore += scorePerNote;
        //totalScore += scorePerNote;
    }

    public static void NotePass()
    {
        totalScore += scorePerNote;
    }
}
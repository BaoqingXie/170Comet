using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    public static bool isTimeline = false; //timeline开始or结束 ->NpcController.cs调用-变值
    //private static bool isEnd = false;
    private static bool isPaused = false;

   //public GameObject DialogBox = GameObject.Find("DialogBox");

    PlayableDirector activeDirector;

    void Awake()
    {
        ins = this;
        //dialogueLineText.gameObject.SetActive(false);
        //activeDirector = GameObject.FindObjectOfType<PlayableDirector>();
    }

    void Update()
    {
        
        //Debug.Log(isTimeline);
    	// //Debug.Log(activeDirector);
     //    if (activeDirector != null) {
     //        isTimeline = true;
     //    }
        if (isTimeline && Input.GetKeyDown(KeyCode.Space) && isPaused)
        {
            ResumeTimeline();
        }
    }

    // public void StartPlay() {
    //     Debug.Log(activeDirector);
    //     isTimeline = true;
    //     //activeDirector.Play();
    // }

    //设置UI文字
    public void SetDialogue(string lineOfDialogue)
    {
        //isEnd = false;
        Dialog.PrintDialog(lineOfDialogue);
        // dialogueLineText.text = lineOfDialogue;

        // dialogueLineText.gameObject.SetActive(true);
    }
    public void GetDirector(PlayableDirector _dic){
        activeDirector = _dic;
    }

    //暂停TimeLine
    public void PauseTimeline(PlayableDirector whichOne)
    {
        activeDirector = whichOne;
        isPaused = true;
        activeDirector.Pause();
    }

    //恢复播放TimeLine
    public void ResumeTimeline()
    {
        activeDirector.Resume();
        Dialog.HideDialog();
        isPaused = false;
    }
    
    public void EndTimeline(bool isEnd){
        isTimeline = isEnd;
    }
    // public void DestroyTimeline() {
    //     isTimeline = false;
    //     isEnd = true;
    //     Destroy(GameObject.FindObjectOfType<PlayableDirector>());
    // }

    // public static bool EndTimeline() {
    //     if (!isTimeline && isEnd) {
    //         return true;
    //     }
    //     else {
    //         return false;
    //     }
    // }
}

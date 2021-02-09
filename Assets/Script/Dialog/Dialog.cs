//By Huazhen Xu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Dialog : MonoBehaviour
{
    public static Text dialogText;
    public static GameObject dialog;
    public TextAsset textFile;

    public static int index;
    public static List<string> AllTextlist = new List<string>();
    public static List<string> CurrentTextlist = new List<string>();
    public static List<string> dialogList;
    public static bool startTyping;
    public static bool isTyping;
    public static string Line;
    public static int j;
    public float textspeed;
   
    bool isTimeline = false; //是否cg动画播放中

    void Awake() {
        //print("start" + textFile.text);
        var linetext = textFile.text.Split('\n');
        index = 2;
        foreach (var line in linetext) {
            AllTextlist.Add(line);
        }
        dialog = GameObject.Find("DialogBox");
        dialogText = GameObject.Find("DialogText").GetComponent<Text>();
        dialog.SetActive(false);
        isTyping = false;
    }

    void Update() {
        if (startTyping) {
            //Stops the previous sentence
            StopAllCoroutines(); 
            StartCoroutine(Typing(Line));
        }
        //检测是否动画播放，用SignalAsset变值
        if (GameObject.Find("GameManager") != null && GameManager.isTimeline == true){
            isTimeline = true;
        } else {
            isTimeline = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isTimeline) {
        	if (isTyping) {
                StopAllCoroutines(); 
                dialogText.text = "";
                dialogText.text = Line;
                isTyping = false;
            } 
            else if (!NextPage()) {
                dialogText.text = "";
                dialog.SetActive(false);
            }
        }
    }

    //check dialog has next sentence or not
    public static bool NextPage() {
    	if (index > CurrentTextlist.Count - 1) {
    			index = 2;
    			//Debug.Log("no more tips");
                // dialog.SetActive(false);
                dialogText.text = "";
                return false;
            }
            Line = CurrentTextlist[index].Replace("=", "\n");
            startTyping = true;
            index ++;
            return true;
    }

    //Call for starting dialog
    public static void PrintDialog(string objName) {
        Debug.Log("PrintDialog");
        j = 0;
    	CurrentTextlist.Clear();
        //if (AllTextlist.Contains(objName)) {
            // int j = AllTextlist.IndexOf(objName);
            //代替IndexOf
            for (int i  = 0; i < AllTextlist.Count; i++) {
                if (string.Compare(AllTextlist[i], objName, StringComparison.Ordinal) == 0) {
                    j = i;
                    break;
                }
            }
            //代替IndexOf

            //while (AllTextlist[j].CompareTo("---") != 0) {
            //代替.CompareTo
            while (string.Compare(AllTextlist[j], "---", StringComparison.Ordinal) != 0) {
            //代替.CompareTo
                CurrentTextlist.Add(AllTextlist[j]);
                j++;
            }
        //}
        Line = CurrentTextlist[1];
        startTyping = true;
        dialog.SetActive(true);
        //print("Set dialog active, index: " + index + ", list length: " + textlist2.Count);
    }

    public static void HideDialog() {
         dialog.SetActive(false);
    }

    //Typewriter effect
    IEnumerator Typing(string sentences) {
         startTyping = false;
         isTyping = true;
         dialogText.text = "";
         //print("sentences : " + sentences);
         foreach(char letter in sentences.ToCharArray()) {
            dialogText.text += letter;
            yield return new WaitForSeconds(textspeed);
         }
         isTyping = false; 
    }
    

}

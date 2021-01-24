﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public static Text dialogText;
    public static GameObject dialog;
    public TextAsset textFile;

    public static int index;
    public static List<string> textlist = new List<string>();
    public static List<string> textlist2 = new List<string>();
    public static List<string> dialogList;
    public static bool startTyping;
    public static bool isTyping;
    public static string Line;
    public float textspeed;
   
    bool isTimeline = false; //是否开始动画播放

    void Awake() {
        //print("start" + textFile.text);
        var linetext = textFile.text.Split('\n');
        index = 2;
        foreach (var line in linetext) {
            textlist.Add(line);
         //           print("read " + line );
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
        //检测是否动画播放
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
    	if (index > textlist2.Count - 1) {
    			index = 2;
    			//Debug.Log("no more tips");
                // dialog.SetActive(false);
                dialogText.text = "";
                return false;
            }
            Line = textlist2[index].Replace("=", "\n");
            startTyping = true;
            index ++;
            return true;
    }

    //Call for starting dialog
    public static void PrintDialog(string objName) {
    	textlist2.Clear();
        if (textlist.Contains(objName)) {
            int j = textlist.IndexOf(objName);
            //int j = i;
            while (textlist[j].CompareTo("---") != 0) {
                textlist2.Add(textlist[j]);
                // print(textlist[j]);
                j++;
            }
        }
        Line = textlist2[1].Replace("=", "\n");
        //dialogText.text = "haha";
        startTyping = true;
        dialog.SetActive(true);
        //print("Set dialog active, index: " + index + ", list length: " + textlist2.Count);
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
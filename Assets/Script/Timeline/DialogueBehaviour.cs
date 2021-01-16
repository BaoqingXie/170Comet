﻿using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
    //播放到该clip时，UI要显示的文字
    public string dialogueLine;

    [Tooltip("播放完该clip后，是否需要暂停TimeLine")]
    public bool PressToPause = false;

    /// <summary>
    /// 未播放该clip？  false为未播放
    /// </summary>
    bool clipPlayed = false;

    /// <summary>
    /// 播放完该clip后是否需要暂停。false为不需要。
    /// 缓存的bool变量，会在时间轴运行到该clip时，根据 hasToPause 值赋值。
    /// </summary>
    bool pauseScheduled = false;

    /// <summary>
    /// 得到当前 PlayableDirector
    /// </summary>
    PlayableDirector director;

    public override void OnPlayableCreate(Playable playable) {   
        director = playable.GetGraph().GetResolver() as PlayableDirector;
        //Debug.Log("OnPlayableCreate" + "director");
    }


    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        //Debug.Log("ProcessFrame");

        //若还未播放该clip，当前clip的权重>0
        //效果是：时间轴刚进入该clip时执行一次
        if (!clipPlayed && info.weight > 0f) {
            //Debug.Log("ProcessFrame true");
            GameManager.ins.SetDialogue(dialogueLine);

            //检测播放完该clip后是否需要暂停，并赋值
            if (Application.isPlaying && PressToPause) {
                pauseScheduled = true;
            }

            clipPlayed = true;
        }
        //playable end
        if (director.state != PlayState.Playing) {
            Debug.Log("timeline end");
            GameManager.ins.DestroyTimeline();
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info) {
        //因为该回调会在刚运行TimeLine、执行完clip时都会执行一次。
        //因此不能直接判断 pressToPause ，否则刚运行TimeLine就给暂停了。

        if (pauseScheduled) {
            pauseScheduled = false;
            GameManager.ins.PauseTimeline(director);
        }
    }
}
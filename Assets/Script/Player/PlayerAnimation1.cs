﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation1 : MonoBehaviour
{
    private Animator anim;

    public string[] staticDirections = {"Static_N", "Static_NW", "Static_W", "Static_SW", "Static_S", "Static_SE", "Static_E,", "Static_NE"};
    public string[] runDirections = {"Run_N", "Run_NW", "Run_W", "Run_SW", "Run_S", "Run_SE", "Run_E", "Run_NE"};
    public string[] FlyAnimation = {"Fly_L", "Fly_R"};
    public string[] FlyInAirAnimation = {"FlyStatic_L", "FlyStatic_R"}; 
    private int FaceDirection = 0; //1 right 0 left
    

    int lastDirection;
    private void Awake(){
        anim = GetComponent<Animator>();
    }


    public void SetDirection(Vector2 _direction){
        string[] directionArray = null;
        if(SceneManager.GetActiveScene().name == "Level2"){
            Debug.Log("play lvl2 animation");

            // if(_direction.magnitude < 0.01 || _direction.y != 0){
            //     if(transform.position.y <= -4.3){
            //         anim.Play(FlyInAirAnimation[FaceDirection]);
            //     }else{
            //         anim.Play(FlyAnimation[FaceDirection]);
            //     }
            // }else{
                if(transform.position.y <= -4.3){
                    //Debug.Log("on the grd");
                    if(_direction.x>0){
                        anim.Play(FlyInAirAnimation[1]);
                        FaceDirection = 1;
                    }else if(_direction.x<0){
                        anim.Play(FlyInAirAnimation[0]); 
                        FaceDirection = 0;
                    }
                }else{
                    //Debug.Log("fly");
                    if(_direction.x>0){
                        anim.Play(FlyAnimation[1]);
                        FaceDirection = 1;
                    }else if(_direction.x<0){
                        anim.Play(FlyAnimation[0]); 
                        FaceDirection = 0;
                    }else if(_direction.x == 0){
                        anim.Play(FlyAnimation[FaceDirection]); 
                    }
                }
            //}
       
        }else{
            Debug.Log("play lvl1 animation");
            if(_direction.magnitude < 0.01){
                directionArray = staticDirections;
                Debug.Log("static");
            }else{
                directionArray = runDirections;
                lastDirection = DirectionToIndex(_direction);
            }
            Debug.Log(lastDirection);
        anim.Play(directionArray[lastDirection]);
        }
    }

    public int DirectionToIndex(Vector2 _direction){
        Vector2 norDir = _direction.normalized;

        float step = 360/8;
        float offset = step/2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += offset;
        if(angle < 0){
            angle +=360;
        }

        float stepCount = angle/step;
        return Mathf.FloorToInt(stepCount);
    }

    public void Level2DefultAnimation(){
        anim.Play(FlyAnimation[1]);
    }

    public void Level1DefultAnimation(){
        anim.Play(staticDirections[4]);
    }
    
}

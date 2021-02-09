using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation2 : MonoBehaviour
{
    private Animator anim;
    public string[] FlyAnimation = {"Fly_L", "Fly_R"};
    public string[] FlyInAirAnimation = {"FlyStatic_L", "FlyStatic_R"}; 
    private int FaceDirection = 0; //1 right 0 left
    

    int lastDirection;
    private void Awake(){
        anim = GetComponent<Animator>();
    }


    public void SetDirection(Vector2 _direction){
        string[] directionArray = null;
        if(SceneManager.GetActiveScene().name == "Level1"){
            Debug.Log("play lvl2 animation");
                if(transform.position.y <= -4.3){
                    //Debug.Log("on the grd");
                    if(_direction.x>0){
                        anim.Play(FlyInAirAnimation[1]);
                        FaceDirection = 1;
                    }else if(_direction.x<0){
                        anim.Play(FlyInAirAnimation[0]); 
                        FaceDirection = 0;
                    }else if(_direction.x == 0){
                        anim.Play(FlyInAirAnimation[FaceDirection]); 
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
       
        }
    }

}

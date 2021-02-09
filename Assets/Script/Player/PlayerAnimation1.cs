using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation1 : MonoBehaviour
{
    private Animator anim;

    public string[] staticDirections = {"Static_N", "Static_NW", "Static_W", "Static_SW", "Static_S", "Static_SE", "Static_E,", "Static_NE"};
    public string[] runDirections = {"Run_N", "Run_NW", "Run_W", "Run_SW", "Run_S", "Run_SE", "Run_E", "Run_NE"};
    public string[] paperDirections = {"Bird_paN", "Bird_paNW", "Bird_paW", "Bird_paSW", "Bird_paS", "Bird_paSE", "Bird_paE", "Bird_paNE"};
    public string[] penDirections = {"Bird_PenN", "Bird_PenNW", "Bird_PenW", "Bird_PenSW", "Bird_PenS", "Bird_PenSE", "Bird_PenE", "Bird_PenNE"};
    private int FaceDirection = 0; //1 right 0 left
    

    int lastDirection;
    private void Awake(){
        anim = GetComponent<Animator>();
    }


    public void SetDirection(Vector2 _direction){
        string[] directionArray = null;
        if(SceneManager.GetActiveScene().name == "Level1" && GameObject.Find("GamePlaySystemManager").GetComponent<GamePlaySystemManager>().ispickPaper == 1){
            if(_direction.magnitude < 0.01){
                directionArray = paperDirections;
                Debug.Log("static");
            }else{
                directionArray = paperDirections;
                lastDirection = DirectionToIndex(_direction);
            }
            Debug.Log(lastDirection);
            anim.Play(directionArray[lastDirection]);
        }else if(SceneManager.GetActiveScene().name == "Level1" && GameObject.Find("GamePlaySystemManager").GetComponent<GamePlaySystemManager>().ispickPen == 1){
            Debug.Log("play penanimation");
            if(_direction.magnitude < 0.01){
                directionArray = penDirections;
                Debug.Log("static");
            }else{
                directionArray = penDirections;
                lastDirection = DirectionToIndex(_direction);
            }
            Debug.Log(lastDirection);
            anim.Play(directionArray[lastDirection]);
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

    public void Level1DefultAnimation(){
        anim.Play(staticDirections[4]);
    }
    
    public void TimelineAnimation(){
        anim.Play("StaticOnGround");
        
    }

    public void FlyOutAnimation(){
        anim.Play("FlyOut");
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine(){
		yield return new WaitForSeconds(5);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAutoRun : MonoBehaviour
{
	public float speed;
	public static float waitTime = 0.1f;
	public Transform[] movePos;
	public GameObject soldier01;
	public GameObject soldier02;
	private int i;
	private bool moveingRight;
	private float wait;
	private Animator Soldier01Anim;
	private Animator Soldier02Anim;
	private AudioSource[] audioSources;
    private AudioClip stampSound;
    private bool isSoundPlayed = false;

	void Awake() {
		audioSources = this.gameObject.GetComponents<AudioSource>();
		stampSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_SoldierMarching");
    	Soldier01Anim = soldier01.GetComponent<Animator>();
    	Soldier02Anim = soldier02.GetComponent<Animator>();
	}
    // Start is called before the first frame update
    void Start()
    {
    	i = 1;
    	moveingRight = false;
    	wait = waitTime;
    	audioSources[0].clip = stampSound;
    }

    // Update is called once per frame
    void Update()
    {
    	if (CaptainAction.isSoldierRun) {
    		if (!isSoundPlayed) {
    			audioSources[0].Play();
    			isSoundPlayed = true;
    		}
	        this.transform.position = Vector2.MoveTowards(this.transform.position, movePos[i].position, speed * Time.deltaTime);
	        Soldier01Anim.SetBool("isWalking",true);
	        Soldier02Anim.SetBool("isWalking",true);
	        if (Vector2.Distance(this.transform.position, this.movePos[i].position) < 0.1f) {
	        	if (waitTime > 0) {
	        		waitTime -= Time.deltaTime;
	        	} 
	        	else {
		        	if (moveingRight) {
		        		Soldier01Anim.SetBool("isOnBack",false);
		        		Soldier02Anim.SetBool("isOnBack",false);
		        		//this.transform.eulerAngles = new Vector3 (0, -180, 0);
		        		moveingRight = false;
		        	} 
		        	else {
		        		Soldier01Anim.SetBool("isOnBack",true);
		        		Soldier02Anim.SetBool("isOnBack",true);
						//this.transform.eulerAngles = new Vector3 (0, 0, 0);
		        		moveingRight = true;
		        	}
		        	if (i == 0) {
		        		i = 1;
		        	}
		        	else {
		        		i = 0;
		        	}
		        	waitTime = wait;
		        }
	        }
		}
		// 追玩家
		else if (CaptainAction.isSoldierTrace) {
			if (!isSoundPlayed) {
    			audioSources[0].Play();
    			isSoundPlayed = true;
    		}
    		Soldier01Anim.SetBool("isOnBack",false);
    		Soldier02Anim.SetBool("isOnBack",false);
			soldier01.GetComponent<SpriteRenderer>().flipX = true; 
			soldier02.GetComponent<SpriteRenderer>().flipX = true;       
			Soldier01Anim.SetBool("isWalking",true);
	        Soldier02Anim.SetBool("isWalking",true);
			this.transform.position = Vector2.MoveTowards(this.transform.position, GameObject.Find("Player").GetComponent<Transform>().position, speed * Time.deltaTime);
			if (Vector2.Distance(this.transform.position, GameObject.Find("Player").GetComponent<Transform>().position) < 0.1f) {
				CaptainAction.isSoldierTrace = false;
			}
		}
		// 静止
		else {
			audioSources[0].Stop();
			isSoundPlayed = false;
			Soldier01Anim.SetBool("isWalking",false);
			Soldier02Anim.SetBool("isWalking",false);
		}
    }
}

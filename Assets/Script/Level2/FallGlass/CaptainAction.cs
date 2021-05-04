using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaptainAction : MonoBehaviour
{
	public static bool isSoldierRun;
	public static bool isSoldierTrace;
	public GameObject glass;
	public GameObject windowGlass;
	public GameObject questionMark;
	public GameObject hint;
	public GameObject failUI;
	public GameObject player;
	float whistleTime = 3f;
	float knockableTime = 1f;
	private Animator captainAnim;
	private Animator birdAnim;
	private bool isKnockable; //玻璃是否可以被敲
	private bool isGameStart;	//游戏开始
	private bool isKnockedGlass; //是否敲过玻璃了
	private int glassNum = 0;
	private int sucCount;
	private int failCount;
	enum failState {CHANGED, UNCHANGE};
	failState curFailState;
	private AudioSource[] audioSources;
    private AudioClip whistleSound;
    private AudioClip glassSound;
    private AudioClip glassBreakSound;
 	private bool isSoundPlay = false;

    // Start is called before the first frame update
    void Awake()
    {
    	captainAnim = this.GetComponent<Animator>();
    	birdAnim = player.GetComponent<Animator>();
    	isSoldierRun = false;
    	isSoldierTrace = false;
    	isKnockable = false;
    	isGameStart = false;
    	isKnockedGlass = false;
    	sucCount = 0;
    	failCount = 0;
    	glass.SetActive(false);
    	failUI.SetActive(false);
    	questionMark.SetActive(false);
    	hint.SetActive(false);
    	audioSources = this.gameObject.GetComponents<AudioSource>();
		glassSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_GlassSound");
		whistleSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_CaptianWhistle");
		glassBreakSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_GlassBreak");
		audioSources[0].clip = glassSound;
		audioSources[1].clip = whistleSound;
		audioSources[2].clip = glassBreakSound;
    
    	Invoke("StartWhistle", 3f);
    }

    // Update is called once per frame
    void Update()
    {
    	if (isGameStart) {
    		Debug.Log("Start");
	        whistleTime -= Time.deltaTime;
	        if (whistleTime <= 0) {
	        	whistleTime = Random.Range(2, 4); //随机2-4秒
	        	isSoldierRun = !isSoldierRun;
	        	isKnockedGlass = false;
	        	knockableTime = 1f; //敲玻璃时间恢复
	        	// Debug.Log("士兵跑步/静止");
	        }
	        else {
	        	if(knockableTime <= 1f){
		        	knockableTime -= Time.deltaTime;
		        	if (knockableTime <= 0) {
		        		knockableTime = 2f;
		        		isKnockedGlass = false;
		        		isKnockable = false;
		        		isSoundPlay = false;
		        		captainAnim.SetBool("isWhistle",false);
		        		hint.SetActive(false);
			        	// Debug.Log("不可以敲");
		        	} else {
		        		if (!isSoundPlay) {
							audioSources[1].PlayOneShot(whistleSound,0.5f);
							isSoundPlay = true;
						}
		        		isKnockable = true;
		        		captainAnim.SetBool("isWhistle",true);
			        	hint.SetActive(true);
			        	// Debug.Log("可以敲");
		        	}
		        }
	        }

	        if (Input.GetKeyDown(KeyCode.Space) && !isKnockedGlass) {
	        	birdAnim.Play("Knock");
	        	audioSources[0].PlayOneShot(glassSound,0.5f);
	        	if (isKnockable) {
	        		sucCount += 1;
	        		glassNum++;
	        		windowGlass.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BreakWindowGame/Glass/A_Glass0"+glassNum);
	        		Debug.Log("敲成功");
	        	} 
	        	else {
	        		failCount += 1 ;
	        		curFailState = failState.UNCHANGE;
	        		Debug.Log("敲失败");
	        	}
	        	isKnockedGlass = true;
	        }

	    }
	    else if (Input.GetKeyDown(KeyCode.Space) && sucCount >= 5) {
	    	Debug.Log("捡起玻璃");
        	LevelLoader.instance.LoadLevel("Level2FallRoom");
	    }

        if (failCount == 1 && curFailState == failState.UNCHANGE) {
        	questionMark.SetActive(true);
        	isSoldierRun = false;
        	whistleTime = Random.Range(3, 6); 
        	Debug.Log("重新计时");
        	curFailState = failState.CHANGED;
        }
        else if (failCount == 2 && curFailState == failState.UNCHANGE) {
        	curFailState = failState.CHANGED;
        	isGameStart = false;
        	isSoldierRun = false;
        	isSoldierTrace = true;
        	// Debug.Log("游戏失败");
        	GameManager.instance.StorePlayerLoc(new Vector2(25f,-1.5f));
        	Invoke("FailScreen",1f);
        	failCount ++;
        }
        else if (failCount > 2){
        	if (hint.activeSelf && Input.GetKeyDown(KeyCode.Space)) {
        		LevelLoader.instance.LoadLevel("Level2FallLose");
        	}
        }
        if (sucCount == 5){
        	audioSources[2].PlayOneShot(glassSound,0.5f);
        	Debug.Log("游戏成功");
        	glass.SetActive(true);
        	isGameStart = false;
        	GameManager.instance.GlassEnd();
        	sucCount++;
        }
    }

    void FailScreen() {
    	failUI.SetActive(true);     
    	Invoke("EndHint",0.7f);
    }
	void EndHint() {
    	hint.SetActive(true);
    }

    void StartWhistle() {
		isGameStart = true;
    }

    // IEnumerator GameEnd(string SceneName) {
    // 	fading.SetActive(true);
    // 	yield return new WaitForSeconds(2f);
    // 	SceneManager.LoadScene(SceneName);
    // }
}


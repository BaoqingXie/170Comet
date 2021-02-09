using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
	public static AudioClip RoomBGM;
    public static AudioSource[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        RoomBGM = Resources.Load<AudioClip>("Sound/RoomBGM");
        audioSources = this.gameObject.GetComponents<AudioSource>();
        audioSources[0].clip = RoomBGM;
        audioSources[0].Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

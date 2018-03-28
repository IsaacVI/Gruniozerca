using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public static MusicController singleton;
    public AudioSource audioSource;
    public AudioListener audioListener;
    public AudioClip[] music;
    public int actualMusic=-1;
    public bool changeMusic;
    public bool change;
    private GameObject listener;

    public static void PlayMusic(int songId)
    {
        if(singleton.actualMusic !=songId)
        {
            singleton.changeMusic = true;
            singleton.actualMusic = songId;
            
        }
    }

    private void OnEnable()
    {
        PlayMusic(GameController.isPlay ? 1:0);
        audioSource.volume = 0;


    }

    public static void SetVolume(float volume)
    {
        singleton.audioSource.volume = volume;
    }

	// Use this for initialization
	void Awake () {
        
            singleton = this;
          
            audioSource.volume = 0;
            audioSource.Stop();
        
    }

    private void Start()
    {
          listener = FindObjectOfType<AudioListener>().gameObject;
    }


    // Update is called once per frame
    void Update () {
        PlayMusic(GameController.isPlay ? 1:0);

        if (GameController.musicVolume > 0 && listener!=null)
            transform.position = listener.transform.position;
        if (changeMusic)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0, Time.deltaTime * GameController.musicVolume * 2);
            if (audioSource.volume <= 0)
            {
                audioSource.Stop();
                audioSource.clip = music[actualMusic];
                audioSource.Play();
                changeMusic = false;
            }
        }
        if(!changeMusic && audioSource.volume!= GameController.musicVolume)
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, GameController.musicVolume, Time.deltaTime * GameController.musicVolume * 2);
    }
}

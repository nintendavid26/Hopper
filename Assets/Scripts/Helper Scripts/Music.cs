using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Reflection;
//using System.Diagnostics;

public class Music : MonoBehaviour {

    public AudioClip CurrentSong;
    private static AudioSource Source1;
    public List<AudioClip> Songs;

    private static Music instance = null;
    public static Music Instance
    {
        get { return instance; }
    }

    private static AudioSource Source
    {
        get
        {
            if (Source1 == null) { Source = instance.GetComponent<AudioSource>(); }
            return Source1;
        }

        set
        {
            Source1 = value;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
       // SceneManager.activeSceneChanged += SceneManager_activeSceneChanged1; ;
    }

    void OnEnable()
    {
       
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (CurrentSong != Songs[SceneManager.GetActiveScene().buildIndex])
        {
            Source.Stop();
            CurrentSong = Songs[SceneManager.GetActiveScene().buildIndex];
            Source.loop = true;
            Source.clip = CurrentSong;
            Source.Play();
        }
    }

    void Start()
    {
        Source.Stop();
        Source = GetComponent<AudioSource>();
        CurrentSong = Songs[SceneManager.GetActiveScene().buildIndex];
        Source.loop = true;
        Source.clip = CurrentSong;
        Source.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public static void PlaySound(AudioClip Sound, float volume=1)
    {
        Source.PlayOneShot(Sound, volume);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    float BaseTimeScale;
    private static bool paused = false;
    public PauseMenu PauseMenu;

    public static bool Paused
    {
        get
        {
            return paused;
        }

        set
        {
            paused = value;
            if (paused)
            {
                Time.timeScale = 0;
            }
            else { Time.timeScale = 0.5f; }
        }
    }

    // Use this for initialization
    void Start () {
        BaseTimeScale = Time.timeScale;
        PauseMenu.gameObject.SetActive(false);
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
	}

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        Debug.Log("Scene Change");
        Paused = false;

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("p")) {
            PausePressed();
        }
	}

    public void PausePressed()
    {
        //play sound
        if (!Paused)
        {
            PauseGame();
        }
        else
        {
            UnPauseGame();
        }
    }
    public void PauseGame()
    {
        Debug.Log("Paused");
        Paused = true;
        PauseMenu.gameObject.SetActive(true);Debug.Log(PauseMenu.gameObject.activeInHierarchy);
       // PauseMenu.PauseButton.gameObject.GetComponentInChildren<Text>().text="Resume";
    }
    public void UnPauseGame()
    {
        Debug.Log("Unpause");
        Paused = false;
        PauseMenu.gameObject.SetActive(false);
      //  PauseMenu.PauseButton.gameObject.GetComponentInChildren<Text>().text = "Pause";
    }
}

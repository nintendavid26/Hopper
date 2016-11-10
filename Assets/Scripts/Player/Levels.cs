using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public bool testing = false;
    public GameObject goal;
    public static int nonLevelScenes = 2;
    public static int MaxLevel {
        get {
            return PlayerPrefs.GetInt("MaxLevel");
        }
        set { PlayerPrefs.SetInt("MaxLevel", value); }
    }

    public void Start()
    {
        if (testing)
        {
            Instantiate(goal, transform.position.setY(transform.position.y + 4), transform.rotation);
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            ResetLevels();
        }
    }
    public void ResetLevels()
    {
        MaxLevel = 1;
        Debug.Log("levels reset");
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Finish")
        {
            GoToNextLevel();
        }

    }
    public static void GoToNextLevel()
    {
        /*
          if  
        */
        int current = SceneManager.GetActiveScene().buildIndex-nonLevelScenes+1;
        int next = current + 1;
        int NumberOfLevels = SceneManager.sceneCountInBuildSettings-nonLevelScenes;
        Debug.Log("current="+current+" next="+next+"NumberOfLevels="+NumberOfLevels+" MaxLevel="+MaxLevel);
        if (next>=MaxLevel) {
            MaxLevel = next;
            }
        if (next > NumberOfLevels)
        {
            SceneManager.LoadScene(1);
        }
        else{
            SceneManager.LoadScene(next+nonLevelScenes-1);
        }
            
        }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public bool testing = false;
    public GameObject goal;
    public static int nonLevelScenes = 3; //I don't like hard coding this, but it's alot easier
    public static int MaxLevel {
        get {
            if (!PlayerPrefs.HasKey("MaxLevel")) { PlayerPrefs.SetInt("MaxLevel", 1); }
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

        if (Input.GetKeyDown("f"))
        {
            Debug.Log(GameObject.Find("Level"));
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
            PlayerPrefs.SetInt("StartFromStart", 1);
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

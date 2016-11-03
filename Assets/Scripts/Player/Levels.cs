using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    
    public void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            ResetLevels();
        }
    }
    public void ResetLevels()
    {
        PlayerPrefs.SetInt("maxLevel", 1);
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
        if (PlayerPrefs.HasKey("maxLevel"))
        {
            int maxLevel = PlayerPrefs.GetInt("maxLevel");
            if (SceneManager.GetActiveScene().buildIndex - 1 >= maxLevel)
            {
                PlayerPrefs.SetInt("maxLevel", maxLevel + 1);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

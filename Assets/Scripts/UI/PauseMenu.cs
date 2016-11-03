using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public Button PauseButton,Settings,LevelSelect;
    public Pause p;
    //Settings
    public Button InputType;

	// Use this for initialization
    public void Start()
    {
        gameObject.SetActive(false);
    }
	public void GoToLevelSelect()
    {
        SceneManager.LoadScene(1);
    }
    public void UnPause()
    {
        p.UnPauseGame();
    }
    public void GoToSettings()
    {
        AbleButtons(false,PauseButton, LevelSelect);
    }
    public void GoBackFromSettings()
    {

    }
    public void ChangeInput()
    {
        
    }

    public void AbleButtons( bool Enable, params Button[] Buttons)
    {
        foreach(Button b in Buttons)
        {
            b.gameObject.SetActive(Enable);
        }
    }
}

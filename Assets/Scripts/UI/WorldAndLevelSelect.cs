using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class WorldAndLevelSelect : MonoBehaviour {

    public enum ButtonState { World, Level,Back }
    public ButtonState State;
    public List<SelectButton> Buttons;
    public SelectButton BackButton;
    public int World = 0;
    public Text display;
    public Color disabledColor, enabledColor;
   // public Button testButton;

    // Use this for initialization
    void Start() {
        InitializeButtons();
        foreach (SelectButton b in Buttons)
        {
            SelectButton temp = b;
            b.Button.onClick.AddListener(() => OnButtonPress(temp));
            //Test(b);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OnButtonPress(SelectButton B)
    {
        if (B.State==ButtonState.Back)
        {
            GoBack();
        }
        else if (State == ButtonState.World)
        {
           GoToLevelSelect(B.Number);
        }
        else if (State == ButtonState.Level)
        {
           GoToLevel(B.Number);
        }

    }

    public void GoToLevelSelect(int world)
    {
        World = world;
        State = ButtonState.Level;
        foreach(SelectButton b in Buttons)
        {
            b.State = ButtonState.Level;
            b.name = "Level " + b.Number;
            b.gameObject.transform.GetChild(0).GetComponent<Text>().text = b.name;
            if (Levels.MaxLevel % 10 >= b.Number)
            {
                b.Button.gameObject.SetActive(true);
                if (b.Number == SceneManager.sceneCountInBuildSettings - 1)
                {
                    b.Button.GetComponentInChildren<Text>().text = "Coming Soon";
                }

            }
            else
            {
                b.Button.gameObject.SetActive(false);
            }
        }
        display.text = "World " + world;
    }
    public void GoToLevel(int Level)
    {
        string l = "_Scenes/World" + World + "/Level " + World + "-" + Level;
        Debug.Log(l);
        SceneManager.LoadScene(l);
    }
    public void GoBack()
    {
        Debug.Log("GoBack");
        World = 0;
        if (State == ButtonState.Level)
        {
            State = ButtonState.World;
            foreach (SelectButton b in Buttons)
            {
                b.State = ButtonState.World;
                b.name = "World " + b.Number;
                b.gameObject.transform.GetChild(0).GetComponent<Text>().text = b.name;
                if (Levels.MaxLevel / 10 >= b.Number - 1)
                {
                    b.Button.gameObject.SetActive(true);

                }
                else
                {
                    b.Button.gameObject.SetActive(false);
                }

            }
            display.text ="Choose World";
        }
        else if (State == ButtonState.World)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Test(Button B)
    {
        Debug.Log(B.name);
    }
    public void InitializeButtons()
    {
        List<Button> temp = FindObjectsOfType<Button>().ToList();
        display = GameObject.Find("Display").GetComponent<Text>();
        Buttons.Capacity = temp.Count;
        foreach (Button b in temp)
        {
            SelectButton B = null;
            
            if (b.gameObject.GetComponent<SelectButton>() == null)
            {
                Debug.Log("Didn't have script");
                B = b.gameObject.AddComponent<SelectButton>();
            }
            else
            {
                B = b.gameObject.GetComponent<SelectButton>();
            }
            if (b.name == "Back")
            {
                BackButton = B;
                BackButton.State = ButtonState.Back;
                BackButton.Button.onClick.AddListener(() => OnButtonPress(BackButton));
            }
            else {
                B.Number = Convert.ToInt32(b.name.Split(' ')[1]);
                Buttons.Add(B);
            }

        }
        Buttons=Buttons.OrderBy( x =>x.Number).ToList();
        foreach (SelectButton b in Buttons)
        {
            if (Levels.MaxLevel / 10 >= b.Number-1)
            {
                b.Button.gameObject.SetActive(true);
                
            }
            else
            {
                b.Button.gameObject.SetActive(false);
            }
        }
    }

}


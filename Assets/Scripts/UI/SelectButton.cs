using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{

    public WorldAndLevelSelect.ButtonState State;
    public int Number;
    public Button Button
    {
        get { return GetComponent<Button>(); }
    }

}

using UnityEngine;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        if (!PlayerPrefs.HasKey("Music")) { PlayerPrefs.SetInt("Music", 1); }
        if (!PlayerPrefs.HasKey("SFX")) { PlayerPrefs.SetInt("SFX", 1); }
    }


    public void Toggle(bool music)
    {
        if (music) {
            if (PlayerPrefs.GetInt("Music") == 0)
            {
                PlayerPrefs.SetInt("Music", 1);
                transform.GetChild(0).GetComponent<Text>().text = "Music: On";
                Music.Play();
            }
            else {
                PlayerPrefs.SetInt("Music", 0);
                transform.GetChild(0).GetComponent<Text>().text = "Music: Off";
                Music.Stop();
            }
        }
        else {
            if (PlayerPrefs.GetInt("SFX") == 0)
            {
                PlayerPrefs.SetInt("SFX", 1);
                transform.GetChild(0).GetComponent<Text>().text = "SFX: On";
            }
            else {
                PlayerPrefs.SetInt("SFX", 0);
                transform.GetChild(0).GetComponent<Text>().text = "SFX: Off";
            }
        }
    }

}

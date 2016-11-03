using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowLevelAtStart : MonoBehaviour {

    public Text Level;

	// Use this for initialization
	void Start () {
        Destroy(Level, 10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

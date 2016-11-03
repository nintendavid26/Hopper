using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class Rope : MonoBehaviour,IParent {

    
    public void BePartOfTileContainer()
    {
        if (transform.parent.parent == null)
        {
            transform.parent.parent = GameObject.Find("Tiles").transform;
        }
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
    [ExecuteInEditMode]
	void Update () {
        BePartOfTileContainer();
        if (transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.zero;
        }
    }
}

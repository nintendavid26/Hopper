using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
    public float speed = 1;
    public float time = 0;
    public bool clockwise=true;
	// Use this for initialization
	void Start () {
        if (time != 0)
        {
            StartCoroutine(Alternate());
        }

    }
	
	// Update is called once per frame
	void Update () {
       
        if (clockwise) { transform.Rotate(Vector3.back*Time.deltaTime*speed); }
        else { transform.Rotate(Vector3.forward*Time.deltaTime*speed); }
	}
    IEnumerator Alternate()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(time);
            clockwise = !clockwise;
        }
    }
}

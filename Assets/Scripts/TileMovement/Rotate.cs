using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
    public float speed = 1;
    public float Time = 3;
    public bool clockwise=true;
	// Use this for initialization
	void Start () {
        if (Time != 0)
        {
            StartCoroutine(Alternate());
        }

    }
	
	// Update is called once per frame
	void Update () {
       
        if (clockwise) { transform.Rotate(Vector3.back); }
        else { transform.Rotate(Vector3.forward); }
	}
    IEnumerator Alternate()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(Time);
            clockwise = !clockwise;
        }
    }
}

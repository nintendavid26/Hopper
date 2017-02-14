using UnityEngine;
using System.Collections;

public class Crumble : MonoBehaviour {

    public float time=1;

	// Use this for initialization
	void Start () {
        Invoke("Adjust", 1);
	}
	public void Adjust()
    {
        time = time*Time.timeScale;
    }
	// Update is called once per frame


    void Destroy()
    {
        if (transform.childCount>0)
        {
            transform.GetChild(0).GetComponent<Jump>().StartFreeFall();
        }
        if (name.Contains("Leaf"))
        {
            GetComponent<SoundEffects>().PlaySound("Snap");
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            Invoke("Destroy", time);
        }
    }
}

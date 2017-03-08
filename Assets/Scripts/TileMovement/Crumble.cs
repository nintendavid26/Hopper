using UnityEngine;
using System.Collections;
using Helper_Scripts;

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
           this.PlaySound("Snap");
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            Invoke("Destroy", time);
        }
    }
}

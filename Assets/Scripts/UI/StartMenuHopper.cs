using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenuHopper : MonoBehaviour {

    public Vector3 point;
    public SpriteRenderer s;
    public bool restart;
    // Use this for initialization
    void Start () {
        s = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (point.x > transform.position.x)
        {
            s.flipX = true;
        }
        else { s.flipX = false; }
    }

    public void StartPressed()
    {
        Parabola p = new Parabola(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        SceneManager.LoadScene(1);
    }
}

using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
    public Vector2 p;
    void OnTriggerEnter2D(Collider2D c) {
        if (c.tag == "Player")
        {
            PlayerPrefs.SetFloat("CheckPointX", p.x);
            PlayerPrefs.SetFloat("CheckPointY", p.y);
            PlayerPrefs.SetInt("StartFromStart",0);
            Debug.Log("Set Check");
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class AutoMoveCamera : MonoBehaviour {
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out Point pos);
    Point cursorPos;
    public GameObject target;
    public float minX;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * Time.deltaTime);
        Vector2 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 newMousePos = new Vector2(currMousePos.x - Time.deltaTime, currMousePos.y);
        Vector2 worldToScreen = Camera.main.WorldToScreenPoint(newMousePos);
        SetCursorPos((int)worldToScreen.x, (int)worldToScreen.y);
        // dj.Vertex = new Vector3(dj.Vertex.x - Time.deltaTime, dj.Vertex.y);
    }
}

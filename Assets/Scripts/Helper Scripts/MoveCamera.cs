using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public struct Point
{
    public float x, y;
}

public class MoveCamera : MonoBehaviour {

   
    public GameObject Target;
    public float maxX,minX, maxY, minY;
    public float WorldMinX=Mathf.NegativeInfinity, WorldMaxX=Mathf.Infinity, WorldMinY=Mathf.NegativeInfinity, WorldMaxY=Mathf.Infinity;
    public DrawJump dj;
	// Use this for initialization
	void Start () {
        if (GameObject.Find("Level Canvas").activeInHierarchy==false) { GameObject.Find("Level Canvas").SetActive(true); }
    }
	
	// Update is called once per frame
	void Update () {
        if (Target == null) { Target = GameObject.Find("GrassHopper"); }
        if (Target == null) { return; }
        if (Target.transform.position.x-transform.position.x > maxX)
        {
            transform.position = new Vector3(Target.transform.position.x-maxX, transform.position.y,transform.position.z);
        }
        if (Target.transform.position.y - transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x,Target.transform.position.y - maxY, transform.position.z);
        }
        if (transform.position.y -Target.transform.position.y > minY)
        {
            transform.position = new Vector3(transform.position.x, Target.transform.position.y + minY, transform.position.z);
        }
        if (transform.position.x - Target.transform.position.x > minX)
        {
            transform.position = new Vector3( Target.transform.position.x + minX, transform.position.y, transform.position.z);
        }
        //Adjust To World Bounds
        
        if (transform.position.x < WorldMinX) { transform.position=transform.position.setX(WorldMinX); }
        if (transform.position.x > WorldMaxX) { transform.position = transform.position.setX(WorldMaxX); }
        if (transform.position.y < WorldMinY) { transform.position = transform.position.setY(WorldMinY); }
        if (transform.position.y > WorldMaxY) { transform.position = transform.position.setY(WorldMaxY); }
        
    }

}

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public struct TileData{
    [SerializeField]
    public string Name;
    [SerializeField]
    public string Tag;
    [SerializeField]
    public Sprite s;
    [SerializeField]
    public MonoBehaviour[] Scripts;
    [SerializeField]
    public string FileName;
}

public interface IParent
{
    void BePartOfTileContainer();
}

[ExecuteInEditMode]
public class Tile : MonoBehaviour,IParent
{
    public Tile BaseTile;
   // public Texture tex;
    public int amount = 1;
   // public ZombieMaker bZMaker;
   [Flags]
    public enum Mode { Move, Create, Stretch };
    public Mode currentMode;
    public GameObject Parent;
    public static Dictionary<string, TileData> TileData;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
 


    public void BePartOfTileContainer()
    {
        if (transform.parent == null)
        {
            transform.parent = GameObject.Find("Tiles").transform;
        }
    }

    public void OnDrawGizmosSelected()
    {
        BePartOfTileContainer();
        transform.localScale = (new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z)));
        float x = transform.localScale.x, y = transform.localScale.y, z = 1;
        if (transform.localScale.y > 1) { y = 1; }
        transform.localScale = (new Vector3(x, y, z));

       // if (name.Contains("Leaf")&&name!="Falling_Leaf") { name = "Leaf"; }
        if (name == "Leaf"&&!Application.isPlaying)
        {
            GetComponent<SpringJoint2D>().connectedAnchor = transform.position;
        }
    }
}
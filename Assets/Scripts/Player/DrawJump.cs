using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class DrawJump : MonoBehaviour {

    public List<Vector3> Points; 
    public LineRenderer Line;
    public Vector3 StartPoint;
    public Vector3 Vertex;
    public Parabola p;
    public float MaxJumpHeight,MaxJumpWidth,MinJumpWidth,MinJumpHeight;
    public bool ShouldDrawLine=true;
    public Jump j;
    public float minAngleDist;//Prevents you from going too much up or to the side
    public Vector3 prev;
    public Vector3 prevT;
    public SpriteRenderer s;
    public bool Dragging=false;

    public bool DragTouch
    {
        get
        {
            int temp = PlayerPrefs.GetInt("DragTouch");
            if (temp == 0) { return false; }
            else { return true; }
        }

        set
        {
            
            if (value) { PlayerPrefs.SetInt("DragTouch",1); }
            else { PlayerPrefs.SetInt("DragTouch", 0); }
        }
    }

    // Use this for initialization
    void Start () {
        Line = GetComponent<LineRenderer>();
        j = GetComponent<Jump>();
        prev= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        prevT = transform.position;
        s = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("i"))
        {
            DragTouch = !DragTouch;
        }
        if (!DragTouch)
        {
            // Debug.Log(p.toString());
            StartPoint = transform.position;
            Vertex = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vertex.x > transform.position.x)
            {
                s.flipX = true;
            }
            else { s.flipX = false; }
            AdjustJump();
            if (!canJump(j)) { ShouldDrawLine = false; }
            else { ShouldDrawLine = true; }
            if (Vertex != prev || transform.position != prevT)
            {
                UpdateLine();
            }
            prev = Vertex;
            prevT = transform.position;
        }
        else {
            
            if (Input.GetMouseButton(0)||Input.GetMouseButtonDown(0))
            {
                Dragging = true;
                ShouldDrawLine = true;
                StartPoint = transform.position;
                Vertex = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vertex.x > transform.position.x)
                {
                    s.flipX = true;
                }
                else { s.flipX = false; }
                AdjustJump();
                if (canJump(j)) { ShouldDrawLine = true; }
                else { ShouldDrawLine = false; }
                UpdateLine();
                prev = Vertex;
                prevT = transform.position;
            }
            else { ShouldDrawLine = false; UpdateLine(); }
        }
	}
    public bool canJump(Jump J)
    {
        if ((J.ms == Jump.MovementState.canJump || J.ms == Jump.MovementState.Rope) && !Pause.Paused)
        {
            return true;
        }
        else {  return false; }
    }
    void UpdateLine()
    {
        Vector3[] empty = { };
        Line.SetPositions(empty);//clear line
        if (ShouldDrawLine)
        {
            Line.enabled = true;
            if (p == null)
            {
                p = new Parabola(transform.position, Vertex);
            }
            else { p.Change(transform.position, Vertex); }
            float x = transform.position.x;
            Points = p.GeneratePoints(x, Vertex.x - x + Vertex.x);
           // Points.ToArray();
            Line.SetVertexCount(Points.ToArray().Length);
            Line.SetPositions(Points.ToArray());
        }
        else { Line.enabled = false;}
    }

    public void AdjustJump()
    {
        float height = Vertex.y - transform.position.y;
        float width =  Mathf.Abs(Vertex.x - transform.position.x);
        if (height > MaxJumpHeight) { Vertex = new Vector3(Vertex.x, transform.position.y + MaxJumpHeight);height = MaxJumpHeight; }
        if (height < MinJumpHeight) { Vertex = new Vector3(Vertex.x, transform.position.y + MinJumpHeight);height = MinJumpHeight; }
        if (width > MaxJumpWidth+height&&Vertex.x>transform.position.x) { Vertex = new Vector3(transform.position.x + MaxJumpWidth + height, Vertex.y); }
        else if (width > MaxJumpWidth + height && Vertex.x<transform.position.x) { Vertex = new Vector3(transform.position.x - MaxJumpWidth - height, Vertex.y); }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SoundEffects))]
public class Jump : MonoBehaviour {
    public DrawJump dj;
    public enum MovementState { canJump,Jumping,FreeFall,Rope,Other}
    public MovementState ms;
    public BoxCollider2D col;
    public float jumpSpeed;
    public Vector2 prev;
    public float DeathHeight;
    public GameObject Wall;
    public static Vector2 Checkpoint;
    Parabola p { get; set; }
    public float maxFallSpeed=0.5f;
    // Use this for initialization
    void Start () {
        dj = GetComponent<DrawJump>();
        col = GetComponent<BoxCollider2D>();
        OnDie += OnDeath;
        Checkpoint = transform.position;
        if(PlayerPrefs.GetInt("StartFromStart")==0)transform.position = new Vector3(PlayerPrefs.GetFloat("CheckPointX"), PlayerPrefs.GetFloat("CheckPointY"));
	}
    void OnDisable()
    {
        OnDie -= OnDeath;
    }

    // Update is called once per frame
    void Update () {
        if (transform.parent==null)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            //transform.localScale=new Vector3(transform.parent)
        }
        if (!dj.DragTouch)
        {
            if (Input.GetMouseButtonDown(0) && dj.canJump(this)&&!MouseOverButton())
            {

                StartCoroutine(StartJump());
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0) && dj.canJump(this)&&!MouseOverButton())
            {

                StartCoroutine(StartJump());
                dj.Dragging = false;
                dj.ShouldDrawLine = false;
            }
        }
        if (transform.position.y < DeathHeight) { OnDeath(); }
        if (ms == MovementState.FreeFall)
        {
            transform.Translate(Vector3.down*Time.deltaTime*jumpSpeed);
        }
        if (ms == MovementState.Rope)
        {
            Vector3 end = transform.parent.GetChild(0).transform.GetChild(0).position;
            transform.position = Vector3.MoveTowards(transform.position, end, Time.deltaTime * jumpSpeed / 4);
            if (transform.position == end)
            {
                StartFreeFall();
            }
        }
        if (Input.GetKeyDown("r"))
        {
            OnDeath();
        }
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        


    }
   public void StartFreeFall()
    {
        transform.parent = null;
        Vector3[] empty = { };
        dj.Line.SetPositions(empty);
        ms = MovementState.FreeFall;
        dj.ShouldDrawLine = false;
        transform.rotation = Quaternion.identity;

    }
    public void GoOnRope(GameObject r)
    {
        ms = MovementState.Rope;
        dj.ShouldDrawLine = true;
       // transform.position = new Vector3(transform.position.x, c.transform.position.y + c.transform.localScale.y);
        transform.parent = r.transform.parent;
        transform.rotation = Quaternion.identity;
        p = null;
    }
    public IEnumerator StartJump() {
        transform.parent = null;
        Vector3[] empty = { };
        dj.Line.SetPositions(empty);
        ms = MovementState.Jumping;
        dj.ShouldDrawLine = false;
        float x;
        p = dj.p;
        float constant = p.Width()/p.Height();//Two jumps with same height should take the same amount of time regardless of width
        //Debug.Log(constant);
        float maxJumpSpeed=2;
        if (constant >maxJumpSpeed) { constant =constant/2; }//there's probably a better way to do this that I'll think of later
        transform.rotation = Quaternion.identity;
        this.PlaySound("Jump",randomPitch: true);
        if (dj.p.h > transform.position.x)
        {
            while (ms==MovementState.Jumping)
            {

                x = transform.position.x + Time.fixedDeltaTime * jumpSpeed*constant;
                float y = p.XtoY(x);
                if (transform.position.y - y > maxFallSpeed&&GoingDown()) { y = p.XtoY((x+transform.position.x) / 2); }
                //Debug.Log("Speed=" + (transform.position.y - y));
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y),Time.deltaTime);
                transform.position = new Vector3(x, y);// setting position is glitchy with physics
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (ms==MovementState.Jumping)
            {
                x = transform.position.x - Time.fixedDeltaTime * jumpSpeed*constant;//* some constant
                float y = p.XtoY(x);
                if (transform.position.y - y > maxFallSpeed&GoingDown()) { y = p.XtoY((x + transform.position.x) / 2); }
                //Debug.Log("Speed=" + (transform.position.y - y));
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y), 1);
                transform.position = new Vector3(x, y);
                yield return new WaitForFixedUpdate();
            }
        }
        // transform.position = prev;

            //transform.parent.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Wall"))
        {
            ms = MovementState.FreeFall;
        }
        else if (c.CompareTag("Ground"))
        {
            if (GoingDown())
            {
                LandOnGround(c);
                if (c.name == "Leaf")
                {
                    Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
                    rb.isKinematic= false;
                    rb.AddForce(Vector2.down*10);
                    Debug.Log("Leaf");

                }
            }
            else if(c.name != "Leaf")
            {
                Debug.Log("Going Up");
                StartFreeFall();

            }
        }
       else if (c.CompareTag("Hazard"))
        {
            OnDeath();
        }
        else if (c.CompareTag("Rope")&&(ms==MovementState.FreeFall||ms==MovementState.Jumping))
        {
            GoOnRope(c.gameObject);
        }
        else if (c.CompareTag("Bridge")&&GoingDown())
        {
            LandOnGround(c,false);
        }

    }

    public void LandOnGround(Collider2D c, bool Snap = true)
    {
        ms = MovementState.canJump;
        dj.ShouldDrawLine = true;
        StopCoroutine(StartJump());
        if (Snap) { transform.position = new Vector3(transform.position.x, c.transform.position.y + c.transform.lossyScale.y); }
        //else { transform.position = new Vector3(transform.position.x, c.transform.position.y); }
        transform.parent = c.transform;
        transform.rotation = c.transform.rotation;
        p = null;
    }
    public delegate void Die();
    public static event Die OnDie;
    public void OnDeath()
    {
        this.PlaySound("Die");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //transform.position = Checkpoint;
        ms = MovementState.canJump;
      //  OnDie();
    }
    public bool GoingDown()//If the player is upside down what does Up mean?
    {
        if (p == null) { Debug.Log("Error, p is null"); return true; }
        int x = GoingRight() ? 1 : -1;
        bool up = p.Slope(transform.position.x) * x < 0||ms==MovementState.FreeFall;
        return up;
    }
    public bool GoingRight()
    {
        if (p == null) { Debug.Log("Error, p is null"); p=new Parabola(transform.position,dj.Vertex); }
        bool r= p.px < p.vx;
        return r;
    }
    public bool MouseOverButton()
    {
        return false;
    }

    public GameObject CheckIfWentThroughObject(GameObject p1,GameObject p2)
    {
        //Draw Ray from p1 to p2
        //if it hits a block
        //land on that block
        return null;
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        PlayerPrefs.SetInt("StartFromStart", 1);
    }


}

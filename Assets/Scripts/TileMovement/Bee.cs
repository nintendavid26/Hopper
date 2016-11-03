using UnityEngine;
using System.Collections;
public class Bee:MonoBehaviour
{
    Parabola p;
    public Vector3 target;
    public float speed;
    public void Initialize(Vector3 t,float Speed)
    {
        target = t;
        speed = Speed;
        p = new Parabola(transform.position, target);
        StartCoroutine(Move());
        Destroy(gameObject, 5);
    }
    public IEnumerator Move()
    {
        float x;
        if (p.px < p.vx) { GetComponent<SpriteRenderer>().flipX = true; }
        while (true)
        {
            float constant = (Mathf.Abs(p.vx - p.px)/10)* (Mathf.Abs(p.vy - p.py)/10);
            if (p.vx > p.px) {
                x = transform.position.x + Time.deltaTime * speed*constant;
            }
            else {
                x = transform.position.x - Time.deltaTime * speed*constant;
            }
            float y = p.XtoY(x);
            transform.position = new Vector3(x, y);
            yield return new WaitForEndOfFrame();
        }
    }

}
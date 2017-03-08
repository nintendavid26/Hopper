using System.Collections;
using UnityEngine;

namespace TileMovement
{
    public class Bee:MonoBehaviour
    {
        Parabola p;
        public Vector3 Target;
        public float Speed;
        public void Initialize(Vector3 t,float speed)
        {
            Target = t;
            Speed = speed;
            p = new Parabola(transform.position, Target);
            StartCoroutine(Move());
            Destroy(gameObject, 5);
        }
        IEnumerator Move()
        {
            float x;
            if (p.px < p.vx) { GetComponent<SpriteRenderer>().flipX = true; }
            while (true)
            {
                float constant = (Mathf.Abs(p.vx - p.px)/10)* (Mathf.Abs(p.vy - p.py)/10);
                if (p.vx > p.px) {
                    x = transform.position.x + Time.deltaTime * Speed*constant;
                }
                else {
                    x = transform.position.x - Time.deltaTime * Speed*constant;
                }
                float y = p.XtoY(x);
                transform.position = new Vector3(x, y);
                yield return new WaitForEndOfFrame();
            }
        }

        public void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Target);
        }

    }
}
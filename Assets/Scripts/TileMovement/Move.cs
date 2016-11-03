using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour {

    public List<Vector3> Checkpoints;
    public float speed=1;
    public int curr = 0;
    public List<int> waitTimes;
    public bool waiting;
    public bool flip;

	// Use this for initialization
	void Start () {
        if (waitTimes.Capacity != Checkpoints.Capacity)
        {
            waitTimes.Capacity = Checkpoints.Capacity;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!waiting){transform.position = Vector3.MoveTowards(transform.position, Checkpoints[curr], Time.deltaTime * speed);}
        if (transform.position == Checkpoints[curr])
        {
            curr++;
            if (flip) { GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX; }
            if (curr >= Checkpoints.Count) { curr = 0; }
            if (waitTimes[curr] > 0) { StartCoroutine(Wait(waitTimes[curr])); }
        }
	}

    IEnumerator Wait(float time) {
        waiting = true;
        yield return new WaitForSeconds(time);
        waiting = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (Checkpoints == null||Checkpoints.Count==0) { Checkpoints = new List<Vector3>();Checkpoints.Add(transform.position); }
        Vector3 prev=Checkpoints[0];
        foreach (Vector3 point in Checkpoints)
        {
            Gizmos.DrawSphere(point, 0.8f);
            Gizmos.DrawLine(point,prev);
            prev = point;
        }
        if (waitTimes.Capacity!=Checkpoints.Capacity) { waitTimes.Capacity = Checkpoints.Capacity; }
    }
}

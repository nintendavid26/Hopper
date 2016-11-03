using UnityEngine;
[RequireComponent(typeof (LineRenderer))]
public class Switch : MonoBehaviour
{
    public enum type { Key,OnOff};
    public type Type;
    public GameObject Unlock;
    public LineRenderer lr;
    public void Start()
    {
        Vector3[] points = { transform.position, Unlock.transform.position };
        lr = GetComponent<LineRenderer>();
        lr.SetPositions(points);
    }
    public void OnTriggerEnter2D()
    {
        if (Type == type.Key)
        {
            GetComponent<SoundEffects>().PlaySound("Unlock",5);
            Destroy(Unlock);
            Destroy(gameObject);
        }
        else if (Type == type.OnOff)
        {
            GetComponent<SoundEffects>().PlaySound("Unlock",5);
            Unlock.SetActive(!Unlock.activeInHierarchy);
            lr.enabled = !lr.enabled;
        }
    }
    void Update()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, Unlock.transform.position);
    }

    public void OnDrawGizmos()
    {
        if (Unlock != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Unlock.transform.position);
        }
    }
}

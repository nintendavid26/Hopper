using UnityEngine;
using System.Collections;

public class BeeHive : MonoBehaviour
{
    public GameObject TargetIcon;
    public GameObject Player;
    public float range;
    public float targetSpeed;
    public Bee bee;
    public float beeSpeed;
    public bool targeting = false;
    public float shootRate;

    public void Start()
    {
        Player = GameObject.Find("GrassHopper");
        InvokeRepeating("ShootBee", 0, shootRate);
    }
    public void Update()
    {

        if (Vector3.Distance(transform.position, Player.transform.position) < range)
        {
            TargetIcon.transform.position = Vector3.MoveTowards(TargetIcon.transform.position, Player.transform.position, targetSpeed * Time.deltaTime);
        }

    }
    public void ShootBee()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) > range) { return; }
        Bee b = (Bee)Instantiate(bee, transform.position, transform.rotation);
        b.Initialize(TargetIcon.transform.position,beeSpeed);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int HitsLeft;
    public GameObject Weakness;
    public List<Vector3> WeaknessSpawnPoints;
    public float BattleStartX;
    public float BattleStartCameraMinX;
    public bool BattleStarted = false;
    public GameObject Player;
    public GameObject Wall;
    public Vector3 WallSpawn;
    public SoundEffects sfx;
    public GameObject Finish;
    public float deathFallSpeed = 6;

    public void Start()
    {
        sfx = GetComponent<SoundEffects>();
        Weakness.AddComponent<BossWeakness>().boss=this;
    }
    public void Update()
    {
        if (!BattleStarted && Player.transform.position.x > BattleStartX)
        {
            StartBattle();        
        }
        if (BattleStarted)
        {

        }
    }
    public void PlayDeathAnimation()
    {

    }
    public void StartBattle() {
        BattleStarted = true;
        Camera.main.GetComponent<MoveCamera>().WorldMinX = BattleStartCameraMinX;
        Instantiate(Wall, WallSpawn, Quaternion.identity);
        SpawnWeakness();
        GetComponent<BeeHive>().enabled = true;
    }
    public void SpawnWeakness()
    {
        Vector3 point = WeaknessSpawnPoints.RandomItemConditional(s => s != Weakness.transform.position);
        Weakness.transform.position = point;
    }
    public void getHit()
    {
        HitsLeft--;
        sfx.PlaySound("Hurt");
        if (HitsLeft <= 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            SpawnWeakness();
        }
    }
    public IEnumerator Die()
    {
        Debug.Log("Die");
        GetComponent<SpriteRenderer>().flipY = true;
        Destroy(GetComponent<BeeHive>().TargetIcon);
        Destroy(GetComponent<BeeHive>());
        Destroy(Weakness);
        Instantiate(Finish, new Vector3(transform.position.x,transform.position.y-2,0), transform.rotation);
        if (deathFallSpeed != 0)
        {
            while (true)
            {
                transform.Translate(Vector3.down * Time.deltaTime * 4);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            yield return new WaitForEndOfFrame();
        }

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        foreach (Vector3 point in WeaknessSpawnPoints)
        {
            Gizmos.DrawSphere(point, 0.8f);
        }
    }
    public class BossWeakness : MonoBehaviour
    {
        public Boss boss;
        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                boss.getHit();
            }
        }
    }
}

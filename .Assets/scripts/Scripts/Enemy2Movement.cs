using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Movement : MonoBehaviour
{
    private int HP = 50;
    private float cnt;
    public GameObject bullet;
    public GameObject Explosion;
    public float speed=10;
    Vector3 Offset = new Vector3(1, 0, 0);
    private GameObject spawnedBullet;
    private GameObject spawnedBeam;
    public GameObject Beam;
    private bool hasScored = false;
    private GameUIManager UI;

    void Start()
    {
        UI = FindObjectOfType<GameUIManager>();
    }


    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        cnt += Time.deltaTime;
        Die();
        GenerateBullet();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet_Player")
        {
            HP -= 1;
        }
        if (other.gameObject.tag == "Player")
        {
            HP -= 10;
        }
    }

    void Die()
    {
        if (HP < 0 && !hasScored)
        {
            hasScored=true;
            Score();
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(spawnedBullet);
            Destroy(spawnedBeam);
            Destroy(gameObject);
        }
    }

    void GenerateBullet()
    {
        if (cnt > 4)
        {
            speed = 0;

        }
        if(cnt>4.5)
        {
            if(spawnedBeam == null)
            spawnedBeam=Instantiate(Beam, transform.position, Quaternion.identity);
        }
        if (cnt > 6.5)
        {
            spawnedBullet = Instantiate(bullet, transform.position + Offset, Quaternion.identity);
            Destroy(spawnedBeam);
            cnt = 0;
        }
    }

    public void Score()
    {
        if (hasScored)
        {
            UI.score+=5;
            hasScored = false;
        }
    }
}

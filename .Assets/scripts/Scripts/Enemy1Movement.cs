using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Movement : MonoBehaviour
{
    int HP = 4;
    private float cnt;
    public GameObject bullet;
    Vector3 Offset = new Vector3(1, 0, 0);
    public float speed = 10;
    public GameObject Explosion;
    private bool hasScored = false;
    private GameUIManager UI;

    // Start is called before the first frame update
    void Start()
    {
        UI = FindObjectOfType<GameUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        cnt += Time.deltaTime;
        Die();
        GenerateBullet();
        Disappear();
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
        if (HP < 0&&!hasScored)
        {
            hasScored = true;
            Score();
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void GenerateBullet()
    {
        if (cnt > 4)
        {
            Instantiate(bullet, transform.position + Offset, Quaternion.identity);
            cnt = 0;
        }
    }

    private void Disappear()
    {
        if (this.transform.position.x > 45)
        {
            Destroy(gameObject);
        }
    }

    public void Score()
    {
        if (hasScored)
        {
            UI.score++;
            hasScored = false;
        }
    }
}

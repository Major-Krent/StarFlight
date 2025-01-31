using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy3Movement : MonoBehaviour
{
    DroneManager droneManager;
    private int HP = 100;
    private float cnt;
    private float cntM;
    public GameObject bullet;
    public GameObject Explosion;
    private float speed = 10;
    Vector3 Offset1 = new Vector3(5, 0, 15);
    Vector3 Offset2 = new Vector3(0, 0, 0);
    Vector3 Offset3 = new Vector3(0, 0, 0);
    private GameObject spawnedBullet;
    private GameObject spawnedBeam;
    public GameObject Beam;
    public int bulletCount = 5;  // 设置扩散的子弹数量
    public float spreadAngle = 45f;
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
        this.transform.Translate(speed * Time.deltaTime, 0, 0);
        cnt += Time.deltaTime;
        cntM+= Time.deltaTime;
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
            hasScored = true;
            Score();
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(spawnedBullet);
            Destroy(spawnedBeam);
            Destroy(gameObject);
        }
    }

    void GenerateBullet()
    {
        if (cntM > 6)
        {
            speed = 0;
        }
        if (cnt > 4)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                // 随机计算每颗子弹的发射角度
                float angle = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
                // 计算子弹的旋转（角度转化为Quaternion）
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                // 实例化子弹，并加上偏移和旋转
                spawnedBullet = Instantiate(bullet, transform.position/* + Offset1*/, rotation);
            }
            cnt = 0;
        }
    }
    public void Score()
    {
        if (hasScored)
        {
            UI.score+=10;
            hasScored = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int HP = 600;
    private float cntM;
    private float cnt1;
    private float cnt2;
    public GameObject bullet0;
    public GameObject bullet;
    public GameObject Explosion;
    public float speed = 10;
    Vector3 Offset1 = new Vector3(24, 0, -20.5f);
    Vector3 Offset2 = new Vector3(24, 0, 20.5f);
    private GameObject spawnedBullet1;
    private GameObject spawnedBullet2;
    private GameObject spawnedBullet3;
    private GameObject spawnedBullet4;
    private GameObject spawnedBeam1;
    private GameObject spawnedBeam2;
    public GameObject Beam;
    public bool isBossDie=false;


    public int bulletCount = 5;  // 设置扩散的子弹数量
    public float spreadAngle = 45f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        cntM += Time.deltaTime;
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
        if (HP < 0)
        {
            isBossDie = true;
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(spawnedBullet1);
            Destroy(spawnedBullet2);
            Destroy(spawnedBeam1);
            Destroy(spawnedBeam2);
            StartCoroutine(waitBoss());

        }
    }

    void GenerateBullet()
    {
        if (cntM > 6)
        {
            speed = 0;
            cnt1 += Time.deltaTime;
            cnt2 += Time.deltaTime;
        }
        
        if (cnt1 > 4.5)
        {
            if (spawnedBeam1 == null)
                spawnedBeam1 = Instantiate(Beam, transform.position + Offset1, Quaternion.identity);
            if (spawnedBeam2 == null)
                spawnedBeam2 = Instantiate(Beam, transform.position + Offset2, Quaternion.identity);
        }
        if (cnt1 > 6.5)
        {
            spawnedBullet1 = Instantiate(bullet0, transform.position + Offset1, Quaternion.identity);
            spawnedBullet2 = Instantiate(bullet0, transform.position + Offset2, Quaternion.identity);
            Destroy(spawnedBeam1);
            Destroy(spawnedBeam2);
            cnt1 = 0;
        }

        if (cnt2 > 4)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                // 随机计算每颗子弹的发射角度
                float angle = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
                // 计算子弹的旋转（角度转化为Quaternion）
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                // 实例化子弹，并加上偏移和旋转
                spawnedBullet3 = Instantiate(bullet, transform.position+ Offset1, rotation);
            }

            for (int i = 0; i < bulletCount; i++)
            {
                // 随机计算每颗子弹的发射角度
                float angle = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
                // 计算子弹的旋转（角度转化为Quaternion）
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                // 实例化子弹，并加上偏移和旋转
                spawnedBullet4 = Instantiate(bullet, transform.position + Offset2, rotation);
            }
            cnt2 = 0;
        }
    }

    IEnumerator waitBoss()
    {
        yield return new WaitForSeconds(3f);

        Destroy(gameObject);

    }
}

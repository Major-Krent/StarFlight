using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject DroneGetterPrefab;
    public GameObject Enemy1Prefab;
    public GameObject Enemy2Prefab;
    public GameObject Enemy3Prefab;
    public GameObject BossPrefab;
    float Enemy1_Span = 1.0f;
    float Enemy2_Span = 15.0f;
    float Enemy3_Span = 30.0f;
    float BossCnt = 0;
    float DroneGetter_Span = 1.5f;

    float Enemy1_delta = 0;
    float Enemy2_delta = 0;
    float Enemy3_delta = 0;
    float DroneGetter_delta = 0;

    float lastZ = 40;

    public bool isBossOn = false;
    public bool isBossGo = false;
    public bool isBossGen = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isBossOn)
        {
            Generate_DroneGetter();
        }

        Generate_Enemy();
    }




    void Generate_DroneGetter()
    {
        DroneGetter_delta += Time.deltaTime;
        if (DroneGetter_delta > this.DroneGetter_Span)
        {
            DroneGetter_delta = 0;
            GameObject item = Instantiate(DroneGetterPrefab);
            float z = Random.Range(-40, 40);
            item.transform.position = new Vector3(-95, 0, z);
        }

    }

    void Generate_Enemy()
    {
        if (!isBossOn)
        {
            GenEnemy1();
            GenEnemy2();
            GenEnemy3();
        }
        GenBoss();
    }


    void GenEnemy1()
    {
        Enemy1_delta += Time.deltaTime;
        if (Enemy1_delta > this.Enemy1_Span)
        {
            Enemy1_delta = 0;
            GameObject item = Instantiate(Enemy1Prefab);
            float z = Random.Range(-40, 40);
            item.transform.position = new Vector3(-95, 0, z);
        }
    }

    void GenEnemy2()
    {
        int enemyCount = FindObjectsOfType<Enemy2Movement>().Length;

        // 如果敌人数少于 2，生成新的敌人
        if (enemyCount < 2)
        {
            Enemy2_delta += Time.deltaTime;
            if (Enemy2_delta > this.Enemy2_Span)
            {
                Enemy2_Span = 8f;
                Enemy2_delta = 0;
                GameObject item = Instantiate(Enemy2Prefab);
                float z = Random.Range(-40, 40);
                item.transform.position = new Vector3(-95, 0, z);
            }
        }
    }
    void GenEnemy3()
    {
        int enemyCount = FindObjectsOfType<Enemy2Movement>().Length;
        if (enemyCount < 2)
        {
            Enemy3_delta += Time.deltaTime;
            if (Enemy3_delta > this.Enemy3_Span)
            {
                Enemy3_Span = 8f;
                Enemy3_delta = 0;
                GameObject item = Instantiate(Enemy3Prefab);
                float z = Random.Range(0, 2) == 0 ? -40 : 40;

                if (z == lastZ)
                {
                    z = -z;
                    lastZ = z;
                }
                else
                {
                    lastZ = -40;
                }
                if (z == 40)
                {
                    item.transform.rotation = Quaternion.Euler(0, 60, 0);
                }
                item.transform.position = new Vector3(-80, 0, z);
            }
        }
    }

    void GenBoss()
    {
        int enemyCount = FindObjectsOfType<BossController>().Length;

        BossCnt += Time.deltaTime;
        if (enemyCount < 1)
        {
            if (BossCnt > 60)
            {
                isBossOn = true;
            }
            if(BossCnt>70)
            {
                isBossGo=true;
            }
            if(BossCnt>75 && !isBossGen)
            {
                GameObject Boss = Instantiate(BossPrefab);
                Boss.transform.position = new Vector3(-135, 0, 0);
                isBossGen = true;
            }
            
        }
    }
}

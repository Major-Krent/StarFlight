using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GenBullet_Drone : MonoBehaviour
{
    public GameObject bullet;
    Vector3 Offset = new Vector3(-1, 0, 0);
    private float cnt = 0;
    private DroneManager droneManager;
    private int currentType;
    private ItemGenerator Boss;
    bool canshoot = true;

    // Start is called before the first frame update
    void Start()
    {
        droneManager = FindObjectOfType<DroneManager>();
        Boss = FindObjectOfType<ItemGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        cnt += Time.deltaTime;
        currentType = droneManager.type;

        switch (currentType)
        {
            case 0:
                Followtype();
                break;
            case 1:
                Wingtype();
                break;
        }
        if (Boss.isBossGo)
        {
            StartCoroutine(StopShooting(12f));
        }
    }

    private void Followtype()
    {
        if (cnt > 1.5)
        {
            if (canshoot)
            {
                Instantiate(bullet, transform.position + Offset, Quaternion.Euler(0, 0, 0));
            }
            cnt = 0;
        }
    }

    private void Wingtype()
    {
        if (cnt > 0.5)
        {
            if (canshoot)
            {
                Instantiate(bullet, transform.position + Offset, Quaternion.Euler(0, 0, 0));
            }
            cnt = 0;
        }
    }

    IEnumerator StopShooting(float seconds)
    {
        canshoot = false;
        yield return new WaitForSeconds(seconds);
        canshoot = true;
    }
}

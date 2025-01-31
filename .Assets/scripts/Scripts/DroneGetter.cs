using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneGetter : MonoBehaviour
{
    public GameObject BombPrefab;
    public Vector3 spawnOffset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }

    void Movement()
    {
        transform.position= new Vector3(transform.position.x + 10 * Time.deltaTime, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(BombPrefab, transform.position + spawnOffset, Quaternion.Euler(0, -90, 0));
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_En1Movement : MonoBehaviour
{
    public float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(speed * Time.deltaTime, 0, 0);

        Disappear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void Disappear()
    {
        if (this.transform.position.x > 45)
        {
            Destroy(gameObject);
        }
    }
}

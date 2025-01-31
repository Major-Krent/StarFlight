using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletfar : MonoBehaviour
{
    Vector3 defaultPosition;
    float cnt = 0;
    float speed=100;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cnt += Time.deltaTime;
        this.transform.Translate(speed * Time.deltaTime, speed * Time.deltaTime, 0);
        if (cnt > 5)
        {
            cnt = 0;
            transform.position = defaultPosition;
        }
    }
}

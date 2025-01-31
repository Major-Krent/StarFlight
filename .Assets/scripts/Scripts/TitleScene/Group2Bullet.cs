using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group2Bullet : MonoBehaviour
{
    Vector3 defaultPosition;
    float cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cnt += Time.deltaTime;
        this.transform.Translate(45 * Time.deltaTime, 45 * Time.deltaTime, 0);
        if (cnt > 20)
        {
            cnt = 0;
            transform.position = defaultPosition;
        }
    }
}

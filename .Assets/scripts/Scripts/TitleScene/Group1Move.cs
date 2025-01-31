using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group1Move : MonoBehaviour
{
    Vector3 defaultPosition;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosition=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(-50 * Time.deltaTime, -2 * Time.deltaTime, 0);
        if(transform.position.x<-300)
        {
            transform.position = defaultPosition;
        }
    }
}

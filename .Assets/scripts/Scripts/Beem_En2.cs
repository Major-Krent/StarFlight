using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beem_En2 : MonoBehaviour
{
    private float cnt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cnt += Time.deltaTime;
        if (cnt > 3)
            Destroy(gameObject);
    }
}

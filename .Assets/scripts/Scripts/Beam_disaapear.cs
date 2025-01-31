using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_disaapear : MonoBehaviour
{
    private float cnt;
    private ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();    
    }

    // Update is called once per frame
    void Update()
    {
        cnt += Time.deltaTime;
        if(cnt>1.2)
        {
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        if (cnt > 1.5)
            Destroy(gameObject);
    }
}

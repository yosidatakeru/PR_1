using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class DestroyObjectScript : MonoBehaviour
{
    HPScript hPScript;
    // Start is called before the first frame update
    void Start()
    {
        hPScript = GameObject.Find("HPGauge").GetComponent<HPScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hPScript.Gauge <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}

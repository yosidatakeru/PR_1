using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    int speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position -= speed * transform.forward * Time.deltaTime;
        if (transform.position.z <= -300) // Œë·‚ð‹–—e‚µ‚½”äŠr
        {

            Destroy(gameObject);
        }
    }
}

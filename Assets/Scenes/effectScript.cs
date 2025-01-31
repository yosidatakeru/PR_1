using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectScript : MonoBehaviour
{
    int speed = -20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //íeÇ5ïbÇ≤è¡ãé
        Destroy(gameObject, 5);
        
        transform.position += speed * transform.forward * Time.deltaTime;
    }
}

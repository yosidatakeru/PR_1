using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WollScript : MonoBehaviour
{
    int Speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 30.0f) 
        {
            transform.position += Speed * transform.up * Time.deltaTime;
        }

        
    }
}

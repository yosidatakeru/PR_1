using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wullScript : MonoBehaviour
{
    float Speed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 25.0f)
        {
            transform.position += Speed * transform.up * Time.deltaTime;
        }
    }
}

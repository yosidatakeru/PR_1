using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    float speed = 8f;
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //’e‚ð‘O‚É”ò‚Î‚·
        transform.position += speed * transform.forward * Time.deltaTime;

        Destroy(gameObject, 10);
    }
}

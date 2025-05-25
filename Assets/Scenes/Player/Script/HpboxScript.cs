using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class HpboxScript : MonoBehaviour
{
    HPScript hpScript;
    public GameObject particle;
    Vector3 particleposition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(particle, new Vector3(transform.position.x+8, transform.position.y + particleposition.y, transform.position.z), Quaternion.identity);
            //âº
            hpScript.Gauge += 200;

            //ìGÇè¡Ç∑/
            Destroy(gameObject);



        }

    }
}

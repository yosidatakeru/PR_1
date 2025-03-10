using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.SocialPlatforms.Impl;

public class TutorialCanonScript : MonoBehaviour
{
    public GameObject particle;
    Vector3 particleposition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        particleposition = new Vector3(0, 3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

            Instantiate(particle, new Vector3(transform.position.x, transform.position.y + particleposition.y, transform.position.z), Quaternion.identity);
            //ìñÇΩÇ¡ÇΩÇÁè¡ñ≈
            // GetComponent<MeshRenderer>().enabled = false;
            //enemySpawnScript.defeats += 1;

          


            //ìGÇè¡Ç∑/
          //  Destroy(gameObject);



        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    //’e‚ÌƒXƒs[ƒh
    int speed = 20;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //’e‚ğ5•b‚²Á‹
        if (transform.position.z <= -10) 
        {
            Destroy(gameObject);
        }
       
        //’e‚ğ‘O‚É”ò‚Î‚·
        transform.position -= speed * transform.forward * Time.deltaTime;
    }

    //“–‚½‚è”»’è
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(enemySpawnScript.enemySpawns);

        if (collision.gameObject.tag == "Player")
        {
            GetComponent<SphereCollider>().enabled = false;
            //“G‚ğÁ‚·/
            Destroy(gameObject);



        }

    }
}

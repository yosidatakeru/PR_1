using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    //íeÇÃÉXÉsÅ[Éh
    int speed = 20;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.z <= -10) 
        {
            Destroy(gameObject);
        }
       
        //íeÇëOÇ…îÚÇŒÇ∑
        transform.position -= speed * transform.forward * Time.deltaTime;
    }

    //ìñÇΩÇËîªíË
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(enemySpawnScript.enemySpawns);

        if (collision.gameObject.tag == "Player")
        {
            GetComponent<SphereCollider>().enabled = false;
            //ìGÇè¡Ç∑/
            Destroy(gameObject);



        }

        if (collision.gameObject.tag == "EnemyWoll")
        {
            GetComponent<SphereCollider>().enabled = false;

            Destroy(gameObject);



        }

    }
}

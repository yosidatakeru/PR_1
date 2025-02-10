using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    //弾のスピード
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
       
        //弾を前に飛ばす
        transform.position -= speed * transform.forward * Time.deltaTime;
    }

    //当たり判定
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(enemySpawnScript.enemySpawns);

        if (collision.gameObject.tag == "Player")
        {
            GetComponent<SphereCollider>().enabled = false;
            //敵を消す/
            Destroy(gameObject);



        }

    }
}

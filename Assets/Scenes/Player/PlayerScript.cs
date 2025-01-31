using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public  class PlayerScript : MonoBehaviour
{
    public GameObject Bullet;
    //プレイヤーの移動スピード
    public float playerSpeed = 10;

   

    ////弾のインタバル制御
    int timeUntilNextShot = 0;

    int bulletNexst = 10;

    Vector3 playerRotation = Vector3.zero;
  
    // Start is called before the first frame update
    void Start()
    {
        playerRotation = Vector3.zero;
    }

    

    // Update is called once per frame
    void Update()
    {

         transform.rotation = Quaternion.Euler(playerRotation.x,playerRotation.y,playerRotation.z);

        //プレイヤーの移動処理
        // Wキー（前方移動）
        if (Input.GetKey(KeyCode.W) && transform.position.y <= 15.0f)
        {

            transform.position += playerSpeed * Vector3.up * Time.deltaTime;
            
            //回転処理
            if (playerRotation.x > -20)
            {
                playerRotation.x--;
            }
        }
        else if (playerRotation.x <= 0)
        {
            //回転処理
            playerRotation.x++;
            
        }


        // Sキー（後方移動）
        if (Input.GetKey(KeyCode.S) && transform.position.y >= -5.0)
        {
            transform.position -= playerSpeed * Vector3.up * Time.deltaTime;

            //回転処理
            if (playerRotation.x <= 20)
            {
                playerRotation.x++;
            }

        }
        else if (playerRotation.x >= 0)
        {
            //回転処理
            playerRotation.x--;

        }
        


        // Dキー（右移動）
        if (Input.GetKey(KeyCode.D) && transform.position.x <= 10.0f)
        {
            transform.position += playerSpeed * Vector3.right * Time.deltaTime;

            //回転処理
            if (playerRotation.z >= -35)
            {
                playerRotation.z--;
            }
        }
        else if (playerRotation.z <= 0)
        {
            //回転処理
            playerRotation.z++;

        }

        // Aキー（左移動）
        if (Input.GetKey(KeyCode.A) && transform.position.x >= -20.0f)
        {
            transform.position -= playerSpeed * Vector3.right * Time.deltaTime;
            //回転処理
            if (playerRotation.z <= 35)
            {
                playerRotation.z++;
            }
        }
        else if (playerRotation.z >= 0)
        {
            //回転処理
            playerRotation.z--;

        }







        timeUntilNextShot--;
        if (Input.GetKey(KeyCode.Space) && timeUntilNextShot <= 0)
        {
         
           
            Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);


            timeUntilNextShot = bulletNexst; 

        }
}

    

}

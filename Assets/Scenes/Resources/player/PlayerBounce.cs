using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class PlayerBounce : MonoBehaviour

{

    float bounceDistance = 2f;

    float bounceDisableTime = 1f;



    private bool isBounced = false;

    private float bounceTimer = 0f;



    private PlayerMovement movement;

    private PlayerRoll roll;

    private HPScript hpScript;



    Vector3 bounceDirection = Vector3.zero;



    void Start()

    {

        movement = GetComponent<PlayerMovement>();

        roll = GetComponent<PlayerRoll>();

        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();

    }



    void Update()

    {



    }

    

    void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("EnemyWoll"))
        {

            // 衝突点と法線を取得

            ContactPoint contact = collision.contacts[0];

            Vector3 normal = contact.normal;

            // 法線の絶対値を取得して、どの方向の成分が最も強いか判定

            Vector3 absNormal = new Vector3(Mathf.Abs(normal.x), Mathf.Abs(normal.y), Mathf.Abs(normal.z));





            // X方向の衝突が一番強い場合（左右の壁にぶつかった）

            if (absNormal.x > absNormal.y && absNormal.x > absNormal.z)

            {

                // ぶつかった方向と逆向きに跳ね返る（左右）

                bounceDirection = new Vector3(-Mathf.Sign(normal.x) *2, 0, 0);

                movement.StopMovement();

            }

            // Y方向の衝突が一番強い場合（上下にぶつかった）

            else if (absNormal.y > absNormal.x && absNormal.y > absNormal.z)

            {

                bounceDirection = new Vector3(0, -Mathf.Sign(normal.y)*2, 0);

                movement.StopMovement();

            }

            // Z方向の衝突が一番強い場合（正面 or 背面にぶつかった）

            else

            {

                hpScript.Gauge = 0;
                movement.StopMovement();

            }



            // Raycastで衝突面を検出し、位置の補正を行う

            Ray ray = new Ray(transform.position, bounceDirection);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, bounceDistance + 2f))

            {

                // 衝突面の少し手前に位置を調整

                transform.position = hit.point - bounceDirection * 2f;

            }

            else

            {

                // Raycastが壁に届かない場合は既存の処理で移動

                transform.position -= bounceDirection * bounceDistance;

            }



            // 跳ね返りフラグを立てる

            isBounced = true;



        }

    }

    
}
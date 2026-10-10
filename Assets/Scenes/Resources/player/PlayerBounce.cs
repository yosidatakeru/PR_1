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

            // 回転していない状態の正面方向を基準に判定
            Vector3 forward = Vector3.forward;

            float frontDot = Vector3.Dot(forward, -normal);

            if (Mathf.Abs(frontDot) > 0.7f)
            {
                hpScript.Gauge = 0;
                movement.StopMovement();
            }
            // 左右方向の衝突
            else if (absNormal.x > absNormal.y)
            {
                bounceDirection = new Vector3(-Mathf.Sign(normal.x) * 2, 0, 0);
                movement.StopMovement();
            }
            // 上下方向の衝突
            else
            {
                bounceDirection = new Vector3(0, -Mathf.Sign(normal.y) * 2, 0);
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
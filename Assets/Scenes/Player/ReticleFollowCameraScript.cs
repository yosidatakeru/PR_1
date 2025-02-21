using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleFollowCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player; // プレイヤーのTransform
     private Vector3 moveDirection;
    public float speed = 30.0f;

    private bool playerPosition = false;
    float moveX;
    float moveY;

    public float maxRange = 200f; // スティックの最大移動範囲（UI座標）
    public Camera mainCamera; // メインカメラ
    private Vector3 initialPosition; // 初期位置
    private Vector3 offset;
    private float radius;
    public float fixedZ = 10f; // Z座標の固定値


    void Start()
    {
      
        transform.position = new Vector3(0f, 0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        

        MoveReticle();
        transform.LookAt(player);   
    }

    void MoveReticle()
    {
        if (playerPosition == false && Input.GetButtonDown("R3"))
        {
            playerPosition = true;
        }
        else if (playerPosition == true && Input.GetButtonDown("R3"))
        {
            playerPosition = false;
        }
              
           
        
                
         


        if (playerPosition == false)
        {
            ////// スティックの傾きに応じて値を取得（-1.0 ～ 1.0）
            float moveX = Input.GetAxis("R_Stick_H"); // 左スティック X軸
            float moveY = Input.GetAxis("R_Stick_V");   // 左スティック Y軸

            Vector3 move = new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;
            Vector3 newPosition = transform.position + move;

            // **カメラのビューポートを超えないように制限**
            // **Y座標が -15 以下にならないように制限**
            newPosition.y = Mathf.Clamp(newPosition.y, -5.0f, Mathf.Infinity);


            newPosition = ClampToCameraBounds(newPosition);

            // 位置を更新
            transform.position = newPosition;
        }else if(playerPosition==true)
        {

            // X・Y座標のみプレイヤーに追従し、Z座標は固定
            Vector3 targetPosition = player.position + offset;
            targetPosition.z = fixedZ;

            // 滑らかに追従
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }


        //デバック用
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y <= 15.0f)
        {
            transform.position += speed * Vector3.up * Time.deltaTime;
           
        }
       

        // Sキー（後方移動）
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y >= -5.0f)
        {
            transform.position -= speed * Vector3.up * Time.deltaTime;
          
        }
        

        // Dキー（右移動）
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= 20.0f)
        {
            transform.position += speed * Vector3.right * Time.deltaTime;
         
        }
        

        // Aキー（左移動）
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -20.0f)
        {
            transform.position -= speed * Vector3.right * Time.deltaTime;
           
        }
       
    }

    Vector3 ClampToCameraBounds(Vector3 position)
    {
        // **オブジェクトのスクリーン座標を取得**
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(position);

        // **スクリーン外に出ないように制限**
        screenPoint.x = Mathf.Clamp(screenPoint.x, 0.05f, 0.95f); // 画面の端に少し余裕を持たせる
        screenPoint.y = Mathf.Clamp(screenPoint.y, 0.05f, 0.95f);

        // **スクリーン座標をワールド座標に戻す**
        return mainCamera.ViewportToWorldPoint(screenPoint);
    }
}



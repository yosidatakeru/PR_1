using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public  class PlayerScript : MonoBehaviour
{
    public GameObject Bullet;
    //プレイヤーの移動スピード
    public float playerSpeed = 15;



    ////弾のインタバル制御
    int timeUntilNextShot = 0;

    int bulletNexst = 10;

    public float rotationSpeed = 2.0f; // 回転の慣性調整
    private Vector3 playerRotation;    // 現在の回転値
    private Vector3 targetRotation;    // 目標の回転値


    public AudioClip shootSound; // 効果音
    private AudioSource audioSource;

    public int frameInterval = 30; // 30フレームごとに再生
    private int frameCount = 0;



    // Start is called before the first frame update
    void Start()
    {
        playerRotation = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }


    }



    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Euler(playerRotation.x, playerRotation.y, playerRotation.z);



        // プレイヤーの移動処理
        MovePlayer();

        // 慣性をつけて回転をスムーズにする
        playerRotation = Vector3.Lerp(playerRotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(playerRotation);

        frameCount++;


        timeUntilNextShot--;
        if (Input.GetKey(KeyCode.Space) && timeUntilNextShot <= 0)
        {
         
            if (frameCount >= frameInterval)
            {
                audioSource.PlayOneShot(shootSound, 0.7f); // 音量 70%
                frameCount = 0; // カウンターリセット
            }

            Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);


            timeUntilNextShot = bulletNexst;

        }
    }
    void MovePlayer()
    {
        // Wキー（前方移動）
        if (Input.GetKey(KeyCode.W) && transform.position.y <= 15.0f)
        {
            transform.position += playerSpeed * Vector3.up * Time.deltaTime;
            targetRotation.x = Mathf.Max(targetRotation.x - 2, -20);
        }
        else
        {

            targetRotation.x = Mathf.Lerp(targetRotation.x, 0, Time.deltaTime * rotationSpeed);
        }

        // Sキー（後方移動）
        if (Input.GetKey(KeyCode.S) && transform.position.y >= -5.0f)
        {
            transform.position -= playerSpeed * Vector3.up * Time.deltaTime;
            targetRotation.x = Mathf.Min(targetRotation.x + 2, 20);
        }
        else
        {
            targetRotation.x = Mathf.Lerp(targetRotation.x, 0, Time.deltaTime * rotationSpeed);
        }

        // Dキー（右移動）
        if (Input.GetKey(KeyCode.D) && transform.position.x <= 20.0f)
        {
            transform.position += playerSpeed * Vector3.right * Time.deltaTime;
            targetRotation.z = Mathf.Max(targetRotation.z - 2, -35);
        }
        else
        {
            targetRotation.z = Mathf.Lerp(targetRotation.z, 0, Time.deltaTime * rotationSpeed);
        }

        // Aキー（左移動）
        if (Input.GetKey(KeyCode.A) && transform.position.x >= -20.0f)
        {
            transform.position -= playerSpeed * Vector3.right * Time.deltaTime;
            targetRotation.z = Mathf.Min(targetRotation.z + 2, 35);
        }
        else
        {
            targetRotation.z = Mathf.Lerp(targetRotation.z, 0, Time.deltaTime * rotationSpeed);
        }
    }
}

    




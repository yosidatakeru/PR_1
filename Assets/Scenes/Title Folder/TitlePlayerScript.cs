using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    //回転
    Vector3 playerRotation = new Vector3(0f, 180f, 0f);
    float RotationSpeed = 0.08f;
    bool Rotation = false;

    //移動
    Vector3 playerPosition = new Vector3(0f, 0f, 0f);
    float PositionSpeed = 0f;
    bool position = false;
    bool move = true;

    CameraTitleScript cameraScript;


    void Start()
    {
        playerRotation = new Vector3(5f, 180f, 0f);
        Rotation = false;

        playerPosition = new Vector3(0f, 0f, 0f);
        position = false;

        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraTitleScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move == false && Input.GetKeyDown(KeyCode.Space) && cameraScript.IsCameraMoveFinished==true)
        {
            Debug.Log("スペースキーが押されました（1フレームだけ）");
        }

        // スペースキーを押したら isPaused をトグル（切り替え）
        if (move == true && Input.GetKeyDown(KeyCode.Space))
        {
            move = false;
            Debug.Log("スペースキーが押されました（1フレームだけ）");
        }

        // 一時停止中は以降の処理をスキップ
        if (move == true)
        {

            // 回転と位置を適用
            transform.rotation = Quaternion.Euler(playerRotation.x, playerRotation.y, playerRotation.z);
            transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);

            if (Rotation == false)
            {
                playerRotation.z += RotationSpeed;
                if (playerRotation.z >= 12f)
                {
                    Rotation = true;
                }
            }

            if (Rotation == true)
            {
                playerRotation.z -= RotationSpeed;

                if (playerRotation.z <= -12f)
                {
                    Rotation = false;
                }
            }

            if (position == false)
            {
                playerPosition.y += PositionSpeed;
                if (playerPosition.y >= 0.5f)
                {
                    position = true;
                }
            }

            if (position == true)
            {
                playerPosition.y -= PositionSpeed;

                if (playerPosition.y <= -0.5f)
                {
                    position = false;
                }
            }
        }

        if (move == false) 
        {
            playerPosition = new Vector3(0f, 0f, 0f);
            playerRotation = new Vector3(0f, 180f, 0f);
            RotationSpeed = 0;
        }

       
    }
}

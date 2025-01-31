using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    //‰ñ“]
    Vector3  playerRotation = new Vector3(0f, 180f, 0f);
    float RotationSpeed = 0.002f;
    bool Rotation = false;

    //ˆÚ“®
    Vector3 playerPosition = new Vector3(5f, 0f, 0f);
    float PositionSpeed = 0.0002f;
    bool position = false;
    void Start()
    {
        playerRotation = new Vector3(5f, 180f, 0f);
        Rotation = false;

        playerPosition = new Vector3(0f, 0f, 0f);
        position = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ‰ñ“]‚ð“K—p
        transform.rotation = Quaternion.Euler(playerRotation.x, playerRotation.y, playerRotation.z);
        transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);

        if (Rotation == false)
        {
            playerRotation.z += RotationSpeed;
            if (playerRotation.z >= 6f) 
            {
                Rotation = true;
            }
        }

        if (Rotation == true)
        {
            playerRotation.z -= RotationSpeed;

            if (playerRotation.z <= -6f)
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
}

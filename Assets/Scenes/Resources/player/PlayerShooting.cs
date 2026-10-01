using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject Bullet;
    int bulletNext = 4;

    private int timeUntilNextShot = 0;
    private PlayerController playerController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {
        timeUntilNextShot--;
        float triggerValue = Input.GetAxis("RightTrigger");
        // 操作可能かチェック
        if (!playerController.IsControlEnabled())
        {
            return; // 操作無効なら発射しない
        }

        if ((Input.GetKey(KeyCode.Space) || triggerValue > 0.1f) && timeUntilNextShot <= 0)
        {
            Instantiate(Bullet, transform.position, transform.rotation);
            timeUntilNextShot = bulletNext;
        }
    }
}

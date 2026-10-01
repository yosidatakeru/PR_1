using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{

    public GameObject defense;
    private bool isRolling = false;
    private bool isInvincible = false;
    private float rollZAngle = 0f;

    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        // 操作不能ならバレルロールを無効化
        if (playerController != null && !playerController.IsControlEnabled())
            return;

        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("LB")) && !isRolling)
        {
            StartCoroutine(DoBarrelRoll());
        }
    }

    IEnumerator DoBarrelRoll()
    {
        isRolling = true;
        isInvincible = true;

        float elapsed = 0f;
        float startZ = rollZAngle;
        int rollCount = 2;
        float duration = 1.5f;
        float endZ = startZ + (360f * rollCount);

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            rollZAngle = Mathf.Lerp(startZ, endZ, t);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rollZAngle);
            elapsed += Time.deltaTime;

            RepelNearbyBullets();

            yield return null;
        }

        rollZAngle = endZ % 720f;
        isRolling = false;
        isInvincible = false;
    }

    void RepelNearbyBullets()
    {
        float repelRadius = 5f;
        int layerMask = 1 << LayerMask.NameToLayer("EnemyBullet");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, repelRadius, layerMask);

        foreach (var hit in hitColliders)
        {
            TrackingBilltScript tracking = hit.GetComponent<TrackingBilltScript>();
            PlayerFollowingBulletScript following = hit.GetComponent<PlayerFollowingBulletScript>();

            if (tracking != null || following != null)
            {
                Instantiate(defense, transform.position, Quaternion.identity);
                Destroy(hit.gameObject);
            }
        }
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Transform player;
    Vector3 startOffset = new Vector3(-4, -3, 10);
    Vector3 defaultOffset = new Vector3(0, 0, -4);
    float duration = 5.0f;
    float smoothSpeed = 5f;

    private float timer = 0f;
    private bool isAnimating = true;

    bool IsAnimating => isAnimating;

    void Start()
    {
        if (player != null)
        {
            transform.position = player.position + startOffset;
            transform.LookAt(player);
        }
    }

    void Update()
    {
        if (!isAnimating || player == null) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);

        Vector3 targetPos = Vector3.Lerp(player.position + startOffset, player.position + defaultOffset, t);
        transform.position = targetPos;

        Vector3 dir = player.position - transform.position;
        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * smoothSpeed);
        }

        if (t >= 1.0f)
        {
            isAnimating = false;
        }
    }
}

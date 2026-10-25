using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnaround : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;

    public Vector3 targetAngle = new Vector3(20f, 0f, 0f);
    public float rotateSpeed = 3f;

    private Quaternion defaultRotation;
    private bool isOutside = false;

    void Start()
    {
        defaultRotation = transform.rotation;
    }

    void Update()
    {
        if (player == null || mainCamera == null)
            return;

        Vector3 viewportPos =
            mainCamera.WorldToViewportPoint(player.position);

        bool outside =
            viewportPos.x < 0f ||
            viewportPos.x > 1f ||
            viewportPos.y < 0f ||
            viewportPos.y > 1f ||
            viewportPos.z < 0f;

        if (outside)
        {
            isOutside = true;
        }
        else
        {
            isOutside = false;
        }

        Quaternion targetRotation;

        if (isOutside)
        {
            targetRotation = Quaternion.Euler(targetAngle);
        }
        else
        {
            targetRotation = defaultRotation;
        }

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }
}

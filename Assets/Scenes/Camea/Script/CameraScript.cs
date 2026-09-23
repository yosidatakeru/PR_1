using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    Vector3 defaultOffset = new Vector3(0, 0, -4);
    float smoothSpeed = 5f;
    float maxTiltAngle = 2.0f;
    float forwardTriggerZ = 3300f;
    Vector3 boostOffset = new Vector3(-4, 3, 10);

    private Vector3 lastPosition;
    private float tiltAmount = 0f;
    private float tiltVelocity = 0f;

    void Start()
    {
        if (target != null)
            lastPosition = target.position;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        if (Time.deltaTime <= 0.00001f)
        {
            return;
        }

        Vector3 offset =
            target.position.z > forwardTriggerZ
                ? boostOffset
                : defaultOffset;

        Vector3 desiredPosition =
            target.position + offset;

        transform.position =
            Vector3.Lerp(
                transform.position,
                desiredPosition,
                Time.deltaTime * smoothSpeed
            );

        float speedX =
            (target.position.x - lastPosition.x)
            / Time.deltaTime;

        if (float.IsNaN(speedX) ||
            float.IsInfinity(speedX))
        {
            speedX = 0f;
        }

        float targetTilt =
            (speedX / 10f) * maxTiltAngle;

        targetTilt =
            Mathf.Clamp(
                targetTilt,
                -maxTiltAngle,
                maxTiltAngle
            );

        tiltAmount =
            Mathf.SmoothDamp(
                tiltAmount,
                targetTilt,
                ref tiltVelocity,
                0.2f
            );

        if (float.IsNaN(tiltAmount) ||
            float.IsInfinity(tiltAmount))
        {
            tiltAmount = 0f;
            tiltVelocity = 0f;
        }

        Vector3 dir =
            target.position - transform.position;

        if (
            float.IsNaN(dir.x) ||
            float.IsNaN(dir.y) ||
            float.IsNaN(dir.z) ||
            float.IsInfinity(dir.x) ||
            float.IsInfinity(dir.y) ||
            float.IsInfinity(dir.z)
        )
        {
            lastPosition = target.position;
            return;
        }

        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion rot =
                Quaternion.LookRotation(dir);

            Quaternion tiltRotation =
                Quaternion.Euler(
                    0f,
                    0f,
                    -tiltAmount
                );

            Quaternion targetRotation =
                rot * tiltRotation;

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    Time.deltaTime * smoothSpeed
                );
        }

        lastPosition = target.position;
    }
}
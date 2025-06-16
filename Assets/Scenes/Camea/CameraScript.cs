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
        if (target == null) return;

        Vector3 offset = target.position.z > forwardTriggerZ ? boostOffset : defaultOffset;
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        float speedX = (target.position.x - lastPosition.x) / Time.deltaTime;
        float targetTilt = Mathf.Clamp((speedX / 10f) * maxTiltAngle, -maxTiltAngle, maxTiltAngle);
        tiltAmount = Mathf.SmoothDamp(tiltAmount, targetTilt, ref tiltVelocity, 0.2f);

        Vector3 dir = target.position - transform.position;
        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot * Quaternion.Euler(0, 0, -tiltAmount), Time.deltaTime * smoothSpeed);
        }

        lastPosition = target.position;
    }
}

using UnityEngine;
using Cinemachine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private CinemachineDollyCart dollyCart;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float lookAhead = 2f;

    void LateUpdate()
    {
        if (dollyCart == null || dollyCart.m_Path == null)
            return;

        float position = dollyCart.m_Position;

        Vector3 currentPosition =
            dollyCart.m_Path.EvaluatePosition(position);

        Vector3 nextPosition =
            dollyCart.m_Path.EvaluatePosition(position + lookAhead);

        Vector3 direction = nextPosition - currentPosition;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction.normalized);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
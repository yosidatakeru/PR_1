
using Cinemachine;
using UnityEngine;

public class DollyObjectMove : MonoBehaviour
{
    [SerializeField] private CinemachineDollyCart dollyCart;
    [SerializeField] private PlayerMovement playerMovement;

    void Awake()
    {
        if (dollyCart == null)
        {
            dollyCart = GetComponent<CinemachineDollyCart>();
        }

        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        if (dollyCart == null)
        {
            Debug.LogError("Dolly Cartが設定されていません。Inspectorで指定してください。", this);
            enabled = false;
            return;
        }

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovementが見つかりません。Inspectorで指定してください。", this);
            enabled = false;
        }
    }

    void Update()
    {
        if (dollyCart == null || playerMovement == null)
        {
            return;
        }

        dollyCart.m_Speed = playerMovement.forwardSpeed;
    }
}

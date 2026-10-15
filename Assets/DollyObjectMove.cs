
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
           
            enabled = false;
            return;
        }

        if (playerMovement == null)
        {
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

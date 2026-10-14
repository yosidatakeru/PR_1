using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyObjectMove : MonoBehaviour
{
    [SerializeField] private CinemachineDollyCart dollyCart;
    [SerializeField] private float moveSpeed = 10f;

    void Start()
    {
        if (dollyCart == null)
        {
            dollyCart = GetComponent<CinemachineDollyCart>();
        }

        if (dollyCart != null)
        {
            dollyCart.m_Speed = moveSpeed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleFollowCameraScript2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player; // ÉvÉåÉCÉÑÅ[ÇÃTransform

    void Start()
    {
        transform.position = new Vector3(0f, 0f, -16f);
    }

    // Update is called once per frame
    void Update()
    {
      

        transform.LookAt(player);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleFollowCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player; // ÉvÉåÉCÉÑÅ[ÇÃTransform
   
    void Start()
    {
        transform.position = new Vector3(0f, 0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
      
        transform.LookAt(player);   
    }
}


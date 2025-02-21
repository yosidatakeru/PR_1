using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleFollowCameraScript2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player; // ƒvƒŒƒCƒ„[‚ÌTransform

    void Start()
    {
        transform.position = new Vector3(0f, 0f, -16f);

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; // ‰e‚ğ“Š‰e‚µ‚È‚¢
            renderer.receiveShadows = false; // ‰e‚ğó‚¯æ‚ç‚È‚¢
        }
    }

    // Update is called once per frame
    void Update()
    {
      

        transform.LookAt(player);
        

    }
}


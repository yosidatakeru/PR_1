using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private HPScript hpScript;
    private bool isFalling = false;
    public float fallSpeed = 100f;

    void Start()
    {
        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();
    }

    void Update()
    {
        if (!isFalling && hpScript.Gauge <= 0)
        {
            isFalling = true;
            StartCoroutine(FallAndRotate());
        }
    }

    IEnumerator FallAndRotate()
    {
        float rotationSpeed = 90f; // 90“x/•b
        Vector3 fallDirection = new Vector3(0, -10, 0).normalized;

        while (true)
        {
            transform.position += fallDirection * fallSpeed;
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
    }
}

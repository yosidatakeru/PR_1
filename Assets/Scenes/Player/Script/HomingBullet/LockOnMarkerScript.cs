using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnMarkerScript : MonoBehaviour
{
    public Vector3 startScale = new Vector3(15f, 15f, 5f); // 初期サイズ
    public Vector3 endScale = new Vector3(10f, 10f, 5f);   // 終了サイズ
    public float shrinkDuration = 0.2f;                    // 縮小にかける時間

    private float elapsedTime = 0f;

    void Start()
    {
        transform.localScale = startScale;
    }

    void Update()
    {
        if (elapsedTime < shrinkDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / shrinkDuration);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
        }
    }
}

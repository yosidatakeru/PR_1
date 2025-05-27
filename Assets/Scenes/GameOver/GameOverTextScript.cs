using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTextScript : MonoBehaviour
{
    public Material materialToFade;  // 対象のマテリアル（Inspectorで指定）
    public float fadeDuration = 5f;  // フェードインにかける時間（秒）

    void Start()
    {
        if (materialToFade != null)
        {
            // 透明度をゼロに初期化（完全に見えない状態からスタート）
            materialToFade.SetFloat("_Alpha", 0f);

            // フェードイン開始
            StartCoroutine(FadeIn3DObject(materialToFade, fadeDuration));
        }
    }

    IEnumerator FadeIn3DObject(Material material, float duration)
    {
        float maxAlpha = 1f;
        float currentAlpha = 0f;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float progress = t / duration;
            currentAlpha = Mathf.Lerp(0f, maxAlpha, progress);

            if (material != null)
            {
                material.SetFloat("_Alpha", currentAlpha);
            }

            yield return null;
        }

        // 最終的に完全に表示
        if (material != null)
        {
            material.SetFloat("_Alpha", 1f);
        }
    }
}


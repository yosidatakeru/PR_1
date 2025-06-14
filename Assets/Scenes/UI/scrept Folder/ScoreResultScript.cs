using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreResultScript : MonoBehaviour
{
    [SerializeField] private TMP_Text addedValueText;

    private Coroutine showCoroutine;

    public void ShowAddedValue(int amount)
    {
        if (addedValueText == null) return;

        if (showCoroutine != null)
            StopCoroutine(showCoroutine);

        addedValueText.text = "+" + amount.ToString();
        addedValueText.gameObject.SetActive(true);
        addedValueText.rectTransform.localScale = Vector3.one; // 初期スケールにリセット

        showCoroutine = StartCoroutine(ShowAndAnimateText(1.5f));
    }

    private System.Collections.IEnumerator ShowAndAnimateText(float seconds)
    {
        RectTransform rt = addedValueText.rectTransform;

        // 拡大アニメーション
        float scaleTime = 0.15f;
        float elapsed = 0f;
        Vector3 startScale = Vector3.one;
        Vector3 targetScale = Vector3.one * 1.3f;

        while (elapsed < scaleTime)
        {
            float t = elapsed / scaleTime;
            rt.localScale = Vector3.Lerp(startScale, targetScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rt.localScale = targetScale;

        // 縮小アニメーション
        elapsed = 0f;
        while (elapsed < scaleTime)
        {
            float t = elapsed / scaleTime;
            rt.localScale = Vector3.Lerp(targetScale, startScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rt.localScale = startScale;

        // 表示待ち
        yield return new WaitForSeconds(seconds - (scaleTime * 2));

        addedValueText.gameObject.SetActive(false);
        showCoroutine = null;
    }
}



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;


public class ResultManager : MonoBehaviour
{
    public CanvasGroup clearUI;
    //フェード用
    public CanvasGroup fade;
    public CanvasGroup fadein;

    //フェードにかける時間
    float fadeDuration = 2f;
    // フェード中かどうかのフラグ
    private bool isFading = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FadeIn());
          
        // スペースキーまたはゲームパッドのAボタンが押されたとき
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton"))
            {
                //フェードアウトして "TitleScene" に遷移
                StartCoroutine(FadeOut("TitleScene"));
                // "NextSceneName" を切り替えたいシーン名に変更

            }
        


        IEnumerator FadeOut(string sceneName)
        {

            isFading = true;
            fade.blocksRaycasts = true;
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                fade.alpha = t / fadeDuration;
                yield return null;
            }
            fade.alpha = 1;
            SceneManager.LoadScene(sceneName);
        }

        IEnumerator FadeIn()
        {

            isFading = true;
            fadein.blocksRaycasts = true;

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                // 1から徐々に0に減衰させる
                fadein.alpha = 1f - (t / fadeDuration);
                yield return null;
            }

            // 完全に透明にして、レイキャストの遮断も解除する
            fadein.alpha = 0f;
            fadein.blocksRaycasts = false;
            isFading = false;
        }
    }
}

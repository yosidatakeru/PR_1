using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    ScoreScript scoreScript;
    PlayerScript playerScript;
    HPScript hpScript;
    public CanvasGroup clearUI;
    public CanvasGroup fade;
    public CanvasGroup GameUI;
    public CanvasGroup gameOver;
    float fadeDuration = 3f;
    private bool isFading = false;
    public Material glitchMaterial; // ★ ゲームオーバーUIに使われてるマテリアル
   
    // Start is called before the first frame update
    void Start()
    {
        scoreScript = GameObject.Find("ScoreText (TMP)").GetComponent<ScoreScript>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();
        glitchMaterial.SetFloat("_GlitchIntensity", 0f);
        clearUI.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.transform.position.z >= 3300 && hpScript.Gauge >= 0)
        {

            GameUI.alpha = 0;
        }


        if (playerScript.transform.position.z >= 3350&&hpScript.Gauge>=0) 
        {
            Debug.Log("クリアシーン");
            clearUI.alpha = 1;
        }
            
        
        


        if (playerScript.transform.position.z >= 3350&& Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton")&&(playerScript.transform.position.z >= 3350))            
        {
            StartCoroutine(FadeOut("TitleScene"));
           // "NextSceneName" を切り替えたいシーン名に変更

        }

        if (hpScript.Gauge <= 0) 
        {
            StartCoroutine(GameOverFadeOut("GameOverScene"));
        }

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


    IEnumerator GameOverFadeOut(string sceneName)
    {
        isFading = true;
        gameOver.blocksRaycasts = true;

        float glitchMax = 1f;
        float glitchVal = 0f;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float progress = t / fadeDuration;

            // UI フェードイン
            gameOver.alpha = progress;

            // グリッチ強度アップ
            glitchVal = Mathf.Lerp(0f, glitchMax, progress);

            if (glitchMaterial != null)
            {
                glitchMaterial.SetFloat("_GlitchIntensity", glitchVal);
                glitchMaterial.SetFloat("_Alpha", progress); // ← ★ ここで透明度制御
            }

            yield return null;
        }

        gameOver.alpha = 1;

        if (glitchMaterial != null)
        {
            glitchMaterial.SetFloat("_GlitchIntensity", glitchMax);
            glitchMaterial.SetFloat("_Alpha", 1f); // ← ★ 最終的に完全表示
        }

        SceneManager.LoadScene(sceneName);
    }
}


using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenScript : MonoBehaviour
{
    public CanvasGroup fade;
    public TMP_Text startText;  // TextMeshProUGUI用
    public TMP_Text titleText;

    bool resumeGame = false;
    bool ReturnToTitle = false;
    private bool isPaused = false;

    private float blinkTimer = 1f;
    private float blinkInterval = 0.5f;

    public CanvasGroup gameFadeOut;
    float fadeDuration = 3f;
    private bool isFading = false;

    void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
        fade.alpha = 0;
        startText.gameObject.SetActive(true);
        titleText.gameObject.SetActive(true);
    }

    void Update()
    {
        // エスケープキーでポーズ切り替え
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // ポーズ中に上キー押下時は点滅処理
        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                resumeGame = true;
                ReturnToTitle = false;
                Debug.Log("押した");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                resumeGame = false;
                ReturnToTitle = true;
                Debug.Log("押した");
            }

        }
        else
        {
            // ポーズ解除時はテキスト非表示・タイマーリセット
            startText.gameObject.SetActive(true);
            titleText.gameObject.SetActive(true);
            blinkTimer = 0f;
        }

        //アローキ上を押した後
        if (resumeGame == true) 
        {
            StartTex();
            titleText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)|| Input.GetButtonDown("Abutton")) 
            {
                TogglePause();
            }
        }

        if (ReturnToTitle == true)
        {
            TitleTex();
            startText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton"))
            {
                StartCoroutine(FadeOut("TitleScene"));
                // "NextSceneName" を切り替えたいシーン名に変更
            }
        }


    }

    public void TogglePause()
    {
        isPaused = !isPaused;  // ポーズ状態をトグルで切り替え

        if (isPaused)
        {
            Time.timeScale = 0f;  // ゲーム停止
            fade.alpha = 1;
        }
        else
        {
            Time.timeScale = 1f;  // ゲーム再開
            resumeGame = false;
            fade.alpha = 0;
        }
    }

    private void StartTex()
    {
        blinkTimer += Time.unscaledDeltaTime;

        if (blinkTimer >= blinkInterval)
        {
            startText.gameObject.SetActive(!startText.gameObject.activeSelf);
            blinkTimer = 0f;
        }
    }

    private void TitleTex()
    {
        blinkTimer += Time.unscaledDeltaTime;

        if (blinkTimer >= blinkInterval)
        {
            titleText.gameObject.SetActive(!titleText.gameObject.activeSelf);
            blinkTimer = 0f;
        }
    }


    IEnumerator FadeOut(string sceneName)
    {
        isFading = true;
        gameFadeOut.blocksRaycasts = true;
        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            gameFadeOut.alpha = t / fadeDuration;
            yield return null;
        }
        gameFadeOut.alpha = 1;
        SceneManager.LoadScene(sceneName);
    }
}


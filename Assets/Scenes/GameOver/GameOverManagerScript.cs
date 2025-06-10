using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManagerScript : MonoBehaviour
{
    //フェード用
    public CanvasGroup fade;
    //フェードにかける時間
    float fadeDuration = 3f;
    // フェード中かどうかのフラグ
    private bool isFading = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // スペースキーまたはゲームパッドのAボタンが押されたとき
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton") )
        {
            //フェードアウトして "TitleScene" に遷移
            StartCoroutine(FadeOut("TitleScene"));
            // "NextSceneName" を切り替えたいシーン名に変更

        }

       

    }

    // フェードイン処理
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
}

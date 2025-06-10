using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class TitleSceneScript : MonoBehaviour
{   // フェード用のCanvasGroup
    public CanvasGroup fadeCanvas;
    // フェードにかかる時間（秒）
    float fadeDuration = 1f;
    // フェード中フラグ
    private bool isFading = false;
    // カメラスクリプトへの参照
    CameraTitleScript cameraScript;
    // Start is called before the first frame update
    void Start()
    {
        // 念のため初期化
        Time.timeScale = 1f;

        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraTitleScript>();
    }

    // Update is called once per frame
    void Update()
    { 
        // カメラ移動完了後にスペースかAボタンでシーン遷移
        if (cameraScript.IsCameraMoveFinished == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton"))
            {
                StartCoroutine(FadeOut("GameScene"));
            }
        }


        IEnumerator FadeOut(string sceneName)
        {
            isFading = true;
            fadeCanvas.blocksRaycasts = true;
            // 徐々にフェード（透明度を上げる）
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                fadeCanvas.alpha = t / fadeDuration;
                yield return null;
            }
            // 完全に不透明に
            fadeCanvas.alpha = 1;
            // シーン読み込み
            SceneManager.LoadScene(sceneName);
        }
    }
}

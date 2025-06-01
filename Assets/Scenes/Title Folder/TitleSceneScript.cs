using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class TitleSceneScript : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    float fadeDuration = 1f;
    private bool isFading = false;
    CameraTitleScript cameraScript;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraTitleScript>();
    }

    // Update is called once per frame
    void Update()
    {
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
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                fadeCanvas.alpha = t / fadeDuration;
                yield return null;
            }
            fadeCanvas.alpha = 1;
            SceneManager.LoadScene(sceneName);
        }
    }
}

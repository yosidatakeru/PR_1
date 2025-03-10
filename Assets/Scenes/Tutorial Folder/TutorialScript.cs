using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public CanvasGroup fadeCanvasIN;
    public CanvasGroup fadeCanvasOUT;
    public TutorialTextScript TutorialText;
    float fadeDuration = 5f;
    private bool isFading = false;
    int tutorialManager = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn()); // シーン開始時にフェードイン
    }

    // Update is called once per frame
    void Update()
    {
        switch (tutorialManager) 
        {
            case 0:

                TutorialText.TutorialText();
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton"))
                {
                    tutorialManager = 1;
                }
                break;
            case 1:

                TutorialText.TutorialText2();
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton"))
                {
                    tutorialManager = 2;
                }
                break;

            case 2:

                TutorialText.TutorialText3();
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton"))
                {
                    tutorialManager = 3;
                }
                break;
            case 3:

                TutorialText.TutorialText4();
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton"))
                {
                    tutorialManager = 4;
                }
                break;
            case 4:

                StartCoroutine(FadeOut("GameScene"));

                break;

           
        }
    }

    IEnumerator FadeIn()
    {
        isFading = true;
        fadeCanvasIN.blocksRaycasts = true;
        for (float t = fadeDuration; t > 0; t -= Time.deltaTime)
        {
            fadeCanvasIN.alpha = t / fadeDuration;
            yield return null;
        }
        fadeCanvasIN.alpha = 0;
        fadeCanvasIN.blocksRaycasts = false;
        isFading = false;
       
    }
    IEnumerator FadeOut(string sceneName)
    {
        isFading = true;
        fadeCanvasOUT.blocksRaycasts = true;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fadeCanvasOUT.alpha = t / fadeDuration;
            yield return null;
        }
        fadeCanvasOUT.alpha = 1;
        SceneManager.LoadScene(sceneName);
    }
}

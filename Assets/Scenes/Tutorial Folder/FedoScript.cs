using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FedoScript : MonoBehaviour
{
    public CanvasGroup fade;
    float fadeDuration = 0.5f;
    private bool isFading = false;
    int tutorialManager = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= 2400)
        {
            StartCoroutine(FadeOut());
            transform.position = Vector3.zero;
          
        }
        if (transform.position.z == 0)
        {
            StartCoroutine(FadeIn());
        }

    }

    IEnumerator FadeIn()
    {
        isFading = true;
        fade.blocksRaycasts = true;
        for (float t = fadeDuration; t > 0; t -= Time.deltaTime)
        {
            fade.alpha = t / fadeDuration;
            yield return null;
        }
        fade.alpha = 0;
        fade.blocksRaycasts = false;
        isFading = false;
       

    }
    IEnumerator FadeOut()
    {

        isFading = true;
        fade.blocksRaycasts = true;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fade.alpha = t / fadeDuration;
            yield return null;
        }
        fade.alpha = 1;
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    ScoreScript scoreScript;
    PlayerScript playerScript;
    public CanvasGroup fade;
    float fadeDuration = 3f;
    private bool isFading = false;
    // Start is called before the first frame update
    void Start()
    {
        scoreScript = GameObject.Find("ScoreText (TMP)").GetComponent<ScoreScript>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.transform.position.z >= 3300)
        {
            StartCoroutine(FadeOut("ClearScene"));
           // "NextSceneName" ÇêÿÇËë÷Ç¶ÇΩÇ¢ÉVÅ[ÉìñºÇ…ïœçX

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
}

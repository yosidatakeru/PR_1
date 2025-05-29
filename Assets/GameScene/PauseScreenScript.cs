using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenScript : MonoBehaviour
{
    public CanvasGroup fade;
   
    public CanvasGroup startBotton;
    public CanvasGroup explanationBotton;
    public CanvasGroup titleBotton;
    public CanvasGroup OperationInstructions;
    int choice = 2;


    bool resumeGame = false;
    bool ReturnToTitle = false;
    bool operationExplanation = false;
    private bool isPaused = false;
    float operationDisplayTimer = 0f;

    public CanvasGroup gameFadeOut;
    float fadeDuration = 3f;
    private bool isFading = false;

    void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
        fade.alpha = 0;
        choice = 2;

    }

    void Update()
    {
        
        float dpv = Input.GetAxis("D_Pad_V");

        if (OperationInstructions.alpha == 1)
        {
            operationDisplayTimer += Time.unscaledDeltaTime;
        }
        else
        {
            operationDisplayTimer = 0f;
        }

        // エスケープキーでポーズ切り替え
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Menu"))
        {
            TogglePause();
        }

        // ポーズ中に上キー押下時は点滅処理
        if (isPaused == true && OperationInstructions.alpha == 0)
        { 
            if (Input.GetKeyDown(KeyCode.UpArrow)|| (dpv == 1.0))
            {
                if(choice != 2) 
                {
                    choice +=  1;
                }
                Debug.Log("押した");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || (dpv == -1.0))
            {
                if (choice != 0)
                {
                    choice -= 1;
                }
                Debug.Log("押した");
            }

        }
       
        

        //アローキ上を押した後
        if (resumeGame == true) 
        {
            StartTex();
           
            if (Input.GetKeyDown(KeyCode.Space)|| Input.GetButtonDown("Abutton")) 
            {
                isPaused = true;
                TogglePause();
            }
        }

        if (operationExplanation == true)
        {
            OperationExplanation();
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton") && OperationInstructions.alpha == 0)
            {
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton")) && OperationInstructions.alpha == 0)
                {
                    OperationInstructions.alpha = 1;
                    operationDisplayTimer = 0f;
                    Debug.Log("操作説明");
                }

            }
            

        }

        if (ReturnToTitle == true)
        {
            TitleTex();
           
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton"))
            {
                StartCoroutine(FadeOut("TitleScene"));
               
            }
        }

        if (operationExplanation && operationDisplayTimer > 0.5f &&
        (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Abutton")) &&
        OperationInstructions.alpha == 1)
        {
            OperationInstructions.alpha = 0;
            Debug.Log("閉じる");
        }


        Choice(choice);
      

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
            isPaused = false;
            fade.alpha = 0;
        }
    }

    private void StartTex()
    {
        startBotton.alpha = 1;
        titleBotton.alpha = 0;
        explanationBotton.alpha = 0;
       
    }

    private void TitleTex()
    {
        startBotton.alpha = 0;
        titleBotton.alpha = 1;
        explanationBotton.alpha = 0;
    }

    private void OperationExplanation()
    {
        startBotton.alpha = 0;
        titleBotton.alpha = 0;
        explanationBotton.alpha = 1;
    }
    

    void Choice(int choice ) 
    {
        if (choice == 2) 
        {
            resumeGame = true;
            operationExplanation = false;
            ReturnToTitle = false;


           
        }
        else if(choice == 1)
        {
            resumeGame = false;
            operationExplanation = true;
            ReturnToTitle = false;

           

        }
        else if (choice == 0)
        {
            resumeGame = false;
            operationExplanation = false;
            ReturnToTitle = true;

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


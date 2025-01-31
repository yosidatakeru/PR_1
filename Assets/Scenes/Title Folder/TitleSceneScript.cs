using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // シーンの切り替え (次のシーンの名前を指定)
            SceneManager.LoadScene("GameScene"); // "NextSceneName" を切り替えたいシーン名に変更
        }
    }
}

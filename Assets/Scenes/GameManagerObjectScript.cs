using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerObjectScript : MonoBehaviour
{
    EnemySpawnScript enemySpawnScript;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnScript = GameObject.Find("EnemySpawnObject").GetComponent<EnemySpawnScript>();
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || enemySpawnScript.wave == 18)
        {
            // シーンの切り替え (次のシーンの名前を指定)
            SceneManager.LoadScene("ClearScene"); // "NextSceneName" を切り替えたいシーン名に変更
        }
    }
}

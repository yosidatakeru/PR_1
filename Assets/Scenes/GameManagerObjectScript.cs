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
       
    }

   
   
}

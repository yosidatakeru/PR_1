using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageScript : MonoBehaviour
{
    public GameObject stage;
    public DestroyStageScript destroyStageScript;
    Vector3 stagePos = Vector3.zero;
    float staget = 0;
    float destroyStage = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
     
        if (transform.position.z >= staget) 
        {
            stagePos.z += 300;
            stagePos.y = -20;
            //ステージの生成 
            Instantiate(stage, new Vector3(stagePos.x, stagePos.y, stagePos.z), Quaternion.identity);
            staget = transform.position.z + 260 ;

        }

        if (transform.position.z >= destroyStage)
        {
           
            destroyStage += transform.position.z + 10;
           
        }
        //Destroy(stage, 5);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStageScript : MonoBehaviour
{
    public PlayerScript playerScript;
    //float i = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

      //  i = playerScript.gameObject.transform.position.y;

       // Debug.Log(i);
       Destroy(gameObject, 30);



    }

   public void DestroyStage()
    {
        Destroy(gameObject,10);
    }
   
}

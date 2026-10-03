using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScreenControlle : MonoBehaviour
{
   

    public static ScoreScreenControlle Instance { get; private set; }

    private void Awake()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(gameObject.tag);
        
        foreach (GameObject obj in allObjects)
        {
            //èdï°ÇâÒî
            if (obj != gameObject && obj.name == gameObject.name)
            {
                Destroy(obj);

            }
           
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
}

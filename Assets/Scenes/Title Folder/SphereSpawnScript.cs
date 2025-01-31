using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawnScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sphere;
    Vector3 positionR =  new Vector3(0f,0f,0f);
    Vector3 positionL = new Vector3(0f, 0f, 0f);
    int Spawntaim = 0;
    void Start()
    {
        // positionR = new Vector3(0f, 0f, 0f);
        //Vector3 positionL = new Vector3(0f, 0f, 0f);
        Spawntaim = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Spawntaim--;
        if (Spawntaim <= 0)
        {
            positionR.x = Random.Range(5f, 14f) + transform.position.x;
            positionR.y = Random.Range(-4f, 4f) + transform.position.y;
            positionR.z = Random.Range(-14f, -17f) + transform.position.z;
            Instantiate(sphere, new Vector3(positionR.x, positionR.y, positionR.z), Quaternion.identity);

            positionL.x = Random.Range(-5f, -14f) + transform.position.x;
            positionL.y = Random.Range(-4f, 4f) + transform.position.y;
            positionL.z = Random.Range(-14f, -17f) + transform.position.z;
            Instantiate(sphere, new Vector3(positionL.x, positionL.y, positionL.z), Quaternion.identity);
            Spawntaim = 800;
        }
    }
}

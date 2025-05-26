using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    public MapSpawnerScript mapSpawner;
    // Start is called before the first frame update
    void Start()
    {
        mapSpawner = FindObjectOfType<MapSpawnerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Debug.Log("É}ÉbÉvê∂ê¨Ç∆îjâÛ");
            mapSpawner.LoadNextChunk(transform.position);        
            Destroy(gameObject);
          
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawnerScript : MonoBehaviour
{
    public GameObject[] mapChunks;//用意したマップのプレハブ
    private int currentIndex = 0;
 
    private List<GameObject> spawndChunks = new List<GameObject>();
    private int maxSpawnedMaps = 2; // 同時に存在するマップパーツの最大数

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextChunk(Vector3 spawnPosition)
    {
        if (currentIndex >= mapChunks.Length) return;
        //新しいマップチャンクを生成
        GameObject newChunk = Instantiate(mapChunks[currentIndex], spawnPosition, Quaternion.identity);
        spawndChunks.Add(newChunk);

        if(spawndChunks.Count > maxSpawnedMaps)
        {
            Destroy(spawndChunks[0]);
            spawndChunks.RemoveAt(0);
        }

        currentIndex ++;
    }
}

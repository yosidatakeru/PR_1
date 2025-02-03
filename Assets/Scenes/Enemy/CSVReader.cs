using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class CSVReader : MonoBehaviour
{
    public GameObject enemyPrefab; // 敵のプレハブ
    private string filePath = "Assets/Resources/EnemyData"; // CSVファイルのパス

    public Vector3[] positions; // 敵の位置を格納する配列

    void Start()
    {
        ReadCSV(filePath);
    }

    void ReadCSV(string path)
    {
        try
        {
            // CSVファイルの内容を読み込む
            string[] lines = File.ReadAllLines(path);
            positions = new Vector3[lines.Length]; // 読み取った行数分だけ配列を用意

            int index = 0;
            foreach (string line in lines)
            {
                // 行をカンマで分割
                string[] values = line.Split(',');

                if (values.Length >= 3) // 位置情報が3つ以上ある場合
                {
                    // CSVから位置情報を抽出
                    float posX = float.Parse(values[0]);
                    float posY = float.Parse(values[1]);
                    float posZ = float.Parse(values[2]);

                    // 位置を配列に格納
                    positions[index] = new Vector3(posX, posY, posZ);
                    index++;
                }
                else
                {
                    Debug.LogWarning("CSV行に不足しているデータがあります");
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("CSV読み込みエラー: " + e.Message);
        }
    }
}


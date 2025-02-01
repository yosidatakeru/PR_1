using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMangerScript : MonoBehaviour
{
    private Transform ChaseAndOrbitObject;  // 追従するターゲット
    private float radius;      // 軌道半径
    private float speed;       // 回転速度
   // public stri rotationDirection = "Right"; // 回転方向を指定（"Up", "Down", "Left", "Right"）
    private float angle;       // 現在の角度
    float x= 0;
    float y= 0;
    float z= 0;
    public string move = "0";
    public void Setup(Transform target, float orbitRadius, float orbitSpeed, float initialAngle)
    {
        ChaseAndOrbitObject = target;
        radius = orbitRadius;
        speed = orbitSpeed;
        angle = initialAngle;
    }

    void Update()
    {
            angle += speed * Time.deltaTime;

        switch (move) 
        {
            case "0":
                // 円軌道の座標を計算
                //-右から左
                //+左から右
                x = ChaseAndOrbitObject.position.x + radius * Mathf.Cos(angle);
                z = ChaseAndOrbitObject.position.z + radius * Mathf.Sin(angle);
                y = ChaseAndOrbitObject.position.y - radius * Mathf.Cos(angle);
                // 敵の位置を更新（高さはそのまま）
                 transform.position = new Vector3(x, transform.position.y, z);
               // transform.position = new Vector3(transform.position.x, y, z);
                break;
        
        }
        　　

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMangerScript : MonoBehaviour
{
    private Transform ChaseAndOrbitObject;  // í«è]Ç∑ÇÈÉ^Å[ÉQÉbÉg
    private float radius;      // ãOìπîºåa
    private float speed;       // âÒì]ë¨ìx
    private float angle;       // åªç›ÇÃäpìx
    float x= 0;
    float y= 0;
    float z= 0;
    public int move = 0;
    int spawnRadiusmove = 0;
    public void Setup(Transform target, float orbitRadius, float orbitSpeed, float initialAngle,int move_, int spawnRadiusmove_)
    {
        ChaseAndOrbitObject = target;
        radius = orbitRadius;
        speed = orbitSpeed;
        angle = initialAngle;
        move = move_;
        spawnRadiusmove = spawnRadiusmove_;
    }

    void Update()
    {
        //âÒì]Çµï˚ÇÃêßå‰
            angle += speed * Time.deltaTime;

        switch (move) 
        {   // -âEÇ©ÇÁç∂
            //+ç∂Ç©ÇÁâE
            case 0:
              //âΩÇ‡ÇµÇ»Ç¢
                break;
            case 1:
                //âEÇ©ÇÁç∂
                // â~ãOìπÇÃç¿ïWÇåvéZ
                x = ChaseAndOrbitObject.position.x - radius * Mathf.Cos(angle);
                z = ChaseAndOrbitObject.position.z + radius * Mathf.Sin(angle);
                // ìGÇÃà íuÇçXêVÅiçÇÇ≥ÇÕÇªÇÃÇ‹Ç‹Åj
                transform.position = new Vector3(x, transform.position.y, z);
                break;
            case 2:
                //ç∂Ç©ÇÁâE
                // â~ãOìπÇÃç¿ïWÇåvéZ
                x = ChaseAndOrbitObject.position.x + radius * Mathf.Cos(angle);
                z = ChaseAndOrbitObject.position.z + radius * Mathf.Sin(angle);
                y = ChaseAndOrbitObject.position.y - radius * Mathf.Cos(angle);
                
                transform.position = new Vector3(x, transform.position.y, z);
                break;
            case 3:
                //â∫Ç©ÇÁè„Ç…
                // â~ãOìπÇÃç¿ïWÇåvéZ
                z = ChaseAndOrbitObject.position.z + radius * Mathf.Sin(angle);
                y = ChaseAndOrbitObject.position.y + radius * Mathf.Cos(angle);
               
                transform.position = new Vector3(transform.position.x, y, z);
                break;
            case 4:
                //è„Ç©ÇÁâ∫
                // â~ãOìπÇÃç¿ïWÇåvéZ
                z = ChaseAndOrbitObject.position.z + radius * Mathf.Sin(angle);
                y = ChaseAndOrbitObject.position.y - radius * Mathf.Cos(angle);
                
                transform.position = new Vector3(transform.position.x, y, z);
                break;
            case 5:
                //âEâ∫Ç©ÇÁç∂è„
                x = ChaseAndOrbitObject.position.x - radius * Mathf.Cos(angle);
                z = ChaseAndOrbitObject.position.z + radius * Mathf.Sin(angle);
                y = ChaseAndOrbitObject.position.y + radius * Mathf.Cos(angle);

                transform.position = new Vector3(x, transform.position.y, z);
                transform.position = new Vector3(transform.position.x, y, z);
                break;
            case 6:
                //ç∂è„Ç©ÇÁâEâ∫
                x = ChaseAndOrbitObject.position.x + radius * Mathf.Cos(angle);
                z = ChaseAndOrbitObject.position.z + radius * Mathf.Sin(angle);
                y = ChaseAndOrbitObject.position.y - radius * Mathf.Cos(angle);

                transform.position = new Vector3(x, transform.position.y, z);
                transform.position = new Vector3(transform.position.x, y, z);
                break;

            case 7:
                //ç∂â∫Ç©ÇÁâEè„
                x = ChaseAndOrbitObject.position.x + radius * Mathf.Cos(angle);
                z = ChaseAndOrbitObject.position.z + radius * Mathf.Sin(angle);
                y = ChaseAndOrbitObject.position.y + radius * Mathf.Cos(angle);

                transform.position = new Vector3(x, transform.position.y, z);
                transform.position = new Vector3(transform.position.x, y, z);
                break;
            case 8:
                //âEè„Ç©ÇÁç∂â∫
                x = ChaseAndOrbitObject.position.x + radius * Mathf.Cos(angle);
                z = ChaseAndOrbitObject.position.z + radius * Mathf.Sin(angle);
                y = ChaseAndOrbitObject.position.y - radius * Mathf.Cos(angle);

                transform.position = new Vector3(x, transform.position.y, z);
                transform.position = new Vector3(transform.position.x, y, z);
                break;

        }
        switch (spawnRadiusmove)
        {
            case 0:
                //âΩÇ‡ÇµÇ»Ç¢
                break;
            case 1:
                radius+=0.01f;
                speed = 0.5f;

                break;
            case 2:
                radius -= 0.01f;
                speed = 0.5f;
                break;




        }

    }

  
}

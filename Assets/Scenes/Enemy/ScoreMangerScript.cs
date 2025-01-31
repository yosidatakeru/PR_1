using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMangerScript : MonoBehaviour
{
    private Transform player;  // ’Ç]‚·‚éƒ^[ƒQƒbƒgiƒvƒŒƒCƒ„[j
    private float radius;      // ‹O“¹”¼Œa
    private float speed;       // ‰ñ“]‘¬“x
    private float angle;       // Œ»İ‚ÌŠp“x

    public void Setup(Transform target, float orbitRadius, float orbitSpeed, float initialAngle)
    {
        player = target;
        radius = orbitRadius;
        speed = orbitSpeed;
        angle = initialAngle;
    }

    void Update()
    {
        if (player != null)
        {
            // Šp“x‚ğ‘‚â‚µ‚Ä‰~‰^“®‚ğì‚é
            angle += speed * Time.deltaTime * 2 * Mathf.PI;

            // ‰~‹O“¹‚ÌÀ•W‚ğŒvZ
            float x = player.position.x + radius * Mathf.Cos(angle);
            float z = player.position.z + radius * Mathf.Sin(angle);

            // “G‚ÌˆÊ’u‚ğXVi‚‚³‚Í‚»‚Ì‚Ü‚Üj
            transform.position = new Vector3(x, transform.position.y, z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    float bounceDistance = 2f;
    float bounceDisableTime = 1f;

    private bool isBounced = false;
    private float bounceTimer = 0f;

    private PlayerMovement movement;
    private PlayerRoll roll;
    private HPScript hpScript;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        roll = GetComponent<PlayerRoll>();
        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();
    }

    void Update()
    {
        if (isBounced)
        {
         
           
                isBounced = false;
           
        }
    }

    void OnCollisionStay(Collision collision)
    {
       

        if (collision.gameObject.CompareTag("EnemyWoll"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 normal = contact.normal;
            Vector3 absNormal = new Vector3(Mathf.Abs(normal.x), Mathf.Abs(normal.y), Mathf.Abs(normal.z));

            Vector3 bounceDirection = Vector3.zero;

            if (absNormal.x > absNormal.y && absNormal.x > absNormal.z)
            {
                bounceDirection = new Vector3(-Mathf.Sign(normal.x), 0, 0);
                movement.StopMovement();
            }
            else if (absNormal.y > absNormal.x && absNormal.y > absNormal.z)
            {
                bounceDirection = new Vector3(0, -Mathf.Sign(normal.y), 0);
                movement.StopMovement();
            }
            else
            {
                bounceDirection = new Vector3(0, 0, -Mathf.Sign(normal.z));
                hpScript.Gauge = 0;
            }

            Ray ray = new Ray(transform.position, bounceDirection);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, bounceDistance + 1f))
            {
                // Õ“Ë–Ê‚Ì­‚µè‘O‚ÉˆÊ’u‚ğ’²®
                transform.position = hit.point - bounceDirection * 0.01f;
            }
            else
            {
                // Raycast‚ª•Ç‚É“Í‚©‚È‚¢ê‡‚ÍŠù‘¶‚Ìˆ—‚ÅˆÚ“®
                transform.position -= bounceDirection * bounceDistance;
            }

            transform.position -= bounceDirection * bounceDistance;
            isBounced = true;
         
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hothomingBulletScript : MonoBehaviour
{
    float speed = 150f;
    float lifetime = 100f;
    Vector3 targetPosition;
    private Vector3 direction;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            targetPosition = player.transform.position;
        }
        else
        {
            targetPosition = transform.position + transform.forward * 10f;
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }

        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetNewTarget(Vector3 newTarget)
    {
        targetPosition = newTarget;

        // タグを「Bullet」に変更（反射後の状態）
        gameObject.tag = "Bullet";
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("EnemyWoll"))
        {
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject);
        }
    }

}

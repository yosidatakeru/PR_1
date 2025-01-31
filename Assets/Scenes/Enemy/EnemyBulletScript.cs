using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    //’e‚ÌƒXƒs[ƒh
    int speed = 15;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //’e‚ğ5•b‚²Á‹
        Destroy(gameObject, 5);
        //’e‚ğ‘O‚É”ò‚Î‚·
        transform.position += speed * transform.forward * Time.deltaTime;
    }
}

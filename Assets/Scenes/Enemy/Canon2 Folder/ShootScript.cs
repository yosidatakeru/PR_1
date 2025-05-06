using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    
    // Start is called before the first frame update
    public float shotInterval = 0.2f; // òAéÀÇÃä‘äuÅiïbÅj


    // Update is called once per frame
    public void Shoot() 
    {
        for (int i = 0; i < 3; i++)
        {

            StartCoroutine(BurstShoot());

        }
    }
    private IEnumerator BurstShoot()
    {
        for (int i = 0; i < 3; i++) // 3âÒåJÇËï‘Ç∑
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(shotInterval); // éûä‘ÇãÛÇØÇÈ
        }
    }

}

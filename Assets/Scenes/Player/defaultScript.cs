using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class defaultScript : MonoBehaviour
{
    ScoreScript scoreScript;
    private bool isInvincible = false;
    public float invincibleTime = 2.0f;
    private MeshRenderer meshRenderer;

    ComboGaugeScript comboGaugeScript;
    HPScript hpScript;

    private float previousHp;
    public GameObject healEffectPrefab;
    public Transform effectSpawnPoint;
    private bool isFirstUpdate = true;
    CameraScript cameraSample;
   
    public GameObject PlreyerDestroy;


    void Start()
    {
        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();
        comboGaugeScript = GameObject.Find("ComboGauge").GetComponent<ComboGaugeScript>();
        scoreScript = GameObject.Find("ScoreText (TMP)").GetComponent<ScoreScript>();

        cameraSample = GameObject.Find("Main Camera").GetComponent<CameraScript>();

        isInvincible = false;
        invincibleTime = 2.0f;
        meshRenderer = GetComponent<MeshRenderer>();
       

        previousHp = hpScript.Gauge;
    }

    void Update()
    {
        if (isFirstUpdate)
        {
            previousHp = hpScript.Gauge;
            isFirstUpdate = false;
            return;
        }

        if (hpScript.Gauge > previousHp)
        {
            if (healEffectPrefab != null && effectSpawnPoint != null)
            {
                Instantiate(healEffectPrefab, effectSpawnPoint.position, Quaternion.identity);
            }
        }


        if (hpScript.Gauge <= 0&& transform.position.z >= 3300)
        {
            GetComponent<Renderer>().enabled = false;
            Instantiate(PlreyerDestroy, transform.position, Quaternion.identity);
        }


        previousHp = hpScript.Gauge;
    }

    void OnParticleCollision(GameObject other)
    {
       

        if (other.CompareTag("EnemyBullet") || other.gameObject.CompareTag("EnemyWoll"))
        {
            if (hpScript.Gauge <= 0)
            {
                GetComponent<Renderer>().enabled = false;
                Instantiate(PlreyerDestroy, transform.position, Quaternion.identity);
            }
            if (isInvincible) return;
            comboGaugeScript.Gauge = 0;
            ScoreScript.score -= 100;
            hpScript.Gauge -= 200;

            

            StartCoroutine(BlinkAndInvincible());
        }

        
           
        

    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("EnemyWoll"))
        {
            if (hpScript.Gauge <= 0)
            {
                GetComponent<Renderer>().enabled = false;
                Instantiate(PlreyerDestroy, transform.position, Quaternion.identity);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
        

      

        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("EnemyWoll"))
        {
            

            
            if (isInvincible) return;
           
            comboGaugeScript.Gauge = 0;
            ScoreScript.score -= 100;
            hpScript.Gauge -= 200;

            cameraSample.TakeDamage();
            if (hpScript.Gauge >= 0)
            {
                StartCoroutine(BlinkAndInvincible());
            }

            

        }
    }

    IEnumerator BlinkAndInvincible()
    {
        isInvincible = true;
        float blinkDuration = 0.1f;
        float elapsed = 0f;

        while (elapsed < invincibleTime)
        {
            meshRenderer.enabled = !meshRenderer.enabled;
            yield return new WaitForSeconds(blinkDuration);
            elapsed += blinkDuration;
        }

        meshRenderer.enabled = true;
        isInvincible = false;
    }


}

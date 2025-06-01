using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboBoxScript : MonoBehaviour
{
    
    public GameObject particle;
    Vector3 particleposition = Vector3.zero;
    ComboGaugeScript comboGaugeScript;
    ComboSceorwScript comboSceorwScript;
    // Start is called before the first frame update
    void Start()
    {
        comboGaugeScript = GameObject.Find("ComboGauge").GetComponent<ComboGaugeScript>();
        comboSceorwScript = GameObject.Find("ComboScore (TMP)").GetComponent<ComboSceorwScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(particle, new Vector3(transform.position.x + 8, transform.position.y + particleposition.y, transform.position.z), Quaternion.identity);
            //âº
            comboGaugeScript.Gauge = 600;

            comboSceorwScript.conboScore += 10 ;


            //ìGÇè¡Ç∑/
            Destroy(gameObject);



        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboxScript : MonoBehaviour
{
    int scoreResult = 0;
    public GameObject particle;
    Vector3 particleposition = Vector3.zero;
    int destroyScore =10000;
    //Ç¢Ç≠Ç¬â¡éZÇ≥ÇÍÇΩÇ©
    int addAmount;
    // Start is called before the first frame update
    void Start()
    {
      
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

            scoreResult += destroyScore;

            addAmount = destroyScore;


            ScoreScript.AddScore(addAmount);
            ScoreScript.score += scoreResult;
            //ìGÇè¡Ç∑/
            Destroy(gameObject);



        }

    }
}

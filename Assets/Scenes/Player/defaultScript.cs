using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class defaultScript : MonoBehaviour
{
    ScoreScript scoreScript;
    private bool isInvincible = false; // 無敵状態フラグ
    public float invincibleTime = 2.0f; // 無敵時間（秒）
    private MeshRenderer meshRenderer;
    private CapsuleCollider capsuleCollider; // プレイヤーの当たり判定用

    ComboGaugeScript comboGaugeScript;
    HPScript hpScript;

    private float previousHp; // 前回のHP値
    public GameObject healEffectPrefab; // 回復エフェクトのプレハブ
    public Transform effectSpawnPoint; // エフェクトの表示位置（プレイヤーなど）
    private bool isFirstUpdate = true;


    // Start is called before the first frame update
    void Start()
    {
        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();
        comboGaugeScript = GameObject.Find("ComboGauge").GetComponent<ComboGaugeScript>();
        isInvincible = false;
        invincibleTime = 2.0f;
        meshRenderer = GetComponent<MeshRenderer>(); // MeshRenderer の取得
        capsuleCollider = GetComponent<CapsuleCollider>(); // 当たり判定の取得
        previousHp = hpScript.Gauge; // 初期HPを保存
    }

    // Update is called once per frame
    void Update()
    {
        scoreScript = GameObject.Find("ScoreText (TMP)").GetComponent<ScoreScript>();

        if (isFirstUpdate)
        {
            // 初回のUpdateでは何もしない（HP比較はスキップ）
            previousHp = hpScript.Gauge;
            isFirstUpdate = false;
            return;
        }
       
        // HPが増えた場合のチェック
        if (hpScript.Gauge > previousHp)
        {
            if (healEffectPrefab != null && effectSpawnPoint != null)
            {
                Instantiate(healEffectPrefab, effectSpawnPoint.position, Quaternion.identity);
            }
        }

        previousHp = hpScript.Gauge;
    }

    void OnParticleCollision(GameObject other)
    {
      
            if (isInvincible) return; // 無敵中はダメージを受けない

            if (other.gameObject.tag == "EnemyBullet")
            {
                comboGaugeScript.Gauge = 0;
                ScoreScript.score -= 100;
                hpScript.Gauge -= 200;
                // 無敵状態にする
                StartCoroutine(BlinkAndInvincible());
            }
      
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isInvincible) return; // 無敵中はダメージを受けない

        if (collision.gameObject.tag == "EnemyBullet"|| collision.gameObject.tag == "EnemyWoll")
        {
            comboGaugeScript.Gauge = 0;
            ScoreScript.score -= 100;
            hpScript.Gauge -= 200;
            // 無敵状態にする
            StartCoroutine(BlinkAndInvincible());
        }
    }

  
    IEnumerator BlinkAndInvincible()
    {
        isInvincible = true;
        float blinkDuration = 0.1f; // 点滅間隔
        float elapsed = 0f;

        while (elapsed < invincibleTime)
        {
            meshRenderer.enabled = !meshRenderer.enabled; // 点滅
            yield return new WaitForSeconds(blinkDuration);
            elapsed += blinkDuration;
        }

        meshRenderer.enabled = true; // 最後に表示状態に戻す
        isInvincible = false;
    }
}

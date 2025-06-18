using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TrackingBilltScript : MonoBehaviour
{
    string targetTag = "Player";
    float speed = -20f;
    float maxSpeed = 60f;
    float rotateSpeed = 1000f;
    float lifeTime = 10f;
    float homingDelay =2f;
    float acceleration = 20f; // 加速度

    private Transform target;
    private bool isHoming = false;
    private float homingTimer = 0f;

    void Start()
    {
        GameObject targetObj = GameObject.FindWithTag(targetTag);
        if (targetObj != null)
        {
            target = targetObj.transform;
        }

        // 発射時に -z 方向に向ける
        transform.rotation = Quaternion.LookRotation(Vector3.back);

        // ランダムなばらけ角を追加（XY軸方向に）
        float angleX = Random.Range(-10f, 10f);
        float angleY = Random.Range(0f, 10f);
        transform.rotation = Quaternion.Euler(angleX, angleY, 0f) * transform.rotation;

        // 初期速度（正の値）を与える（ただし進行方向は -z なので逆に進む）
        speed = Random.Range(15f, 30f);

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 常に前進
        transform.position += transform.forward * speed * Time.deltaTime;

        if (target == null) return;

        // プレイヤーより後ろに行ったら追尾停止
        if (isHoming && transform.position.z < target.position.z)
        {
            isHoming = false;
            return;
        }

        // Z座標が近づいたら追尾停止（必要なら残す）
        if (isHoming && Mathf.Abs(transform.position.z - target.position.z) < 0.1f)
        {
            isHoming = false;
            return;
        }

        // 一定時間後に追尾開始
        if (!isHoming)
        {
            homingTimer += Time.deltaTime;
            if (homingTimer >= homingDelay)
            {
                isHoming = true;
            }
            return;
        }

        // 加速
        speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);

        // ターゲット方向に回転
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }

    public void SetNewTarget(Vector3 newTargetPosition)
    {
        GameObject dummyTarget = new GameObject("ReflectedTarget");
        dummyTarget.transform.position = newTargetPosition;
        target = dummyTarget.transform;

        isHoming = true;
        homingTimer = homingDelay;

        // 反射後の初期速度と加速の調整（任意）
        speed = Mathf.Max(speed, 20f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          

            // プレイヤーに当たった場合
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("EnemyWoll"))
        {
            // 壁などに当たった場合も破壊
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject);
        }
    }

}




   
    


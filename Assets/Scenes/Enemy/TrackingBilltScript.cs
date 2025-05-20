using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TrackingBilltScript : MonoBehaviour
{
    string targetTag = "Player";
    float speed = 40f;
    float rotateSpeed = 1000f;
    float lifeTime = 10f;
    float homingDelay = 0.5f; // 追尾開始までの遅延

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

        // ランダムな方向とスピード（ばらけ発射）
        float angleX = Random.Range(-10f, 10f);
        float angleY = Random.Range(-10f, 10f);
        transform.rotation = Quaternion.Euler(angleX, angleY, 0f) * transform.rotation;
        speed = Random.Range(15f, 30f);

        Destroy(gameObject, lifeTime);
    }


    void Update()
    {
        // 常に前進（現在の向きに直進）
        transform.position += transform.forward * speed * Time.deltaTime;

        if (target == null) return;

        // Z座標が近づいたら追尾をやめてそのまま進む
        if (isHoming && Mathf.Abs(transform.position.z - target.position.z) < 0.1f)
        {
            isHoming = false;
            return;
        }

        // 一定時間経過後に追尾開始
        if (!isHoming)
        {
            homingTimer += Time.deltaTime;
            if (homingTimer >= homingDelay)
            {
                isHoming = true;
            }
            return; // 追尾開始前は直進のみ
        }

        // なめらかにターゲット方向へ向く
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
        // 一時的な仮ターゲットとして Transform を生成（破棄されないように管理しても良い）
        GameObject dummyTarget = new GameObject("ReflectedTarget");
        dummyTarget.transform.position = newTargetPosition;
        target = dummyTarget.transform;

        // 追尾状態を強制ONにする
        isHoming = true;
        homingTimer = homingDelay; // すぐ追尾に移る

        // 速度・回転速度を反射用に調整してもよい
        speed = Mathf.Max(speed, 20f);
    }

}




   
    


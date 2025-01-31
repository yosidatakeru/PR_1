using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(Rigidbody))] //Rigidbodyをアタッチして爆弾を自然落下させる
public class ExplosionScript : MonoBehaviour
{
    public ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<Renderer>().material.color = Color.black;
        particle.startColor = new Color32(255, 25, 0, 255); //パーティクルの色を変更
        particle.startSize = 5.0f; //パーティクルのサイズを変更
        particle.startLifetime = 1.0f; //パーティクルの生存時間を変更
        particle.emission.SetBursts(
            new ParticleSystem.Burst[]
            {
                new ParticleSystem.Burst(0, 300) //パーティクルの発生個数を300個に設定
            });

        var main = particle.main;
        main.loop = false; //パーティクルの発生をループさせない
        main.duration = 1.0f; //パーティクルを発生させる時間を変更

        //パーティクルを球状に放出する
        var shape = particle.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;

        //パーティクルの当たり判定を有効にする
        var collision = particle.collision;
        collision.enabled = true;
        collision.type = ParticleSystemCollisionType.World;
        collision.sendCollisionMessages = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        
            Explode(); //爆弾が地面に当たったら爆発させる
        
    }

    void Explode()
    {
        GetComponent<Renderer>().material.color = new Color32(255, 25, 0, 255);
        Instantiate(particle, this.transform.position, Quaternion.identity); //パーティクルを発生させる
        Destroy(this.gameObject, 0.5f); //0.5秒後に爆弾のオブジェクトを消滅させる
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
